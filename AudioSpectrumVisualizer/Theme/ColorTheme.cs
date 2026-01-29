using System.Drawing;

namespace AudioSpectrumVisualizer.Theme
{
    /// <summary>
    /// 颜色主题类 - 定义控件的颜色方案
    /// Color theme class - defines color scheme for controls
    /// </summary>
    public class ColorTheme
    {
        /// <summary>
        /// 主题名称
        /// Theme name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 背景颜色
        /// Background color
        /// </summary>
        public Color BackgroundColor { get; set; }

        /// <summary>
        /// 频谱柱/曲线颜色
        /// Spectrum bar/line color
        /// </summary>
        public Color SpectrumColor { get; set; }

        /// <summary>
        /// 网格颜色
        /// Grid color
        /// </summary>
        public Color GridColor { get; set; }

        /// <summary>
        /// 坐标轴颜色
        /// Axis color
        /// </summary>
        public Color AxisColor { get; set; }

        /// <summary>
        /// 文字颜色
        /// Text color
        /// </summary>
        public Color TextColor { get; set; }

        /// <summary>
        /// 峰值颜色（第1个峰值）
        /// Peak color (1st peak)
        /// </summary>
        public Color Peak1Color { get; set; }

        /// <summary>
        /// 峰值颜色（第2个峰值）
        /// Peak color (2nd peak)
        /// </summary>
        public Color Peak2Color { get; set; }

        /// <summary>
        /// 峰值颜色（第3个峰值）
        /// Peak color (3rd peak)
        /// </summary>
        public Color Peak3Color { get; set; }

        /// <summary>
        /// 窗体背景颜色
        /// Form background color
        /// </summary>
        public Color FormBackColor { get; set; }

        /// <summary>
        /// 标题栏背景颜色
        /// Title bar background color
        /// </summary>
        public Color TitleBarBackColor { get; set; }

        /// <summary>
        /// 标题栏文字颜色
        /// Title bar text color
        /// </summary>
        public Color TitleBarTextColor { get; set; }

        /// <summary>
        /// 按钮背景颜色
        /// Button background color
        /// </summary>
        public Color ButtonBackColor { get; set; }

        /// <summary>
        /// 按钮文字颜色
        /// Button text color
        /// </summary>
        public Color ButtonTextColor { get; set; }

        /// <summary>
        /// 复选框文字颜色
        /// CheckBox text color
        /// </summary>
        public Color CheckBoxTextColor { get; set; }

        /// <summary>
        /// 分隔条颜色
        /// Splitter color
        /// </summary>
        public Color SplitterColor { get; set; }

        /// <summary>
        /// 获取暗色主题（默认）
        /// Get dark theme (default)
        /// </summary>
        public static ColorTheme Dark => new ColorTheme
        {
            Name = "Dark",
            BackgroundColor = Color.FromArgb(20, 20, 20),
            SpectrumColor = Color.FromArgb(0, 150, 255),
            GridColor = Color.FromArgb(40, 40, 40),
            AxisColor = Color.FromArgb(150, 150, 150),
            TextColor = Color.FromArgb(200, 200, 200),
            Peak1Color = Color.FromArgb(255, 50, 50),
            Peak2Color = Color.FromArgb(255, 150, 50),
            Peak3Color = Color.FromArgb(255, 255, 50),
            FormBackColor = Color.FromArgb(30, 30, 30),
            TitleBarBackColor = Color.FromArgb(45, 45, 48),
            TitleBarTextColor = Color.White,
            ButtonBackColor = Color.FromArgb(60, 60, 60),
            ButtonTextColor = Color.White,
            CheckBoxTextColor = Color.White,
            SplitterColor = Color.FromArgb(50, 50, 50)
        };

        /// <summary>
        /// 获取亮色主题
        /// Get light theme
        /// </summary>
        public static ColorTheme Light => new ColorTheme
        {
            Name = "Light",
            BackgroundColor = Color.FromArgb(250, 250, 250),
            SpectrumColor = Color.FromArgb(0, 120, 215),
            GridColor = Color.FromArgb(220, 220, 220),
            AxisColor = Color.FromArgb(100, 100, 100),
            TextColor = Color.FromArgb(50, 50, 50),
            Peak1Color = Color.FromArgb(220, 20, 20),
            Peak2Color = Color.FromArgb(220, 120, 20),
            Peak3Color = Color.FromArgb(200, 180, 0),
            FormBackColor = Color.FromArgb(240, 240, 240),
            TitleBarBackColor = Color.FromArgb(230, 230, 230),
            TitleBarTextColor = Color.Black,
            ButtonBackColor = Color.FromArgb(225, 225, 225),
            ButtonTextColor = Color.Black,
            CheckBoxTextColor = Color.Black,
            SplitterColor = Color.FromArgb(200, 200, 200)
        };

        /// <summary>
        /// 根据名称获取主题
        /// Get theme by name
        /// </summary>
        public static ColorTheme GetTheme(string name)
        {
            switch (name?.ToLower())
            {
                case "light":
                    return Light;
                case "dark":
                default:
                    return Dark;
            }
        }
    }
}
