public class MinEventActionDecayGunLight : MinEventActionDecayLightAbstract
{

    public override string BuffName => "buffPoweredGunLight";

    public override PoweredLightAbstract PoweredLightSource => new PoweredGunLight();

    public override void AfterExecute(EntityAlive player)
    {
        // update the itemStack UI
        player.inventory.notifyListeners();
    }
}