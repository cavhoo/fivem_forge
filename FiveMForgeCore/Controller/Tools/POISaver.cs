using System;
using System.Drawing.Drawing2D;
using CitizenFX.Core;
using CitizenFX.Core.Native;
using FiveMForge.Controller.Base;
using FiveMForge.Database;
using FiveMForge.Database.Contexts;
using FiveMForge.Database.Models;
using FiveMForge.Models;
using MySqlConnector;
using Player = CitizenFX.Core.Player;

namespace FiveMForge.Controller.Tools
{
    public class PoiSaver : BaseClass
    {
        public PoiSaver()
        {
            EventHandlers[ServerEvents.SavePOIPosition] += new Action<Player, string>(OnSavePOIPosition);
        }

        private async void OnSavePOIPosition([FromSource] Player player, string type)
        {
            if (type == "Unkown") return;

            var currentPosition = player.Character?.Position ?? Vector3.Zero;
            using var ctx = new CoreContext();
            var pointOfInterest = new Poi();
            pointOfInterest.X = currentPosition.X;
            pointOfInterest.Y = currentPosition.Y;
            pointOfInterest.Z = currentPosition.Z;
            pointOfInterest.Type = type;

            var poiAlreadyExists = ctx.PoiExists(pointOfInterest);
            if (!poiAlreadyExists)
            {
                ctx.Poi.Add(pointOfInterest);
                await ctx.SaveChangesAsync();
                player.TriggerEvent("FiveMForge:POISaved", "Point of Interest saved to Database.");
            }
            else
            {
                player.TriggerEvent("FiveMForge:POIExists", "Point of interest already exists.");
            }
        }
    }
}