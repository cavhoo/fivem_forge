extern alias CFX;
using System;
using System.Globalization;
using CFX::CitizenFX.Core;
using CFX::CitizenFX.Core.UI;
using CFX::System.Drawing;

 using static CFX::CitizenFX.Core.Native.API;                  

namespace FiveMForgeClient.View.UI.Hud
{
  public class Speedometer : Base
  {
    private readonly Text _speedText;

    public Speedometer()
    {
      int x = 0, y = 0;
      GetActiveScreenResolution(ref x, ref y);
      _speedText = new Text("0 km/h", new PointF(300, 200), 0.5f);
    }

    public override void Update()
    {
      if (!Game.PlayerPed.IsInVehicle()) return;
      _speedText.Caption =
        $"{Math.Floor(Game.PlayerPed.CurrentVehicle.Speed * 3.6).ToString(CultureInfo.CurrentCulture)} km/h";
    }

    public override void Draw()
    {
      if (!Game.PlayerPed.IsInVehicle()) return;
      _speedText.Draw();
    }
  }
}