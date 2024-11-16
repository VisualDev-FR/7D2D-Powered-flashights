using UnityEngine;

public class PoweredFlashLight : PoweredLightAbstract
{
    private const string flashLightProp = "meleeToolFlashlight02";

    public override float MinIntensity => 0.2f;

    public override float MaxIntensity => 1.3f;

    public override ItemValue GetLightItemValue(MinEventParams _params)
    {
        var itemValue = _params.Self.inventory.holdingItemItemValue;

        if (itemValue != null && itemValue.ItemClass != null && itemValue.ItemClass.Name == flashLightProp)
        {
            return itemValue;
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