#!/bin/bash
# 打包脚本 - 将 MOD 打包为 Thunderstore 格式

echo "========================================"
echo "  每轮能量回满 MOD 打包脚本"
echo "========================================"

# 检查必要文件
if [ ! -f "manifest.json" ]; then
    echo "错误: manifest.json 不存在"
    exit 1
fi

if [ ! -f "README.md" ]; then
    echo "错误: README.md 不存在"
    exit 1
fi

if [ ! -f "icon.png" ]; then
    echo "错误: icon.png 不存在"
    exit 1
fi

if [ ! -f "plugins/RoundStartEnergyReset.dll" ]; then
    echo "警告: plugins/RoundStartEnergyReset.dll 不存在"
    echo "请先编译项目: dotnet build"
fi

# 创建临时打包目录
mkdir -p package

# 复制文件到打包目录
cp manifest.json package/
cp README.md package/
cp icon.png package/

# 如果 dll 存在，复制到 package 根目录
if [ -f "plugins/RoundStartEnergyReset.dll" ]; then
    cp plugins/RoundStartEnergyReset.dll package/
fi

# 打包为 zip
zip -r RoundStart_EnergyReset_v1.0.0.zip package/*

# 清理临时目录
rm -rf package

echo ""
echo "========================================"
echo "  打包完成！"
echo "  输出文件: RoundStart_EnergyReset_v1.0.0.zip"
echo "========================================"
