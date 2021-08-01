extern alias CFX;

using System;
using System.Drawing;
using System.Threading.Tasks;
using CFX::CitizenFX.Core;
using CFX::CitizenFX.Core.UI;
using PointF = CFX::System.Drawing.PointF;

namespace Speedometer
{
  public class Speedometer : BaseScript
  {
    private Text _speed;
    public Speedometer()
    {
      EventHandlers["onClientResourceStart"] += new Action<string>(Initialize);
    }

    private void Initialize(string resName)
    {
      Debug.WriteLine("Starting speedometer");
      _speed = new Text("0 km/h", new PointF(100.0f, 100.0f), 1.0f);
      Tick += DrawMeter;
    }

    private async Task DrawMeter()
    {
      if (Game.PlayerPed.IsInVehicle())
      {
        _speed.Caption = $"{Game.PlayerPed.CurrentVehicle.Speed * 3.6} km/h";
        _speed.Draw();
      }
    }
  }
}