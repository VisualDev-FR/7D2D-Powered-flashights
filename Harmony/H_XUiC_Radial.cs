using Audio;
using HarmonyLib;


[HarmonyPatch(typeof(XUiC_Radial), "handleActivatableItemCommand")]
public class XUiC_Radial_handleActivatableItemCommand
{
    public static bool Prefix(XUiC_Radial __instance, XUiC_Radial _sender, int _commandIndex, XUiC_Radial.RadialContextAbs _context)
    {
        EntityPlayerLocal entityPlayer = _sender.xui.playerUI.entityPlayer;
        __instance.activatableItemPool.Clear();
        entityPlayer.CollectActivatableItems(__instance.activatableItemPool);
        ItemValue itemValue = __instance.activatableItemPool[_commandIndex];

        if (itemValue.Activated == 0 && itemValue.HasQuality && itemValue.UseTimes >= itemValue.MaxUseTimes)
        {
            Manager.PlayInsidePlayerHead("flashlight_toggle");
            return false;
        }

        if (itemValue.Activated == 0 && entityPlayer.Buffs.HasBuff("buffFlashlightDisabled"))
        {
            Manager.PlayInsidePlayerHead("flashlight_toggle");
            return false;
        }

        return true;
    }

}