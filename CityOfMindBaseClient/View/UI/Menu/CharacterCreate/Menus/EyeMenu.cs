﻿using System.ComponentModel;
using FiveMForgeClient.Services.Language;
using LemonUI.Menus;

namespace FiveMForgeClient.View.UI.Menu.CharacterCreate
{
  public delegate void EyesChangedEventHandler(object sender, EyeMenuChangedEventArgs args);
  
  public class EyeMenuChangedEventArgs
  {
    public int EyeColor { get; private set; }
    public float EyeBrowHeight { get; private set; }
    public float EyeBrowBulkiness { get; private set; }
    public float EyeOpening { get; private set; }

    internal EyeMenuChangedEventArgs(int eyeColor, float eyeBrowHeight, float eyeBrowBulkiness, float eyeOpening)
    {
      EyeColor = eyeColor;
      EyeOpening = eyeOpening;
      EyeBrowBulkiness = eyeBrowBulkiness;
      EyeBrowHeight = eyeBrowHeight;
    }
  }
  
  public class EyeMenu : NativeMenu
  {
    public event EyesChangedEventHandler EyesChanged;

    private NativeListItem<int> EyeColorList;
    private NativeSliderItem EyeBrowHeight;
    private NativeSliderItem EyeBulkiness;
    private NativeSliderItem EyeOpening;
    
    public EyeMenu(string title) : base(title)
    {
      Initialize();
    }

    private void Initialize()
    {
      // Eye Color
      EyeColorList = new NativeListItem<int>(LanguageService.Translate("menu.character.creator.face.eye.color"));
      EyeColorList.ItemChanged += (sender, itemIndex) => {
        OnEyeValuesChanged();
      };
      
      // Eye brow height
      EyeBrowHeight = new NativeSliderItem(LanguageService.Translate("menu.character.creator.face.eye.brow.height"), 50, 0);
      
      // Eye brow bulkiness
      EyeBulkiness = new NativeSliderItem(LanguageService.Translate("menu.character.creator.face.eye.brow.bulkiness"), 50, 0);
      
      // Eye opening
      EyeOpening = new NativeSliderItem(LanguageService.Translate("menu.character.creator.face.eye.opening"), 50, 0);
      

      Add(EyeColorList);
      Add(EyeBrowHeight);
      Add(EyeBulkiness);
      Add(EyeOpening);
    }

    private void OnEyeValuesChanged()
    {
      // Convert our range to the actual allowed values of -1.0 to 1.0.
      var eyeBrowHeightValue = (EyeBrowHeight.Value - EyeBrowHeight.Maximum / 2) / (EyeBrowHeight.Maximum / 2);
      var eyeBulkinessValue = (EyeBulkiness.Value - EyeBulkiness.Maximum / 2) / (EyeBulkiness.Maximum / 2);
      var eyeOpeningValue = (EyeOpening.Value - EyeOpening.Maximum / 2) / (EyeOpening.Maximum / 2);

      // Fire Event to let parent menu know about changes.
      EyesChanged?.Invoke(this, new EyeMenuChangedEventArgs(EyeColorList.SelectedIndex, eyeBrowHeightValue, eyeBulkinessValue, eyeOpeningValue));
    }
  }
}
