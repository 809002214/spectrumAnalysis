using System;
using System.Numerics;

namespace AudioSpectrumVisualizer.DSP
{
    /// <summary>
    /// FFT处理器 - 实现快速傅里叶变换（Cooley-Tukey算法）
    /// Fast Fourier Transform processor implementing the Cooley-Tukey algorithm
    /// </summary>
    public class FFTProcessor
    {
        private readonly int _fftSize;          // FFT大小（必须是2的幂次方）
        private readonly Complex[] _fftBuffer;  // FFT计算缓冲区（复数数组）
        private readonly float[] _window;       // 窗函数数组（Hann窗）
        private float _gain = 1.0f;             // 增益（用于放大微弱信号）

        /// <summary>
        /// 构造函数
        /// Constructor
        /// </summary>
        /// <param name="fftSize">FFT大小，必须是2的幂次方 (FFT size, must be a power of 2)</param>
        public FFTProcessor(int fftSize = 2048)
        {
            // 检查FFT大小是否为2的幂次方
            if ((fftSize & (fftSize - 1)) != 0)
                throw new ArgumentException("FFT size must be a power of 2");

            _fftSize = fftSize;
            _fftBuffer = new Complex[fftSize];
            _window = CreateHannWindow(fftSize);
        }

        /// <summary>
        /// 获取FFT大小
        /// Gets the FFT size
        /// </summary>
        public int FFTSize => _fftSize;

        /// <summary>
        /// 设置或获取增益（用于放大微弱信号）
        /// 范围：0.1 到 100.0
        /// 默认：1.0（无增益）
        /// Set or get gain (for amplifying weak signals)
        /// Range: 0.1 to 100.0
        /// Default: 1.0 (no gain)
        /// </summary>
        public float Gain
        {
            get { return _gain; }
            set { _gain = Math.Max(0.1f, Math.Min(100.0f, value)); }
        }

        /// <summary>
        /// 创建Hann窗函数
        /// Hann窗可以减少频谱泄漏，提高频谱分析的准确性
        /// Creates a Hann window function to reduce spectral leakage
        /// </summary>
        /// <param name="size">窗函数大小 (Window size)</param>
        /// <returns>窗函数数组 (Window function array)</returns>
        private float[] CreateHannWindow(int size)
        {
            float[] window = new float[size];
            for (int i = 0; i < size; i++)
            {
                // Hann窗公式: w(n) = 0.5 * (1 - cos(2πn/(N-1)))
                window[i] = (float)(0.5 * (1 - Math.Cos(2 * Math.PI * i / (size - 1))));
            }
            return window;
        }

        /// <summary>
        /// 处理FFT并返回频谱幅度（dB）- 单声道模式
        /// Processes FFT and returns spectrum magnitude in dB scale - Mono mode
        /// </summary>
        /// <param name="audioData">输入的音频数据 (Input audio data)</param>
        /// <returns>频谱幅度数组（dB刻度），长度为FFT大小的一半 (Spectrum magnitude array in dB, length is half of FFT size)</returns>
        public float[] ProcessFFT(float[] audioData)
        {
            // 检查输入数据长度
            if (audioData.Length < _fftSize)
                return new float[_fftSize / 2];

            // 将音频数据应用窗函数和增益后转换为复数
            for (int i = 0; i < _fftSize; i++)
            {
                _fftBuffer[i] = new Complex(audioData[i] * _window[i] * _gain, 0);
            }

            // 执行FFT变换
            FFT(_fftBuffer, false);

            // 计算幅度谱（只需要前一半，因为FFT结果是对称的）
            float[] magnitudes = new float[_fftSize / 2];
            for (int i = 0; i < _fftSize / 2; i++)
            {
                // 计算复数的模（幅度）
                double magnitude = Math.Sqrt(_fftBuffer[i].Real * _fftBuffer[i].Real +
                                            _fftBuffer[i].Imaginary * _fftBuffer[i].Imaginary);

                // 转换为dB刻度: dB = 20 * log10(magnitude)
                // 加上1e-10避免log(0)
                magnitudes[i] = (float)(20 * Math.Log10(magnitude + 1e-10));
            }

            return magnitudes;
        }

