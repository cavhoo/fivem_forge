using System;
using System.Collections.Generic;
using System.Linq;
using CitizenFX.Core;
using FiveMForgeClient.Models.Character;
using FiveMForgeClient.Services.Language;
using LemonUI.Menus;

namespace FiveMForgeClient.View.UI.Menu.CharacterCreate
{
  public delegate void FaceChangedEventHandler(object sender, FaceChangedEventArgs args);

  public class FaceChangedEventArgs
  {
    public EyeTemplate Eyes { get; set; }
    public CheekTemplate Cheeks { get; set; }
    public NoseTemplate Nose { get; set; }
    public LipTemplate Lips { get; set; }
    public ChinTemplate Chin { get; set; }

    internal FaceChangedEventArgs(EyeTemplate eyes, CheekTemplate cheeks, NoseTemplate nose, LipTemplate lips,
      ChinTemplate chin)
    {
      Eyes = eyes;
      Cheeks = cheeks;
      Nose = nose;
      Lips = lips;
      Chin = chin;
    }
  }


  public class FaceMenu : NativeMenu
  {
    public event FaceChangedEventHandler FaceChanged;

    private int MaxSliderValue = 10;
    private List<NativeListItem<dynamic>> FaceTemplateLists;

    public FaceMenu(string title) : base(title)
    {
      Initialize();
    }

    private void Initialize()
    {
      FaceTemplateLists = new List<NativeListItem<dynamic>>();

      var eyeShapeList = new NativeListItem<EyeTemplate>("Eye Shape", CharacterFaceComponents.EyeTemplates);
      var noseShapeList = new NativeListItem<NoseTemplate>("Nose Shape", CharacterFaceComponents.NoseTemplates);
      var lipShapeList = new NativeListItem<LipTemplate>("Lip Shape", CharacterFaceComponents.LipTemplates);
      var cheekShapeList = new NativeListItem<CheekTemplate>("Cheek Shape", CharacterFaceComponents.CheekTemplates);
      var chinShapeList = new NativeListItem<ChinTemplate>("Chin Shape", CharacterFaceComponents.ChinTemplates);
      var backButton = new NativeItem("Back");
      backButton.Selected += (sender, args) =>
      {
        Close();
      };

      eyeShapeList.ItemChanged += (sender, args) =>
      {
        FaceChanged?.Invoke(this,
          new FaceChangedEventArgs(eyeShapeList.SelectedItem, cheekShapeList.SelectedItem, noseShapeList.SelectedItem,
            lipShapeList.SelectedItem, chinShapeList.SelectedItem));
      };

      noseShapeList.ItemChanged += (sender, args) =>
      {
        FaceChanged?.Invoke(this,
          new FaceChangedEventArgs(eyeShapeList.SelectedItem, cheekShapeList.SelectedItem, noseShapeList.SelectedItem,
            lipShapeList.SelectedItem, chinShapeList.SelectedItem));
      };

      lipShapeList.ItemChanged += (sender, args) =>
      {
        FaceChanged?.Invoke(this,
          new FaceChangedEventArgs(eyeShapeList.SelectedItem, cheekShapeList.SelectedItem, noseShapeList.SelectedItem,
            lipShapeList.SelectedItem, chinShapeList.SelectedItem));
      };

      cheekShapeList.ItemChanged += (sender, args) =>
      {
        FaceChanged?.Invoke(this,
          new FaceChangedEventArgs(eyeShapeList.SelectedItem, cheekShapeList.SelectedItem, noseShapeList.SelectedItem,
            lipShapeList.SelectedItem, chinShapeList.SelectedItem));
      };

      chinShapeList.ItemChanged += (sender, args) =>
      {
        FaceChanged?.Invoke(this,
          new FaceChangedEventArgs(eyeShapeList.SelectedItem, cheekShapeList.SelectedItem, noseShapeList.SelectedItem,
            lipShapeList.SelectedItem, chinShapeList.SelectedItem));
      };
    }
  }
}