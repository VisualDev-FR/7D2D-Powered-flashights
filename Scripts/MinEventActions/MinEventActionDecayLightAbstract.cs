using System.Collections.Generic;
using UnityEngine;

public abstract class MinEventActionDecayLightAbstract : MinEventActionTargetedBase
{

    // values in seconds, which say when the sparkle effect must be played
    // follows a cubic curve, to have more frequent sparkles when the light has a low battery
    public static readonly HashSet<float> sparkleTimes = new HashSet<float>() {
        0, 13, 25, 41, 61, 85, 113, 145, 181, 221,
        265, 313, 365, 421, 481, 545, 613, 685, 761,
        841, 925, 1013, 1105, 1201, 1301, 1405, 1513,
        1625, 1741, 1861, 1985, 2113, 2245, 2381, 2521,
        2665, 2813, 2965, 3121, 3281, 3445, 3613, 3785,
        3961, 4141, 4325, 4513, 4705, 4901, 5101, 5305,
        5513, 5725, 5941, 6161, 6385, 6613, 6845, 7081
     };

    public abstract PoweredLightAbstract PoweredLightSource { get; }

    public abstract string BuffName { get; }

    public virtual void AfterExecute(EntityAlive player) { }

    public override void Execute(MinEventParams _params)
    {
        var player = _params.Self;
        var lightSource = PoweredLightSource;
        var lightItemValue = lightSource.GetLightItemValue(_params);
        var lightTransform = lightSource.GetLightTransform(_params);

        if (lightItemValue is null || lightItemValue.Activated == 0)
        {
            player.Buffs.RemoveBuff(BuffName);
            return;
        }

        float remainingUseTimes = lightItemValue.MaxUseTimes - lightItemValue.UseTimes;

        if (lightTransform != null && sparkleTimes.Contains(remainingUseTimes))
        {
            GameManager.Instance.StartCoroutine(lightSource.LightSparklingCoroutine(lightTransform, keepActivated: remainingUseTimes > 0));
        }

        if (remainingUseTimes <= 0)
        {
            lightSource.DeactivateFlashlight(player, lightItemValue);
            return;
        }

        DecreaseLight(lightTransform, lightItemValue);
        AfterExecute(player);

        lightItemValue.UseTimes++;
        player.inventory.notifyListeners();
    }

    private void DecreaseLight(Transform lightSource, ItemValue itemValue)
    {
        Light light = lightSource.GetComponent<Light>();
        float percent = 1f - (itemValue.UseTimes / itemValue.MaxUseTimes);

        light.intensity = Utils.FastLerp(
            itemValue.ItemClass.Properties.GetFloat("MinIntensity"),
            itemValue.ItemClass.Properties.GetFloat("MaxIntensity"),
            percent
        );
    }
}