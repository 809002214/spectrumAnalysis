using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using AudioSpectrumVisualizer.Theme;

namespace AudioSpectrumVisualizer.Controls
{
    /// <summary>
    /// 声谱图控件 - 显示时间-频率的二维热力图
    /// Spectrogram visualization control - displays time-frequency 2D heatmap
    /// 可复用的自定义控件，使用unsafe代码优化性能
    /// Reusable custom control with unsafe code for performance optimization
    /// 时间轴从上到下（瀑布式），频率轴从左到右
    /// Time axis from top to bottom (waterfall), frequency axis from left to right
    /// 新数据从顶部进入，旧数据从底部流出，保持实时滚动效果
    /// New data enters from top, old data flows out from bottom, maintaining real-time scrolling
    /// </summary>
    public class SpectrogramControl : UserControl
    {
        private readonly List<float[]> _spectrogramData;  // 声谱图数据列表（每行是一个频谱）
        private Bitmap _backBuffer;                       // 后台缓冲区（双缓冲）
        private readonly object _lockObject = new object();  // 线程同步锁
        private int _maxRows = 300;                       // 最大保留行数（控制显示窗口大小）
        private float _minDb = -80f;                      // 最小dB值
        private float _maxDb = 0f;                        // 最大dB值
        private int _sampleRate = 44100;                  // 采样率
        private float _minFrequency = 0;                  // 最小显示频率（Hz）
        private float _maxFrequency = 22050;              // 最大显示频率（Hz）

        // 显示区域边距（为坐标轴留出空间）
        private int _leftMargin = 60;
        private int _rightMargin = 20;
        private int _topMargin = 20;
        private int _bottomMargin = 40;

        #region Public Properties - 公共属性

        /// <summary>
        /// 色彩映射表（256色）
        /// Color map (256 colors)
        /// </summary>
        public Color[] ColorMap { get; set; }

        /// <summary>
        /// 背景颜色
        /// Background color
        /// </summary>
        public Color BackgroundColor { get; set; } = Color.FromArgb(20, 20, 20);

        /// <summary>
        /// 最大显示行数（时间窗口大小）
        /// Maximum display rows (time window size)
        /// 默认300行，约15秒的历史数据
        /// Default 300 rows, approximately 15 seconds of history
        /// 可以调整此值来控制显示的时间范围
        /// Adjust this value to control the displayed time range
        /// </summary>
        public int MaxDisplayRows
        {
            get { return _maxRows; }
            set { _maxRows = Math.Max(10, value); }  // 最小10行
        }

        /// <summary>
        /// 最小dB值
        /// Minimum dB value
        /// </summary>
        public float MinDb
        {
            get { return _minDb; }
            set { _minDb = value; Invalidate(); }
        }

        /// <summary>
        /// 最大dB值
        /// Maximum dB value
        /// </summary>
        public float MaxDb
        {
            get { return _maxDb; }
            set { _maxDb = value; Invalidate(); }
        }

        /// <summary>
        /// 设置采样率
        /// Sample rate
        /// </summary>
        public int SampleRate
        {
            get { return _sampleRate; }
            set
            {
                _sampleRate = value;
                // 根据IQ模式设置频率范围
                if (IsIQMode)
                {
                    _minFrequency = -value / 2f;  // 负频率
                    _maxFrequency = value / 2f;   // 正频率
                }
                else
                {
                    _minFrequency = 0;
                    _maxFrequency = value / 2f;  // 奈奎斯特频率
                }
            }
        }

        /// <summary>
        /// 是否为IQ模式（像SDRSharp那样处理立体声）
        /// Whether in IQ mode (process stereo like SDRSharp)
        /// </summary>
        public bool IsIQMode { get; set; } = false;

        /// <summary>
        /// 最小显示频率（Hz）
        /// Minimum display frequency in Hz
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
        /// Maximum display frequency in Hz
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
        /// 是否显示坐标轴
        /// Whether to show axis
        /// </summary>
        public bool ShowAxis { get; set; } = true;

        /// <summary>
        /// 坐标轴颜色
        /// Axis color
        /// </summary>
        public Color AxisColor { get; set; } = Color.FromArgb(150, 150, 150);

