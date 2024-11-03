public class MinEventActionDecayFlashLight : MinEventActionDecayLightAbstract
{
    public override string BuffName => "buffPoweredFlashLight";

    public override PoweredLightAbstract PoweredLightSource => new PoweredFlashLight();
}