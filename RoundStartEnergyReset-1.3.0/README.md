# RoundStartEnergyReset - 开局自动回满能量

A simple client-side mod for **R.E.P.O.** that automatically refills the **Charging Station energy crystals** (used to charge weapons in the truck) and stamina to max at the start of each round/level.

一个适用于 **R.E.P.O.** 的客户端模组，在每局/关卡开始时自动将**基地充电站的能量晶体**（用于给武器充电的能量条）和体力补满。

## Features / 功能

- ✅ Automatically refills **Charging Station energy** (ChargingStation.chargeTotal) and stamina when a new round starts
- ✅ Works in both singleplayer and multiplayer (affects only you)
- ✅ Lightweight - no configuration needed, just install and play
- ✅ Clean and minimal logging
- ✅ Client-side only, no host required

- ✅ 每局开始时自动将**基地充电站能量**（给武器充电的能量条）和体力补满
- ✅ 支持单人模式和多人模式（仅影响你自己）
- ✅ 轻量级 - 无需配置，安装即玩
- ✅ 无控制台刷屏 - 日志简洁
- ✅ 纯客户端模组，无需主机

## Installation / 安装

### Method 1: Mod Manager (Recommended)
1. Install a mod manager like [Gale](https://thunderstore.io/c/repo/p/Kesomannen/GaleModManager/) or r2modman
2. Search for "RoundStartEnergyReset" and install it
3. Launch the game via the mod manager

### Method 2: Manual
1. Install [BepInExPack for R.E.P.O.](https://thunderstore.io/c/repo/p/BepInEx/BepInExPack/)
2. Run the game once to generate BepInEx folders
3. Download the mod zip and extract `RoundStartEnergyReset.dll` into `BepInEx/plugins/`
4. Launch the game

### 方法一：使用模组管理器（推荐）
1. 安装 Gale 或 r2modman 模组管理器
2. 搜索 "RoundStartEnergyReset" 并安装
3. 通过模组管理器启动游戏

### 方法二：手动安装
1. 安装 [BepInExPack for R.E.P.O.](https://thunderstore.io/c/repo/p/BepInEx/BepInExPack/)
2. 运行一次游戏以生成 BepInEx 文件夹
3. 下载模组压缩包，将 `RoundStartEnergyReset.dll` 解压到 `BepInEx/plugins/` 目录
4. 启动游戏

## How it works / 工作原理

This mod uses Harmony to patch `GameDirector.SetStart()` - the method called by the game when a new level/round initializes. At round start, it does two things:
1. Sets `PlayerController.EnergyCurrent` to `EnergyStart` (refills stamina)
2. Sets `ChargingStation.chargeTotal` to max (refills the Charging Station energy crystals in the truck/base, used to charge weapons)

Backup: It also patches `PlayerController.Start()` to ensure stamina is max when the player first spawns.

本模组使用 Harmony 补丁技术拦截 `GameDirector.SetStart()`（游戏在关卡/回合初始化时调用的方法）。在回合开始时：
1. 将 `PlayerController.EnergyCurrent` 设置为 `EnergyStart`（补满体力）
2. 将 `ChargingStation.chargeTotal` 补满至最大值（恢复基地充电站能量，用于给武器充电）

备用机制：同时补丁了 `PlayerController.Start()`，确保玩家首次生成时体力为满。

## Source Code / 源码

https://github.com/your-username/RoundStartEnergyReset

## Changelog / 更新日志

### 1.3.0
- **Fixed**: Charging station energy now properly refills to normal max (chargeTotal=100, chargeFloat=1.0) instead of an unrealistic 999999
- Found game's default maximum values by analyzing assembly IL: chargeTotal is clamped to [0, 100], chargeFloat=1.0 = full bar
- Also updates `chargeFloat` and `chargeSegmentCurrent` so the charge bar display actually shows full
- Added `ChargingStation.Start()` prefix patch for initial max charge

### 1.2.0
- **Changed**: Now refills **Charging Station energy crystals** (`ChargingStation.chargeTotal`) instead of base hand energy (`SpectateCamera.headEnergy`)
- Direct compile-time access to fields via BepInEx publicizer

### 1.1.0
- **Fixed**: Now refills the **base hand energy bar** (headEnergy) instead of weapon battery
- Added automatic runtime field detection via reflection (tries PlayerController, PlayerAvatar, and more)
- Removed ItemBattery refill (user preference - only refills player's innate energy now)

### 1.0.0
- Initial release
- Stamina refill on round start via GameDirector.SetStart
- Fallback on PlayerController.Start
- ItemBattery weapon refill (removed in 1.1.0)

## Credits / 致谢

- [R.E.P.O. Modding Wiki](https://repomods.com/) - For modding documentation
- [BepInEx](https://github.com/BepInEx/BepInEx) - Mod loader framework
- [Harmony](https://github.com/pardeike/Harmony) - Runtime patching library
