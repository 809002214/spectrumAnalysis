# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

实时音频频谱和声谱图分析工具，包含可复用的开源可视化控件库。

**AudioSpectrumVisualizer** - 独立的可视化控件库，适用于任何 .NET WinForms 项目
**SpectrumAnalysis** - 示例应用程序，展示如何使用库

## Project Structure

```
spectrumAnalysis/
├── AudioSpectrumVisualizer/          # 可复用的可视化控件库
│   ├── Controls/
│   │   ├── SpectrumControl.cs       # 频谱图控件（柱状图/曲线/填充）
│   │   └── SpectrogramControl.cs    # 声谱图控件（瀑布式热力图）
│   ├── DSP/
│   │   └── FFTProcessor.cs          # FFT处理器（Cooley-Tukey算法）
│   ├── Theme/
│   │   └── ColorTheme.cs            # 主题系统（深色/浅色主题）
│   ├── Properties/
│   │   └── AssemblyInfo.cs
│   ├── AudioSpectrumVisualizer.csproj
│   ├── README.md                    # 完整的API文档
│   └── LICENSE                      # MIT许可证
│
└── SpectrumAnalysis/                # 示例应用程序
    ├── Audio/
    │   └── AudioCapture.cs          # 麦克风音频采集
    ├── MainForm.cs                  # 主窗体
    ├── MainForm.Designer.cs         # 设计器文件
    ├── Program.cs                   # 程序入口
    └── SpectrumAnalysis.csproj
```

## Build Commands

```bash
# 使用 MSBuild 构建项目
msbuild SpectrumAnalysis.sln /p:Configuration=Release

# 或在 Visual Studio 中直接构建
# Build -> Build Solution (Ctrl+Shift+B)
```

## Run Commands

```bash
# 运行编译后的程序
.\SpectrumAnalysis\bin\Debug\SpectrumAnalysis.exe

# 或在 Visual Studio 中按 F5 运行
```

## Dependencies

### AudioSpectrumVisualizer 库
- **System.dll** - 基础系统库
- **System.Core.dll** - 核心功能
- **System.Numerics.dll** - FFT 计算使用的复数类型
- **System.Drawing.dll** - 图形渲染
- **System.Windows.Forms.dll** - UI 框架

**无外部 NuGet 依赖** - 库本身不依赖任何第三方包

### SpectrumAnalysis 示例应用
- **AudioSpectrumVisualizer** - 项目引用（可视化控件库）
- **NAudio 1.10.0** - 音频采集和处理库

安装依赖：
```bash
# 在 Visual Studio 中使用 NuGet Package Manager
# 或使用 NuGet CLI
nuget restore SpectrumAnalysis.sln
```

## Architecture

### AudioSpectrumVisualizer 库（可复用）

#### 1. Controls/SpectrumControl.cs - 频谱图控件
**功能：** 实时频率能量分布可视化

**核心特性：**
- 三种显示模式：柱状图、曲线、填充曲线
- 完整的坐标轴系统：横轴（频率 Hz/kHz）、纵轴（幅度 dB）
- 多峰值检测：自动标记最强的 N 个峰值（1-10个）
- 鼠标交互：悬停显示频率和幅度、滚轮缩放、拖动平移
- 频率范围控制：支持全频段/低频/中频/高频快速切换
- 可配置选项：网格、坐标轴、峰值标记的显示开关

**可配置属性（25+）：**
- 颜色：`BarColor`, `BackgroundColor`, `GridColor`, `AxisColor`, `TextColor`
- 显示：`ShowAxis`, `ShowGrid`, `ShowPeaks`, `DisplayMode`
- 峰值：`PeakCount`, `PeakMinDistance`
- 范围：`MinFrequency`, `MaxFrequency`, `MinDb`, `MaxDb`
- 边距：`LeftMargin`, `RightMargin`, `TopMargin`, `BottomMargin`
- 其他：`BarSpacing`, `SampleRate`

**性能优化：**
- 双缓冲渲染避免闪烁
- 抗锯齿和 ClearType 文字渲染
- 线程安全的数据更新

#### 2. Controls/SpectrogramControl.cs - 声谱图控件
**功能：** 时间-频率二维热力图（瀑布式）

**核心特性：**
- 瀑布式显示：时间轴从上到下，频率轴从左到右
- 完整的坐标轴系统：与频谱图对齐
- 多种色彩映射：默认、灰度、热力图、冷色调、自定义
- 频率范围缩放：与频谱图同步
- 可配置时间窗口：调整历史数据显示

**可配置属性（15+）：**
- 基本：`SampleRate`, `BackgroundColor`, `MaxDisplayRows`
- 范围：`MinFrequency`, `MaxFrequency`, `MinDb`, `MaxDb`
- 坐标轴：`ShowAxis`, `AxisColor`, `TextColor`
- 边距：`LeftMargin`, `RightMargin`, `TopMargin`, `BottomMargin`
- 色彩：`ColorMap`（256色数组）

**性能优化：**
- Unsafe 代码直接操作像素数据
- 双缓冲渲染
- 最近邻插值快速缩放
- 固定内存占用

