using UnityEngine;

public class PoweredGunLight : PoweredLightAbstract
{
    private const string gunLightProp = "modGunFlashlight";

    private const string flashLightSourceProp = "flashlight_lightSource";

    public override ItemValue GetLightItemValue(MinEventParams _params)
    {
        if (!(_params.Self is EntityPlayerLocal player))
        {
            return null;
        }

        var itemValue = player.inventory.holdingItemItemValue;

        if (TryGetGunLight(itemValue, out var lightItemValue))
        {
            return lightItemValue;
        }

        return null;
    }

    public override Transform GetLightTransform(MinEventParams _params)
    {
        var parent = _params.Self.inventory.GetHoldingItemTransform();

        if (parent is null)
        {
            return null;
        }

        return GameUtils.FindDeepChild(parent, flashLightSourceProp);
    }

    private bool TryGetGunLight(ItemValue holdingItem, out ItemValue gunLightItemValue)
    {
        gunLightItemValue = null;

        if (holdingItem is null || !holdingItem.HasMods() || holdingItem.Modifications is null)
            return false;

        foreach (var mod in holdingItem.Modifications)
        {
            if (mod != null && mod.ItemClass != null && mod.ItemClass.Name == gunLightProp)
            {
                gunLightItemValue = mod;
                return true;
            }
        }

        return false;
    }

}