using HarmonyLib;

namespace RoundStartEnergyReset.Patches;

/// <summary>
/// Patches ChargingStation.Start to set initial charge to near-infinite.
/// This prevents the station from ever running out, as long as it's loaded 
/// after the mod is installed (new saves and existing saves).
/// 
/// Based on the InfiniteEnergy mod by ncfcj.
/// </summary>
[HarmonyPatch(typeof(ChargingStation))]
internal static class ChargingStationPatch
{
    [HarmonyPrefix]
    [HarmonyPatch("Start")]
    private static void OnStart(ChargingStation __instance)
    {
        // Default max from ChargingStation constructor: chargeTotal = 100
        // This ensures the station starts at normal full capacity even if save data
        // had a lower value from a previous run.
        __instance.chargeTotal = 100;
        RoundStartEnergyReset.Logger.LogInfo("[RoundStartEnergyReset] ChargingStation.Start() - chargeTotal set to 100 (full)");
    }
}
