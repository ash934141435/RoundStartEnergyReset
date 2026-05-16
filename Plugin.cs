using System;
using System.Collections;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace RoundStartEnergyReset
{
    /// <summary>
    /// 每轮能量回满 MOD
    /// 功能：每轮进入新地图时，自动将所有玩家的能量晶体回满
    /// 作者：YourName
    /// 版本：1.0.0
    /// </summary>
    [BepInPlugin("com.roundstart.energyreset", "RoundStart_EnergyReset", "1.0.0")]
    [BepInDependency("BepInEx", BepInDependency.DependencyFlags.HardDependency)]
    public class Plugin : BaseUnityPlugin
    {
        internal static ManualLogSource Log;
        private static Harmony _harmony;
        
        // 配置项：是否启用
        internal static bool Enabled = true;
        // 配置项：延迟时间（秒）
        internal static float DelaySeconds = 1.5f;

        private void Awake()
        {
            Log = Logger;
            Log.LogInfo("========================================");
            Log.LogInfo("【每轮能量回满】插件正在初始化...");
            Log.LogInfo("========================================");
            
            // 应用 Harmony 补丁
            _harmony = new Harmony("com.roundstart.energyreset");
            _harmony.PatchAll();
            
            Log.LogInfo("【每轮能量回满】插件加载完成！");
        }

        private void OnDestroy()
        {
            _harmony?.UnpatchSelf();
            Log.LogInfo("【每轮能量回满】插件已卸载。");
        }
    }

    /// <summary>
    /// Hook RunManager.ChangeLevel 方法
    /// 当游戏切换关卡（进入新地图）时触发
    /// </summary>
    [HarmonyPatch(typeof(RunManager), "ChangeLevel")]
    public static class RunManagerChangeLevelPatch
    {
        public static void Postfix(RunManager __instance)
        {
            if (!Plugin.Enabled) return;
            
            Plugin.Log.LogInfo("【每轮能量回满】检测到关卡切换，准备回满能量...");
            
            // 延迟执行，确保玩家控制器已加载
            if (PlayerController.instance != null)
            {
                PlayerController.instance.StartCoroutine(DelayedEnergyReset());
            }
            else
            {
                Plugin.Log.LogWarning("【每轮能量回满】PlayerController.instance 为空，尝试备用方案...");
                // 备用方案：直接查找 Player 对象
                var playerObj = GameObject.Find("Player");
                if (playerObj != null)
                {
                    var controller = playerObj.transform.Find("Controller")?.gameObject;
                    if (controller != null)
                    {
                        var pc = controller.GetComponent<PlayerController>();
                        if (pc != null)
                        {
                            pc.StartCoroutine(DelayedEnergyReset());
                        }
                    }
                }
            }
        }

        private static IEnumerator DelayedEnergyReset()
        {
            // 等待玩家完全加载
            yield return new WaitForSeconds(Plugin.DelaySeconds);
            
            int resetCount = 0;
            float totalEnergy = 0f;

            // 尝试获取所有玩家（联机时遍历全队）
            try
            {
                // 方法1：使用 SemiFunc.PlayerGetAllPlayerAvatar（如果存在）
                var allAvatars = SemiFunc.PlayerGetAllPlayerAvatar();
                if (allAvatars != null && allAvatars.Count > 0)
                {
                    foreach (var playerAvatar in allAvatars)
                    {
                        if (playerAvatar == null) continue;
                        
                        var pc = playerAvatar.GetComponent<PlayerController>();
                        if (pc == null) continue;

                        // 回满能量
                        float maxEnergy = pc.EnergyStart;
                        pc.EnergyCurrent = maxEnergy;
                        totalEnergy += maxEnergy;
                        resetCount++;
                    }
                }
                else
                {
                    // 方法2：直接使用 PlayerController.instance（单人模式）
                    if (PlayerController.instance != null)
                    {
                        var pc = PlayerController.instance;
                        float maxEnergy = pc.EnergyStart;
                        pc.EnergyCurrent = maxEnergy;
                        totalEnergy = maxEnergy;
                        resetCount = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"【每轮能量回满】获取玩家列表时出错: {ex.Message}");
                
                // 最终备用方案
                if (PlayerController.instance != null)
                {
                    var pc = PlayerController.instance;
                    float maxEnergy = pc.EnergyStart;
                    pc.EnergyCurrent = maxEnergy;
                    totalEnergy = maxEnergy;
                    resetCount = 1;
                }
            }

            if (resetCount > 0)
            {
                Plugin.Log.LogInfo($"========================================");
                Plugin.Log.LogInfo($"【每轮能量回满】成功！");
                Plugin.Log.LogInfo($"  - 已为 {resetCount} 名玩家回满能量");
                Plugin.Log.LogInfo($"  - 总能量值: {totalEnergy:F1}");
                Plugin.Log.LogInfo($"========================================");
            }
            else
            {
                Plugin.Log.LogWarning("【每轮能量回满】未找到任何玩家，跳过能量重置。");
            }
        }
    }

    /// <summary>
    /// 备用 Hook：Hook PlayerController.LateStart
    /// 在玩家控制器初始化完成后触发（作为备用触发点）
    /// </summary>
    [HarmonyPatch(typeof(PlayerController), "LateStart")]
    public static class PlayerControllerLateStartPatch
    {
        private static bool _firstInit = true;
        
        public static void Postfix(PlayerController __instance)
        {
            if (!Plugin.Enabled) return;
            if (!_firstInit) return; // 只在首次初始化时触发
            
            _firstInit = false;
            
            Plugin.Log.LogInfo("【每轮能量回满】PlayerController 初始化完成，回满能量...");
            
            // 回满能量
            __instance.EnergyCurrent = __instance.EnergyStart;
            
            Plugin.Log.LogInfo($"【每轮能量回满】能量已回满: {__instance.EnergyCurrent:F1}/{__instance.EnergyStart:F1}");
        }
    }
}
