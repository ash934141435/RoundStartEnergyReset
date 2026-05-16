# RoundStartEnergyReset - 开局自动回满能量

A simple client-side mod for **R.E.P.O.** that automatically refills your weapon energy bar to maximum at the start of each round/level. No more scrambling for charging stations!

一个适用于 **R.E.P.O.** 的客户端模组，在每局/关卡开始时自动将武器能量条回满。再也不用到处找充电站了！

## Features / 功能

- ✅ Automatically refills energy to max when a new round starts
- ✅ Works in both singleplayer and multiplayer (affects only you)
- ✅ Lightweight - no configuration needed, just install and play
- ✅ No console spam - clean and minimal logging
- ✅ Client-side only, no host required

- ✅ 每局开始时自动将能量回满
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

This mod uses Harmony to patch `GameDirector.SetStart()` - the method called by the game when a new level/round initializes. When detected, it sets `PlayerController.EnergyCurrent` to `EnergyStart` (the max value).

Backup: It also patches `PlayerController.Start()` to ensure energy is max when the player first spawns.

本模组使用 Harmony 补丁技术拦截 `GameDirector.SetStart()`（游戏在关卡/回合初始化时调用的方法）。检测到新回合后，将 `PlayerController.EnergyCurrent` 设置为 `EnergyStart`（最大值）。

备用机制：同时补丁了 `PlayerController.Start()`，确保玩家首次生成时能量为满。

## Source Code / 源码

https://github.com/your-username/RoundStartEnergyReset

## Changelog / 更新日志

### 1.0.0
- Initial release
- Energy refill on round start via GameDirector.SetStart
- Fallback on PlayerController.Start

## Credits / 致谢

- [R.E.P.O. Modding Wiki](https://repomods.com/) - For modding documentation
- [BepInEx](https://github.com/BepInEx/BepInEx) - Mod loader framework
- [Harmony](https://github.com/pardeike/Harmony) - Runtime patching library
