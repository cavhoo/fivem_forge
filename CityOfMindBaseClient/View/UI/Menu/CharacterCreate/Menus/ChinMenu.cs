using FiveMForgeClient.Services.Language;
using LemonUI.Menus;

namespace CityOfMindClient.View.UI.Menu.CharacterCreate.Menus
{
  public delegate void ChinChangedEventHandler(object sender, ChinChangedEventArgs args);

  public class ChinChangedEventArgs
  {
    public float ChinWidth { get; }
    public float ChinForward { get; }
    public float ChinHeight { get; }
    public float ChinGapSize { get; }
    
    internal ChinChangedEventArgs(float chinWidth, float chinHeight, float chinForward, float chinGapSize)
    {
      ChinWidth = chinWidth;
      ChinForward = chinForward;
      ChinHeight = chinHeight;
      ChinGapSize = chinGapSize;
    }
  }
  
  public class ChinMenu : NativeMenu
  {
    public event ChinChangedEventHandler ChinChanged;

    private NativeSliderItem ChinWidth;
    private NativeSliderItem ChinForward;
    private NativeSliderItem ChinHeight;
    private NativeSliderItem ChinGapSize;
    
    public ChinMenu(string title) : base(title)
    {
      Initialize();
    }

    private void Initialize()
    {
      ChinWidth = new NativeSliderItem(LanguageService.Translate("menu.character.creator.face.chin.width"), 50, 25);
      ChinWidth.ValueChanged += (sender, args) => OnChinChanged();
      ChinForward = new NativeSliderItem(LanguageService.Translate("menu.character.creator.face.chin.forward"), 50, 25);
      ChinForward.ValueChanged += (sender, args) => OnChinChanged();
      ChinHeight = new NativeSliderItem(LanguageService.Translate("menu.character.creator.face.chin.height"), 50, 25);
      ChinHeight.ValueChanged += (sender, args) => OnChinChanged();
      ChinGapSize = new NativeSliderItem(LanguageService.Translate("menu.chracter.creator.face.chin.gap"), 50, 25);
      ChinGapSize.ValueChanged += (sender, args) => OnChinChanged();
    }

    private void OnChinChanged()
    {
      var chinWidthValue = (ChinWidth.Value - ChinWidth.Maximum / 2) / (ChinWidth.Maximum / 2.0f);
      var chinForwardValue = (ChinForward.Value - ChinForward.Maximum / 2) / (ChinForward.Maximum / 2.0f);
      var chinHeightValue = (ChinHeight.Value - ChinHeight.Maximum / 2) / (ChinHeight.Maximum / 2.0f);
      var chinGapValue = (ChinGapSize.Value - ChinGapSize.Maximum / 2) / (ChinGapSize.Maximum / 2.0f);
      
      
      ChinChanged?.Invoke(this, new ChinChangedEventArgs(
          chinWidthValue,
          chinHeightValue,
          chinForwardValue,
          chinGapValue
        ));
    }
  }
}