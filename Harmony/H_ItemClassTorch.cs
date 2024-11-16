using HarmonyLib;


[HarmonyPatch(typeof(ItemClassTorch), "OnHoldingUpdate")]
public class ItemClassTorch_StartHolding
{
    private static float TorchLightIntensity => ItemClass.GetItemClass("meleeToolTorch").Properties.GetFloat("MaxIntensity");

    public static void Postfix(ItemInventoryData _data)
    {
        foreach (LightLOD light in _data.model.GetComponentsInChildren<LightLOD>())
        {
            if (light != null)
            {
                light.lightIntensity = TorchLightIntensity;
            }
        }
    }
}
