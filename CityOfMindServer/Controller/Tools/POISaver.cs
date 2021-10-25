using System;
using System.Drawing.Drawing2D;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FiveMForge.Controller.Base;
using FiveMForge.Models;
using Player = CitizenFX.Core.Player;

namespace FiveMForge.Controller.Tools
{
  public class PoiSaver : BaseClass
  {
    public PoiSaver()
    {
      EventHandlers[ServerEvents.SavePoiPosition] += new Action<Player, string>(OnSavePOIPosition);
    }

    private async void OnSavePOIPosition([FromSource] Player player, string type)
    {
      if (type == "Unkown") return;

      var currentPosition = player.Character?.Position ?? Vector3.Zero;
      var pointOfInterest = new Poi();
      pointOfInterest.X = currentPosition.X;
      pointOfInterest.Y = currentPosition.Y;
      pointOfInterest.Z = currentPosition.Z;
      pointOfInterest.Type = type;

      Context.Poi.Add(pointOfInterest);
      await Context.SaveChangesAsync();
      player.TriggerEvent("FiveMForge:POISaved", "Point of Interest saved to Database.");
    }
  }
}