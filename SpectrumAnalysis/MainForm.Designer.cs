namespace SpectrumAnalysis
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // 控件声明
        private System.Windows.Forms.Panel _titleBarPanel;
        private System.Windows.Forms.Label _appTitleLabel;
        private System.Windows.Forms.Label _appIconLabel;
        private System.Windows.Forms.Button _minimizeButton;
        private System.Windows.Forms.Button _maximizeButton;
        private System.Windows.Forms.Button _closeButton;
        private System.Windows.Forms.Panel _controlPanel;
        private System.Windows.Forms.Button _startButton;
        private System.Windows.Forms.Button _stopButton;
        private System.Windows.Forms.Button _clearButton;
        private System.Windows.Forms.Button _displayModeButton;
        private System.Windows.Forms.Button _resetZoomButton;
        private System.Windows.Forms.Button _themeButton;
        private System.Windows.Forms.Button _fullRangeButton;
        private System.Windows.Forms.Button _lowFreqButton;
        private System.Windows.Forms.Button _midFreqButton;
        private System.Windows.Forms.Button _highFreqButton;
        private System.Windows.Forms.CheckBox _showGridCheckBox;
        private System.Windows.Forms.CheckBox _showAxisCheckBox;
        private System.Windows.Forms.CheckBox _showPeaksCheckBox;
        private System.Windows.Forms.CheckBox _iqModeCheckBox;
        private System.Windows.Forms.Label _displayOptionsLabel;
        private System.Windows.Forms.Label _gainLabel;
        private System.Windows.Forms.TrackBar _gainTrackBar;
        private System.Windows.Forms.Label _gainValueLabel;
        private System.Windows.Forms.Panel _separator1;
        private System.Windows.Forms.Panel _separator2;
        private System.Windows.Forms.Label _amplitudeRangeLabel;
        private System.Windows.Forms.NumericUpDown _minDbNumeric;
        private System.Windows.Forms.Label _dbRangeSeparatorLabel;
        private System.Windows.Forms.NumericUpDown _maxDbNumeric;
        private System.Windows.Forms.Button _applyAmplitudeButton;
        private System.Windows.Forms.SplitContainer _splitContainer;
        private AudioSpectrumVisualizer.Controls.SpectrumControl _spectrumControl;
        private AudioSpectrumVisualizer.Controls.SpectrogramControl _spectrogramControl;

        /// <summary>
        /// 清理所有正在使用的资源
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this._titleBarPanel = new System.Windows.Forms.Panel();
            this._appIconLabel = new System.Windows.Forms.Label();
            this._appTitleLabel = new System.Windows.Forms.Label();
            this._minimizeButton = new System.Windows.Forms.Button();
            this._maximizeButton = new System.Windows.Forms.Button();
            this._closeButton = new System.Windows.Forms.Button();
            this._controlPanel = new System.Windows.Forms.Panel();
            this._startButton = new System.Windows.Forms.Button();
            this._stopButton = new System.Windows.Forms.Button();
            this._clearButton = new System.Windows.Forms.Button();
            this._separator1 = new System.Windows.Forms.Panel();
            this._displayModeButton = new System.Windows.Forms.Button();
            this._resetZoomButton = new System.Windows.Forms.Button();
            this._themeButton = new System.Windows.Forms.Button();
            this._separator2 = new System.Windows.Forms.Panel();
            this._fullRangeButton = new System.Windows.Forms.Button();
            this._lowFreqButton = new System.Windows.Forms.Button();
            this._midFreqButton = new System.Windows.Forms.Button();
            this._highFreqButton = new System.Windows.Forms.Button();
            this._displayOptionsLabel = new System.Windows.Forms.Label();
            this._showGridCheckBox = new System.Windows.Forms.CheckBox();
            this._showAxisCheckBox = new System.Windows.Forms.CheckBox();
            this._showPeaksCheckBox = new System.Windows.Forms.CheckBox();
            this._iqModeCheckBox = new System.Windows.Forms.CheckBox();
            this._gainLabel = new System.Windows.Forms.Label();
            this._gainTrackBar = new System.Windows.Forms.TrackBar();
            this._gainValueLabel = new System.Windows.Forms.Label();
            this._amplitudeRangeLabel = new System.Windows.Forms.Label();
            this._minDbNumeric = new System.Windows.Forms.NumericUpDown();
            this._dbRangeSeparatorLabel = new System.Windows.Forms.Label();
            this._maxDbNumeric = new System.Windows.Forms.NumericUpDown();
            this._applyAmplitudeButton = new System.Windows.Forms.Button();
            this._splitContainer = new System.Windows.Forms.SplitContainer();
            this._spectrumControl = new AudioSpectrumVisualizer.Controls.SpectrumControl();
            this._spectrogramControl = new AudioSpectrumVisualizer.Controls.SpectrogramControl();
            this._titleBarPanel.SuspendLayout();
            this._controlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._gainTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._minDbNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._maxDbNumeric)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).BeginInit();
            this._splitContainer.Panel1.SuspendLayout();
            this._splitContainer.Panel2.SuspendLayout();
            this._splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // _titleBarPanel
            // 
            this._titleBarPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this._titleBarPanel.Controls.Add(this._appIconLabel);
            this._titleBarPanel.Controls.Add(this._appTitleLabel);
            this._titleBarPanel.Controls.Add(this._minimizeButton);
            this._titleBarPanel.Controls.Add(this._maximizeButton);
            this._titleBarPanel.Controls.Add(this._closeButton);
            this._titleBarPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._titleBarPanel.Location = new System.Drawing.Point(0, 0);
            this._titleBarPanel.Name = "_titleBarPanel";
            this._titleBarPanel.Size = new System.Drawing.Size(1200, 37);
            this._titleBarPanel.TabIndex = 0;
            // 
            // _appIconLabel
            // 
            this._appIconLabel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this._appIconLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this._appIconLabel.Location = new System.Drawing.Point(10, 5);
            this._appIconLabel.Name = "_appIconLabel";
            this._appIconLabel.Size = new System.Drawing.Size(35, 28);
            this._appIconLabel.TabIndex = 0;
            this._appIconLabel.Text = "♪";
            this._appIconLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _appTitleLabel
            // 
            this._appTitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._appTitleLabel.Font = new System.Drawing.Font("微软雅黑", 14F, System.Drawing.FontStyle.Bold);
            this._appTitleLabel.ForeColor = System.Drawing.Color.White;
            this._appTitleLabel.Location = new System.Drawing.Point(45, 0);
            this._appTitleLabel.Name = "_appTitleLabel";
            this._appTitleLabel.Size = new System.Drawing.Size(1035, 37);
            this._appTitleLabel.TabIndex = 1;
            this._appTitleLabel.Text = "实时音频频谱分析器";
            this._appTitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _minimizeButton
            // 
            this._minimizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._minimizeButton.FlatAppearance.BorderSize = 0;
            this._minimizeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this._minimizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._minimizeButton.Font = new System.Drawing.Font("Segoe UI", 12F);
            this._minimizeButton.ForeColor = System.Drawing.Color.White;
            this._minimizeButton.Location = new System.Drawing.Point(1080, 0);
            this._minimizeButton.Name = "_minimizeButton";
            this._minimizeButton.Size = new System.Drawing.Size(40, 37);
            this._minimizeButton.TabIndex = 2;
            this._minimizeButton.Text = "─";
            this._minimizeButton.UseVisualStyleBackColor = true;
            this._minimizeButton.Click += new System.EventHandler(this.MinimizeButton_Click);
            // 
            // _maximizeButton
            // 
            this._maximizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._maximizeButton.FlatAppearance.BorderSize = 0;
            this._maximizeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this._maximizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._maximizeButton.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._maximizeButton.ForeColor = System.Drawing.Color.White;
            this._maximizeButton.Location = new System.Drawing.Point(1120, 0);
            this._maximizeButton.Name = "_maximizeButton";
            this._maximizeButton.Size = new System.Drawing.Size(40, 37);
            this._maximizeButton.TabIndex = 3;
            this._maximizeButton.Text = "□";
            this._maximizeButton.UseVisualStyleBackColor = true;
            this._maximizeButton.Click += new System.EventHandler(this.MaximizeButton_Click);
            // 
            // _closeButton
            // 
            this._closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._closeButton.FlatAppearance.BorderSize = 0;
            this._closeButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this._closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._closeButton.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._closeButton.ForeColor = System.Drawing.Color.White;
            this._closeButton.Location = new System.Drawing.Point(1160, 0);
            this._closeButton.Name = "_closeButton";
            this._closeButton.Size = new System.Drawing.Size(40, 37);
            this._closeButton.TabIndex = 4;
            this._closeButton.Text = "✕";
            this._closeButton.UseVisualStyleBackColor = true;
            this._closeButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // _controlPanel
            // 
            this._controlPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this._controlPanel.Controls.Add(this._startButton);
            this._controlPanel.Controls.Add(this._stopButton);
            this._controlPanel.Controls.Add(this._clearButton);
            this._controlPanel.Controls.Add(this._separator1);
            this._controlPanel.Controls.Add(this._displayModeButton);
            this._controlPanel.Controls.Add(this._resetZoomButton);
            this._controlPanel.Controls.Add(this._themeButton);
            this._controlPanel.Controls.Add(this._separator2);
            this._controlPanel.Controls.Add(this._fullRangeButton);
            this._controlPanel.Controls.Add(this._lowFreqButton);
            this._controlPanel.Controls.Add(this._midFreqButton);
            this._controlPanel.Controls.Add(this._highFreqButton);
            this._controlPanel.Controls.Add(this._displayOptionsLabel);
            this._controlPanel.Controls.Add(this._showGridCheckBox);
            this._controlPanel.Controls.Add(this._showAxisCheckBox);
            this._controlPanel.Controls.Add(this._showPeaksCheckBox);
            this._controlPanel.Controls.Add(this._iqModeCheckBox);
            this._controlPanel.Controls.Add(this._gainLabel);
            this._controlPanel.Controls.Add(this._gainTrackBar);
            this._controlPanel.Controls.Add(this._gainValueLabel);
            this._controlPanel.Controls.Add(this._amplitudeRangeLabel);
            this._controlPanel.Controls.Add(this._minDbNumeric);
            this._controlPanel.Controls.Add(this._dbRangeSeparatorLabel);
            this._controlPanel.Controls.Add(this._maxDbNumeric);
            this._controlPanel.Controls.Add(this._applyAmplitudeButton);
            this._controlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._controlPanel.Location = new System.Drawing.Point(0, 37);
            this._controlPanel.Name = "_controlPanel";
            this._controlPanel.Padding = new System.Windows.Forms.Padding(15, 15, 15, 10);
            this._controlPanel.Size = new System.Drawing.Size(1200, 90);
            this._controlPanel.TabIndex = 1;
            // 
            // _startButton
            // 
            this._startButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this._startButton.FlatAppearance.BorderSize = 0;
            this._startButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._startButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this._startButton.ForeColor = System.Drawing.Color.White;
            this._startButton.Location = new System.Drawing.Point(0, 12);
            this._startButton.Name = "_startButton";
            this._startButton.Size = new System.Drawing.Size(100, 30);
            this._startButton.TabIndex = 0;
            this._startButton.Text = "▶ 开始采集";
            this._startButton.UseVisualStyleBackColor = false;
            this._startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // _stopButton
            // 
            this._stopButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this._stopButton.Enabled = false;
            this._stopButton.FlatAppearance.BorderSize = 0;
            this._stopButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._stopButton.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this._stopButton.ForeColor = System.Drawing.Color.White;
            this._stopButton.Location = new System.Drawing.Point(110, 12);
            this._stopButton.Name = "_stopButton";
            this._stopButton.Size = new System.Drawing.Size(100, 30);
            this._stopButton.TabIndex = 1;
            this._stopButton.Text = "■ 停止采集";
            this._stopButton.UseVisualStyleBackColor = false;
            this._stopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // _clearButton
            // 
            this._clearButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this._clearButton.FlatAppearance.BorderSize = 0;
            this._clearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._clearButton.Font = new System.Drawing.Font("微软雅黑", 9F);
            this._clearButton.ForeColor = System.Drawing.Color.White;
            this._clearButton.Location = new System.Drawing.Point(220, 12);
            this._clearButton.Name = "_clearButton";
            this._clearButton.Size = new System.Drawing.Size(100, 30);
            this._clearButton.TabIndex = 2;
            this._clearButton.Text = "🗑️ 清除";
            this._clearButton.UseVisualStyleBackColor = false;
            this._clearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // _separator1
            // 
            this._separator1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this._separator1.Location = new System.Drawing.Point(340, 12);
            this._separator1.Name = "_separator1";
            this._separator1.Size = new System.Drawing.Size(1, 30);
            this._separator1.TabIndex = 3;
            // 
            // _displayModeButton
            // 
            this._displayModeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this._displayModeButton.FlatAppearance.BorderSize = 0;
            this._displayModeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._displayModeButton.Font = new System.Drawing.Font("微软雅黑", 9F);
            this._displayModeButton.ForeColor = System.Drawing.Color.White;
            this._displayModeButton.Location = new System.Drawing.Point(360, 12);
            this._displayModeButton.Name = "_displayModeButton";
            this._displayModeButton.Size = new System.Drawing.Size(100, 30);
            this._displayModeButton.TabIndex = 4;
            this._displayModeButton.Text = "📊 柱状图";
            this._displayModeButton.UseVisualStyleBackColor = false;
            this._displayModeButton.Click += new System.EventHandler(this.DisplayModeButton_Click);
            // 
            // _resetZoomButton
            // 
            this._resetZoomButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this._resetZoomButton.FlatAppearance.BorderSize = 0;
            this._resetZoomButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._resetZoomButton.Font = new System.Drawing.Font("微软雅黑", 9F);
            this._resetZoomButton.ForeColor = System.Drawing.Color.White;
            this._resetZoomButton.Location = new System.Drawing.Point(470, 12);
            this._resetZoomButton.Name = "_resetZoomButton";
            this._resetZoomButton.Size = new System.Drawing.Size(100, 30);
            this._resetZoomButton.TabIndex = 5;
            this._resetZoomButton.Text = "🔄 重置";
            this._resetZoomButton.UseVisualStyleBackColor = false;
            this._resetZoomButton.Click += new System.EventHandler(this.ResetZoomButton_Click);
            // 
            // _themeButton
            // 
            this._themeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this._themeButton.FlatAppearance.BorderSize = 0;
            this._themeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._themeButton.Font = new System.Drawing.Font("微软雅黑", 9F);
            this._themeButton.ForeColor = System.Drawing.Color.White;
            this._themeButton.Location = new System.Drawing.Point(580, 12);
            this._themeButton.Name = "_themeButton";
            this._themeButton.Size = new System.Drawing.Size(100, 30);
            this._themeButton.TabIndex = 6;
            this._themeButton.Text = "🌙 暗色";
            this._themeButton.UseVisualStyleBackColor = false;
            this._themeButton.Click += new System.EventHandler(this.ThemeButton_Click);
            // 
            // _separator2
            // 
            this._separator2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this._separator2.Location = new System.Drawing.Point(700, 12);
            this._separator2.Name = "_separator2";
            this._separator2.Size = new System.Drawing.Size(1, 30);
            this._separator2.TabIndex = 7;
            this._separator2.Paint += new System.Windows.Forms.PaintEventHandler(this._separator2_Paint);
            // 
            // _fullRangeButton
            // 
            this._fullRangeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this._fullRangeButton.FlatAppearance.BorderSize = 0;
            this._fullRangeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._fullRangeButton.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this._fullRangeButton.ForeColor = System.Drawing.Color.White;
            this._fullRangeButton.Location = new System.Drawing.Point(720, 12);
            this._fullRangeButton.Name = "_fullRangeButton";
            this._fullRangeButton.Size = new System.Drawing.Size(80, 30);
            this._fullRangeButton.TabIndex = 8;
            this._fullRangeButton.Text = "🌐 全频段";
            this._fullRangeButton.UseVisualStyleBackColor = false;
            this._fullRangeButton.Click += new System.EventHandler(this.FullRangeButton_Click);
            // 
            // _lowFreqButton
            // 
            this._lowFreqButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this._lowFreqButton.FlatAppearance.BorderSize = 0;
            this._lowFreqButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._lowFreqButton.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this._lowFreqButton.ForeColor = System.Drawing.Color.White;
            this._lowFreqButton.Location = new System.Drawing.Point(810, 12);
            this._lowFreqButton.Name = "_lowFreqButton";
            this._lowFreqButton.Size = new System.Drawing.Size(80, 30);
            this._lowFreqButton.TabIndex = 9;
            this._lowFreqButton.Text = "🔉 低频";
            this._lowFreqButton.UseVisualStyleBackColor = false;
            this._lowFreqButton.Click += new System.EventHandler(this.LowFreqButton_Click);
            // 
            // _midFreqButton
            // 
            this._midFreqButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this._midFreqButton.FlatAppearance.BorderSize = 0;
            this._midFreqButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._midFreqButton.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this._midFreqButton.ForeColor = System.Drawing.Color.White;
            this._midFreqButton.Location = new System.Drawing.Point(900, 12);
            this._midFreqButton.Name = "_midFreqButton";
            this._midFreqButton.Size = new System.Drawing.Size(80, 30);
            this._midFreqButton.TabIndex = 10;
            this._midFreqButton.Text = "🔊 中频";
            this._midFreqButton.UseVisualStyleBackColor = false;
            this._midFreqButton.Click += new System.EventHandler(this.MidFreqButton_Click);
            // 
            // _highFreqButton
            // 
            this._highFreqButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this._highFreqButton.FlatAppearance.BorderSize = 0;
            this._highFreqButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._highFreqButton.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this._highFreqButton.ForeColor = System.Drawing.Color.White;
            this._highFreqButton.Location = new System.Drawing.Point(990, 12);
            this._highFreqButton.Name = "_highFreqButton";
            this._highFreqButton.Size = new System.Drawing.Size(80, 30);
            this._highFreqButton.TabIndex = 11;
            this._highFreqButton.Text = "📢 高频";
            this._highFreqButton.UseVisualStyleBackColor = false;
            this._highFreqButton.Click += new System.EventHandler(this.HighFreqButton_Click);
            // 
            // _displayOptionsLabel
            // 
            this._displayOptionsLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this._displayOptionsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this._displayOptionsLabel.Location = new System.Drawing.Point(0, 53);
            this._displayOptionsLabel.Name = "_displayOptionsLabel";
            this._displayOptionsLabel.Size = new System.Drawing.Size(80, 26);
            this._displayOptionsLabel.TabIndex = 12;
            this._displayOptionsLabel.Text = "显示选项";
            this._displayOptionsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _showGridCheckBox
            // 
            this._showGridCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this._showGridCheckBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this._showGridCheckBox.Checked = true;
            this._showGridCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._showGridCheckBox.FlatAppearance.BorderSize = 0;
            this._showGridCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this._showGridCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._showGridCheckBox.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this._showGridCheckBox.ForeColor = System.Drawing.Color.White;
            this._showGridCheckBox.Location = new System.Drawing.Point(90, 53);
            this._showGridCheckBox.Name = "_showGridCheckBox";
            this._showGridCheckBox.Size = new System.Drawing.Size(80, 26);
            this._showGridCheckBox.TabIndex = 13;
            this._showGridCheckBox.Text = "☑ 网格";
            this._showGridCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._showGridCheckBox.UseVisualStyleBackColor = false;
            this._showGridCheckBox.CheckedChanged += new System.EventHandler(this.ShowGridCheckBox_CheckedChanged);
            // 
            // _showAxisCheckBox
            // 
            this._showAxisCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this._showAxisCheckBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this._showAxisCheckBox.Checked = true;
            this._showAxisCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._showAxisCheckBox.FlatAppearance.BorderSize = 0;
            this._showAxisCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this._showAxisCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._showAxisCheckBox.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this._showAxisCheckBox.ForeColor = System.Drawing.Color.White;
            this._showAxisCheckBox.Location = new System.Drawing.Point(180, 53);
            this._showAxisCheckBox.Name = "_showAxisCheckBox";
            this._showAxisCheckBox.Size = new System.Drawing.Size(90, 26);
            this._showAxisCheckBox.TabIndex = 14;
            this._showAxisCheckBox.Text = "☑ 坐标轴";
            this._showAxisCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._showAxisCheckBox.UseVisualStyleBackColor = false;
            this._showAxisCheckBox.CheckedChanged += new System.EventHandler(this.ShowAxisCheckBox_CheckedChanged);
            // 
            // _showPeaksCheckBox
            // 
            this._showPeaksCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this._showPeaksCheckBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this._showPeaksCheckBox.Checked = true;
            this._showPeaksCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this._showPeaksCheckBox.FlatAppearance.BorderSize = 0;
            this._showPeaksCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this._showPeaksCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._showPeaksCheckBox.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this._showPeaksCheckBox.ForeColor = System.Drawing.Color.White;
            this._showPeaksCheckBox.Location = new System.Drawing.Point(280, 53);
            this._showPeaksCheckBox.Name = "_showPeaksCheckBox";
            this._showPeaksCheckBox.Size = new System.Drawing.Size(100, 26);
            this._showPeaksCheckBox.TabIndex = 15;
            this._showPeaksCheckBox.Text = "☑ 峰值标记";
            this._showPeaksCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._showPeaksCheckBox.UseVisualStyleBackColor = false;
            this._showPeaksCheckBox.CheckedChanged += new System.EventHandler(this.ShowPeaksCheckBox_CheckedChanged);
            //
            // _iqModeCheckBox
            //
            this._iqModeCheckBox.Appearance = System.Windows.Forms.Appearance.Button;
            this._iqModeCheckBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this._iqModeCheckBox.FlatAppearance.BorderSize = 0;
            this._iqModeCheckBox.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(100)))));
            this._iqModeCheckBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._iqModeCheckBox.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this._iqModeCheckBox.ForeColor = System.Drawing.Color.White;
            this._iqModeCheckBox.Location = new System.Drawing.Point(390, 53);
            this._iqModeCheckBox.Name = "_iqModeCheckBox";
            this._iqModeCheckBox.Size = new System.Drawing.Size(110, 26);
            this._iqModeCheckBox.TabIndex = 16;
            this._iqModeCheckBox.Text = "📡 IQ模式";
            this._iqModeCheckBox.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._iqModeCheckBox.UseVisualStyleBackColor = false;
            this._iqModeCheckBox.CheckedChanged += new System.EventHandler(this.IQModeCheckBox_CheckedChanged);
            //
            // _gainLabel
            //
            this._gainLabel.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this._gainLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this._gainLabel.Location = new System.Drawing.Point(870, 53);
            this._gainLabel.Name = "_gainLabel";
            this._gainLabel.Size = new System.Drawing.Size(50, 26);
            this._gainLabel.TabIndex = 22;
            this._gainLabel.Text = "增益:";
            this._gainLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // _gainTrackBar
            //
            this._gainTrackBar.Location = new System.Drawing.Point(920, 53);
            this._gainTrackBar.Maximum = 40;
            this._gainTrackBar.Minimum = 0;
            this._gainTrackBar.Name = "_gainTrackBar";
            this._gainTrackBar.Size = new System.Drawing.Size(150, 45);
            this._gainTrackBar.TabIndex = 23;
            this._gainTrackBar.TickFrequency = 5;
            this._gainTrackBar.Value = 20;
            this._gainTrackBar.Scroll += new System.EventHandler(this.GainTrackBar_Scroll);
            //
            // _gainValueLabel
            //
            this._gainValueLabel.Font = new System.Drawing.Font("微软雅黑", 8.5F, System.Drawing.FontStyle.Bold);
            this._gainValueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this._gainValueLabel.Location = new System.Drawing.Point(1075, 53);
            this._gainValueLabel.Name = "_gainValueLabel";
            this._gainValueLabel.Size = new System.Drawing.Size(50, 26);
            this._gainValueLabel.TabIndex = 24;
            this._gainValueLabel.Text = "0dB";
            this._gainValueLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // _amplitudeRangeLabel
            //
            this._amplitudeRangeLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold);
            this._amplitudeRangeLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this._amplitudeRangeLabel.Location = new System.Drawing.Point(520, 53);
            this._amplitudeRangeLabel.Name = "_amplitudeRangeLabel";
            this._amplitudeRangeLabel.Size = new System.Drawing.Size(100, 26);
            this._amplitudeRangeLabel.TabIndex = 17;
            this._amplitudeRangeLabel.Text = "幅度范围 (dB)";
            this._amplitudeRangeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // _minDbNumeric
            //
            this._minDbNumeric.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this._minDbNumeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._minDbNumeric.Font = new System.Drawing.Font("微软雅黑", 9F);
            this._minDbNumeric.ForeColor = System.Drawing.Color.White;
            this._minDbNumeric.Location = new System.Drawing.Point(630, 53);
            this._minDbNumeric.Maximum = new decimal(new int[] { 0, 0, 0, 0 });
            this._minDbNumeric.Minimum = new decimal(new int[] { 120, 0, 0, -2147483648 });
            this._minDbNumeric.Name = "_minDbNumeric";
            this._minDbNumeric.Size = new System.Drawing.Size(60, 23);
            this._minDbNumeric.TabIndex = 18;
            this._minDbNumeric.Value = new decimal(new int[] { 80, 0, 0, -2147483648 });
            //
            // _dbRangeSeparatorLabel
            //
            this._dbRangeSeparatorLabel.Font = new System.Drawing.Font("微软雅黑", 9F);
            this._dbRangeSeparatorLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this._dbRangeSeparatorLabel.Location = new System.Drawing.Point(695, 53);
            this._dbRangeSeparatorLabel.Name = "_dbRangeSeparatorLabel";
            this._dbRangeSeparatorLabel.Size = new System.Drawing.Size(20, 26);
            this._dbRangeSeparatorLabel.TabIndex = 19;
            this._dbRangeSeparatorLabel.Text = "~";
            this._dbRangeSeparatorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // _maxDbNumeric
            //
            this._maxDbNumeric.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this._maxDbNumeric.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._maxDbNumeric.Font = new System.Drawing.Font("微软雅黑", 9F);
            this._maxDbNumeric.ForeColor = System.Drawing.Color.White;
            this._maxDbNumeric.Location = new System.Drawing.Point(720, 53);
            this._maxDbNumeric.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            this._maxDbNumeric.Minimum = new decimal(new int[] { 60, 0, 0, -2147483648 });
            this._maxDbNumeric.Name = "_maxDbNumeric";
            this._maxDbNumeric.Size = new System.Drawing.Size(60, 23);
            this._maxDbNumeric.TabIndex = 20;
            this._maxDbNumeric.Value = new decimal(new int[] { 0, 0, 0, 0 });
            //
            // _applyAmplitudeButton
            //
            this._applyAmplitudeButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this._applyAmplitudeButton.FlatAppearance.BorderSize = 0;
            this._applyAmplitudeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._applyAmplitudeButton.Font = new System.Drawing.Font("微软雅黑", 8.5F);
            this._applyAmplitudeButton.ForeColor = System.Drawing.Color.White;
            this._applyAmplitudeButton.Location = new System.Drawing.Point(790, 53);
            this._applyAmplitudeButton.Name = "_applyAmplitudeButton";
            this._applyAmplitudeButton.Size = new System.Drawing.Size(60, 26);
            this._applyAmplitudeButton.TabIndex = 21;
            this._applyAmplitudeButton.Text = "应用";
            this._applyAmplitudeButton.UseVisualStyleBackColor = false;
            this._applyAmplitudeButton.Click += new System.EventHandler(this.ApplyAmplitudeButton_Click);
            //
            // _splitContainer
            //
            this._splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this._splitContainer.Location = new System.Drawing.Point(0, 127);
            this._splitContainer.Name = "_splitContainer";
            this._splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // _splitContainer.Panel1
            // 
            this._splitContainer.Panel1.Controls.Add(this._spectrumControl);
            this._splitContainer.Panel1.Padding = new System.Windows.Forms.Padding(5);
            // 
            // _splitContainer.Panel2
            // 
            this._splitContainer.Panel2.Controls.Add(this._spectrogramControl);
            this._splitContainer.Panel2.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this._splitContainer.Size = new System.Drawing.Size(1200, 519);
            this._splitContainer.SplitterDistance = 259;
            this._splitContainer.TabIndex = 2;
            // 
            // _spectrumControl
            // 
            this._spectrumControl.AxisColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this._spectrumControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this._spectrumControl.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this._spectrumControl.BarColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(255)))));
            this._spectrumControl.BarSpacing = 2;
            this._spectrumControl.BottomMargin = 40;
            this._spectrumControl.DisplayMode = AudioSpectrumVisualizer.Controls.SpectrumDisplayMode.Bars;
            this._spectrumControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._spectrumControl.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(40)))));
            this._spectrumControl.LeftMargin = 60;
            this._spectrumControl.Location = new System.Drawing.Point(5, 5);
            this._spectrumControl.MaxDb = 0F;
            this._spectrumControl.MaxFrequency = 22050F;
            this._spectrumControl.MinDb = -80F;
            this._spectrumControl.MinFrequency = 0F;
            this._spectrumControl.Name = "_spectrumControl";
            this._spectrumControl.PeakCount = 3;
            this._spectrumControl.PeakMinDistance = 100F;
            this._spectrumControl.RightMargin = 20;
            this._spectrumControl.SampleRate = 44100;
            this._spectrumControl.ShowAxis = true;
            this._spectrumControl.ShowGrid = true;
            this._spectrumControl.ShowPeaks = true;
            this._spectrumControl.Size = new System.Drawing.Size(1190, 249);
            this._spectrumControl.TabIndex = 0;
            this._spectrumControl.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this._spectrumControl.TopMargin = 20;
            // 
            // _spectrogramControl
            // 
            this._spectrogramControl.AxisColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(150)))), ((int)(((byte)(150)))));
            this._spectrogramControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this._spectrogramControl.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this._spectrogramControl.BottomMargin = 40;
            this._spectrogramControl.ColorMap = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(4))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(8))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(12))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(16))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(20))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(24))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(28))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(32))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(36))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(40))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(44))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(48))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(52))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(56))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(60))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(68))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(72))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(76))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(80))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(84))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(88))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(92))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(96))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(100))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(104))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(108))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(112))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(116))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(120))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(124))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(128))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(132))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(136))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(140))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(144))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(148))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(152))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(156))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(160))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(164))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(168))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(172))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(176))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(180))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(184))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(188))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(196))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(200))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(204))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(208))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(212))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(216))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(220))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(224))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(228))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(232))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(236))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(240))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(244))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(248))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(252))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(1)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(5)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(9)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(13)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(17)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(21)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(25)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(29)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(33)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(37)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(45)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(49)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(53)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(57)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(61)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(65)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(69)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(73)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(77)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(81)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(85)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(89)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(93)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(97)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(101)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(109)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(113)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(117)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(121)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(125)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(129)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(133)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(137)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(141)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(149)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(157)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(161)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(165)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(169)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(173)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(177)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(181)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(185)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(189)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(193)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(197)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(201)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(205)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(209)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(213)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(217)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(221)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(225)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(229)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(233)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(237)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(241)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(245)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(249)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(253)))), ((int)(((byte)(255))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(255)))), ((int)(((byte)(252))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(6)))), ((int)(((byte)(255)))), ((int)(((byte)(248))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(255)))), ((int)(((byte)(244))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(255)))), ((int)(((byte)(240))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(255)))), ((int)(((byte)(236))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(255)))), ((int)(((byte)(232))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(255)))), ((int)(((byte)(228))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(255)))), ((int)(((byte)(224))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(255)))), ((int)(((byte)(220))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(255)))), ((int)(((byte)(216))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(255)))), ((int)(((byte)(212))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(255)))), ((int)(((byte)(208))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(255)))), ((int)(((byte)(204))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(255)))), ((int)(((byte)(200))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(255)))), ((int)(((byte)(196))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(255)))), ((int)(((byte)(192))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(66)))), ((int)(((byte)(255)))), ((int)(((byte)(188))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(255)))), ((int)(((byte)(184))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(255)))), ((int)(((byte)(180))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(78)))), ((int)(((byte)(255)))), ((int)(((byte)(176))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(82)))), ((int)(((byte)(255)))), ((int)(((byte)(172))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(86)))), ((int)(((byte)(255)))), ((int)(((byte)(168))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(255)))), ((int)(((byte)(164))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(255)))), ((int)(((byte)(160))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(98)))), ((int)(((byte)(255)))), ((int)(((byte)(156))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(102)))), ((int)(((byte)(255)))), ((int)(((byte)(152))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(255)))), ((int)(((byte)(148))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(110)))), ((int)(((byte)(255)))), ((int)(((byte)(144))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(255)))), ((int)(((byte)(140))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(118)))), ((int)(((byte)(255)))), ((int)(((byte)(136))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(255)))), ((int)(((byte)(132))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(255)))), ((int)(((byte)(128))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(255)))), ((int)(((byte)(124))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(255)))), ((int)(((byte)(120))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(255)))), ((int)(((byte)(116))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(255)))), ((int)(((byte)(112))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(146)))), ((int)(((byte)(255)))), ((int)(((byte)(108))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(255)))), ((int)(((byte)(104))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(255)))), ((int)(((byte)(100))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(255)))), ((int)(((byte)(96))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(162)))), ((int)(((byte)(255)))), ((int)(((byte)(92))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(255)))), ((int)(((byte)(88))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(255)))), ((int)(((byte)(84))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(255)))), ((int)(((byte)(80))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(255)))), ((int)(((byte)(76))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(255)))), ((int)(((byte)(72))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(186)))), ((int)(((byte)(255)))), ((int)(((byte)(68))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(255)))), ((int)(((byte)(64))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(194)))), ((int)(((byte)(255)))), ((int)(((byte)(60))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(198)))), ((int)(((byte)(255)))), ((int)(((byte)(56))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(202)))), ((int)(((byte)(255)))), ((int)(((byte)(52))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(206)))), ((int)(((byte)(255)))), ((int)(((byte)(48))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(255)))), ((int)(((byte)(44))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(255)))), ((int)(((byte)(40))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(218)))), ((int)(((byte)(255)))), ((int)(((byte)(36))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(222)))), ((int)(((byte)(255)))), ((int)(((byte)(32))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(255)))), ((int)(((byte)(28))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(255)))), ((int)(((byte)(24))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(255)))), ((int)(((byte)(20))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(255)))), ((int)(((byte)(16))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(255)))), ((int)(((byte)(12))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(255)))), ((int)(((byte)(8))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(255)))), ((int)(((byte)(4))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(254)))), ((int)(((byte)(255)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(253)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(251)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(249)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(247)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(243)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(241)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(239)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(235)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(233)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(231)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(229)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(227)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(225)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(223)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(221)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(219)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(217)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(215)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(213)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(211)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(209)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(207)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(205)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(203)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(201)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(199)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(197)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(195)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(191)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(189)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(187)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(185)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(183)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(181)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(179)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(177)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(175)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(173)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(171)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(169)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(167)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(163)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(161)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(159)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(157)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(155)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(153)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(151)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(149)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(147)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(145)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(143)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(141)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(139)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(137)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(135)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(133)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(131)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(129)))), ((int)(((byte)(0))))),
        System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(127)))), ((int)(((byte)(0)))))};
            this._spectrogramControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this._spectrogramControl.LeftMargin = 60;
            this._spectrogramControl.Location = new System.Drawing.Point(5, 0);
            this._spectrogramControl.MaxDb = 0F;
            this._spectrogramControl.MaxDisplayRows = 10;
            this._spectrogramControl.MaxFrequency = 22050F;
            this._spectrogramControl.MinDb = -80F;
            this._spectrogramControl.MinFrequency = 0F;
            this._spectrogramControl.Name = "_spectrogramControl";
            this._spectrogramControl.RightMargin = 20;
            this._spectrogramControl.SampleRate = 44100;
            this._spectrogramControl.ShowAxis = true;
            this._spectrogramControl.Size = new System.Drawing.Size(1190, 251);
            this._spectrogramControl.TabIndex = 0;
            this._spectrogramControl.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this._spectrogramControl.TopMargin = 20;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(1200, 646);
            this.Controls.Add(this._splitContainer);
            this.Controls.Add(this._controlPanel);
            this.Controls.Add(this._titleBarPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MinimumSize = new System.Drawing.Size(800, 554);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "频谱分析器";
            this._titleBarPanel.ResumeLayout(false);
            this._controlPanel.ResumeLayout(false);
            this._splitContainer.Panel1.ResumeLayout(false);
            this._splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._splitContainer)).EndInit();
            this._splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._minDbNumeric)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._maxDbNumeric)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
    }
}
