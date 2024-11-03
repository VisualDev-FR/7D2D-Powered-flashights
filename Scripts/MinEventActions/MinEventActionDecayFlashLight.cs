public class MinEventActionDecayFlashLight : MinEventActionDecayLightAbstract
{
    public override string BuffName => "buffPoweredFlashLight";

    public override PoweredLightAbstract PoweredLightSource => new PoweredFlashLight();

    public override void AfterExecute(EntityAlive player)
    {
        // update the itemStack UI
        player.inventory.notifyListeners();
    }
}