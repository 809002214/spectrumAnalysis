using System;
using NAudio.Wave;
using AudioSpectrumVisualizer.DSP;

namespace SpectrumAnalysis.Audio
{
    /// <summary>
    /// 音频采集类 - 负责从麦克风采集音频并进行FFT处理
    /// 支持单声道和立体声IQ模式
    /// </summary>
    public class AudioCapture : IDisposable
    {
        private WaveInEvent _waveIn;                    // NAudio音频输入设备
        private readonly FFTProcessor _fftProcessor;    // FFT处理器
        private readonly int _sampleRate;               // 采样率（Hz）
        private readonly int _bufferSize;               // 缓冲区大小（样本数）
        private float[] _audioBuffer;                   // 音频数据缓冲区
        private int _bufferPosition;                    // 当前缓冲区位置
        private int _updateCounter;                     // 更新计数器
        private int _updateInterval = 2;                // 更新间隔（每N次FFT更新一次显示）
        private bool _isIQMode = false;                 // 是否为IQ模式（立体声）

        /// <summary>
        /// FFT数据就绪事件 - 当FFT计算完成时触发
        /// </summary>
        public event EventHandler<float[]> FFTDataAvailable;

        /// <summary>
        /// 波形数据就绪事件 - 当音频缓冲区填满时触发
        /// </summary>
        public event EventHandler<float[]> WaveformDataAvailable;

        /// <summary>
        /// 获取采样率
        /// </summary>
        public int SampleRate => _sampleRate;

        /// <summary>
        /// 获取FFT大小
        /// </summary>
        public int FFTSize => _fftProcessor.FFTSize;

        /// <summary>
        /// 设置或获取更新间隔（每N次FFT更新一次显示）
        /// 值越大，刷新越慢。默认为2（约10帧/秒）
        /// </summary>
        public int UpdateInterval
        {
            get { return _updateInterval; }
            set { _updateInterval = Math.Max(1, value); }
        }

        /// <summary>
        /// 是否为IQ模式（立体声当作IQ数据处理）
        /// </summary>
        public bool IsIQMode
        {
            get { return _isIQMode; }
            set
            {
                if (_isIQMode != value)
                {
                    _isIQMode = value;
                    // 如果正在采集，需要重启以应用新的声道设置
                    if (_waveIn != null)
                    {
                        Stop();
                        Start();
                    }
                }
            }
        }

