using System.Collections.Generic;

public class ConsoleCmdDebugCaveLight : ConsoleCmdAbstract
{
    public override string[] getCommands()
    {
        return new string[] { "cl", "cavelight" };
    }

    public override string getDescription()
    {
        return "Helpers to debug cave lights";
    }

    public override void Execute(List<string> _params, CommandSenderInfo _senderInfo)
    {
        if (_params.Count == 0)
        {
            Log.Out(getDescription());
            return;
        }

        switch (_params[0].ToLower())
        {
            case "torch":
                TorchScaleCommand(_params);
                break;

            case "moon":
                MoonScaleCommand(_params);
                break;

            default:
                Logging.Error($"Invalid or not implemented command: '{_params[0]}'");
                break;
        }
    }

    private void TorchScaleCommand(List<string> _params)
    {
        if (_params.Count == 0)
        {
            Log.Error($"[Cave] Missing argument: 'scale' (float)");
            return;
        }

        if (!float.TryParse(_params[1], out var scale))
        {
            Log.Error($"[Cave] Invalid argument: '{_params[1]}'");
            return;
        }

        ItemClass.GetItemClass("meleeToolTorch").Properties.SetValue("MaxIntensity", scale.ToString());
    }

    private static void MoonScaleCommand(List<string> _params)
    {
        if (_params.Count == 0)
        {
            Logging.Error($"Missing argument: 'scale' (float)");
            return;
        }

        if (!float.TryParse(_params[1], out var scale))
        {
            Logging.Error($"Invalid argument: '{_params[1]}'");
            return;
        }

        CaveLightConfig.moonLightScale = scale;
    }

}