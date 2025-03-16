using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = System.Random;

public abstract class PoweredLightAbstract
{
    protected const string lightSourceProp = "lightSource";

    public abstract ItemValue GetLightItemValue(MinEventParams _params);

    public abstract Transform GetLightTransform(MinEventParams _params);

    private static readonly Random random = new Random();

    public static List<PoweredLightAbstract> All()
    {
        return new List<PoweredLightAbstract>
        {
            new PoweredFlashLight(),
            new PoweredGunLight(),
            new PoweredHeadLight(),
        };
    }

    public IEnumerator LightSparklingCoroutine(Transform transform, bool keepActivated)
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

    public void DeactivateFlashlight(EntityAlive player, ItemValue itemValue)
    {
        player.MinEventContext.ItemValue = itemValue;
        itemValue.FireEvent(MinEventTypes.onSelfItemDeactivate, player.MinEventContext);
        itemValue.Activated = 0;
    }

    public void SetLightActive(bool isActive, Transform transform)
    {
        if (transform is null)
        {
            Logging.Warning($"PoweredLightAbstract::SetLightActive => null transform");
            return;
        }

        transform.gameObject.SetActive(isActive);
        LightManager.LightChanged(transform.position + Origin.position);
    }

}