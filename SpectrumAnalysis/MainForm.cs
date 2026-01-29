using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using SpectrumAnalysis.Audio;
using AudioSpectrumVisualizer.Controls;
using AudioSpectrumVisualizer.Theme;

namespace SpectrumAnalysis
{
    /// <summary>
    /// 主窗体 - 音频频谱分析器
    /// </summary>
    public partial class MainForm : Form
    {
        // 音频配置常量
        private const int SAMPLE_RATE = 48000;           // 采样率 (Hz)
        private const int FFT_SIZE = 2048;               // FFT大小
        private const int NYQUIST_FREQUENCY = SAMPLE_RATE / 2;  // 奈奎斯特频率 (22050 Hz)
        private const int UPDATE_INTERVAL = 2;           // 更新间隔（每N次FFT更新一次）
        private const int PEAK_COUNT = 1;                // 峰值检测数量
        private const int PEAK_MIN_DISTANCE = 100;       // 峰值最小间隔 (Hz)

        // 频率范围预设
        private const float FREQ_MIN_FULL = 0f;          // 全频段最小频率
        private const float FREQ_MAX_FULL = NYQUIST_FREQUENCY;  // 全频段最大频率
        private const float FREQ_MIN_LOW = 20f;          // 低频段最小频率
        private const float FREQ_MAX_LOW = 500f;         // 低频段最大频率
        private const float FREQ_MIN_MID = 500f;         // 中频段最小频率
        private const float FREQ_MAX_MID = 4000f;        // 中频段最大频率
        private const float FREQ_MIN_HIGH = 4000f;       // 高频段最小频率
        private const float FREQ_MAX_HIGH = 20000f;      // 高频段最大频率

        private AudioCapture _audioCapture;  // 音频采集器

        // 当前主题
        private ColorTheme _currentTheme = ColorTheme.Dark;
        private bool _isDarkTheme = true;

        // 自定义标题栏相关
        private bool _dragging = false;
        private Point _dragCursorPoint;
        private Point _dragFormPoint;

        /// <summary>
        /// 构造函数
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            InitializeCustomTitleBar();
            InitializeAudioCapture();

            // 加载配置
            LoadSettings();

            // 应用主题
            ApplyTheme(_currentTheme);
        }

        /// <summary>
        /// 初始化自定义标题栏
        /// </summary>
        private void InitializeCustomTitleBar()
        {
            // 设置无边框窗体
            this.FormBorderStyle = FormBorderStyle.None;

            // 启用标题栏拖动
            _titleBarPanel.MouseDown += TitleBar_MouseDown;
            _titleBarPanel.MouseMove += TitleBar_MouseMove;
            _titleBarPanel.MouseUp += TitleBar_MouseUp;

            _appTitleLabel.MouseDown += TitleBar_MouseDown;
            _appTitleLabel.MouseMove += TitleBar_MouseMove;
            _appTitleLabel.MouseUp += TitleBar_MouseUp;

            // 设置按钮悬停效果
            SetupButtonHoverEffects();
        }

        /// <summary>
        /// 设置按钮悬停效果
        /// </summary>
        private void SetupButtonHoverEffects()
        {
            // 为所有按钮添加悬停效果
            foreach (Control control in this.Controls)
            {
                if (control is Button btn && btn != _closeButton && btn != _maximizeButton && btn != _minimizeButton)
                {
                    AddButtonHoverEffect(btn);
                }
            }
        }

        /// <summary>
        /// 添加按钮悬停效果
        /// </summary>
        private void AddButtonHoverEffect(Button button)
        {
            Color originalColor = button.BackColor;

            button.MouseEnter += (s, e) =>
            {
                button.BackColor = ControlPaint.Light(originalColor, 0.2f);
            };

            button.MouseLeave += (s, e) =>
            {
                button.BackColor = originalColor;
            };
        }

