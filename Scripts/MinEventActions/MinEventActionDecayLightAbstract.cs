using System.Collections.Generic;

public abstract class MinEventActionDecayLightAbstract : MinEventActionTargetedBase
{

    // values in seconds, which say when the sparkle effect must be played
    // follows a cubic curve, to have more frequent sparkles when the light has a low battery
    public static readonly HashSet<float> sparkleTimes = new HashSet<float>() { 0, 13, 25, 41, 61, 85, 113, 145, 181, 221, 265, 313, 365, 421, 481, 545, 613, 685, 761, 841, 925, 1013, 1105, 1201, 1301, 1405, 1513, 1625, 1741, 1861 };

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
            GameManager.Instance.StartCoroutine(FlashlightActions.LightSparklingCoroutine(lightTransform, keepActivated: remainingUseTimes > 0));
        }

        if (remainingUseTimes <= 0)
        {
            FlashlightActions.DeactivateFlashlight(player, lightItemValue);
            return;
        }

        lightItemValue.UseTimes++;

        AfterExecute(player);
    }

}