using System.Collections;
using UnityEngine;

using Random = System.Random;

public class FlashlightActions
{
    private static readonly Random random = new Random();

    public static IEnumerator LightSparklingCoroutine(Transform transform, bool keepActivated)
    {
        float scale = 0.10f;

        for (int i = 0; i < random.Next(2, 6); i++)
        {
            SetLightActive(false, transform);
            yield return new WaitForSeconds((float)random.NextDouble() * scale);

            SetLightActive(true, transform);
            yield return new WaitForSeconds((float)random.NextDouble() * scale);
        }

        SetLightActive(false, transform);
        yield return new WaitForSeconds((float)random.NextDouble() * 2f);

        if (keepActivated)
        {
            SetLightActive(true, transform);
        }

        yield break;
    }

    public static void DeactivateFlashlight(EntityAlive player, ItemValue itemValue)
    {
        player.MinEventContext.ItemValue = itemValue;
        itemValue.FireEvent(MinEventTypes.onSelfItemDeactivate, player.MinEventContext);
        itemValue.Activated = 0;
    }

    public static void SetLightActive(bool isActive, Transform transform)
    {
        if (transform is null)
        {
            Log.Warning($"[PoweredFlashLights] MinEventActionDecayLightAbstract::SetLightActive => null transform");
            return;
        }

        transform.gameObject.SetActive(isActive);
        LightManager.LightChanged(transform.position + Origin.position);
    }

}