        /// <summary>
        /// 处理立体声IQ数据（像SDRSharp那样）
        /// 将左声道当作I（实部），右声道当作Q（虚部）
        /// Process stereo as IQ data (like SDRSharp)
        /// Left channel as I (Real), Right channel as Q (Imaginary)
        /// </summary>
        /// <param name="interleavedStereo">交错的立体声数据 [L0, R0, L1, R1, ...] (Interleaved stereo data)</param>
        /// <returns>完整的频谱数据（负频率到正频率），长度为FFT大小 (Full spectrum from negative to positive frequencies)</returns>
        public float[] ProcessStereoAsIQ(float[] interleavedStereo)
        {
            // 检查输入数据长度（立体声需要 FFT大小 * 2 个样本）
            if (interleavedStereo.Length < _fftSize * 2)
                return new float[_fftSize];

            // 将立体声数据转换为复数IQ数据，应用窗函数和增益
            // 左声道 = I（实部），右声道 = Q（虚部）
            for (int i = 0; i < _fftSize; i++)
            {
                float left = interleavedStereo[i * 2];      // 左声道 (I)
                float right = interleavedStereo[i * 2 + 1]; // 右声道 (Q)
                _fftBuffer[i] = new Complex(left * _window[i] * _gain, right * _window[i] * _gain);
            }

            // 执行FFT变换
            FFT(_fftBuffer, false);

            // 计算完整的幅度谱（包括负频率和正频率）
            // FFT输出顺序：[0, 1, 2, ..., N/2-1, -N/2, -N/2+1, ..., -1]
            // 重新排列为：[-N/2, -N/2+1, ..., -1, 0, 1, ..., N/2-1]
            float[] magnitudes = new float[_fftSize];

            // 负频率部分（从FFT输出的后半部分）
            for (int i = 0; i < _fftSize / 2; i++)
            {
                int fftIndex = _fftSize / 2 + i;
                double magnitude = Math.Sqrt(_fftBuffer[fftIndex].Real * _fftBuffer[fftIndex].Real +
                                            _fftBuffer[fftIndex].Imaginary * _fftBuffer[fftIndex].Imaginary);
                magnitudes[i] = (float)(20 * Math.Log10(magnitude + 1e-10));
            }

            // 正频率部分（从FFT输出的前半部分）
            for (int i = 0; i < _fftSize / 2; i++)
            {
                double magnitude = Math.Sqrt(_fftBuffer[i].Real * _fftBuffer[i].Real +
                                            _fftBuffer[i].Imaginary * _fftBuffer[i].Imaginary);
                magnitudes[_fftSize / 2 + i] = (float)(20 * Math.Log10(magnitude + 1e-10));
            }

            return magnitudes;
        }

        /// <summary>
        /// 处理分离的立体声IQ数据
        /// Process separated stereo as IQ data
        /// </summary>
        /// <param name="leftChannel">左声道数据（I路/实部）(Left channel / I / Real)</param>
        /// <param name="rightChannel">右声道数据（Q路/虚部）(Right channel / Q / Imaginary)</param>
        /// <returns>完整的频谱数据（负频率到正频率）(Full spectrum from negative to positive frequencies)</returns>
        public float[] ProcessStereoAsIQ(float[] leftChannel, float[] rightChannel)
        {
            // 检查输入数据长度
            if (leftChannel.Length < _fftSize || rightChannel.Length < _fftSize)
                return new float[_fftSize];

            // 将左右声道转换为复数IQ数据，应用窗函数和增益
            for (int i = 0; i < _fftSize; i++)
            {
                _fftBuffer[i] = new Complex(leftChannel[i] * _window[i] * _gain, rightChannel[i] * _window[i] * _gain);
            }

            // 执行FFT变换
            FFT(_fftBuffer, false);

            // 重新排列频谱（负频率到正频率）
            float[] magnitudes = new float[_fftSize];

            // 负频率部分
            for (int i = 0; i < _fftSize / 2; i++)
            {
                int fftIndex = _fftSize / 2 + i;
                magnitudes[i] = (float)(20 * Math.Log10(_fftBuffer[fftIndex].Magnitude + 1e-10));
            }

            // 正频率部分
            for (int i = 0; i < _fftSize / 2; i++)
            {
                magnitudes[_fftSize / 2 + i] = (float)(20 * Math.Log10(_fftBuffer[i].Magnitude + 1e-10));
            }

            return magnitudes;
        }

        /// <summary>
        /// Cooley-Tukey FFT算法实现
        /// Cooley-Tukey FFT algorithm implementation
        /// </summary>
        /// <param name="buffer">复数缓冲区 (Complex buffer)</param>
        /// <param name="inverse">是否为逆变换 (Whether to perform inverse transform)</param>
        private void FFT(Complex[] buffer, bool inverse)
        {
            int n = buffer.Length;
            int bits = (int)Math.Log(n, 2);

            // 位反转排序（Bit-reversal permutation）
            for (int i = 1; i < n; i++)
            {
                int swapPos = BitReverse(i, bits);
                if (swapPos > i)
                {
                    var temp = buffer[i];
                    buffer[i] = buffer[swapPos];
                    buffer[swapPos] = temp;
                }
            }

            // 迭代计算FFT（蝶形运算）
            for (int size = 2; size <= n; size *= 2)
            {
                // 计算旋转因子
                double angle = 2 * Math.PI / size * (inverse ? 1 : -1);
                Complex wlen = new Complex(Math.Cos(angle), Math.Sin(angle));

                for (int i = 0; i < n; i += size)
                {
                    Complex w = new Complex(1, 0);
                    for (int j = 0; j < size / 2; j++)
                    {
                        // 蝶形运算
                        Complex u = buffer[i + j];
                        Complex v = buffer[i + j + size / 2] * w;
                        buffer[i + j] = u + v;
                        buffer[i + j + size / 2] = u - v;
                        w *= wlen;
                    }
                }
            }

            // 如果是逆变换，需要除以N
            if (inverse)
            {
                for (int i = 0; i < n; i++)
                {
                    buffer[i] /= n;
                }
            }
        }

        /// <summary>
        /// 位反转函数
        /// Bit reversal function
        /// </summary>
        /// <param name="n">输入数字 (Input number)</param>
        /// <param name="bits">位数 (Number of bits)</param>
        /// <returns>位反转后的数字 (Bit-reversed number)</returns>
        private int BitReverse(int n, int bits)
        {
            int reversed = 0;
            for (int i = 0; i < bits; i++)
            {
                reversed = (reversed << 1) | (n & 1);
                n >>= 1;
            }
            return reversed;
        }
    }
}
