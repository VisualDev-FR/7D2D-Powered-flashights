public class MinEventActionTurnOffLights : MinEventActionTargetedBase
{
    public override void Execute(MinEventParams _params)
    {
        var player = _params.Self;
        var lightSources = PoweredLightAbstract.All();

        Log.Out($"[PoweredFlashLight] lightSources: {lightSources.Count}");

        foreach (var lightSource in lightSources)
        {
            var itemValue = lightSource.GetLightItemValue(_params);
            var transform = lightSource.GetLightTransform(_params);

            if (itemValue is null || itemValue.Activated == 0)
                continue;

            GameManager.Instance.StartCoroutine(FlashlightActions.LightSparklingCoroutine(transform, keepActivated: false));
            FlashlightActions.DeactivateFlashlight(player, itemValue);

            Log.Out($"[PoweredFlashLight] deactivate lightsource: '{itemValue.ItemClass.Name}'");
        }

        player.inventory.notifyListeners();
    }
}