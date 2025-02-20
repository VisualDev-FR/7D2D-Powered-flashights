public class CaveLightConfig
{
    private static readonly ModConfig config = new ModConfig("CaveLights");

    public static float moonLightScale = config.GetFloat("moonLightScale");

    public static float sunIntensityScale = config.GetFloat("sunIntensityScale");
}