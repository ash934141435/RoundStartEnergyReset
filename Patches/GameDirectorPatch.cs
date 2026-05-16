using HarmonyLib;

namespace RoundStartEnergyReset.Patches;

/// <summary>
/// Patches GameDirector.SetStart to refill the Charging Station energy crystals 
/// (used to charge weapons in the truck/base) and stamina at the start of each round.
/// 
/// Systems:
///   - PlayerController.EnergyCurrent = stamina (for sprinting)
///   - ChargingStation.chargeTotal = total charge energy in the base charging station
///   - ChargingStation.chargeFloat = display value for charge bar animation
///   - ChargingStation.chargeSegmentCurrent = active charge segments (display)
/// </summary>
[HarmonyPatch(typeof(GameDirector))]
internal static class GameDirectorPatch
{
    [HarmonyPostfix]
    [HarmonyPatch("SetStart")]
    private static void OnRoundStart()
    {
        // 1. Refill stamina
        var pc = PlayerController.instance;
        if (pc != null)
        {
            pc.EnergyCurrent = pc.EnergyStart;
            RoundStartEnergyReset.Logger.LogInfo("[RoundStartEnergyReset] Stamina refilled!");
        }
        else
        {
            RoundStartEnergyReset.Logger.LogWarning("[RoundStartEnergyReset] PlayerController.instance is null at SetStart");
        }

        // 2. Refill Charging Station energy
        RefillChargingStation();
    }

    /// <summary>
    /// Refills the Charging Station completely.
    /// Must update ALL display fields, not just chargeTotal.
    /// 
    /// The charging station display chain (from IL analysis):
    ///   Update(): chargeScaleTarget = chargeSegmentCurrent / chargeSegments
    ///   End of Update(): chargeSegmentCurrent = RoundToInt(chargeFloat * chargeSegments)
    ///   chargeFloat = chargeTotal / 100.0 (set in Start())
    /// 
    /// Setting only chargeTotal is not enough — chargeFloat must also be updated
    /// so that the segment recalculation in Update() produces full segments.
    /// </summary>
    private static void RefillChargingStation()
    {
        var cs = ChargingStation.instance;
        if (cs == null)
        {
            RoundStartEnergyReset.Logger.LogWarning("[RoundStartEnergyReset] ChargingStation.instance is null");
            return;
        }

        // Default max from ChargingStation constructor: chargeTotal = 100, chargeFloat = 1.0
        // chargeTotal is clamped to [0, 100] during consumption (ChargeAreaCheck).
        // Full bar condition: chargeFloat >= 1.0 → chargeTotal >= 100.
        cs.chargeTotal = 100;

        // chargeFloat = chargeTotal / 100f.
        // This is the source of truth for the display — Update() reads chargeFloat,
        // not chargeTotal, when recalculating chargeSegmentCurrent every frame.
        cs.chargeFloat = 1.0f;

        // Set segment display to full immediately (before the first Update() recalc).
        // Update() end: chargeSegmentCurrent = RoundToInt(chargeFloat * chargeSegments)
        // With chargeFloat = 1.0: RoundToInt(1.0 * chargeSegments) = chargeSegments ✓
        cs.chargeSegmentCurrent = cs.chargeSegments;

        RoundStartEnergyReset.Logger.LogInfo("[RoundStartEnergyReset] Charging station refilled to max!");
    }
}
