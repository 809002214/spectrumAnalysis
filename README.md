# AudioSpectrumVisualizer

实时音频频谱和声谱图可视化控件库 + 示例应用程序

Real-time audio spectrum and spectrogram visualization library + example application

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![.NET Framework](https://img.shields.io/badge/.NET%20Framework-4.6.1-blue.svg)](https://dotnet.microsoft.com/)

---

## 📦 项目结构

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
│   ├── README.md                    # 完整的API文档
│   └── LICENSE                      # MIT许可证
│
└── SpectrumAnalysis/                # 示例应用程序
    ├── Audio/
    │   └── AudioCapture.cs          # 麦克风音频采集
    ├── MainForm.cs                  # 主窗体
    ├── MainForm.Designer.cs         # 设计器文件
    └── Program.cs                   # 程序入口
```

---

## ✨ 核心特性

### AudioSpectrumVisualizer 库

- ✅ **SpectrumControl** - 频谱图控件（柱状图/曲线/填充曲线）
- ✅ **SpectrogramControl** - 声谱图控件（瀑布式热力图）
- ✅ **FFTProcessor** - FFT处理器（Cooley-Tukey算法）
- ✅ **ColorTheme** - 主题系统（深色/浅色主题）
- ✅ **完全可配置** - 40+ 个公共属性
- ✅ **线程安全** - 支持多线程数据更新
- ✅ **高性能** - 双缓冲、unsafe代码优化
- ✅ **MIT许可证** - 完全开源，可商用

### 示例应用程序

- 🎤 实时麦克风音频采集（48000 Hz）
- 📊 多种频谱显示模式（柱状图/曲线/填充）
- 🌊 瀑布式声谱图显示
- 🔍 鼠标滚轮缩放 + 拖动平移
- 🎯 自动峰值检测和标记（1-10个峰值）
- ⚙️ 可配置的显示选项（网格/坐标轴/峰值）
- 🎨 响应式布局设计（可拖动分隔条）
- 🌓 深色/浅色主题切换
- 💾 设置自动保存（窗口状态、主题、显示选项）
- 🎚️ 增益控制（-20dB 到 +20dB，dB 刻度）
- 📡 IQ 模式支持（立体声 I/Q 信号，负频率显示）

---

## 🚀 快速开始

### 构建项目

```bash
# 在 Visual Studio 中
1. 打开 SpectrumAnalysis.sln
2. 按 Ctrl+Shift+B 构建解决方案
3. 按 F5 运行程序

# 或使用 MSBuild
msbuild SpectrumAnalysis.sln /p:Configuration=Release
```

### 使用库

```csharp
using AudioSpectrumVisualizer.Controls;
using AudioSpectrumVisualizer.DSP;

// 创建FFT处理器
var fftProcessor = new FFTProcessor(2048);

// 创建频谱图控件
var spectrum = new SpectrumControl
{
    SampleRate = 44100,
    BarColor = Color.Cyan,
    ShowAxis = true,
    ShowPeaks = true
};

// 创建声谱图控件
var spectrogram = new SpectrogramControl
{
    SampleRate = 44100,
    ShowAxis = true
};

// 处理音频数据
float[] audioData = GetAudioSamples();
float[] fftData = fftProcessor.ProcessFFT(audioData);

// 更新可视化
spectrum.UpdateSpectrum(fftData);
spectrogram.UpdateSpectrogram(fftData);
```

---

## 📖 文档

- **[AudioSpectrumVisualizer/README.md](AudioSpectrumVisualizer/README.md)** - 完整的API文档
- **[CLAUDE.md](CLAUDE.md)** - 项目指导文档（Claude Code）

---

## 🎯 核心功能说明

### 增益控制

**增益范围：** -20dB 到 +20dB（dB 刻度）

| 滑块位置 | dB 值 | 线性倍数 | 说明 |
|---------|-------|---------|------|
| 0 | -20dB | 0.1x | 最小（衰减 90%）|
| 10 | -10dB | 0.316x | 衰减 68% |
| **20** | **0dB** | **1.0x** | **无增益（默认）** |
| 30 | +10dB | 3.16x | 放大 3 倍 |
| 40 | +20dB | 10.0x | 最大（放大 10 倍）|

**使用方法：**
- 信号太强（削波/饱和）？→ 向**左**拖动滑块到负值
- 信号太弱（看不清）？→ 向**右**拖动滑块到正值
- 恢复默认？→ 拖动滑块到**中间位置**（0dB）
- 实时生效，无需停止采集

### IQ 模式

**功能：** 支持立体声 I/Q 信号采集，显示负频率（-24kHz 到 +24kHz）

**使用方法：**
1. 停止采集
2. 勾选/取消 "📡 IQ模式" 复选框
3. 重新开始采集

⚠️ **注意：** 采集运行时 IQ 模式复选框会被禁用，必须先停止采集才能切换。

**适用场景：**
- SoftRock 等硬件 IQ 混频器
- SDR 接收机
- 复数信号分析

---

## 🎯 验证频谱图正确性

### 快速测试方法

1. **单频正弦波测试**
   - 使用在线频率生成器：https://www.szynalski.com/tone-generator/
   - 播放 440Hz 音调
   - 观察频谱图在 440Hz 位置是否有明显峰值

2. **人声测试**
   - 对着麦克风说话
   - 观察基频（100-300Hz）和共振峰（500-4000Hz）

3. **白噪声测试**
   - 播放白噪声
   - 观察频谱是否平坦分布

### 详细验证方法

#### 多频测试
生成包含多个频率的音频（例如：440Hz + 880Hz + 1320Hz），应该看到 3 个明显的峰值。

#### 频率扫描测试
使用频率扫描音频（20Hz → 20kHz），频谱图的峰值应该从左向右移动，声谱图应该显示从左到右的亮线。

#### 对比专业软件
推荐对比软件：
- **Audacity** - 免费，内置频谱分析
- **Sonic Visualiser** - 专业音频分析工具
- **Spek** - 轻量级频谱分析器

### 验证清单

- [ ] **单频测试**：440Hz 正弦波显示正确
- [ ] **多频测试**：多个峰值位置正确
- [ ] **白噪声测试**：频谱分布平坦
- [ ] **频率扫描**：峰值从左向右移动
- [ ] **人声测试**：基频和共振峰清晰
- [ ] **峰值检测**：自动标记正确的峰值
- [ ] **鼠标交互**：悬停显示正确的频率和幅度
- [ ] **缩放功能**：滚轮缩放工作正常
- [ ] **坐标轴**：刻度标签正确
- [ ] **声谱图**：时间-频率变化清晰

---

## 🛠️ 系统要求

- .NET Framework 4.6.1 or higher
- Windows Forms
- Visual Studio 2015 or higher (for development)

### 依赖项

**库项目（AudioSpectrumVisualizer）：**
- System.dll
- System.Core.dll
- System.Numerics.dll
- System.Drawing.dll
- System.Windows.Forms.dll

**示例应用（SpectrumAnalysis）：**
- AudioSpectrumVisualizer（项目引用）
- NAudio 1.10.0（音频采集）

---

## 📊 性能指标

| 指标 | 应用程序 | 库默认 |
|------|---------|--------|
| 采样率 | 48000 Hz | 44100 Hz |
| FFT 大小 | 4096 点 | 2048 点 |
| 频率分辨率 | 11.7 Hz | 21.5 Hz |
| 刷新率 | ~10 FPS | 可配置 |
| CPU 占用 | ~5% | - |
| 内存占用 | 10-20 MB | - |

---

## 🎨 界面预览

```
┌─────────────────────────────────────────────────┐
│ 标题栏 + 控制面板                               │
├─────────────────────────────────────────────────┤
│ ┌─────────────────────────────────────────────┐ │
│ │ 频谱图 (SpectrumControl)                    │ │
│ │ ✓ 坐标轴 ✓ 网格 ✓ 峰值检测                 │ │
│ └─────────────────────────────────────────────┘ │
│ ═══════════════════════════════════════════════ │ ← 可拖动分隔条
│ ┌─────────────────────────────────────────────┐ │
│ │ 声谱图 (SpectrogramControl)                 │ │
│ │ ✓ 坐标轴 ✓ 瀑布式 ✓ 色彩映射                │ │
│ └─────────────────────────────────────────────┘ │
└─────────────────────────────────────────────────┘
```

---

## 🤝 贡献

欢迎贡献代码、报告问题或提出建议！

Contributions, issues, and feature requests are welcome!

---

## 📄 许可证

MIT License - 完全开源，可商用

详见 [LICENSE](AudioSpectrumVisualizer/LICENSE) 文件

---

## 🙏 致谢

- **NAudio** - 音频采集库
- **Cooley-Tukey FFT** - FFT算法

---

**Made with ❤️ for audio visualization enthusiasts**

**为音频可视化爱好者打造**
