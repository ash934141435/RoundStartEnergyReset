# 每轮能量回满 | Round Start Energy Reset

[![Thunderstore](https://img.shields.io/badge/Thunderstore-Download-blue)](https://thunderstore.io/c/repo/p/YourName/RoundStart_EnergyReset/)
[![BepInEx](https://img.shields.io/badge/BepInEx-5.4.21+-green)](https://thunderstore.io/c/repo/p/BepInEx/BepInExPack/)

## 📖 功能说明

每轮进入新地图时，自动将**所有玩家**的能量晶体（体力/耐力）回满。

### ✨ 特性

- ✅ **每轮自动回满**：进入新地图时自动将能量设为最大值
- ✅ **全队生效**：联机时房主安装即可，所有玩家都会生效
- ✅ **非无限能量**：能量正常消耗，只是每轮开始时重置为满值
- ✅ **兼容性好**：支持单人及多人联机模式

## 🎮 使用方法

### 方法一：通过 Mod 管理器安装（推荐）

1. 下载并安装 [r2modman](https://thunderstore.io/package/ebkr/r2modman/) 或 [Gale](https://thunderstore.io/package/Kesomannen/GaleModManager/)
2. 在 Mod 管理器中选择 **R.E.P.O.**
3. 搜索 **"RoundStart_EnergyReset"**
4. 点击 **安装**
5. 通过 Mod 管理器启动游戏

### 方法二：手动安装

1. 确保已安装 [BepInEx](https://thunderstore.io/c/repo/p/BepInEx/BepInExPack/)
2. 下载本 MOD 的最新版本
3. 将 `RoundStartEnergyReset.dll` 放入游戏目录的 `BepInEx/plugins/` 文件夹
4. 启动游戏

## 📁 游戏目录位置

```
Steam/steamapps/common/REPO/
├── BepInEx/
│   ├── plugins/
│   │   └── RoundStartEnergyReset.dll  ← 放这里
│   └── config/
└── REPO.exe
```

## ⚙️ 依赖

| 依赖 | 版本 | 说明 |
|------|------|------|
| BepInEx | 5.4.21+ | MOD 加载框架 |

## ❓ 常见问题

### Q: MOD 安装后没有生效？
A: 
1. 确保已正确安装 BepInEx
2. 确保通过 Mod 管理器启动游戏（而不是直接启动游戏）
3. 检查 `BepInEx/LogOutput.log` 日志文件是否有错误信息

### Q: 联机时其他玩家没有生效？
A: 
- 本 MOD 只需要**房主安装**即可
- 确保你是房主（创建房间的人）
- 其他玩家无需安装

### Q: 能量没有回满？
A: 
- 确保你进入的是**新地图**（从卡车/大厅进入新关卡时触发）
- 查看日志文件确认 MOD 是否正常加载

## 📝 更新日志

### v1.0.0
- 首次发布
- 支持每轮进入新地图时自动回满能量
- 支持单人及联机模式

## 👤 作者

**YourName**

- GitHub: [yourname](https://github.com/yourname)

## 📜 许可证

MIT License

---

如果觉得这个 MOD 有用，欢迎在 Thunderstore 上点赞支持！ ⭐
