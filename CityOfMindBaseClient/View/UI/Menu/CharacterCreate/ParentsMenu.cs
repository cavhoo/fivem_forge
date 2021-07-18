using System;
using CitizenFX.Core;
using CitizenFX.Core.NaturalMotion;
using FiveMForgeClient.Models.Character;
using FiveMForgeClient.Services.Language;
using LemonUI.Menus;

namespace FiveMForgeClient.View.UI.Menu.CharacterCreate
{
  /// <summary>
  /// Event handler function for parents in character creator.
  /// </summary>
  public delegate void ParentsChangedHandler(object sender, ParentsChangedEventArgs args);

  /// <summary>
  /// Event args object that is created everytime a parent value has been changed.
  /// </summary>
  public class ParentsChangedEventArgs
  
  {
    /// <summary>
    /// Index of the Mom character that is used to resemble the player.
    /// </summary>
    public int Mom { get; set; }
    /// <summary>
    /// Index of the Dad character that is used to resemble the player.
    /// </summary>
    public int Dad { get; set; }
    /// <summary>
    /// Factor that describes how mixed the Mom and Dad are. Range from 0..1
    /// </summary>
    public float ResemblanceFactor { get; set; }
    /// <summary>
    /// Factor that describes how mixed the skin tone is between Mom and Dad. Range from 0..1
    /// </summary>
    public float SkinToneFactor { get; set; }

    internal ParentsChangedEventArgs(int mom, int dad, float resemblanceFactor, float skinToneFactor)
    {
      Mom = mom;
      Dad = dad;
      ResemblanceFactor = resemblanceFactor;
      SkinToneFactor = skinToneFactor;
    }
  }
  
  public class ParentsMenu : NativeMenu
  {
    public event ParentsChangedHandler ParentsChanged;

    private int CurrentMom;
    private int CurrentDad;
    private float ResemblanceFactor;
    private float SkinResemblanceFactor;
    
    public ParentsMenu(string title) : base(title)
    {
      Initialize();
    }

    private void Initialize()
    {
      CurrentDad = 1;
      CurrentMom = 1;
      ResemblanceFactor = 0.5f;
      SkinResemblanceFactor = 0.5f;
      var momListItem = new NativeListItem<string>(LanguageService.Translate("menu.character.creator.parents.mom"), CharacterComponents.InheritanceMoms);
      momListItem.ItemChanged += (sender, args) =>
      {
        CurrentMom = momListItem.SelectedIndex;
        ParentsChanged.Invoke(this, new ParentsChangedEventArgs(CurrentMom, CurrentDad, ResemblanceFactor, SkinResemblanceFactor));
      };
      var dadListItem = new NativeListItem<string>(LanguageService.Translate("menu.character.creator.parents.dad"), CharacterComponents.InheritanceDads);
      dadListItem.ItemChanged += (sender, args) =>
      {
        CurrentDad = dadListItem.SelectedIndex;
        ParentsChanged.Invoke(this, new ParentsChangedEventArgs(CurrentMom, CurrentDad, ResemblanceFactor, SkinResemblanceFactor));
      };

      var faceResemblance = new NativeSliderItem(LanguageService.Translate("menu.character.creator.parents.blend"));
      faceResemblance.Maximum = 10;
      faceResemblance.ValueChanged += (sender, args) =>
      {
        ResemblanceFactor = (float)faceResemblance.Value / faceResemblance.Maximum;
        Debug.WriteLine($"Resemblence Factor: {ResemblanceFactor}");
        ParentsChanged.Invoke(this, new ParentsChangedEventArgs(CurrentMom, CurrentDad, ResemblanceFactor, SkinResemblanceFactor));
      };

      var skinToneResemblance = new NativeSliderItem(LanguageService.Translate("menu.character.creator.parents.skin"));
      skinToneResemblance.Maximum = 10;
      skinToneResemblance.ValueChanged += (sender, args) =>
      {
        SkinResemblanceFactor = (float)skinToneResemblance.Value / skinToneResemblance.Maximum;
        Debug.WriteLine($"Skintone factor: {SkinResemblanceFactor}");
        ParentsChanged.Invoke(this, new ParentsChangedEventArgs(CurrentMom, CurrentDad, ResemblanceFactor, SkinResemblanceFactor));
      };

      var backButton = new NativeItem(LanguageService.Translate("menu.back"));
      
      Add(momListItem);
      Add(dadListItem);
      Add(faceResemblance);
      Add(skinToneResemblance);
      Add(backButton);
    }
  }
}