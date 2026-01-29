# AudioSpectrumVisualizer

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.6.1-blue.svg)](https://dotnet.microsoft.com/)

Real-time audio spectrum and spectrogram visualization controls for .NET WinForms applications.

实时音频频谱和声谱图可视化控件库，适用于 .NET WinForms 应用程序。

## Features | 功能特性

### SpectrumControl - 频谱图控件
- **Multiple display modes**: Bars, Line, Filled Line | **多种显示模式**：柱状图、曲线、填充曲线
- **Interactive features**: Mouse hover tooltips, scroll wheel zoom, drag to pan | **交互功能**：鼠标悬停提示、滚轮缩放、拖动平移
- **Peak detection**: Automatic detection and labeling of frequency peaks | **峰值检测**：自动检测并标记频率峰值
- **Customizable appearance**: Colors, grid, axis, margins | **可定制外观**：颜色、网格、坐标轴、边距
- **Frequency range control**: Full spectrum or custom range | **频率范围控制**：全频段或自定义范围

### SpectrogramControl - 声谱图控件
- **Waterfall display**: Time-frequency 2D heatmap | **瀑布式显示**：时间-频率二维热力图
- **Multiple color maps**: Default, Grayscale, Hot, Cool, or custom | **多种色彩映射**：默认、灰度、热力图、冷色调或自定义
- **Frequency range zoom**: Synchronized with spectrum control | **频率范围缩放**：与频谱图同步
- **Performance optimized**: Uses unsafe code for direct pixel manipulation | **性能优化**：使用 unsafe 代码直接操作像素
- **Configurable time window**: Adjustable history display | **可配置时间窗口**：可调整历史数据显示

### FFTProcessor - FFT处理器
- **Cooley-Tukey algorithm**: Fast and efficient FFT implementation | **Cooley-Tukey 算法**：快速高效的 FFT 实现
- **Hann window**: Reduces spectral leakage | **Hann 窗函数**：减少频谱泄漏
- **dB scale output**: Industry-standard decibel scale | **dB 刻度输出**：行业标准分贝刻度
- **Configurable FFT size**: Any power of 2 | **可配置 FFT 大小**：任意 2 的幂次方

## Installation | 安装

### Option 1: Add as Project Reference | 选项1：添加项目引用

1. Add the `AudioSpectrumVisualizer` project to your solution
2. Add a reference to `AudioSpectrumVisualizer` in your project
3. Build the solution

### Option 2: Use Compiled DLL | 选项2：使用编译的 DLL

1. Build the `AudioSpectrumVisualizer` project
2. Reference `AudioSpectrumVisualizer.dll` in your project
3. Copy the DLL to your output directory

## Quick Start | 快速开始

### Basic Usage | 基本用法

```csharp
using AudioSpectrumVisualizer.Controls;
using AudioSpectrumVisualizer.DSP;

// Create FFT processor
var fftProcessor = new FFTProcessor(fftSize: 2048);

// Create spectrum control
var spectrumControl = new SpectrumControl
{
    SampleRate = 44100,
    BarColor = Color.Cyan,
    BackgroundColor = Color.Black,
    ShowAxis = true,
    ShowGrid = true,
    ShowPeaks = true,
    DisplayMode = SpectrumDisplayMode.Bars
};

// Create spectrogram control
var spectrogramControl = new SpectrogramControl
{
    SampleRate = 44100,
    BackgroundColor = Color.Black,
    MaxDisplayRows = 300
};

// Add controls to form
this.Controls.Add(spectrumControl);
this.Controls.Add(spectrogramControl);

// Process audio data
float[] audioData = GetAudioSamples(); // Your audio source
float[] fftData = fftProcessor.ProcessFFT(audioData);

// Update visualizations
spectrumControl.UpdateSpectrum(fftData);
spectrogramControl.UpdateSpectrogram(fftData);
```

### Advanced Configuration | 高级配置

```csharp
// Customize spectrum control
spectrumControl.MinDb = -80f;
spectrumControl.MaxDb = 0f;
spectrumControl.BarSpacing = 2;
spectrumControl.PeakCount = 5;
spectrumControl.PeakMinDistance = 200;
spectrumControl.LeftMargin = 60;
spectrumControl.BottomMargin = 40;

// Set frequency range
spectrumControl.MinFrequency = 20;    // 20 Hz
spectrumControl.MaxFrequency = 20000; // 20 kHz

// Customize spectrogram control
spectrogramControl.MinDb = -80f;
spectrogramControl.MaxDb = 0f;
spectrogramControl.MaxDisplayRows = 500; // More history

// Use different color maps
spectrogramControl.SetHotColorMap();      // Red-yellow-white
spectrogramControl.SetCoolColorMap();     // Blue-cyan-white
spectrogramControl.SetGrayscaleColorMap(); // Grayscale

// Or create custom color map
Color[] customColors = new Color[256];
for (int i = 0; i < 256; i++)
{
    customColors[i] = Color.FromArgb(i, 0, 255 - i);
}
spectrogramControl.SetColorMap(customColors);

// Synchronize frequency range changes to spectrogram
spectrumControl.FrequencyRangeChanged += (s, e) =>
{
    spectrogramControl.MinFrequency = spectrumControl.MinFrequency;
    spectrogramControl.MaxFrequency = spectrumControl.MaxFrequency;
};
```

### Interactive Controls | 交互控制

**Mouse Wheel Zoom | 鼠标滚轮缩放**
- Scroll up to zoom in (narrow frequency range)
- Scroll down to zoom out (wider frequency range)
- Zoom centers on the current frequency range

**Drag to Pan | 拖动平移**
- Click and drag left/right to pan the frequency view when zoomed in
- Only works when zoomed in (not at full spectrum view)
- Cursor changes to hand icon when dragging is available

**Mouse Hover | 鼠标悬停**
- Hover over spectrum to see frequency and amplitude values
- Tooltip displays: "1234.5 Hz, -45.2 dB"

```csharp
// Example: Synchronize zoom and pan between controls
spectrumControl.FrequencyRangeChanged += (s, e) =>
{
    spectrogramControl.MinFrequency = spectrumControl.MinFrequency;
    spectrogramControl.MaxFrequency = spectrumControl.MaxFrequency;
};
```

## API Reference | API 参考

### SpectrumControl Properties | 属性

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `SampleRate` | int | 44100 | Audio sample rate in Hz |
| `BarColor` | Color | Cyan | Color of spectrum bars/line |
| `BackgroundColor` | Color | Dark Gray | Background color |
| `GridColor` | Color | Gray | Grid line color |
| `AxisColor` | Color | Light Gray | Axis line color |
| `TextColor` | Color | Light Gray | Text color |
| `BarSpacing` | int | 2 | Spacing between bars in pixels |
| `ShowAxis` | bool | true | Show axis and labels |
| `ShowGrid` | bool | true | Show grid lines |
| `ShowPeaks` | bool | true | Show peak markers |
| `PeakCount` | int | 3 | Number of peaks to detect (1-10) |
| `PeakMinDistance` | float | 100 | Minimum distance between peaks in Hz |
| `DisplayMode` | enum | Bars | Display mode: Bars, Line, FilledLine |
| `MinFrequency` | float | 0 | Minimum display frequency in Hz |
| `MaxFrequency` | float | 22050 | Maximum display frequency in Hz |
| `MinDb` | float | -80 | Minimum dB value |
| `MaxDb` | float | 0 | Maximum dB value |
| `LeftMargin` | int | 60 | Left margin in pixels |
| `RightMargin` | int | 20 | Right margin in pixels |
| `TopMargin` | int | 20 | Top margin in pixels |
| `BottomMargin` | int | 40 | Bottom margin in pixels |

### SpectrumControl Methods | 方法

- `UpdateSpectrum(float[] spectrumData)` - Update spectrum data (thread-safe)
- `ResetZoom()` - Reset frequency range to full spectrum

### SpectrumControl Events | 事件

- `FrequencyRangeChanged` - Fired when frequency range changes (zoom or pan)

### SpectrogramControl Properties | 属性

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `SampleRate` | int | 44100 | Audio sample rate in Hz |
| `BackgroundColor` | Color | Dark Gray | Background color |
| `MaxDisplayRows` | int | 300 | Maximum rows to display (time window) |
| `MinFrequency` | float | 0 | Minimum display frequency in Hz |
| `MaxFrequency` | float | 22050 | Maximum display frequency in Hz |
| `MinDb` | float | -80 | Minimum dB value |
| `MaxDb` | float | 0 | Maximum dB value |
| `ColorMap` | Color[] | Default | 256-color array for heatmap |
| `ShowAxis` | bool | true | Show axis and labels |
| `AxisColor` | Color | Light Gray | Axis line color |
| `TextColor` | Color | Light Gray | Text color |
| `LeftMargin` | int | 60 | Left margin in pixels |
| `RightMargin` | int | 20 | Right margin in pixels |
| `TopMargin` | int | 20 | Top margin in pixels |
| `BottomMargin` | int | 40 | Bottom margin in pixels |

### SpectrogramControl Methods | 方法

- `UpdateSpectrogram(float[] spectrumData)` - Update spectrogram data (thread-safe)
- `Clear()` - Clear all spectrogram history
- `SetColorMap(Color[] colorMap)` - Set custom 256-color map
- `SetGrayscaleColorMap()` - Use grayscale colors
- `SetHotColorMap()` - Use hot colors (red-yellow-white)
- `SetCoolColorMap()` - Use cool colors (blue-cyan-white)

### FFTProcessor Properties | 属性

| Property | Type | Description |
|----------|------|-------------|
| `FFTSize` | int | FFT size (read-only) |

### FFTProcessor Methods | 方法

- `FFTProcessor(int fftSize = 2048)` - Constructor, fftSize must be power of 2
- `ProcessFFT(float[] audioData)` - Process FFT and return magnitude in dB

## Performance Tips | 性能提示

1. **Use appropriate FFT size**: Larger FFT = better frequency resolution but slower
   - 1024: Fast, lower resolution
   - 2048: Balanced (recommended)
   - 4096: Slower, higher resolution

2. **Adjust MaxDisplayRows**: More rows = more memory and slower rendering
   - 100-200: Fast, short history
   - 300-500: Balanced
   - 500+: Slower, long history

3. **Thread safety**: Both controls are thread-safe for data updates
   - Use `BeginInvoke` when updating from audio thread
   - Controls handle synchronization internally

4. **Unsafe code**: SpectrogramControl uses unsafe code for performance
   - Ensure "Allow unsafe code" is enabled in project settings
   - Already configured in the library project

## Requirements | 系统要求

- .NET Framework 4.6.1 or higher
- Windows Forms
- Visual Studio 2015 or higher (for development)

## Dependencies | 依赖项

- System.dll
- System.Core.dll
- System.Numerics.dll
- System.Drawing.dll
- System.Windows.Forms.dll

No external NuGet packages required for the library itself.

## Example Project | 示例项目

See the `SpectrumAnalysis` project in this repository for a complete working example that demonstrates:
- Real-time audio capture from microphone
- FFT processing
- Spectrum and spectrogram visualization
- Frequency range synchronization
- Interactive controls

## License | 许可证

MIT License - see [LICENSE](LICENSE) file for details

## Contributing | 贡献

Contributions are welcome! Please feel free to submit a Pull Request.

欢迎贡献！请随时提交 Pull Request。

## Author | 作者

Created for real-time audio visualization applications.

为实时音频可视化应用程序创建。

## Changelog | 更新日志

### Version 1.0.0
- Initial release
- SpectrumControl with multiple display modes
- SpectrogramControl with waterfall display
- FFTProcessor with Cooley-Tukey algorithm
- Full API documentation
- Chinese and English comments