        /// <summary>
        /// 标题栏鼠标按下事件
        /// </summary>
        private void TitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _dragging = true;
                _dragCursorPoint = Cursor.Position;
                _dragFormPoint = this.Location;
            }
        }

        /// <summary>
        /// 标题栏鼠标移动事件
        /// </summary>
        private void TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(_dragCursorPoint));
                this.Location = Point.Add(_dragFormPoint, new Size(diff));
            }
        }

        /// <summary>
        /// 标题栏鼠标释放事件
        /// </summary>
        private void TitleBar_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }

        /// <summary>
        /// 最小化按钮点击事件
        /// </summary>
        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        /// <summary>
        /// 最大化/还原按钮点击事件
        /// </summary>
        private void MaximizeButton_Click(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                _maximizeButton.Text = "□";
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                _maximizeButton.Text = "❐";
            }
        }

        /// <summary>
        /// 关闭按钮点击事件
        /// </summary>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 初始化音频采集器
        /// </summary>
        private void InitializeAudioCapture()
        {
            // 创建音频采集器
            _audioCapture = new AudioCapture(SAMPLE_RATE, FFT_SIZE);

            // 设置刷新速度（默认每N次FFT更新一次，约10帧/秒）
            _audioCapture.UpdateInterval = UPDATE_INTERVAL;

            // 设置频谱控件的采样率
            _spectrumControl.SampleRate = SAMPLE_RATE;

            // 设置峰值检测数量
            _spectrumControl.PeakCount = PEAK_COUNT;
            _spectrumControl.PeakMinDistance = PEAK_MIN_DISTANCE;

            // 设置声谱图控件的采样率
            _spectrogramControl.SampleRate = SAMPLE_RATE;

            // 订阅FFT数据就绪事件
            _audioCapture.FFTDataAvailable += AudioCapture_FFTDataAvailable;

            // 订阅频谱控件的频率范围变化事件，同步到声谱图
            _spectrumControl.FrequencyRangeChanged += SpectrumControl_FrequencyRangeChanged;
        }

        /// <summary>
        /// 频谱控件频率范围变化事件 - 同步到声谱图
        /// </summary>
        private void SpectrumControl_FrequencyRangeChanged(object sender, EventArgs e)
        {
            // 同步频率范围到声谱图
            _spectrogramControl.MinFrequency = _spectrumControl.MinFrequency;
            _spectrogramControl.MaxFrequency = _spectrumControl.MaxFrequency;
        }

        /// <summary>
        /// 开始按钮点击事件
        /// </summary>
        private void StartButton_Click(object sender, EventArgs e)
        {
            try
            {
                _audioCapture.Start();
                _startButton.Enabled = false;
                _stopButton.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"启动音频采集失败: {ex.Message}", "错误",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 停止按钮点击事件
        /// </summary>
        private void StopButton_Click(object sender, EventArgs e)
        {
            _audioCapture.Stop();
            _startButton.Enabled = true;
            _stopButton.Enabled = false;
        }

        /// <summary>
        /// 清除按钮点击事件
        /// </summary>
        private void ClearButton_Click(object sender, EventArgs e)
        {
            _spectrogramControl.Clear();
        }

        /// <summary>
        /// 显示模式按钮点击事件
        /// </summary>
        private void DisplayModeButton_Click(object sender, EventArgs e)
        {
            // 循环切换显示模式
            switch (_spectrumControl.DisplayMode)
            {
                case SpectrumDisplayMode.Bars:
                    _spectrumControl.DisplayMode = SpectrumDisplayMode.Line;
                    _displayModeButton.Text = "📈 曲线";
                    break;
                case SpectrumDisplayMode.Line:
                    _spectrumControl.DisplayMode = SpectrumDisplayMode.FilledLine;
                    _displayModeButton.Text = "📉 填充";
                    break;
                case SpectrumDisplayMode.FilledLine:
                    _spectrumControl.DisplayMode = SpectrumDisplayMode.Bars;
                    _displayModeButton.Text = "📊 柱状图";
                    break;
            }
        }

        /// <summary>
        /// 重置缩放按钮点击事件
        /// </summary>
        private void ResetZoomButton_Click(object sender, EventArgs e)
        {
            _spectrumControl.ResetZoom();
            // FrequencyRangeChanged 事件会自动同步到声谱图
        }

        /// <summary>
        /// 全频段按钮点击事件
        /// </summary>
        private void FullRangeButton_Click(object sender, EventArgs e)
        {
            _spectrumControl.MinFrequency = FREQ_MIN_FULL;
            _spectrumControl.MaxFrequency = FREQ_MAX_FULL;
            // FrequencyRangeChanged 事件会自动同步到声谱图
        }

        /// <summary>
        /// 低频段按钮点击事件
        /// </summary>
        private void LowFreqButton_Click(object sender, EventArgs e)
        {
            _spectrumControl.MinFrequency = FREQ_MIN_LOW;
            _spectrumControl.MaxFrequency = FREQ_MAX_LOW;
            // FrequencyRangeChanged 事件会自动同步到声谱图
        }

        /// <summary>
        /// 中频段按钮点击事件
        /// </summary>
        private void MidFreqButton_Click(object sender, EventArgs e)
        {
            _spectrumControl.MinFrequency = FREQ_MIN_MID;
            _spectrumControl.MaxFrequency = FREQ_MAX_MID;
            // FrequencyRangeChanged 事件会自动同步到声谱图
        }

        /// <summary>
        /// 高频段按钮点击事件
        /// </summary>
        private void HighFreqButton_Click(object sender, EventArgs e)
        {
            _spectrumControl.MinFrequency = FREQ_MIN_HIGH;
            _spectrumControl.MaxFrequency = FREQ_MAX_HIGH;
            // FrequencyRangeChanged 事件会自动同步到声谱图
        }

        /// <summary>
        /// 显示网格复选框状态改变事件
        /// </summary>
        private void ShowGridCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _spectrumControl.ShowGrid = _showGridCheckBox.Checked;
        }

        /// <summary>
        /// 显示坐标轴复选框状态改变事件
        /// </summary>
        private void ShowAxisCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _spectrumControl.ShowAxis = _showAxisCheckBox.Checked;
        }

        /// <summary>
        /// 显示峰值复选框状态改变事件
        /// </summary>
        private void ShowPeaksCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _spectrumControl.ShowPeaks = _showPeaksCheckBox.Checked;
        }

        /// <summary>
        /// 应用幅度范围按钮点击事件
        /// </summary>
        private void ApplyAmplitudeButton_Click(object sender, EventArgs e)
        {
            // 获取用户输入的幅度范围
            float minDb = (float)_minDbNumeric.Value;
            float maxDb = (float)_maxDbNumeric.Value;

            // 验证范围有效性
            if (minDb >= maxDb)
            {
                MessageBox.Show("最小值必须小于最大值！", "无效范围",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 应用到频谱控件
            _spectrumControl.MinDb = minDb;
            _spectrumControl.MaxDb = maxDb;

            // 应用到声谱图控件
            _spectrogramControl.MinDb = minDb;
            _spectrogramControl.MaxDb = maxDb;
        }

        /// <summary>
        /// 主题切换按钮点击事件
        /// </summary>
        private void ThemeButton_Click(object sender, EventArgs e)
        {
            // 切换主题
            _isDarkTheme = !_isDarkTheme;
            _currentTheme = _isDarkTheme ? ColorTheme.Dark : ColorTheme.Light;

            // 应用主题
            ApplyTheme(_currentTheme);

            // 更新按钮文字
            _themeButton.Text = _isDarkTheme ? "🌙 暗色" : "☀️ 亮色";

            // 保存主题设置
            Properties.Settings.Default.ThemeName = _currentTheme.Name;
            Properties.Settings.Default.Save();
        }

        /// <summary>
        /// 应用主题到所有控件
        /// </summary>
        private void ApplyTheme(ColorTheme theme)
        {
            // 应用到可视化控件
            _spectrumControl.ApplyTheme(theme);
            _spectrogramControl.ApplyTheme(theme);

            // 应用到窗体
            this.BackColor = theme.FormBackColor;

            // 应用到标题栏
            _titleBarPanel.BackColor = theme.TitleBarBackColor;
            _appTitleLabel.ForeColor = theme.TitleBarTextColor;

            // 应用到控制面板
            _controlPanel.BackColor = theme.FormBackColor;

            // 应用到所有按钮
            foreach (Control control in _controlPanel.Controls)
            {
                if (control is Button btn)
                {
                    btn.BackColor = theme.ButtonBackColor;
                    btn.ForeColor = theme.ButtonTextColor;
                }
                else if (control is CheckBox chk)
                {
                    chk.ForeColor = theme.CheckBoxTextColor;
                }
                else if (control is Label lbl)
                {
                    lbl.ForeColor = theme.TextColor;
                }
            }

            // 应用到标题栏按钮
            _minimizeButton.ForeColor = theme.TitleBarTextColor;
            _maximizeButton.ForeColor = theme.TitleBarTextColor;
            _closeButton.ForeColor = theme.TitleBarTextColor;

            // 应用到分隔条
            _splitContainer.BackColor = theme.SplitterColor;
        }

        /// <summary>
        /// FFT数据到达事件处理
        /// 在UI线程上更新控件
        /// </summary>
        private void AudioCapture_FFTDataAvailable(object sender, float[] fftData)
        {
            if (InvokeRequired)
            {
                // 如果不在UI线程，使用BeginInvoke异步调用
                BeginInvoke(new Action(() =>
                {
                    _spectrumControl.UpdateSpectrum(fftData);
                    _spectrogramControl.UpdateSpectrogram(fftData);
                }));
            }
            else
            {
                // 如果已在UI线程，直接更新
                _spectrumControl.UpdateSpectrum(fftData);
                _spectrogramControl.UpdateSpectrogram(fftData);
            }
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // 保存配置
            SaveSettings();

            _audioCapture?.Dispose();
            base.OnFormClosing(e);
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        private void LoadSettings()
        {
            try
            {
                // 如果是第一次运行，使用默认值
                if (Properties.Settings.Default.FirstRun)
                {
                    Properties.Settings.Default.FirstRun = false;
                    Properties.Settings.Default.Save();
                    return;
                }

                // 加载主题
                string themeName = Properties.Settings.Default.ThemeName;
                _currentTheme = ColorTheme.GetTheme(themeName);
                _isDarkTheme = themeName.Equals("Dark", StringComparison.OrdinalIgnoreCase);
                _themeButton.Text = _isDarkTheme ? "🌙 暗色" : "☀️ 亮色";

                // 加载窗口位置和大小
                if (Properties.Settings.Default.WindowLocation.X > 0 &&
                    Properties.Settings.Default.WindowLocation.Y > 0)
                {
                    this.StartPosition = FormStartPosition.Manual;
                    this.Location = Properties.Settings.Default.WindowLocation;
                }

                if (Properties.Settings.Default.WindowSize.Width > 0 &&
                    Properties.Settings.Default.WindowSize.Height > 0)
                {
                    this.Size = Properties.Settings.Default.WindowSize;
                }

                this.WindowState = Properties.Settings.Default.WindowState;

                // 加载显示选项
                _showGridCheckBox.Checked = Properties.Settings.Default.ShowGrid;
                _showAxisCheckBox.Checked = Properties.Settings.Default.ShowAxis;
                _showPeaksCheckBox.Checked = Properties.Settings.Default.ShowPeaks;

                // 加载显示模式
                string displayMode = Properties.Settings.Default.DisplayMode;
                switch (displayMode)
                {
                    case "Line":
                        _spectrumControl.DisplayMode = SpectrumDisplayMode.Line;
                        _displayModeButton.Text = "📈 曲线";
                        break;
                    case "FilledLine":
                        _spectrumControl.DisplayMode = SpectrumDisplayMode.FilledLine;
                        _displayModeButton.Text = "📉 填充";
                        break;
                    default:
                        _spectrumControl.DisplayMode = SpectrumDisplayMode.Bars;
                        _displayModeButton.Text = "📊 柱状图";
                        break;
                }
            }
            catch (Exception ex)
            {
                // 如果加载配置失败，使用默认值
                System.Diagnostics.Debug.WriteLine($"加载配置失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        private void SaveSettings()
        {
            try
            {
                // 保存主题
                Properties.Settings.Default.ThemeName = _currentTheme.Name;

                // 保存窗口位置和大小（只在正常状态下保存）
                if (this.WindowState == FormWindowState.Normal)
                {
                    Properties.Settings.Default.WindowLocation = this.Location;
                    Properties.Settings.Default.WindowSize = this.Size;
                }
                Properties.Settings.Default.WindowState = this.WindowState;

                // 保存显示选项
                Properties.Settings.Default.ShowGrid = _showGridCheckBox.Checked;
                Properties.Settings.Default.ShowAxis = _showAxisCheckBox.Checked;
                Properties.Settings.Default.ShowPeaks = _showPeaksCheckBox.Checked;

                // 保存显示模式
                Properties.Settings.Default.DisplayMode = _spectrumControl.DisplayMode.ToString();

                // 保存到文件
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"保存配置失败: {ex.Message}");
            }
        }

        private void _separator2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
