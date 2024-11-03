using UnityEngine;

public class MinEventActionDecayHeadLight : MinEventActionDecayLightAbstract
{
    private const string headLightPropFPV = "HeadLight";

    private const string headLightPropTPV = "HeadLightCam";

    public override string BuffName => "buffPoweredHeadLight";

    public override PoweredLightAbstract PoweredLightSource => new PoweredHeadLight();

    public override bool CanExecute(MinEventTypes _eventType, MinEventParams _params)
    {
        var player = _params.Self;

        if (player == null || player.parts == null || (!player.parts.ContainsKey(headLightPropFPV) && !player.parts.ContainsKey(headLightPropTPV)))
        {
            return false;
        }

        return base.CanExecute(_eventType, _params);
    }


}