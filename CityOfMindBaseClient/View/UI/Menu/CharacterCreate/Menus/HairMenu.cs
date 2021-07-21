extern alias CFX;
using System.Collections.Generic;
using System.Drawing;
using CFX::CitizenFX.Core;
using FiveMForgeClient.Services.Language;
using FiveMForgeClient.View.UI.Hud;
using LemonUI.Menus;
using static CFX::CitizenFX.Core.Native.API;

namespace CityOfMindClient.View.UI.Menu.CharacterCreate.Menus
{
  public delegate void HairChangedEventHandler(object sender, HairChangedEventArgs args);

  public class HairChangedEventArgs
  {
    public int BaseColor { get;}
    public int HighlightColor { get; }
    public int HairShape { get; }
    internal HairChangedEventArgs(int baseColor, int highlightColor, int hairShape)
    {
      BaseColor = baseColor;
      HighlightColor = highlightColor;
      HairShape = hairShape;
    }
  }
  public class HairMenu : NativeMenu
  {
    public event HairChangedEventHandler HairChanged;
    private NativeListItem<string> HairShape;
    private NativeColorPanel HairColor;
    
    public HairMenu(string title) : base(title)
    {
      Initialize();
    }

    private void Initialize()
    {
      var availableColors = GetHairColors();
      HairShape = new NativeListItem<string>(LanguageService.Translate("menu.character.creator.hair.shape"),
        new[] {"Hair"});

      var hairColorItem = new NativeItem(LanguageService.Translate("menu.character.creator.hair.color.choose"));
      HairColor = new NativeColorPanel(LanguageService.Translate("menu.character.creator.hair.color"), availableColors.ToArray());
      hairColorItem.Panel = HairColor;
      Add(HairShape);
      Add(hairColorItem);
      OnHairChanged();
    }

    private List<NativeColorData> GetHairColors()
    {
      var hairColors = new List<NativeColorData>();
      var hairColorCount = GetNumHairColors();
      for (var i = 0; i < hairColorCount; i++)
      {
        var r = 0;
        var g = 0;
        var b = 0;
        GetHairRgbColor(i,  ref r, ref g, ref b);
        hairColors.Add(new NativeColorData($"Color{i}", Color.FromArgb(r, g, b)));
      }
      return hairColors;
    }

    private void OnHairChanged()
    {
      HairChanged?.Invoke(this, new HairChangedEventArgs(HairColor.SelectedIndex, HairColor.SelectedIndex, HairShape.SelectedIndex));
    }
  }
}