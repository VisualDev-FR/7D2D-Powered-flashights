using Audio;
using HarmonyLib;


/*
    Patch allowing to prevent player from activating light if
    broken, or if buffFlashlightDisabled is active
*/
[HarmonyPatch(typeof(XUiC_Radial), "handleActivatableItemCommand")]
public class XUiC_Radial_handleActivatableItemCommand
{
    private const string flashlightToggleSoundProp = "flashlight_toggle";

    private const string buffFlashlightDisabledProp = "buffFlashlightDisabled";

    public static bool Prefix(XUiC_Radial __instance, XUiC_Radial _sender, int _commandIndex, XUiC_Radial.RadialContextAbs _context)
    {
        EntityPlayerLocal entityPlayer = _sender.xui.playerUI.entityPlayer;
        __instance.activatableItemPool.Clear();
        entityPlayer.CollectActivatableItems(__instance.activatableItemPool);
        ItemValue itemValue = __instance.activatableItemPool[_commandIndex];

        if (!IsPoweredItem(itemValue))
        {
            return true;
        }

        if (itemValue.Activated == 0 && itemValue.HasQuality && itemValue.UseTimes >= itemValue.MaxUseTimes)
        {
            Manager.PlayInsidePlayerHead(flashlightToggleSoundProp);
            return false;
        }

        if (itemValue.Activated == 0 && entityPlayer.Buffs.HasBuff(buffFlashlightDisabledProp))
        {
            Manager.PlayInsidePlayerHead(flashlightToggleSoundProp);
            return false;
        }

        return true;
    }

    private static bool IsPoweredItem(ItemValue itemValue)
    {
        if (itemValue is null || itemValue.ItemClass is null)
        {
            return false;
        }

        return itemValue.ItemClass.HasAnyTags(CaveLightConfig.tagPowered);
    }

}