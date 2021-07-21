extern alias CFX;
using System.Collections.Generic;
using System.ComponentModel;
using CFX::CitizenFX.Core;
using System.Drawing;
using CityOfMindClient.Models.Character;
using FiveMForgeClient.Services.Language;
using LemonUI.Menus;
using static CFX::CitizenFX.Core.Native.API;

namespace CityOfMindClient.View.UI.Menu.CharacterCreate.Menus
{
  public delegate void MakeUpChangedEventHandler(object sender, MakeUpChangedEventArgs args);

  public class MakeUpChangedEventArgs
  {
    public int MakeUpVariant { get; }
    public int MakeUpColor { get; }
    public int LipstickVariant { get; }
    public int LipstickColor { get; }
    public int BlushVariant { get; }
    public int BlushColor { get; }

    internal MakeUpChangedEventArgs(int makeUpVariant, int lipstickVariant, int blushVariant, int lipstickColor,
      int makeupColor, int blushColor)
    {
      MakeUpVariant = makeUpVariant;
      MakeUpColor = makeupColor;
      LipstickVariant = lipstickVariant;
      LipstickColor = lipstickColor;
      BlushVariant = blushVariant;
      BlushColor = blushColor;
    }
  }

  public class MakeUpMenu : NativeMenu
  {
    public event MakeUpChangedEventHandler MakeupChanged;
    private NativeColorPanel MakeUpColors;
    private NativeColorPanel LipstickColor;
    private NativeColorPanel BlushColor;
    private NativeListItem<string> MakeUpVariants;
    private NativeListItem<string> LipStickVariant;
    private NativeListItem<string> BlushVariant;

    public MakeUpMenu(string title) : base(title)
    {
      Initialize();
    }

    private void Initialize()
    {
      var availableColors = GetMakeUpColors();
      // Makeup 
      MakeUpColors = new NativeColorPanel(LanguageService.Translate("menu.character.creator.face.makeup.colors"),
        availableColors.ToArray());
      MakeUpVariants =
        new NativeListItem<string>(LanguageService.Translate("menu.character.creator.face.makeup.variant"),
          CharacterComponents.MakeUp);
      MakeUpVariants.ItemChanged += (sender, args) => OnMakeUpChanged();
      MakeUpVariants.Panel = MakeUpColors;
      Add(MakeUpVariants);

      // LipStick
      LipstickColor = new NativeColorPanel(LanguageService.Translate("menu.character.creator.face.makeup.colors"),
        availableColors.ToArray());
      LipStickVariant =
        new NativeListItem<string>(LanguageService.Translate("menu.character.creator.face.makeup.lipstick"),
          CharacterComponents.LipStick);
      LipStickVariant.ItemChanged += (sender, args) => OnMakeUpChanged();
      LipStickVariant.Panel = LipstickColor;
      Add(LipStickVariant);

      // Blush
      BlushVariant = new NativeListItem<string>(LanguageService.Translate("menu.character.creator.face.makeup.blush"),
        CharacterComponents.Blush);
      BlushColor = new NativeColorPanel(LanguageService.Translate("menu.character.creator.face.makeup.colors"),
        availableColors.ToArray());
      BlushVariant.ItemChanged += (sender, args) => OnMakeUpChanged();
      BlushVariant.Panel = BlushColor;
      Add(BlushVariant);
      
      OnMakeUpChanged();
    }

    private List<NativeColorData> GetMakeUpColors()
    {
      var makeUpColorCount = GetNumMakeupColors();
      var makeUpColors = new List<NativeColorData>();

      for (var i = 0; i < makeUpColorCount; i++)
      {
        var r = 0;
        var g = 0;
        var b = 0;

        GetMakeupRgbColor(i, ref r, ref g, ref b);
        makeUpColors.Add(new NativeColorData($"Color{i}", Color.FromArgb(r, g, b)));
      }

      return makeUpColors;
    }

    private void OnMakeUpChanged()
    {
      MakeupChanged?.Invoke(this,
        new MakeUpChangedEventArgs(MakeUpVariants.SelectedIndex, LipStickVariant.SelectedIndex,
          BlushVariant.SelectedIndex, LipstickColor.SelectedIndex, MakeUpColors.SelectedIndex,
          BlushColor.SelectedIndex));
    }
  }
}