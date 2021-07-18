using System;
using System.Collections.Generic;
using System.Linq;
using CitizenFX.Core;
using FiveMForgeClient.Models.Character;
using FiveMForgeClient.Services.Language;
using LemonUI;
using LemonUI.Menus;

namespace FiveMForgeClient.View.UI.Menu.CharacterCreate
{
  public delegate void FaceChangedEventHandler(object sender, FaceChangedEventArgs args);

  public class FaceChangedEventArgs
  {
    public float CheekBoneWidth { get; set; }
    public float CheekBoneHeight { get; set; }
    public float CheekWidth { get; set; }
    public float ChinWidth { get; set; }
    public float ChinForward { get; set; }
    public float ChinHeight { get; set; }
    public float ChinGapSize { get; set; }
    public int EyeColor { get; set; }
    public float EyeBrowHeight { get; set; }
    public float EyeBrowBulkiness { get; set; }
    public float EyeOpening { get; set; }
    public float Thickness { get; set; }
    public float NoseWidth { get; set; }
    public float NoseTipLength { get; set; }
    public float NoseTipHeight { get; set; }
    public float NoseTipLowering { get; set; }
    public float NoseBoneBend { get; set; }
    public float NoseBoneOffset { get; set; }

    internal FaceChangedEventArgs()
    {
    }
  }


  public class FaceMenu : NativeMenu
  {
    public event FaceChangedEventHandler FaceChanged;

    private int MaxSliderValue = 10;
    private List<NativeListItem<dynamic>> FaceTemplateLists;
    private ObjectPool Pool;

    public FaceMenu(string title, ref ObjectPool pool) : base(title)
    {
      Initialize();
      Pool = pool;
    }

    private void Initialize()
    {
      FaceTemplateLists = new List<NativeListItem<dynamic>>();
      var subMenuEye = new NativeMenu(LanguageService.Translate("menu.character.creator.face.eye.title"));
      var subMenuNose = new NativeMenu(LanguageService.Translate("menu.character.creator.face.nose.title"));
      var subMenuLips = new NativeMenu(LanguageService.Translate("menu.character.creator.face.lips.title"));
      var subMenuCheeks = new NativeMenu(LanguageService.Translate("menu.character.creator.face.cheeks.title"));
      var subMenuChin = new NativeMenu(LanguageService.Translate("menu.character.creator.face.chin.title"));


      // Add all menus to the face menu
      AddSubMenu(subMenuEye);
      AddSubMenu(subMenuNose);
      AddSubMenu(subMenuLips);
      AddSubMenu(subMenuCheeks);
      AddSubMenu(subMenuChin);

      var backButton = new NativeItem("Back");
      backButton.Selected += (sender, args) => { Close(); };
    }
  }
}