using System.Linq;

public class RequirementHasLightActivated : TargetedCompareRequirementBase
{
    public override bool IsValid(MinEventParams _params)
    {
        if (!IsValid(_params))
        {
            return false;
        }

        int activeLightSourceCount = PoweredLightAbstract.All()
            .Select(light => light.GetLightItemValue(_params))
            .Count(itemValue => itemValue != null && itemValue.Activated > 0);

        bool hasLightActivated = activeLightSourceCount > 0;

        return invert ? !hasLightActivated : hasLightActivated;
    }
}