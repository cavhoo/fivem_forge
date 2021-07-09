extern alias CFX;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Remoting.Channels;
using CFX::CitizenFX.Core;
using FiveMForgeClient.Controller.Language;
using FiveMForgeClient.Models.Character;
using NativeUI;
using static CFX::CitizenFX.Core.Native.API;

namespace FiveMForgeClient.View.UI.Menu
{
  public class CharacterCreator : UIMenu
  {
    private MenuPool Pool;
    private UIMenu Inheritance;
    private UIMenu Face;
    private UIMenu Appearance;
    private UIMenu Clothes;

    public CharacterCreator(string title, string subtitle, bool glare = false) : base(title, subtitle, glare)
    {
      Visible = false;
    }

    public void SetMenuPool(ref MenuPool pool)
    {
      if (Pool != null)
      {
        Debug.WriteLine("Can only register one MenuPool for character selector.");
      }

      Pool = pool;
      Initialize();
    }

    private void Initialize()
    {
      Inheritance = Pool.AddSubMenu(this, LanguageController.Translate("herritage"),
        LanguageController.Translate("choose_herritage"));
      CreateHeritageMenu();

      Face = Pool.AddSubMenu(this, LanguageController.Translate("face"), LanguageController.Translate("face_choose"));
      CreateFaceShapeMenu();
      Appearance = Pool.AddSubMenu(this, LanguageController.Translate("appearance"),
        LanguageController.Translate("appearance_choose"));
      Clothes = Pool.AddSubMenu(this, LanguageController.Translate("clothes"),
        LanguageController.Translate("clothes_choose"));
    }

    private void CreateHeritageMenu()
    {
      var mom = 1;
      var dad = 1;

      var skinMix = 0.5f;
      var shapeMix = 0.5f;

      var heritageWindow = new UIMenuHeritageWindow(mom, dad);
      Inheritance.AddWindow(heritageWindow);

      var itemMom = new UIMenuListItem(LanguageController.Translate("mom"), CharacterComponents.InheritanceMoms, 1, "");
      Inheritance.AddItem(itemMom);
      var itemDad = new UIMenuListItem(LanguageController.Translate("dad"), CharacterComponents.InheritanceDads, 1, "");
      Inheritance.AddItem(itemDad);


      var shapeMixSlider = new UIMenuSliderHeritageItem(LanguageController.Translate("ressemblence"),
        LanguageController.Translate("resemblence_description"), true);


      // Sliders have a default range of 100, to make sure that the mixing value is between 0..1 we have to divide by
      // the max value of the slider.
      shapeMixSlider.OnSliderChanged += (sender, index) =>
      {
        shapeMix = (float) index / shapeMixSlider.Maximum;
        SetPedHeadBlendData(1, dad, mom, -1, dad, mom, -1, shapeMix, skinMix, -1, true);
      };

      var skinToneSlider = new UIMenuSliderHeritageItem(LanguageController.Translate("skintone"),
        LanguageController.Translate("skintone_description"), true);

      skinToneSlider.OnSliderChanged += (sender, index) =>
      {
        skinMix = (float) index / skinToneSlider.Maximum;
        SetPedHeadBlendData(1, dad, mom, -1, dad, mom, -1, shapeMix, skinMix, -1, true);
      };

      Inheritance.OnListChange += (_, listItem, newIndex) =>
      {
        if (listItem == itemMom)
        {
          mom = newIndex;
        }

        if (listItem == itemDad)
        {
          dad = newIndex;
        }

        heritageWindow.Index(mom, dad);

        // Blending Mom and Dad to create a Kevin
        SetPedBlendFromParents(1, dad, mom, shapeMix, shapeMix);
      };

      Inheritance.OnSliderChange += (sender, item, index) =>
      {
        if (item == shapeMixSlider)
        {
        }
      };
    }

    private void CreateFaceShapeMenu()
    {
      var noseShape = new UIMenuListItem(LanguageController.Translate("nose"), new List<dynamic>(){"default"}, 1);
      var noseGridPanel = new UIMenuGridPanel(LanguageController.Translate("up"),
        LanguageController.Translate("narrow"), LanguageController.Translate("wide"),
        LanguageController.Translate("down"), new PointF(0.5f, 0.5f));
      
      noseShape.Activated += (sender, item) =>
      {
        // Set Nose basic features onto current model.
        SetPedFaceFeature(1, 0, noseGridPanel.CirclePosition.X);
        SetPedFaceFeature(1, 1, noseGridPanel.CirclePosition.Y);
      };
      noseShape.AddPanel(noseGridPanel);
      Face.AddItem(noseShape);


      var noseProfile = new UIMenuListItem(LanguageController.Translate("nose_profile"), new List<dynamic>(){"default"}, 1);
      var noseProfileGridPanel = new UIMenuGridPanel(LanguageController.Translate("nose_crooked"),
        LanguageController.Translate("nose_short"), LanguageController.Translate("nose_long"),
        LanguageController.Translate("nose_curved"), new PointF(0.5f, 0.5f));
      noseProfile.Activated += (sender, item) =>
      {
        SetPedFaceFeature(1, 2, noseProfileGridPanel.CirclePosition.X);
        SetPedFaceFeature(1, 3, noseProfileGridPanel.CirclePosition.Y);
      };
      noseProfile.AddPanel(noseProfileGridPanel);
      Face.AddItem(noseProfile);
      
      
      var noseTip = new UIMenuListItem(LanguageController.Translate("nose_tip"), new List<dynamic>(){"default"}, 1);
      var noseTipGridPanel = new UIMenuGridPanel(LanguageController.Translate("tip_up"),
        LanguageController.Translate("tip_lef"), LanguageController.Translate("tip_right"),
        LanguageController.Translate("down"), new PointF(0.5f, 0.5f));
      noseTip.Activated += (sender, item) =>
      {
        SetPedFaceFeature(1, 4, noseTipGridPanel.CirclePosition.X);
        SetPedFaceFeature(1, 5, noseTipGridPanel.CirclePosition.Y);
      };
      noseTip.AddPanel(noseTipGridPanel);
      Face.AddItem(noseTip);
    }
  }
}