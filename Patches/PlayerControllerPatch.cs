using HarmonyLib;

namespace RoundStartEnergyReset.Patches;

/// <summary>
/// Patches PlayerController.Start to ensure energy is at max when the player controller initializes.
/// This handles the initial spawn/join case as a backup to the GameDirector patch.
/// </summary>
[HarmonyPatch(typeof(PlayerController))]
internal static class PlayerControllerPatch
{
    [HarmonyPostfix]
    [HarmonyPatch("Start")]
    private static void OnPlayerControllerStart(PlayerController __instance)
    {
        __instance.EnergyCurrent = __instance.EnergyStart;
        RoundStartEnergyReset.Logger.LogDebug("[RoundStartEnergyReset] Player energy set to max on controller start");
    }
}
