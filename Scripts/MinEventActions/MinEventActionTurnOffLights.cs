public class MinEventActionTurnOffLights : MinEventActionTargetedBase
{
    public override void Execute(MinEventParams _params)
    {
        var player = _params.Self;
        var lightSources = PoweredLightAbstract.All();

        foreach (var lightSource in lightSources)
        {
            var itemValue = lightSource.GetLightItemValue(_params);
            var transform = lightSource.GetLightTransform(_params);

            if (itemValue is null || itemValue.Activated == 0)
                continue;

            GameManager.Instance.StartCoroutine(lightSource.LightSparklingCoroutine(transform, keepActivated: false));
            lightSource.DeactivateFlashlight(player, itemValue);
        }

        player.inventory.notifyListeners();
    }
}