        /// <summary>
        /// 文字颜色
        /// Text color
        /// </summary>
        public Color TextColor { get; set; } = Color.FromArgb(200, 200, 200);

        /// <summary>
        /// 左边距（像素）
        /// Left margin in pixels
        /// </summary>
        public int LeftMargin
        {
            get { return _leftMargin; }
            set { _leftMargin = value; Invalidate(); }
        }

        /// <summary>
        /// 右边距（像素）
        /// Right margin in pixels
        /// </summary>
        public int RightMargin
        {
            get { return _rightMargin; }
            set { _rightMargin = value; Invalidate(); }
        }

        /// <summary>
        /// 上边距（像素）
        /// Top margin in pixels
        /// </summary>
        public int TopMargin
        {
            get { return _topMargin; }
            set { _topMargin = value; Invalidate(); }
        }

        /// <summary>
        /// 下边距（像素）
        /// Bottom margin in pixels
        /// </summary>
        public int BottomMargin
        {
            get { return _bottomMargin; }
            set { _bottomMargin = value; Invalidate(); }
        }

        #endregion

        /// <summary>
        /// 构造函数
        /// Constructor
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
        /// Initialize color map
        /// 使用蓝-青-黄-红渐变（类似热力图）
        /// Uses blue-cyan-yellow-red gradient (similar to heatmap)
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
        /// 设置自定义色彩映射表
        /// Set custom color map
        /// </summary>
        /// <param name="colorMap">256色的色彩数组 (256-color array)</param>
        public void SetColorMap(Color[] colorMap)
        {
            if (colorMap == null || colorMap.Length != 256)
                throw new ArgumentException("Color map must contain exactly 256 colors");

            ColorMap = colorMap;
            Invalidate();
        }

        /// <summary>
        /// 创建灰度色彩映射
        /// Create grayscale color map
        /// </summary>
        public void SetGrayscaleColorMap()
        {
            ColorMap = new Color[256];
            for (int i = 0; i < 256; i++)
            {
                ColorMap[i] = Color.FromArgb(i, i, i);
            }
            Invalidate();
        }

        /// <summary>
        /// 创建热力图色彩映射（红-黄-白）
        /// Create hot color map (red-yellow-white)
        /// </summary>
        public void SetHotColorMap()
        {
            ColorMap = new Color[256];
            for (int i = 0; i < 256; i++)
            {
                float t = i / 255f;
                if (t < 0.33f)
                {
                    // 黑色 -> 红色
                    float localT = t / 0.33f;
                    ColorMap[i] = Color.FromArgb((int)(255 * localT), 0, 0);
                }
                else if (t < 0.66f)
                {
                    // 红色 -> 黄色
                    float localT = (t - 0.33f) / 0.33f;
                    ColorMap[i] = Color.FromArgb(255, (int)(255 * localT), 0);
                }
                else
                {
                    // 黄色 -> 白色
                    float localT = (t - 0.66f) / 0.34f;
                    ColorMap[i] = Color.FromArgb(255, 255, (int)(255 * localT));
                }
            }
            Invalidate();
        }

        /// <summary>
        /// 创建冷色调色彩映射（蓝-青-白）
        /// Create cool color map (blue-cyan-white)
        /// </summary>
        public void SetCoolColorMap()
        {
            ColorMap = new Color[256];
            for (int i = 0; i < 256; i++)
            {
                float t = i / 255f;
                if (t < 0.5f)
                {
                    // 黑色 -> 蓝色
                    float localT = t / 0.5f;
                    ColorMap[i] = Color.FromArgb(0, 0, (int)(255 * localT));
                }
                else
                {
                    // 蓝色 -> 白色
                    float localT = (t - 0.5f) / 0.5f;
                    ColorMap[i] = Color.FromArgb((int)(255 * localT), (int)(255 * localT), 255);
                }
            }
            Invalidate();
        }

