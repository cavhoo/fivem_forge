using CitizenFX.Core;
using FiveMForgeClient.Services.Language;
using LemonUI.Menus;

namespace CityOfMindClient.View.UI.Menu.CharacterCreate.Menus
{
  public delegate void LipsChangedEventHandler(object sender, LipsChangedEventArgs args);

  public class LipsChangedEventArgs
  {
    public float Thickness { get; private set; }

    internal LipsChangedEventArgs(float thickness)
    {
      Thickness = thickness;
    }
  }


  public class LipsMenu : NativeMenu
  {
    public event LipsChangedEventHandler LipsChanged;
    private NativeSliderItem LipThickness;

    public LipsMenu(string title) : base(title)
    {
      Initialize();
    }

    private void Initialize()
    {
      LipThickness =
        new NativeSliderItem(LanguageService.Translate("menu.character.creator.face.lip.thickness"), 50, 25);
    }

    private void OnLipsChanged()
    {
      var lipThicknessValue = (LipThickness.Value - LipThickness.Maximum / 2) / (LipThickness.Maximum / 2);
      LipsChanged?.Invoke(this, new LipsChangedEventArgs(lipThicknessValue));
    }
  }
}