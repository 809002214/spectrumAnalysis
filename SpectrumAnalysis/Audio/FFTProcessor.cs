using System;
using System.Numerics;

namespace SpectrumAnalysis.Audio
{
    /// <summary>
    /// FFT处理器 - 实现快速傅里叶变换（Cooley-Tukey算法）
    /// </summary>
    public class FFTProcessor
    {
        private readonly int _fftSize;          // FFT大小（必须是2的幂次方）
        private readonly Complex[] _fftBuffer;  // FFT计算缓冲区（复数数组）
        private readonly float[] _window;       // 窗函数数组（Hann窗）

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fftSize">FFT大小，必须是2的幂次方</param>
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
        /// </summary>
        public int FFTSize => _fftSize;

        /// <summary>
        /// 创建Hann窗函数
        /// Hann窗可以减少频谱泄漏，提高频谱分析的准确性
        /// </summary>
        /// <param name="size">窗函数大小</param>
        /// <returns>窗函数数组</returns>
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
        /// 处理FFT并返回频谱幅度（dB）
        /// </summary>
        /// <param name="audioData">输入的音频数据</param>
        /// <returns>频谱幅度数组（dB刻度），长度为FFT大小的一半</returns>
        public float[] ProcessFFT(float[] audioData)
        {
            // 检查输入数据长度
            if (audioData.Length < _fftSize)
                return new float[_fftSize / 2];

            // 将音频数据应用窗函数后转换为复数
            for (int i = 0; i < _fftSize; i++)
            {
                _fftBuffer[i] = new Complex(audioData[i] * _window[i], 0);
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
        /// Cooley-Tukey FFT算法实现
        /// </summary>
        /// <param name="buffer">复数缓冲区</param>
        /// <param name="inverse">是否为逆变换</param>
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
        /// </summary>
        /// <param name="n">输入数字</param>
        /// <param name="bits">位数</param>
        /// <returns>位反转后的数字</returns>
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
