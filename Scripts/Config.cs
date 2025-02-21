public class CaveLightConfig
{
    private static readonly ModConfig config = new ModConfig("CaveLights");

    public static float moonLightScale = config.GetFloat("moonLightScale");

    public static float sunIntensityScale = config.GetFloat("sunIntensityScale");

    public static bool enableFlashlightsDecay = config.GetBool("enableFlashlightsDecay");

    public static bool enableLightSparkling = config.GetBool("enableLightSparkling");

    public static readonly FastTags<TagGroup.Global> tagPowered = FastTags<TagGroup.Global>.Parse("powered");
}