using System;
using System.Collections.Generic;
using System.Linq;
using CitizenFX.Core;
using CityOfMindClient.View.UI.Menu.CharacterCreate.Menus;
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
    public float LipThickness { get; set; }
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
    private FaceChangedEventArgs CurrentFaceShape;

    public FaceMenu(string title, ref ObjectPool pool) : base(title)
    {
      Pool = pool;
      CurrentFaceShape = new FaceChangedEventArgs();
      Initialize();
    }

    private void Initialize()
    {
      FaceTemplateLists = new List<NativeListItem<dynamic>>();
      var subMenuEye = new EyeMenu(LanguageService.Translate("menu.character.creator.face.eye.title"));
      Pool.Add(subMenuEye);
      subMenuEye.EyesChanged += (sender, args) =>
      {
        CurrentFaceShape.EyeColor = args.EyeColor;
        CurrentFaceShape.EyeOpening = args.EyeOpening;
        CurrentFaceShape.EyeBrowBulkiness = args.EyeBrowBulkiness;
        CurrentFaceShape.EyeBrowHeight = args.EyeBrowHeight;
        OnFaceValuesChanged();
      };
      var subMenuNose = new NoseMenu(LanguageService.Translate("menu.character.creator.face.nose.title"));
      Pool.Add(subMenuNose);
      subMenuNose.NoseChanged += (sender, args) =>
      {
        Debug.WriteLine($"Updating Nose Data {args.NoseTipLength}");
        CurrentFaceShape.NoseWidth = args.NoseWidth;
        CurrentFaceShape.NoseBoneBend = args.NoseBoneBend;
        CurrentFaceShape.NoseBoneOffset = args.NoseBoneOffset;
        CurrentFaceShape.NoseTipHeight = args.NoseTipHeight;
        CurrentFaceShape.NoseTipLength = args.NoseTipLength;
        CurrentFaceShape.NoseTipLowering = args.NoseTipLowering;
        OnFaceValuesChanged();
      };
      var subMenuLips = new LipsMenu(LanguageService.Translate("menu.character.creator.face.lips.title"));
      Pool.Add(subMenuLips);
      subMenuLips.LipsChanged += (sender, args) =>
      {
        CurrentFaceShape.LipThickness = args.Thickness;
        OnFaceValuesChanged();
      };
      
      var subMenuCheeks = new CheekMenu(LanguageService.Translate("menu.character.creator.face.cheeks.title"));
      Pool.Add(subMenuCheeks);
      subMenuCheeks.CheekChanged += (sender, args) =>
      {
        CurrentFaceShape.CheekWidth = args.CheekWidth;
        CurrentFaceShape.CheekBoneHeight = args.CheekBoneHeight;
        CurrentFaceShape.CheekBoneWidth = args.CheekBoneWidth;
        OnFaceValuesChanged();
      };
      var subMenuChin = new ChinMenu(LanguageService.Translate("menu.character.creator.face.chin.title"));
      Pool.Add(subMenuChin);
      subMenuChin.ChinChanged += (sender, args) =>
      {
        CurrentFaceShape.ChinWidth = args.ChinWidth;
        CurrentFaceShape.ChinGapSize = args.ChinGapSize;
        CurrentFaceShape.ChinForward = args.ChinForward;
        CurrentFaceShape.ChinHeight = args.ChinHeight;
        OnFaceValuesChanged();
      };

      // Add all menus to the face menu
      var subEye = AddSubMenu(subMenuEye);
      subEye.Title = LanguageService.Translate("menu.character.creator.face.eye.title");
      var subNose = AddSubMenu(subMenuNose);
      subNose.Title = LanguageService.Translate("menu.character.creator.face.nose.title");
      var subLips = AddSubMenu(subMenuLips);
      subLips.Title = LanguageService.Translate("menu.character.creator.face.lips.title");
      var subCheeks = AddSubMenu(subMenuCheeks);
      subCheeks.Title = LanguageService.Translate("menu.character.creator.face.cheeks.title");
      var subChin = AddSubMenu(subMenuChin);
      subChin.Title = LanguageService.Translate("menu.character.creator.face.chin.title");

      var backButton = new NativeItem(LanguageService.Translate("menu.back"));
      backButton.Selected += (sender, args) => { Close(); };
      OnFaceValuesChanged();
    }

    private void OnFaceValuesChanged()
    {
      Debug.WriteLine("Face values updated...");
      FaceChanged?.Invoke(this, CurrentFaceShape);
    }
  }
}