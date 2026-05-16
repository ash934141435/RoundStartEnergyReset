using HarmonyLib;

namespace RoundStartEnergyReset.Patches;

/// <summary>
/// Patches GameDirector.SetStart to refill player energy at the start of each round.
/// GameDirector.SetStart is called when a new level/round initializes after generation.
/// </summary>
[HarmonyPatch(typeof(GameDirector))]
internal static class GameDirectorPatch
{
    [HarmonyPostfix]
    [HarmonyPatch("SetStart")]
    private static void OnRoundStart()
    {
        var playerController = PlayerController.instance;
        if (playerController == null)
        {
            return;
        }

        playerController.EnergyCurrent = playerController.EnergyStart;
        RoundStartEnergyReset.Logger.LogInfo("[RoundStartEnergyReset] Energy refilled to max at round start!");
    }
}
