# 发布指南

## 📦 打包步骤

### 1. 编译项目

在 Visual Studio 中：
1. 打开 `RoundStartEnergyReset.csproj`
2. **重要**：修改 `.csproj` 中的 `<GamePath>` 为你的 R.E.P.O. 游戏安装路径
3. 按 `Ctrl+Shift+B` 编译项目
4. 编译成功后，`RoundStartEnergyReset.dll` 会生成在 `plugins/` 目录

或使用命令行：
```bash
dotnet build
```

### 2. 打包为 ZIP

确保以下文件存在：
- ✅ `manifest.json`
- ✅ `README.md`
- ✅ `icon.png` (256x256)
- ✅ `plugins/RoundStartEnergyReset.dll`

打包命令：
```bash
# 创建 zip 包
zip RoundStart_EnergyReset_v1.0.0.zip manifest.json README.md icon.png plugins/RoundStartEnergyReset.dll
```

或运行打包脚本：
```bash
chmod +x package.sh
./package.sh
```

---

## 🚀 发布到 Thunderstore

### 步骤

1. **登录 Thunderstore**
   - 访问 [https://thunderstore.io](https://thunderstore.io)
   - 点击右上角 **Sign In** 登录（可使用 GitHub 账号）

2. **上传 MOD**
   - 点击右上角 **Upload** 按钮
   - 选择游戏：**R.E.P.O.**
   - 选择分类：**Gameplay**（游戏玩法类）
   - 拖入打包好的 `RoundStart_EnergyReset_v1.0.0.zip`
   - 点击 **Submit** 提交

3. **等待审核**
   - 上传后通常几分钟内就会通过
   - 通过后 MOD 会出现在 Thunderstore 列表中

4. **同步到雷神加速器**
   - Thunderstore 上的 MOD 会自动同步到雷神加速器模组仓库
   - 通常需要 **几小时到一天** 时间

---

## 🔄 更新 MOD

如果需要更新 MOD：

1. 修改代码
2. **更新版本号**：
   - 修改 `manifest.json` 中的 `version_number`（如 `1.0.0` → `1.0.1`）
   - 修改 `Plugin.cs` 中的版本号
   - 修改 `.csproj` 中的 `<Version>`
3. 重新编译并打包
4. 上传到 Thunderstore（会自动识别为更新）

⚠️ **注意**：不要修改 `manifest.json` 中的 `name`，否则会创建新的 MOD 而不是更新旧的。

---

## 📋 Thunderstore 文件结构要求

```
RoundStart_EnergyReset.zip
├── manifest.json      # 必需 - 元信息
├── README.md          # 必需 - 说明文档
├── icon.png           # 必需 - 256x256 图标
└── RoundStartEnergyReset.dll  # MOD 主文件
```

---

## ❓ 常见问题

### Q: 上传失败，提示版本号已存在？
A: 每个版本号只能上传一次，需要更新版本号后重新上传。

### Q: MOD 在 Thunderstore 上看不到？
A: 上传后可能需要几分钟才能显示，刷新页面或等待一段时间。

### Q: 雷神加速器里找不到这个 MOD？
A: Thunderstore 同步到雷神加速器需要时间，通常几小时到一天。也可以直接在 Thunderstore 下载。

---

## 📝 发布前检查清单

- [ ] 已修改 `.csproj` 中的游戏路径
- [ ] 已成功编译生成 `.dll` 文件
- [ ] `manifest.json` 版本号正确
- [ ] `README.md` 内容完整
- [ ] `icon.png` 为 256x256 像素
- [ ] 已打包为 zip 文件
- [ ] zip 文件结构正确（文件在根目录）
