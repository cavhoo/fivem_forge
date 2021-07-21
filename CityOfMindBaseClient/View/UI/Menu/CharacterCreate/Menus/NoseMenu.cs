using CitizenFX.Core;
using FiveMForgeClient.Services.Language;
using LemonUI.Menus;

namespace CityOfMindClient.View.UI.Menu.CharacterCreate.Menus
{
  public delegate void NoseChangedEventHandler(object sender, NoseChangedEventArgs args);


  public class NoseChangedEventArgs
  {
    public float NoseWidth { get; }
    public float NoseTipLength { get; }
    public float NoseTipHeight { get; }
    public float NoseTipLowering { get; set; }
    public float NoseBoneBend { get; }
    public float NoseBoneOffset { get; }

    internal NoseChangedEventArgs(float noseWidth, float noseTipLength, float noseTipHeight, float noseTipLowering,
      float noseBoneBend, float noseBoneOffset)
    {
      NoseWidth = noseWidth;
      NoseTipLength = noseTipLength;
      NoseTipHeight = noseTipHeight;
      NoseTipLowering = noseTipLowering;
      NoseBoneBend = noseBoneBend;
      NoseBoneOffset = noseBoneOffset;
    }
  }

  public class NoseMenu : NativeMenu
  {
    public event NoseChangedEventHandler NoseChanged;

    private NativeSliderItem _noseWidthSlider;
    private NativeSliderItem _noseTipLength;
    private NativeSliderItem _noseTipHeight;
    private NativeSliderItem _noseTipLowering;
    private NativeSliderItem _noseBoneBend;
    private NativeSliderItem _noseBoneOffset;

    public NoseMenu(string title) : base(title)
    {
      Initialize();
    }

    private void Initialize()
    {
      _noseWidthSlider =
        new NativeSliderItem(LanguageService.Translate("menu.character.creator.face.nose.width.titleh"), 50, 25);
      _noseWidthSlider.ValueChanged += (sender, args) => OnNoseShapeChanged();
      _noseTipLength =
        new NativeSliderItem(LanguageService.Translate("menu.character.creator.face.nose.length.title"), 50, 25);
      _noseTipLength.ValueChanged += (sender, args) => OnNoseShapeChanged();
      _noseTipHeight =
        new NativeSliderItem(LanguageService.Translate("menu.character.creator.face.nose.tip.height.title"), 50, 25);
      _noseTipLowering = new NativeSliderItem(LanguageService.Translate("menu.character.creator.face.nose.tip.offset.title"),
        50, 25);
      _noseTipLowering.ValueChanged += (sender, args) => OnNoseShapeChanged();
      _noseBoneBend =
        new NativeSliderItem(LanguageService.Translate("menu.character.creator.face.nose.curve.title"), 50, 25);
      _noseBoneBend.ValueChanged += (sender, args) => OnNoseShapeChanged();
      _noseBoneOffset = new NativeSliderItem(LanguageService.Translate("menu.character.creator.face.nose.curve.offset.title"),
        50, 25);
      _noseBoneOffset.ValueChanged += (sender, args) => OnNoseShapeChanged();

      Add(_noseWidthSlider);
      Add(_noseTipLength);
      Add(_noseTipHeight);
      Add(_noseTipLowering);
      Add(_noseBoneBend);
      Add(_noseBoneOffset);
      OnNoseShapeChanged(); // Trigger inital settings
    }

    private void OnNoseShapeChanged()
    {
      Debug.WriteLine("Updated nose shape.");
      // Convert the slider value to actual accepted values of -1.0f to 1.0f.
      var widthValue = (_noseWidthSlider.Value - _noseWidthSlider.Maximum / 2) / (_noseWidthSlider.Maximum / 2.0f);
      var tipLengthValue = (_noseTipLength.Value - _noseTipLength.Maximum / 2) / (_noseTipLength.Maximum / 2.0f);
      var tipHeightValue = (_noseTipHeight.Value - _noseTipHeight.Maximum / 2) / (_noseTipHeight.Maximum / 2.0f);
      var tipLoweringValue = (_noseTipLowering.Value - _noseTipLowering.Maximum / 2) / (_noseTipLowering.Maximum / 2.0f);
      var noseBendValue = (_noseBoneBend.Value - _noseBoneBend.Maximum / 2 ) / (_noseBoneBend.Maximum / 2.0f);
      var noseBendOffsetValue = (_noseBoneOffset.Value - _noseBoneOffset.Maximum / 2) / (_noseBoneOffset.Maximum / 2.0f);

      // Dispatch event with new values on slider change.
      NoseChanged?.Invoke(this, new NoseChangedEventArgs(
        widthValue,
        tipLengthValue,
        tipHeightValue,
        tipLoweringValue,
        noseBendValue,
        noseBendOffsetValue
      ));
    }
  }
}