        /// <summary>
        /// 更新声谱图数据（线程安全）
        /// Updates spectrogram data (thread-safe)
        /// 新数据添加到顶部，像瀑布一样从上往下流动
        /// New data added to top, flows down like a waterfall
        /// 旧数据自动从底部清理，保持固定的显示窗口
        /// Old data automatically cleaned from bottom, maintains fixed display window
        /// </summary>
        /// <param name="spectrumData">新的频谱数据（一行） (New spectrum data, one row)</param>
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
        /// Clear all spectrogram data
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
        /// 应用颜色主题
        /// Apply color theme
        /// </summary>
        /// <param name="theme">颜色主题 (Color theme)</param>
        public void ApplyTheme(ColorTheme theme)
        {
            if (theme == null) return;

            BackgroundColor = theme.BackgroundColor;
            AxisColor = theme.AxisColor;
            TextColor = theme.TextColor;

            Invalidate();
        }

        /// <summary>
        /// 绘制控件
        /// Paint control
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
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                g.Clear(BackgroundColor);

                lock (_lockObject)
                {
                    if (_spectrogramData.Count > 0)
                    {
                        DrawSpectrogram(g);
                    }
                }

                if (ShowAxis)
                    DrawAxis(g);
            }

            // 将后台缓冲区绘制到屏幕
            e.Graphics.DrawImage(_backBuffer, 0, 0);
        }

        /// <summary>
        /// 绘制声谱图
        /// Draw spectrogram
        /// 使用unsafe代码直接操作像素数据以提升性能
        /// Uses unsafe code to directly manipulate pixel data for performance
        /// 时间轴：从上到下（最新的在顶部）
        /// Time axis: top to bottom (newest at top)
        /// 频率轴：从左到右（根据MinFrequency和MaxFrequency显示）
        /// Frequency axis: left to right (displays according to MinFrequency and MaxFrequency)
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

            // 计算绘图区域大小（减去边距）
            int plotWidth = Width - _leftMargin - _rightMargin;
            int plotHeight = Height - _topMargin - _bottomMargin;

            if (plotWidth <= 0 || plotHeight <= 0) return;

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

                // 使用最近邻插值缩放到绘图区域大小（保持清晰度）
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(spectrogramBitmap, _leftMargin, _topMargin, plotWidth, plotHeight);
            }
        }

        /// <summary>
        /// 绘制坐标轴
        /// Draw axis
        /// </summary>
        private void DrawAxis(Graphics g)
        {
            int plotWidth = Width - _leftMargin - _rightMargin;
            int plotHeight = Height - _topMargin - _bottomMargin;

            using (Pen axisPen = new Pen(AxisColor, 2))
            using (Font font = new Font("Arial", 8))
            using (SolidBrush textBrush = new SolidBrush(TextColor))
            {
                // 绘制Y轴（时间）
                g.DrawLine(axisPen, _leftMargin, _topMargin, _leftMargin, Height - _bottomMargin);

                // Y轴标题
                using (Font titleFont = new Font("Arial", 9, FontStyle.Bold))
                {
                    string yTitle = "时间";
                    SizeF titleSize = g.MeasureString(yTitle, titleFont);
                    g.TranslateTransform(15, Height / 2 + titleSize.Width / 2);
                    g.RotateTransform(-90);
                    g.DrawString(yTitle, titleFont, textBrush, 0, 0);
                    g.ResetTransform();
                }

                // 绘制X轴（频率）
                g.DrawLine(axisPen, _leftMargin, Height - _bottomMargin, Width - _rightMargin, Height - _bottomMargin);

                // X轴刻度标签
                for (int i = 0; i <= 10; i++)
                {
                    int x = _leftMargin + plotWidth * i / 10;
                    float freq = MinFrequency + (MaxFrequency - MinFrequency) * i / 10f;
                    string label = freq >= 1000 ? $"{freq / 1000:F1}k" : $"{freq:F0}";
                    SizeF size = g.MeasureString(label, font);
                    g.DrawString(label, font, textBrush, x - size.Width / 2, Height - _bottomMargin + 5);
                }

                // X轴标题
                using (Font titleFont = new Font("Arial", 9, FontStyle.Bold))
                {
                    string xTitle = "频率 (Hz)";
                    SizeF titleSize = g.MeasureString(xTitle, titleFont);
                    g.DrawString(xTitle, titleFont, textBrush,
                        (Width - titleSize.Width) / 2, Height - 15);
                }
            }
        }

        /// <summary>
        /// 释放资源
        /// Dispose resources
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
