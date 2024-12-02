using HarmonyLib;

namespace PowerSourceFix.Patches;
public class PowerSourcePatches
{
    [HarmonyPatch(typeof(PowerSource), nameof(PowerSource.IsPowered))]
    [HarmonyPostfix]
    public static void Postfix(PowerSource __instance)
    {
        try
        {

            if (__instance == null)
                return;

            if (__instance.RequiresFuel && __instance._Fuel.FuelSlot.slot == null)
            {
                __instance.RequiresFuel = false;

                if (__instance.Parent == null)
                {
                    Plugin.Log.LogInfo("Repairing a broken PowerSource with no Parent");
                    return;
                }

                string oName = __instance.Parent.name ?? "Unnamed Object";
                Plugin.Log.LogMessage($"Repairing a broken PowerSource of type: {oName}");
            }
        }
        catch (System.Exception ex)
        {
            Plugin.Log.LogWarning($"Exception in patch of bool PowerSource::IsPowered():\n{ex}");
        }
    }

}