#### 3. DSP/FFTProcessor.cs - FFT处理器
**功能：** 快速傅里叶变换

**核心特性：**
- Cooley-Tukey FFT 算法实现
- Hann 窗函数减少频谱泄漏
- dB 刻度输出（行业标准）
- 可配置 FFT 大小（任意 2 的幂次方）

**技术参数：**
- 默认 FFT 大小：2048 点
- 频率分辨率：采样率 / FFT大小（44100/2048 ≈ 21.5 Hz）
- 输出：dB 刻度的频谱幅度数组

#### 4. Theme/ColorTheme.cs - 主题系统
**功能：** 统一的颜色主题管理

**核心特性：**
- 内置深色主题（默认）：深色背景、青色频谱、红/橙/黄峰值
- 内置浅色主题：浅色背景、蓝色频谱、深色元素
- 统一管理控件和UI颜色
- 支持主题切换和持久化

**主题属性：**
- 控件颜色：BackgroundColor, SpectrumColor, GridColor, AxisColor, TextColor
- 峰值颜色：Peak1Color, Peak2Color, Peak3Color
- UI颜色：FormBackColor, TitleBarBackColor, ButtonBackColor 等

### SpectrumAnalysis 示例应用

#### 1. Audio/AudioCapture.cs - 音频采集引擎
**功能：** 从麦克风采集音频并进行 FFT 处理

**核心特性：**
- 使用 NAudio 的 WaveInEvent 采集音频
- 采样率：48000 Hz（高于库默认的 44100 Hz），单声道
- FFT 大小：4096 样本（大于库默认的 2048）
- 缓冲区大小：50ms
- 可配置更新间隔（刷新速度控制）
- 触发事件：`FFTDataAvailable`, `WaveformDataAvailable`

#### 2. MainForm.cs - 主窗体
**功能：** 协调音频采集和可视化控件

**核心特性：**
- 使用 SplitContainer 实现响应式布局
- 两个控件始终保持相同高度（50/50 分割）
- 用户可拖动分隔条调整比例
- 频率范围同步：频谱图和声谱图联动
- 自定义标题栏：无边框窗体，支持拖动、最小化、最大化、关闭
- 主题切换：支持深色/浅色主题切换
- 设置持久化：窗口位置、大小、状态、主题、显示选项自动保存

**布局结构：**
```
┌─────────────────────────────────────┐
│ 标题栏 (40px, Dock.Top)             │
├─────────────────────────────────────┤
│ 控制面板 (90px, Dock.Top)           │
├─────────────────────────────────────┤
│ ┌─────────────────────────────────┐ │
│ │ SplitContainer (Dock.Fill)      │ │
│ │ ┌─────────────────────────────┐ │ │
│ │ │ 频谱图 (Panel1, Dock.Fill)  │ │ │
│ │ └─────────────────────────────┘ │ │
│ │ ═══════════════════════════════ │ │ ← 可拖动分隔条
│ │ ┌─────────────────────────────┐ │ │
│ │ │ 声谱图 (Panel2, Dock.Fill)  │ │ │
│ │ └─────────────────────────────┘ │ │
│ └─────────────────────────────────┘ │
└─────────────────────────────────────┘
```

### 数据流

```
麦克风 → AudioCapture → FFTProcessor → SpectrumControl (频谱图)
                                    ↘ SpectrogramControl (声谱图)

频率范围同步：
SpectrumControl (鼠标滚轮/按钮) → MainForm → SpectrogramControl
```

1. AudioCapture 从麦克风采集原始音频数据
2. 累积到 4096 样本后触发 FFT 处理
3. FFTProcessor 计算频谱数据（dB 值数组）
4. 根据更新间隔决定是否触发显示更新
5. 通过事件将频谱数据分发到两个可视化控件
6. 控件使用 BeginInvoke 确保 UI 线程安全更新
7. 频谱图的缩放/平移操作自动同步到声谱图

### 性能优化策略

- **双缓冲渲染**：两个控件都使用离屏 Bitmap 避免闪烁
- **Unsafe 代码**：SpectrogramControl 使用指针直接写入像素数据
- **对象复用**：FFT 缓冲区和窗函数预分配，避免 GC 压力
- **异步更新**：使用 BeginInvoke 而非 Invoke 避免阻塞音频线程
- **最近邻插值**：声谱图缩放使用 NearestNeighbor 模式提升速度
- **智能采样**：根据显示宽度调整柱子数量
- **刷新速度控制**：可配置更新间隔，降低 CPU 占用

## Key Files

### 库文件（AudioSpectrumVisualizer）
- `Controls/SpectrumControl.cs` - 频谱图可视化（935 行）
- `Controls/SpectrogramControl.cs` - 声谱图可视化（574 行）
- `DSP/FFTProcessor.cs` - 快速傅里叶变换实现（167 行）
- `Theme/ColorTheme.cs` - 主题系统（172 行）

