extern alias CFX;
using System;
using System.Collections.Generic;
using System.Drawing;
using CFX::CitizenFX.Core;
using FiveMForgeClient.Models.Character;
using NativeUI;

using static CFX::CitizenFX.Core.Native.API;

namespace FiveMForgeClient.View.CharacterCreation
{
    public class CharacterCreation : BaseScript
    {
        private Dictionary<string, decimal> _character;
        private MenuPool _pool;
        private UIMenu _main;
        private UIMenu _charMenu;
        private UIMenu _otherCharMenu;

        private UIMenu _herritageMenu;
        private UIMenu _faceShapeMenu;
        private UIMenu _appearanceMenu;
        private UIMenu _clothesMenu;

        public CharacterCreation()
        {
            _character = new Dictionary<string, decimal>();
            _pool = new MenuPool();
            _main = new UIMenu("Character Creator", "~b~Create your Character.");
            _charMenu = new UIMenu("Character Creator", "~b~Create your Character.");
            _otherCharMenu = new UIMenu("Other stuff", "~b~Other things");

            _pool.Add(_main);
            _pool.Add(_charMenu);
            _pool.Add(_otherCharMenu);

            _herritageMenu = _pool.AddSubMenu(_charMenu, "Herritage", "~b~Choose your heritage", new PointF(5, 5));
            _faceShapeMenu = _pool.AddSubMenu(_charMenu, "Face Shape", "~b~Make it pretty", new PointF(5, 5));
            _appearanceMenu = _pool.AddSubMenu(_charMenu, "Appearance", "~b~Pick your looks", new PointF(5, 5));
            _clothesMenu = _pool.AddSubMenu(_charMenu, "Clothes", "~b~Put some clothes on", new PointF(5, 5));
            Initialize();
        }

        private void Initialize()
        {
            InitHerritageMenu();
        }

        private void InitHerritageMenu()
        {
            var cMum = 1;
            var cDad = 1;
            var shapeMixValue = 50;
            var skinMixValue = 50;
            var mum = new List<dynamic>()
            {
                "Hannah", "Aubrey", "Jasmine", "Gisele", "Amelia", "Isabella", "Zoe", "Ava", "Camila", "Violet",
                "Sophia", "Evelyn", "Nicole", "Ashley", "Gracie", "Brianna", "Natalie", "Olivia", "Elizabeth",
                "Charlotte", "Emma"
            };

            var dad = new List<dynamic>
            {
                "Benjamin", "Daniel", "Joshua", "Noah", "Andrew", "Juan", "Alex", "Isaac", "Evan", "Ethan", "Vincent",
                "Angel", "Diego", "Adrian", "Gabriel", "Michael", "Santiago", "Kevin", "Louis", "Samuel", "Anthony",
                " Claude", "Niko"
            };

            var heritageWindow = new UIMenuHeritageWindow(cMum, cDad);
            _herritageMenu.AddWindow(heritageWindow);
            var mumItems = new UIMenuListItem("Mom", mum, 1, "");
            var dadItems = new UIMenuListItem("Dad", dad, 1, "");
            var amount = new[] {0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1.0};
            var descShape =
                $"Set how much of your head shape should be inherited from your father or mother. All the way to the left means 100% father, to the right means 100% mother.";
            var descSkinTone = $"Set how much your skintone shall be based on your parents. All the way to the left means 100% father, to the right means 100% mother.";

            var shapeMixer = new UIMenuSliderHeritageItem("Ressemblence", descShape, true);
            shapeMixer.Maximum = 100;
            shapeMixer.Value = 50;
            var skinMixer = new UIMenuSliderHeritageItem("Skintone", descSkinTone, true);
            shapeMixer.Maximum = 100;
            shapeMixer.Value = 50;
            _herritageMenu.AddItem(mumItems);
            _herritageMenu.AddItem(dadItems);
            _herritageMenu.AddItem(shapeMixer);
            _herritageMenu.AddItem(skinMixer);
            _herritageMenu.OnListChange += (sender, item, index) =>
            {
                if (item == mumItems)
                {
                    cMum = index;
                }
                else
                {
                    cDad = index;
                }
                heritageWindow.Index(cMum, cDad);
                _character["mom"] = cMum;
                _character["dad"] = cDad;
                SetPedHeadBlendData(GetPlayerPed(-1), cDad, cMum, -1, cDad, cMum, -1, shapeMixValue, skinMixValue,
                    -1, true);
            };

            _herritageMenu.OnSliderChange += (sender, item, index) =>
            {
                if (item == shapeMixer)
                {
                    shapeMixValue = item.Value;
                }
                else
                {
                    skinMixValue = item.Value;
                }

                _character["face"] = shapeMixValue;
                _character["skin"] = skinMixValue;
                SetPedHeadBlendData(GetPlayerPed(-1), cDad, cMum, -1, cDad, cMum, -1, shapeMixValue, skinMixValue,
                    -1, true);
            };
        }

        private void InitFaceShapeMenu()
        {
            var listItems = new List<dynamic>()
            {
                "Custom",
                "Set 1 come soon",
                "Set 2 come soon"
            };
            var noseGrid = new UIMenuListItem("Nose", listItems, 1);
            var noseGridPanel = new UIMenuGridPanel($"Up", "Narrow", "Wide", "Down", PointF.Empty);
            _faceShapeMenu.AddItem(noseGrid);
            noseGrid.AddPanel(noseGridPanel);
            noseGrid.Activated += (_, _) =>
            {
                SetPedFaceFeature(GetPlayerPed(-1), 0, noseGridPanel.CirclePosition.X);
                SetPedFaceFeature(GetPlayerPed(-1), 1, noseGridPanel.CirclePosition.Y);
            };

            var noseProfile = new UIMenuListItem("Nose Profile", listItems, 1);
            var noseProfilePanel = new UIMenuGridPanel("Crooked", "Short", "Long", "Curved", PointF.Empty);
            noseProfile.Activated += (_, _) =>
            {
                SetPedFaceFeature(GetPlayerPed(-1), 2, noseProfilePanel.CirclePosition.X);
                SetPedFaceFeature(GetPlayerPed(-1), 3, noseProfilePanel.CirclePosition.Y);
            };
        }

        private void InitAppearanceMenu()
        {
        }

        private void InitClotheMenu()
        {
        }
    }
}