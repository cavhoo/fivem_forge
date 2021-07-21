using FiveMForgeClient.Services.Language;
using LemonUI.Menus;

namespace CityOfMindClient.View.UI.Menu.CharacterCreate.Menus
{
  public delegate void CheekMenuEventHandler(object sender, CheekChangedEventArgs args);
  
  public class CheekChangedEventArgs 
  {
    public float CheekBoneWidth { get; }
    public float CheekBoneHeight { get; }
    public float CheekWidth { get; }
    
    internal CheekChangedEventArgs(float cheekWidth, float cheekBoneHeight, float cheekBoneWidth)
    {
      CheekWidth = cheekWidth;
      CheekBoneHeight = cheekBoneHeight;
      CheekBoneWidth = cheekBoneWidth;
    }
  }

  public class CheekMenu : NativeMenu
  {
    public event CheekMenuEventHandler CheekChanged;

    private NativeSliderItem CheekWidth;
    private NativeSliderItem CheekBoneHeight;
    private NativeSliderItem CheekBoneWidth;
    
    public CheekMenu(string title) : base(title)
    {
      Initialize();
    }

    private void Initialize()
    {
      CheekWidth = new NativeSliderItem(LanguageService.Translate("menu.character.creator.face.cheek.width"), 50, 25);
      CheekWidth.ValueChanged += (sender, args) => OnCheekValuesChanged();
      CheekBoneHeight = new NativeSliderItem(LanguageService.Translate("menu.character.creator.face.cheek.boneheight"),
        50, 25);
      CheekBoneHeight.ValueChanged += (sender, args) => OnCheekValuesChanged();
      CheekBoneWidth =
        new NativeSliderItem(LanguageService.Translate("menu.character.creator.face.cheek.bonewidth"), 50, 25);
      CheekBoneWidth.ValueChanged += (sender, args) => OnCheekValuesChanged();
      
      Add(CheekWidth);
      Add(CheekBoneHeight);
      Add(CheekBoneWidth);
    }

    private void OnCheekValuesChanged()
    {
      var cheekWidthValue = (CheekWidth.Value - CheekWidth.Maximum / 2) / (CheekWidth.Maximum / 2.0f);
      var cheekBoneHeightValue = (CheekBoneHeight.Value - CheekBoneHeight.Maximum / 2) / (CheekBoneHeight.Maximum / 2.0f);
      var cheekBoneWidthValue = (CheekBoneWidth.Value - CheekBoneWidth.Maximum / 2) / (CheekBoneWidth.Maximum / 2.0f);
      
      CheekChanged?.Invoke(this, new CheekChangedEventArgs(
          cheekWidthValue,
          cheekBoneHeightValue,
          cheekBoneWidthValue
        ));
    }
  }
}