### 示例应用文件（SpectrumAnalysis）
- `MainForm.cs` - 主窗口，协调音频采集和可视化控件（586 行）
- `MainForm.Designer.cs` - 设计器文件（使用 SplitContainer）
- `Audio/AudioCapture.cs` - 音频采集和 FFT 触发逻辑（153 行）
- `Program.cs` - 程序入口

## Control Reusability

两个可视化控件设计为完全可复用：

```csharp
using AudioSpectrumVisualizer.Controls;
using AudioSpectrumVisualizer.DSP;

// 创建 FFT 处理器
var fftProcessor = new FFTProcessor(2048);

// 创建频谱图控件（完全可配置）
var spectrum = new SpectrumControl
{
    SampleRate = 44100,
    BarColor = Color.Cyan,
    BackgroundColor = Color.Black,
    ShowAxis = true,
    ShowGrid = true,
    ShowPeaks = true,
    PeakCount = 3,
    DisplayMode = SpectrumDisplayMode.Bars,
    MinFrequency = 0,
    MaxFrequency = 22050
};

// 创建声谱图控件（完全可配置）
var spectrogram = new SpectrogramControl
{
    SampleRate = 44100,
    BackgroundColor = Color.Black,
    ShowAxis = true,
    MaxDisplayRows = 300,
    MinFrequency = 0,
    MaxFrequency = 22050
};

// 更新数据（线程安全）
spectrum.UpdateSpectrum(fftData);
spectrogram.UpdateSpectrogram(fftData);

// 同步频率范围
spectrogram.MinFrequency = spectrum.MinFrequency;
spectrogram.MaxFrequency = spectrum.MaxFrequency;
```

## Technical Parameters

### 音频参数
- **采样率**：48000 Hz（应用程序）/ 44100 Hz（库默认）
- **FFT 大小**：4096 点（应用程序）/ 2048 点（库默认）
- **频率分辨率**：11.7 Hz (48000/4096) 或 21.5 Hz (44100/2048)
- **奈奎斯特频率**：24000 Hz (48000/2) 或 22050 Hz (44100/2)
- **声道**：单声道（Mono）

### 显示参数
- **dB 范围**：-80 到 0 dB（可配置）
- **刷新率**：约 10 FPS（可配置）
- **声谱图历史**：默认 300 行（约 15 秒）
- **峰值检测**：默认 3 个峰值，最小间隔 100Hz

### 性能指标
- **CPU 占用**：约 5%（默认配置）
- **内存占用**：10-20 MB（包含历史数据）
- **FFT 处理时间**：约 0.5ms（2048 点）
- **渲染性能**：60+ FPS（取决于控件大小）

## Notes

### 技术要点
- 应用程序使用 48000 Hz 采样率和 4096 FFT 大小，提供更高的频率分辨率（11.7 Hz）
- 库默认使用 44100 Hz 采样率和 2048 FFT 大小（21.5 Hz 分辨率）
- 声谱图默认显示最近 300 行数据（约 15 秒），旧数据自动从底部清理
- 可通过 `MaxDisplayRows` 属性调整显示窗口大小
- dB 范围默认为 -80 到 0 dB，可通过控件属性调整
- 使用 unsafe 代码需要在项目属性中启用"允许不安全代码"（已在 .csproj 中配置）
- 主题设置和窗口状态会自动保存到用户配置文件

### 布局设计
- 使用 SplitContainer 实现响应式布局
- 两个控件始终保持相同高度（默认 50/50 分割）
- 用户可拖动分隔条调整比例
- 窗口大小变化时，控件自动适应
- 无底部空白区域，充分利用窗口空间

### 坐标轴对齐
- 频谱图和声谱图使用相同的边距（左 60px，下 40px）
- 频率刻度标签在同一垂直线上
- 两个控件的 X 轴完全对齐

### 代码质量
- 所有代码已添加详细的中文和英文注释
- MainForm 设计器已修复，可在 Visual Studio 中正常打开设计视图
- 所有控件都是线程安全的
- 使用双缓冲和抗锯齿优化渲染性能

## Validation

### 验证频谱图正确性

**快速测试方法：**
1. **在线频率生成器**：https://www.szynalski.com/tone-generator/
   - 播放 440Hz 音调
   - 观察频谱图在 440Hz 位置是否有明显峰值

2. **人声测试**：对着麦克风说话
   - 男声基频：100-150Hz
   - 女声基频：200-300Hz
   - 共振峰：500-4000Hz

3. **白噪声测试**：播放白噪声
   - 所有频率能量应该大致相等
   - 频谱图显示为平坦分布

详细验证方法请查看：[DEVELOPMENT_GUIDE.md](DEVELOPMENT_GUIDE.md#验证频谱图正确性)

## Documentation

- **[README.md](README.md)** - 项目主页和快速开始
- **[DEVELOPMENT_GUIDE.md](DEVELOPMENT_GUIDE.md)** - 开发指南和验证方法
- **[AudioSpectrumVisualizer/README.md](AudioSpectrumVisualizer/README.md)** - 完整的API文档

## License

MIT License - 完全开源，可商用

---

**最后更新：** 2026-01-28
