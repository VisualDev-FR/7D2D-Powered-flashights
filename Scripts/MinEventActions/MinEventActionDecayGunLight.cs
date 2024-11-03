public class MinEventActionDecayGunLight : MinEventActionDecayLightAbstract
{
    public override string BuffName => "buffPoweredGunLight";

    public override PoweredLightAbstract PoweredLightSource => new PoweredGunLight();
}