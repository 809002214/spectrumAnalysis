using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SpectrumAnalysis.Controls
{
    /// <summary>
    /// 频谱图控件 - 显示实时频率能量分布的柱状图
    /// 可复用的自定义控件，支持缩放、坐标轴显示等功能
    /// </summary>
    public class SpectrumControl : UserControl
    {
        private float[] _spectrumData;              // 频谱数据数组
        private Bitmap _backBuffer;                 // 后台缓冲区（双缓冲）
        private readonly object _lockObject = new object();  // 线程同步锁
        private float _minDb = -80f;                // 最小dB值
        private float _maxDb = 0f;                  // 最大dB值
        private int _sampleRate = 44100;            // 采样率
        private Point _lastMousePos;                // 鼠标位置
        private bool _showTooltip = false;          // 是否显示提示
        private string _tooltipText = "";           // 提示文本

        // 显示区域边距（为坐标轴留出空间）
        private const int LeftMargin = 60;
        private const int RightMargin = 20;
        private const int TopMargin = 20;
        private const int BottomMargin = 40;

        /// <summary>
        /// 柱状图颜色
        /// </summary>
        public Color BarColor { get; set; } = Color.FromArgb(0, 150, 255);

        /// <summary>
        /// 背景颜色
        /// </summary>
        public Color BackgroundColor { get; set; } = Color.FromArgb(20, 20, 20);

        /// <summary>
        /// 网格颜色
        /// </summary>
        public Color GridColor { get; set; } = Color.FromArgb(40, 40, 40);

        /// <summary>
        /// 坐标轴颜色
        /// </summary>
        public Color AxisColor { get; set; } = Color.FromArgb(150, 150, 150);

        /// <summary>
        /// 文字颜色
        /// </summary>
        public Color TextColor { get; set; } = Color.FromArgb(200, 200, 200);

        /// <summary>
        /// 柱状图间距（像素）
        /// </summary>
        public int BarSpacing { get; set; } = 2;

        /// <summary>
        /// 是否显示坐标轴
        /// </summary>
        public bool ShowAxis { get; set; } = true;

        /// <summary>
        /// 是否显示网格
        /// </summary>
        public bool ShowGrid { get; set; } = true;

        /// <summary>
        /// 是否显示峰值标记
        /// </summary>
        public bool ShowPeaks { get; set; } = true;

        /// <summary>
        /// 峰值检测数量（1-10）
        /// </summary>
        public int PeakCount { get; set; } = 3;

        /// <summary>
        /// 峰值最小间隔（Hz）- 避免检测到相邻的峰值
        /// </summary>
        public float PeakMinDistance { get; set; } = 100;

        /// <summary>
        /// 显示模式
        /// </summary>
        public SpectrumDisplayMode DisplayMode { get; set; } = SpectrumDisplayMode.Bars;

        /// <summary>
        /// 最小显示频率（Hz）
        /// </summary>
        public float MinFrequency { get; set; } = 0;

        /// <summary>
        /// 最大显示频率（Hz）
        /// </summary>
        public float MaxFrequency { get; set; } = 22050;

        /// <summary>
        /// 设置采样率
        /// </summary>
        public int SampleRate
        {
            get { return _sampleRate; }
            set
            {
                _sampleRate = value;
                MaxFrequency = value / 2f;  // 奈奎斯特频率
            }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public SpectrumControl()
        {
            // 启用双缓冲和自定义绘制，减少闪烁
            SetStyle(ControlStyles.UserPaint |
                     ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer, true);

            ResizeRedraw = true;

            // 注册鼠标事件
            this.MouseMove += SpectrumControl_MouseMove;
            this.MouseLeave += SpectrumControl_MouseLeave;
            this.MouseWheel += SpectrumControl_MouseWheel;
        }

        /// <summary>
        /// 更新频谱数据（线程安全）
        /// </summary>
        /// <param name="spectrumData">频谱数据数组（dB值）</param>
        public void UpdateSpectrum(float[] spectrumData)
        {
            lock (_lockObject)
            {
                _spectrumData = spectrumData;
            }
            Invalidate();  // 触发重绘
        }

        /// <summary>
        /// 鼠标移动事件 - 显示频率和幅度信息
        /// </summary>
        private void SpectrumControl_MouseMove(object sender, MouseEventArgs e)
        {
            _lastMousePos = e.Location;

            if (_spectrumData != null && _spectrumData.Length > 0)
            {
                int plotWidth = Width - LeftMargin - RightMargin;
                int plotHeight = Height - TopMargin - BottomMargin;

                if (e.X >= LeftMargin && e.X <= Width - RightMargin &&
                    e.Y >= TopMargin && e.Y <= Height - BottomMargin)
                {
                    // 计算鼠标位置对应的频率
                    float relativeX = (e.X - LeftMargin) / (float)plotWidth;
                    float frequency = MinFrequency + relativeX * (MaxFrequency - MinFrequency);

                    // 计算对应的数据索引
                    int dataIndex = (int)(relativeX * _spectrumData.Length);
                    dataIndex = Math.Max(0, Math.Min(_spectrumData.Length - 1, dataIndex));

                    float db = _spectrumData[dataIndex];

                    _tooltipText = $"{frequency:F1} Hz, {db:F1} dB";
                    _showTooltip = true;
                }
                else
                {
                    _showTooltip = false;
                }

                Invalidate();
            }
        }

        /// <summary>
        /// 鼠标离开事件
        /// </summary>
        private void SpectrumControl_MouseLeave(object sender, EventArgs e)
        {
            _showTooltip = false;
            Invalidate();
        }

        /// <summary>
        /// 鼠标滚轮事件 - 缩放频率范围
        /// </summary>
        private void SpectrumControl_MouseWheel(object sender, MouseEventArgs e)
        {
            if (_spectrumData == null || _spectrumData.Length == 0) return;

            float zoomFactor = e.Delta > 0 ? 0.9f : 1.1f;
            float range = MaxFrequency - MinFrequency;
            float center = (MaxFrequency + MinFrequency) / 2f;

            float newRange = range * zoomFactor;
            MinFrequency = Math.Max(0, center - newRange / 2f);
            MaxFrequency = Math.Min(_sampleRate / 2f, center + newRange / 2f);

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
                _backBuffer = new Bitmap(Width, Height);
            }

            // 在后台缓冲区上绘制
            using (Graphics g = Graphics.FromImage(_backBuffer))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                g.Clear(BackgroundColor);

                if (ShowGrid)
                    DrawGrid(g);

                lock (_lockObject)
                {
                    if (_spectrumData != null && _spectrumData.Length > 0)
                    {
                        DrawSpectrum(g);
                    }
                }

                if (ShowAxis)
                    DrawAxis(g);

                if (_showTooltip)
                    DrawTooltip(g);
            }

            // 将后台缓冲区绘制到屏幕
            e.Graphics.DrawImage(_backBuffer, 0, 0);
        }

        /// <summary>
        /// 绘制网格
        /// </summary>
        private void DrawGrid(Graphics g)
        {
            int plotWidth = Width - LeftMargin - RightMargin;
            int plotHeight = Height - TopMargin - BottomMargin;

            using (Pen gridPen = new Pen(GridColor, 1))
            {
                // 绘制水平网格线（dB刻度）
                for (int i = 0; i <= 8; i++)
                {
                    int y = TopMargin + plotHeight * i / 8;
                    g.DrawLine(gridPen, LeftMargin, y, Width - RightMargin, y);
                }

                // 绘制垂直网格线（频率刻度）
                for (int i = 0; i <= 10; i++)
                {
                    int x = LeftMargin + plotWidth * i / 10;
                    g.DrawLine(gridPen, x, TopMargin, x, Height - BottomMargin);
                }
            }
        }

        /// <summary>
        /// 绘制坐标轴
        /// </summary>
        private void DrawAxis(Graphics g)
        {
            int plotWidth = Width - LeftMargin - RightMargin;
            int plotHeight = Height - TopMargin - BottomMargin;

            using (Pen axisPen = new Pen(AxisColor, 2))
            using (Font font = new Font("Arial", 8))
            using (SolidBrush textBrush = new SolidBrush(TextColor))
            {
                // 绘制Y轴（dB）
                g.DrawLine(axisPen, LeftMargin, TopMargin, LeftMargin, Height - BottomMargin);

                // Y轴刻度标签
                for (int i = 0; i <= 8; i++)
                {
                    int y = TopMargin + plotHeight * i / 8;
                    float db = _maxDb - (_maxDb - _minDb) * i / 8f;
                    string label = $"{db:F0}";
                    SizeF size = g.MeasureString(label, font);
                    g.DrawString(label, font, textBrush, LeftMargin - size.Width - 5, y - size.Height / 2);
                }

                // Y轴标题
                using (Font titleFont = new Font("Arial", 9, FontStyle.Bold))
                {
                    string yTitle = "幅度 (dB)";
                    SizeF titleSize = g.MeasureString(yTitle, titleFont);
                    g.TranslateTransform(15, Height / 2 + titleSize.Width / 2);
                    g.RotateTransform(-90);
                    g.DrawString(yTitle, titleFont, textBrush, 0, 0);
                    g.ResetTransform();
                }

                // 绘制X轴（频率）
                g.DrawLine(axisPen, LeftMargin, Height - BottomMargin, Width - RightMargin, Height - BottomMargin);

                // X轴刻度标签
                for (int i = 0; i <= 10; i++)
                {
                    int x = LeftMargin + plotWidth * i / 10;
                    float freq = MinFrequency + (MaxFrequency - MinFrequency) * i / 10f;
                    string label = freq >= 1000 ? $"{freq / 1000:F1}k" : $"{freq:F0}";
                    SizeF size = g.MeasureString(label, font);
                    g.DrawString(label, font, textBrush, x - size.Width / 2, Height - BottomMargin + 5);
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
        /// 绘制频谱
        /// </summary>
        private void DrawSpectrum(Graphics g)
        {
            int plotWidth = Width - LeftMargin - RightMargin;
            int plotHeight = Height - TopMargin - BottomMargin;

            // 计算显示的频率范围对应的数据索引
            int startIndex = (int)(MinFrequency / (_sampleRate / 2f) * _spectrumData.Length);
            int endIndex = (int)(MaxFrequency / (_sampleRate / 2f) * _spectrumData.Length);
            startIndex = Math.Max(0, startIndex);
            endIndex = Math.Min(_spectrumData.Length - 1, endIndex);

            int dataCount = endIndex - startIndex + 1;
            if (dataCount <= 0) return;

            if (DisplayMode == SpectrumDisplayMode.Bars)
            {
                DrawBars(g, plotWidth, plotHeight, startIndex, endIndex);
            }
            else if (DisplayMode == SpectrumDisplayMode.Line)
            {
                DrawLine(g, plotWidth, plotHeight, startIndex, endIndex);
            }
            else if (DisplayMode == SpectrumDisplayMode.FilledLine)
            {
                DrawFilledLine(g, plotWidth, plotHeight, startIndex, endIndex);
            }

            // 绘制峰值标记
            if (ShowPeaks)
            {
                DrawPeaks(g, plotWidth, plotHeight, startIndex, endIndex);
            }
        }

        /// <summary>
        /// 绘制柱状图模式
        /// </summary>
        private void DrawBars(Graphics g, int plotWidth, int plotHeight, int startIndex, int endIndex)
        {
            int dataCount = endIndex - startIndex + 1;
            int barCount = Math.Min(dataCount, plotWidth / (BarSpacing + 1));
            float barWidth = (float)plotWidth / barCount - BarSpacing;

            using (SolidBrush brush = new SolidBrush(BarColor))
            {
                for (int i = 0; i < barCount; i++)
                {
                    int dataIndex = startIndex + (int)((float)i / barCount * dataCount);
                    float db = _spectrumData[dataIndex];

                    float normalizedValue = (db - _minDb) / (_maxDb - _minDb);
                    normalizedValue = Math.Max(0, Math.Min(1, normalizedValue));

                    float barHeight = normalizedValue * plotHeight;
                    float x = LeftMargin + i * (barWidth + BarSpacing);
                    float y = TopMargin + plotHeight - barHeight;

                    int alpha = (int)(255 * (0.5f + 0.5f * normalizedValue));
                    brush.Color = Color.FromArgb(alpha, BarColor);

                    g.FillRectangle(brush, x, y, barWidth, barHeight);
                }
            }
        }

        /// <summary>
        /// 绘制曲线模式
        /// </summary>
        private void DrawLine(Graphics g, int plotWidth, int plotHeight, int startIndex, int endIndex)
        {
            int dataCount = endIndex - startIndex + 1;
            PointF[] points = new PointF[dataCount];

            for (int i = 0; i < dataCount; i++)
            {
                int dataIndex = startIndex + i;
                float db = _spectrumData[dataIndex];
                float normalizedValue = (db - _minDb) / (_maxDb - _minDb);
                normalizedValue = Math.Max(0, Math.Min(1, normalizedValue));

                float x = LeftMargin + (float)i / dataCount * plotWidth;
                float y = TopMargin + plotHeight * (1 - normalizedValue);
                points[i] = new PointF(x, y);
            }

            using (Pen pen = new Pen(BarColor, 2))
            {
                if (points.Length > 1)
                    g.DrawLines(pen, points);
            }
        }

        /// <summary>
        /// 绘制填充曲线模式
        /// </summary>
        private void DrawFilledLine(Graphics g, int plotWidth, int plotHeight, int startIndex, int endIndex)
        {
            int dataCount = endIndex - startIndex + 1;
            PointF[] points = new PointF[dataCount + 2];

            // 底部左角
            points[0] = new PointF(LeftMargin, TopMargin + plotHeight);

            for (int i = 0; i < dataCount; i++)
            {
                int dataIndex = startIndex + i;
                float db = _spectrumData[dataIndex];
                float normalizedValue = (db - _minDb) / (_maxDb - _minDb);
                normalizedValue = Math.Max(0, Math.Min(1, normalizedValue));

                float x = LeftMargin + (float)i / dataCount * plotWidth;
                float y = TopMargin + plotHeight * (1 - normalizedValue);
                points[i + 1] = new PointF(x, y);
            }

            // 底部右角
            points[dataCount + 1] = new PointF(LeftMargin + plotWidth, TopMargin + plotHeight);

            using (Brush brush = new SolidBrush(Color.FromArgb(100, BarColor)))
            {
                g.FillPolygon(brush, points);
            }

            // 绘制曲线部分（跳过底部两个角点）
            using (Pen pen = new Pen(BarColor, 2))
            {
                PointF[] linePoints = new PointF[dataCount];
                Array.Copy(points, 1, linePoints, 0, dataCount);
                if (linePoints.Length > 1)
                    g.DrawLines(pen, linePoints);
            }
        }

        /// <summary>
        /// 峰值信息结构
        /// </summary>
        private struct PeakInfo
        {
            public int Index;
            public float Db;
            public float Frequency;

            public PeakInfo(int index, float db, float frequency)
            {
                Index = index;
                Db = db;
                Frequency = frequency;
            }
        }

        /// <summary>
        /// 绘制峰值标记（支持多峰值检测）
        /// </summary>
        private void DrawPeaks(Graphics g, int plotWidth, int plotHeight, int startIndex, int endIndex)
        {
            // 峰值信息列表
            var peaks = new System.Collections.Generic.List<PeakInfo>();

            // 查找所有局部峰值
            for (int i = startIndex + 1; i < endIndex; i++)
            {
                // 检查是否为局部最大值（比左右两边都大）
                if (_spectrumData[i] > _spectrumData[i - 1] &&
                    _spectrumData[i] > _spectrumData[i + 1] &&
                    _spectrumData[i] > _minDb + 10)  // 只考虑明显的峰值
                {
                    float frequency = (float)i / _spectrumData.Length * (_sampleRate / 2f);
                    peaks.Add(new PeakInfo(i, _spectrumData[i], frequency));
                }
            }

            // 按幅度排序，取前N个最大的峰值
            peaks.Sort((a, b) => b.Db.CompareTo(a.Db));
            int peakCountToShow = Math.Min(PeakCount, peaks.Count);

            // 过滤掉距离太近的峰值
            var filteredPeaks = new System.Collections.Generic.List<PeakInfo>();
            for (int i = 0; i < peakCountToShow; i++)
            {
                var currentPeak = peaks[i];
                bool tooClose = false;

                // 检查是否与已选择的峰值距离太近
                foreach (var existingPeak in filteredPeaks)
                {
                    float freqDistance = Math.Abs(currentPeak.Frequency - existingPeak.Frequency);
                    if (freqDistance < PeakMinDistance)
                    {
                        tooClose = true;
                        break;
                    }
                }

                if (!tooClose)
                {
                    filteredPeaks.Add(currentPeak);
                }
            }

            // 绘制峰值标记
            Color[] peakColors = new Color[]
            {
                Color.Red,           // 第1个峰值：红色
                Color.Orange,        // 第2个峰值：橙色
                Color.Yellow,        // 第3个峰值：黄色
                Color.Lime,          // 第4个峰值：绿色
                Color.Cyan,          // 第5个峰值：青色
                Color.DeepSkyBlue,   // 第6个峰值：天蓝色
                Color.Magenta,       // 第7个峰值：洋红色
                Color.Pink,          // 第8个峰值：粉色
                Color.LightGreen,    // 第9个峰值：浅绿色
                Color.Violet         // 第10个峰值：紫色
            };

            using (Font font = new Font("Arial", 8, FontStyle.Bold))
            {
                for (int i = 0; i < filteredPeaks.Count; i++)
                {
                    var peak = filteredPeaks[i];
                    float relativePos = (peak.Frequency - MinFrequency) / (MaxFrequency - MinFrequency);

                    if (relativePos >= 0 && relativePos <= 1)
                    {
                        float normalizedValue = (peak.Db - _minDb) / (_maxDb - _minDb);
                        float x = LeftMargin + relativePos * plotWidth;
                        float y = TopMargin + plotHeight * (1 - normalizedValue);

                        Color peakColor = peakColors[i % peakColors.Length];

                        using (Pen pen = new Pen(peakColor, 2))
                        using (SolidBrush brush = new SolidBrush(peakColor))
                        {
                            // 绘制峰值标记圆圈
                            g.DrawEllipse(pen, x - 4, y - 4, 8, 8);

                            // 绘制峰值信息
                            string peakText = $"#{i + 1}: {peak.Frequency:F1} Hz";
                            SizeF textSize = g.MeasureString(peakText, font);

                            // 计算文本位置（避免重叠）
                            float textX = x - textSize.Width / 2;
                            float textY = y - textSize.Height - 10 - (i % 3) * 15;  // 错开显示

                            // 绘制文本背景
                            g.FillRectangle(new SolidBrush(Color.FromArgb(200, 0, 0, 0)),
                                textX - 2, textY - 2,
                                textSize.Width + 4, textSize.Height + 2);

                            // 绘制文本
                            g.DrawString(peakText, font, brush, textX, textY);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 绘制鼠标提示
        /// </summary>
        private void DrawTooltip(Graphics g)
        {
            using (Font font = new Font("Arial", 9))
            using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(220, 40, 40, 40)))
            using (SolidBrush textBrush = new SolidBrush(Color.White))
            using (Pen borderPen = new Pen(Color.FromArgb(100, 100, 100), 1))
            {
                SizeF textSize = g.MeasureString(_tooltipText, font);
                float tooltipX = _lastMousePos.X + 15;
                float tooltipY = _lastMousePos.Y - 25;

                // 确保提示框不超出边界
                if (tooltipX + textSize.Width + 10 > Width)
                    tooltipX = _lastMousePos.X - textSize.Width - 15;
                if (tooltipY < 0)
                    tooltipY = _lastMousePos.Y + 15;

                RectangleF tooltipRect = new RectangleF(tooltipX, tooltipY, textSize.Width + 10, textSize.Height + 6);
                g.FillRectangle(bgBrush, tooltipRect);
                g.DrawRectangle(borderPen, tooltipRect.X, tooltipRect.Y, tooltipRect.Width, tooltipRect.Height);
                g.DrawString(_tooltipText, font, textBrush, tooltipX + 5, tooltipY + 3);
            }
        }

        /// <summary>
        /// 重置缩放到全频段
        /// </summary>
        public void ResetZoom()
        {
            MinFrequency = 0;
            MaxFrequency = _sampleRate / 2f;
            Invalidate();
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

    /// <summary>
    /// 频谱显示模式枚举
    /// </summary>
    public enum SpectrumDisplayMode
    {
        /// <summary>柱状图</summary>
        Bars,
        /// <summary>曲线</summary>
        Line,
        /// <summary>填充曲线</summary>
        FilledLine
    }
}
