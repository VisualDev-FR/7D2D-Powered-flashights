
using UnityEngine;

public class MinEventActionDecayFlashLight : MinEventActionDecayLightAbstract
{
    public override ItemValue GetLightItemValue(MinEventParams _params)
    {
        return _params.Self.inventory.holdingItemItemValue;
    }

    public override string GetBuffName()
    {
        return "buffPoweredFlashLight";
    }

    public override Transform GetLightTransform(MinEventParams _params)
    {
        Transform parent = _params.Self.inventory.GetHoldingItemTransform();
        return GameUtils.FindDeepChild(parent, lightSourceProp);
    }

    public override void AfterExecute(EntityAlive player)
    {
        // update the itemStack UI
        player.inventory.notifyListeners();
    }
}