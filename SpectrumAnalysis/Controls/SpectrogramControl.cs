using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace SpectrumAnalysis.Controls
{
    /// <summary>
    /// 声谱图控件 - 显示时间-频率的二维热力图
    /// 可复用的自定义控件，使用unsafe代码优化性能
    /// 时间轴从上到下（瀑布式），频率轴从左到右
    /// 新数据从顶部进入，旧数据从底部流出，保持实时滚动效果
    /// </summary>
    public class SpectrogramControl : UserControl
    {
        private readonly List<float[]> _spectrogramData;  // 声谱图数据列表（每行是一个频谱）
        private Bitmap _backBuffer;                       // 后台缓冲区（双缓冲）
        private readonly object _lockObject = new object();  // 线程同步锁
        private int _maxRows = 10;                       // 最大保留行数（控制显示窗口大小）
        private float _minDb = -80f;                      // 最小dB值
        private float _maxDb = 0f;                        // 最大dB值
        private int _sampleRate = 44100;                  // 采样率
        private float _minFrequency = 0;                  // 最小显示频率（Hz）
        private float _maxFrequency = 22050;              // 最大显示频率（Hz）

        /// <summary>
        /// 色彩映射表（256色）
        /// </summary>
        public Color[] ColorMap { get; set; }

        /// <summary>
        /// 背景颜色
        /// </summary>
        public Color BackgroundColor { get; set; } = Color.FromArgb(20, 20, 20);

        /// <summary>
        /// 最大显示行数（时间窗口大小）
        /// 默认300行，约15秒的历史数据
        /// 可以调整此值来控制显示的时间范围
        /// </summary>
        public int MaxDisplayRows
        {
            get { return _maxRows; }
            set { _maxRows = Math.Max(10, value); }  // 最小50行
        }

        /// <summary>
        /// 设置采样率
        /// </summary>
        public int SampleRate
        {
            get { return _sampleRate; }
            set
            {
                _sampleRate = value;
                _maxFrequency = value / 2f;  // 奈奎斯特频率
            }
        }

        /// <summary>
        /// 最小显示频率（Hz）
        /// </summary>
        public float MinFrequency
        {
            get { return _minFrequency; }
            set
            {
                _minFrequency = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 最大显示频率（Hz）
        /// </summary>
        public float MaxFrequency
        {
            get { return _maxFrequency; }
            set
            {
                _maxFrequency = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SpectrogramControl()
        {
            // 启用双缓冲和自定义绘制，减少闪烁
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);

            ResizeRedraw = true;
            _spectrogramData = new List<float[]>();
            InitializeColorMap();
        }

        /// <summary>
        /// 初始化色彩映射表
        /// 使用蓝-青-黄-红渐变（类似热力图）
        /// </summary>
        private void InitializeColorMap()
        {
            ColorMap = new Color[256];
            for (int i = 0; i < 256; i++)
            {
                float t = i / 255f;  // 归一化到[0, 1]

                // 分段线性插值创建渐变色
                if (t < 0.25f)
                {
                    // 黑色 -> 蓝色
                    float localT = t / 0.25f;
                    ColorMap[i] = Color.FromArgb(0, 0, (int)(255 * localT));
                }
                else if (t < 0.5f)
                {
                    // 蓝色 -> 青色
                    float localT = (t - 0.25f) / 0.25f;
                    ColorMap[i] = Color.FromArgb(0, (int)(255 * localT), 255);
                }
                else if (t < 0.75f)
                {
                    // 青色 -> 黄色
                    float localT = (t - 0.5f) / 0.25f;
                    ColorMap[i] = Color.FromArgb((int)(255 * localT), 255, (int)(255 * (1 - localT)));
                }
                else
                {
                    // 黄色 -> 红色
                    float localT = (t - 0.75f) / 0.25f;
                    ColorMap[i] = Color.FromArgb(255, (int)(255 * (1 - localT * 0.5f)), 0);
                }
            }
        }

        /// <summary>
        /// 更新声谱图数据（线程安全）
        /// 新数据添加到顶部，像瀑布一样从上往下流动
        /// 旧数据自动从底部清理，保持固定的显示窗口
        /// </summary>
        /// <param name="spectrumData">新的频谱数据（一行）</param>
        public void UpdateSpectrogram(float[] spectrumData)
        {
            lock (_lockObject)
            {
                // 在列表开头插入新数据（最新的在顶部）
                _spectrogramData.Insert(0, spectrumData);

                // 如果超过最大行数，删除最旧的数据（底部）
                // 保持瀑布式流动效果
                if (_spectrogramData.Count > _maxRows)
                {
                    _spectrogramData.RemoveAt(_spectrogramData.Count - 1);
                }
            }
            Invalidate();  // 触发重绘
        }

        /// <summary>
        /// 清除所有声谱图数据
        /// </summary>
        public void Clear()
        {
            lock (_lockObject)
            {
                _spectrogramData.Clear();
            }
            Invalidate();
        }

        /// <summary>
        /// 绘制控件
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            // 如果后台缓冲区不存在或大小改变，重新创建
            if (_backBuffer == null || _backBuffer.Width != Width || _backBuffer.Height != Height)
            {
                _backBuffer?.Dispose();
                _backBuffer = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            }

            // 在后台缓冲区上绘制
            using (Graphics g = Graphics.FromImage(_backBuffer))
            {
                g.Clear(BackgroundColor);

                lock (_lockObject)
                {
                    if (_spectrogramData.Count > 0)
                    {
                        DrawSpectrogram(g);
                    }
                }
            }

            // 将后台缓冲区绘制到屏幕
            e.Graphics.DrawImage(_backBuffer, 0, 0);
        }

        /// <summary>
        /// 绘制声谱图
        /// 使用unsafe代码直接操作像素数据以提升性能
        /// 时间轴：从上到下（最新的在顶部）
        /// 频率轴：从左到右（根据MinFrequency和MaxFrequency显示）
        /// </summary>
        private void DrawSpectrogram(Graphics g)
        {
            int rowCount = _spectrogramData.Count;  // 时间轴（行数）
            if (rowCount == 0) return;

            int totalColCount = _spectrogramData[0].Length;  // 总频率点数

            // 计算显示的频率范围对应的数据索引
            int startCol = (int)(_minFrequency / (_sampleRate / 2f) * totalColCount);
            int endCol = (int)(_maxFrequency / (_sampleRate / 2f) * totalColCount);
            startCol = Math.Max(0, startCol);
            endCol = Math.Min(totalColCount - 1, endCol);
            int displayColCount = endCol - startCol + 1;

            if (displayColCount <= 0) return;

            // 创建临时位图用于绘制声谱图
            // 宽度=显示的频率数量，高度=时间行数
            using (Bitmap spectrogramBitmap = new Bitmap(displayColCount, rowCount))
            {
                // 锁定位图数据以进行直接内存访问
                BitmapData bmpData = spectrogramBitmap.LockBits(
                    new Rectangle(0, 0, displayColCount, rowCount),
                    ImageLockMode.WriteOnly,
                    PixelFormat.Format32bppArgb);

                unsafe
                {
                    byte* ptr = (byte*)bmpData.Scan0;  // 获取位图数据指针
                    int stride = bmpData.Stride;        // 每行字节数

                    // 遍历每一行（时间轴，从上到下）
                    for (int row = 0; row < rowCount; row++)
                    {
                        float[] rowData = _spectrogramData[row];

                        // 遍历显示范围内的每一列（频率轴，从左到右）
                        for (int displayCol = 0; displayCol < displayColCount; displayCol++)
                        {
                            int actualCol = startCol + displayCol;

                            // 获取频谱数据
                            float db = rowData[actualCol];

                            // 归一化到[0, 1]范围
                            float normalizedValue = (db - _minDb) / (_maxDb - _minDb);
                            normalizedValue = Math.Max(0, Math.Min(1, normalizedValue));

                            // 映射到色彩表
                            int colorIndex = (int)(normalizedValue * 255);
                            Color color = ColorMap[colorIndex];

                            // 直接写入像素数据（BGRA格式）
                            int pixelIndex = row * stride + displayCol * 4;
                            ptr[pixelIndex] = color.B;      // Blue
                            ptr[pixelIndex + 1] = color.G;  // Green
                            ptr[pixelIndex + 2] = color.R;  // Red
                            ptr[pixelIndex + 3] = 255;      // Alpha
                        }
                    }
                }

                // 解锁位图数据
                spectrogramBitmap.UnlockBits(bmpData);

                // 使用最近邻插值缩放到控件大小（保持清晰度）
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(spectrogramBitmap, 0, 0, Width, Height);
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _backBuffer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
