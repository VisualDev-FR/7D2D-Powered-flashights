public class CaveLightConfig
{
    private static readonly ModConfig config = new ModConfig("CaveLights", version: 1);

    public static bool enableFlashlightsDecay = config.GetBool("enableFlashlightsDecay");

    public static bool enableLightSparkling = config.GetBool("enableLightSparkling");

    public static readonly FastTags<TagGroup.Global> tagPowered = FastTags<TagGroup.Global>.Parse("powered");

    public static bool forceFlicking = false;
}