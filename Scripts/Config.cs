public class CaveLightConfig
{
    private static readonly ModConfig config = new ModConfig("CaveLights");

    public static float moonLightScale = config.GetPropertyFloat("moonLightScale");

    public static float sunIntensityScale = config.GetPropertyFloat("sunIntensityScale");
}