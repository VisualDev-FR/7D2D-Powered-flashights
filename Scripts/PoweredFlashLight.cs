using UnityEngine;

public class PoweredFlashLight : PoweredLightAbstract
{
    private const string flashLightProp = "meleeToolFlashlight02";

    public override ItemValue GetLightItemValue(MinEventParams _params)
    {
        var holdingItemValue = _params.Self.inventory.holdingItemItemValue;

        if (holdingItemValue != null && holdingItemValue.ItemClass.Name == flashLightProp)
        {
            return holdingItemValue;
        }

        return null;
    }

    public override Transform GetLightTransform(MinEventParams _params)
    {
        Transform parent = _params.Self.inventory.GetHoldingItemTransform();

        if (parent != null)
        {
            return GameUtils.FindDeepChild(parent, lightSourceProp);
        }

        return null;
    }
}