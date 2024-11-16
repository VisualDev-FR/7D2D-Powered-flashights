using System.Collections.Generic;
using UnityEngine;

public abstract class PoweredLightAbstract
{
    protected const string lightSourceProp = "lightSource";

    public abstract float MinIntensity { get; }

    public abstract float MaxIntensity { get; }

    public abstract ItemValue GetLightItemValue(MinEventParams _params);

    public abstract Transform GetLightTransform(MinEventParams _params);

    public static List<PoweredLightAbstract> All()
    {
        return new List<PoweredLightAbstract>
        {
            new PoweredFlashLight(),
            new PoweredGunLight(),
            new PoweredHeadLight(),
        };
    }
}