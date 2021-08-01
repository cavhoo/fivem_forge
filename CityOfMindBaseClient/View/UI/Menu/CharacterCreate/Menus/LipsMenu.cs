using CitizenFX.Core;
using FiveMForgeClient.Services.Language;
using LemonUI.Menus;

namespace CityOfMindClient.View.UI.Menu.CharacterCreate.Menus
{
  public delegate void LipsChangedEventHandler(object sender, LipsChangedEventArgs args);

  public class LipsChangedEventArgs
  {
    public float Thickness { get; }

    internal LipsChangedEventArgs(float thickness)
    {
      Thickness = thickness;
    }
  }


  public class LipsMenu : NativeMenu
  {
    public event LipsChangedEventHandler LipsChanged;
    private NativeSliderItem _lipThickness;

    public LipsMenu(string title) : base(title)
    {
      Initialize();
    }

    private void Initialize()
    {
      _lipThickness =
        new NativeSliderItem(LanguageService.Translate("menu.character.creator.face.lip.thickness"), 50, 25);
      _lipThickness.ValueChanged += (sender, args) => OnLipsChanged();
      Add(_lipThickness);
    }

    private void OnLipsChanged()
    {
      var lipThicknessValue = (_lipThickness.Value - _lipThickness.Maximum / 2) / (_lipThickness.Maximum / 2.0f);
      LipsChanged?.Invoke(this, new LipsChangedEventArgs(lipThicknessValue));
    }
  }
}