        /// <summary>
        /// 设置或获取增益（用于放大微弱信号）
        /// 范围：0.1 到 100.0
        /// 默认：1.0（无增益）
        /// Gain for amplifying weak signals
        /// Range: 0.1 to 100.0
        /// Default: 1.0 (no gain)
        /// </summary>
        public float Gain
        {
            get { return _fftProcessor.Gain; }
            set { _fftProcessor.Gain = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sampleRate">采样率，默认44100Hz</param>
        /// <param name="fftSize">FFT大小，必须是2的幂次方，默认2048</param>
        public AudioCapture(int sampleRate = 44100, int fftSize = 2048)
        {
            _sampleRate = sampleRate;
            _bufferSize = fftSize;
            _fftProcessor = new FFTProcessor(fftSize);
            _audioBuffer = new float[_bufferSize * 2];  // 为立体声预留空间
            _bufferPosition = 0;
            _updateCounter = 0;
        }

        /// <summary>
        /// 开始音频采集
        /// </summary>
        public void Start()
        {
            // 如果已经在采集，直接返回
            if (_waveIn != null)
                return;

            // 创建音频输入设备
            _waveIn = new WaveInEvent
            {
                WaveFormat = new WaveFormat(_sampleRate, _isIQMode ? 2 : 1),  // IQ模式用立体声，否则单声道
                BufferMilliseconds = 50                                         // 50ms缓冲
            };

            // 订阅数据到达事件
            _waveIn.DataAvailable += OnDataAvailable;
            _waveIn.StartRecording();
        }

        /// <summary>
        /// 停止音频采集
        /// </summary>
        public void Stop()
        {
            if (_waveIn != null)
            {
                _waveIn.StopRecording();
                _waveIn.DataAvailable -= OnDataAvailable;
                _waveIn.Dispose();
                _waveIn = null;
            }
        }

        /// <summary>
        /// 音频数据到达事件处理
        /// </summary>
        private void OnDataAvailable(object sender, WaveInEventArgs e)
        {
            if (_isIQMode)
            {
                // IQ模式：立体声处理
                ProcessStereoIQ(e.Buffer, e.BytesRecorded);
            }
            else
            {
                // 单声道模式
                ProcessMono(e.Buffer, e.BytesRecorded);
            }
        }

        /// <summary>
        /// 处理单声道音频数据
        /// </summary>
        private void ProcessMono(byte[] buffer, int bytesRecorded)
        {
            // 遍历接收到的字节数据（16位PCM，每个样本2字节）
            for (int i = 0; i < bytesRecorded; i += 2)
            {
                // 将两个字节组合成一个16位样本（小端序）
                short sample = (short)((buffer[i + 1] << 8) | buffer[i]);

                // 归一化到[-1, 1]范围
                float sampleFloat = sample / 32768f;

                // 存入缓冲区
                _audioBuffer[_bufferPosition] = sampleFloat;
                _bufferPosition++;

                // 当缓冲区填满时，进行FFT处理
                if (_bufferPosition >= _bufferSize)
                {
                    // 复制波形数据并触发事件
                    float[] waveformCopy = new float[_bufferSize];
                    Array.Copy(_audioBuffer, waveformCopy, _bufferSize);
                    WaveformDataAvailable?.Invoke(this, waveformCopy);

                    // 进行FFT处理
                    float[] fftData = _fftProcessor.ProcessFFT(_audioBuffer);

                    // 根据更新间隔决定是否触发显示更新
                    _updateCounter++;
                    if (_updateCounter >= _updateInterval)
                    {
                        FFTDataAvailable?.Invoke(this, fftData);
                        _updateCounter = 0;
                    }

                    // 重置缓冲区位置
                    _bufferPosition = 0;
                }
            }
        }

        /// <summary>
        /// 处理立体声IQ数据（像SDRSharp那样）
        /// </summary>
        private void ProcessStereoIQ(byte[] buffer, int bytesRecorded)
        {
            // 立体声：每4个字节是一对样本（左声道2字节 + 右声道2字节）
            for (int i = 0; i < bytesRecorded; i += 4)
            {
                // 左声道（I路）
                short leftSample = (short)((buffer[i + 1] << 8) | buffer[i]);
                float left = leftSample / 32768f;

                // 右声道（Q路）
                short rightSample = (short)((buffer[i + 3] << 8) | buffer[i + 2]);
                float right = rightSample / 32768f;

                // 交错存储：[L0, R0, L1, R1, ...]
                _audioBuffer[_bufferPosition] = left;
                _audioBuffer[_bufferPosition + 1] = right;
                _bufferPosition += 2;

                // 当缓冲区填满时，进行FFT处理
                if (_bufferPosition >= _bufferSize * 2)
                {
                    // 复制波形数据并触发事件
                    float[] waveformCopy = new float[_bufferSize * 2];
                    Array.Copy(_audioBuffer, waveformCopy, _bufferSize * 2);
                    WaveformDataAvailable?.Invoke(this, waveformCopy);

                    // 进行FFT处理（立体声当作IQ）
                    float[] fftData = _fftProcessor.ProcessStereoAsIQ(_audioBuffer);

                    // 根据更新间隔决定是否触发显示更新
                    _updateCounter++;
                    if (_updateCounter >= _updateInterval)
                    {
                        FFTDataAvailable?.Invoke(this, fftData);
                        _updateCounter = 0;
                    }

                    // 重置缓冲区位置
                    _bufferPosition = 0;
                }
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Stop();
        }
    }
}
