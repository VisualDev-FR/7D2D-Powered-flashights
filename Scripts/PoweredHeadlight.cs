using UnityEngine;

public class PoweredHeadLight : PoweredLightAbstract
{
    private const string headLightPropFPV = "HeadLight";

    private const string headLightPropTPV = "HeadLightCam";

    private const string modArmorHelmetLightProp = "modArmorHelmetLight";

    public override float MinIntensity => 1f;

    public override float MaxIntensity => 0.2f;

    public override ItemValue GetLightItemValue(MinEventParams _params)
    {
        if (TryGetHeadLight(_params.Self, out var headLightItemValue))
        {
            return headLightItemValue;
        }

        return null;
    }

    public override Transform GetLightTransform(MinEventParams _params)
    {
        Transform parent = null;

        if (!(_params.Self is EntityPlayerLocal player))
        {
            return null;
        }

        if (player.bFirstPersonView && player.parts.TryGetValue(headLightPropFPV, out var fpvTransform))
        {
            parent = fpvTransform;
        }

        if (!player.bFirstPersonView && player.parts.TryGetValue(headLightPropTPV, out var tpvTransform))
        {
            parent = tpvTransform;
        }

        if (parent is null)
            return null;

        return GameUtils.FindDeepChild(parent, lightSourceProp);
    }

    private bool TryGetHeadLight(EntityAlive player, out ItemValue headLightMod)
    {
        headLightMod = null;

        foreach (var itemValue in player.equipment.GetItems())
        {
            if (TryGetHeadlightItemValue(itemValue, ref headLightMod))
            {
                return true;
            }
        }

        return false;
    }

    private bool TryGetHeadlightItemValue(ItemValue equipment, ref ItemValue headlightMod)
    {
        if (equipment is null || !equipment.HasMods() || equipment.Modifications == null)
            return false;

        foreach (var mod in equipment.Modifications)
        {
            if (mod.ItemClass.Name == modArmorHelmetLightProp)
            {
                headlightMod = mod;
                return true;
            }
        }

        return false;
    }

}