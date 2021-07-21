extern alias CFX;
using CityOfMindClient.Models.Character;
using CityOfMindClient.View.UI.Menu.CharacterCreate.Menus;
using FiveMForgeClient.Services.Language;
using FiveMForgeClient.View.UI.Menu.CharacterCreate;
using LemonUI;
using LemonUI.Menus;
using Debug = CFX::CitizenFX.Core.Debug;

namespace CityOfMindClient.View.UI.Menu.CharacterCreate
{
  public delegate void CharacterChangedEventHandler(object sender, CharacterChangedEventArgs args);

  public class CharacterChangedEventArgs
  {
    public int Sex { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public FaceChangedEventArgs FaceData { get; set; }
    public ParentsChangedEventArgs ParentData { get; set; }
    public HairChangedEventArgs HairData { get; set; }
    public MakeUpChangedEventArgs MakeUpData { get; set; }
    //public dynamic TattooData { get; set; }
    //public dynamic BlemishData { get; set; }
    //public dynamic ClothingData { get; set; }
  }

  public class CharacterCreator : NativeMenu
  {
    public event CharacterChangedEventHandler CharacterChanged;
    private ObjectPool Pool;

    private CharacterPreset[] _presets;
    private CharacterChangedEventArgs CurrentCharacter;

    public CharacterCreator(string title, string subtitle) : base(title, subtitle)
    {
      Visible = false;
    }

    public void SetPedId(int pedId)
    {
      Debug.WriteLine($"Setting ped id to: {pedId}");
    }

    public void SetMenuPool(ref ObjectPool pool)
    {
      if (Pool != null)
      {
        Debug.WriteLine("Can only register one MenuPool for character creator.");
      }

      Pool = pool;
    }

    public void SetPresets(CharacterPreset[] presets)
    {
      _presets = presets;
    }

    public void Initialize()
    {
      CurrentCharacter = new CharacterChangedEventArgs();
      var faceMenu = new FaceMenu(LanguageService.Translate("menu.character.creator.face.title"), ref Pool);
      faceMenu.FaceChanged += (sender, args) =>
      {
        CurrentCharacter.FaceData = args;
        OnCharacterChanged();
      };
      Pool.Add(faceMenu);

      var presetList = new NativeListItem<string>(LanguageService.Translate("menu.character.creator.presets"),
        new[] {"Preset 1", "Preset 2", "Prseset 3"});
      Add(presetList);

      var parentsMenu = new ParentsMenu(LanguageService.Translate("menu.character.creator.parents.title"));
      parentsMenu.ParentsChanged += (sender, args) =>
      {
        Debug.WriteLine($"Changing Parents...{args.Dad} | {args.Mom} | {args.ResemblanceFactor}");
        CurrentCharacter.ParentData = args;
        OnCharacterChanged();
      };
      Pool.Add(parentsMenu);

      var makeUpMenu = new MakeUpMenu(LanguageService.Translate("menu.character.creator.makeup"));
      makeUpMenu.MakeupChanged += (sender, args) => { CurrentCharacter.MakeUpData = args; OnCharacterChanged(); };
      Pool.Add(makeUpMenu);
      var clothingMenu = new ClothingMenu(LanguageService.Translate("menu.character.creator.clothing"));
      Pool.Add(clothingMenu);

      var tattoosMenu = new TattooMenu(LanguageService.Translate("menu.character.creator.tattoo"));
      Pool.Add(tattoosMenu);

      var blemishesMenu = new BlemishesMenu(LanguageService.Translate("menu.character.creator.blemishes"));
      Pool.Add(blemishesMenu);

      var hairMenu = new HairMenu(LanguageService.Translate("menu.character.creator.hair"));
      hairMenu.HairChanged += (sender, args) => { CurrentCharacter.HairData = args; };
      Pool.Add(hairMenu);

      var customizeMenu = new NativeMenu(LanguageService.Translate("menu.character.creator.customize"));
      var subParents = customizeMenu.AddSubMenu(parentsMenu);
      subParents.Title = LanguageService.Translate("menu.character.creator.parents.title");
      var subFace = customizeMenu.AddSubMenu(faceMenu);
      subFace.Title = LanguageService.Translate("menu.character.creator.face.title");
      var subMakeUp = customizeMenu.AddSubMenu(makeUpMenu);
      subMakeUp.Title = LanguageService.Translate("menu.character.creator.makeup");
      var subTattoo = customizeMenu.AddSubMenu(tattoosMenu);
      subTattoo.Title = LanguageService.Translate("menu.character.creator.tattoo");
      var subBlemishes = customizeMenu.AddSubMenu(blemishesMenu);
      subBlemishes.Title = LanguageService.Translate("menu.character.creator.blemishes");
      var subHair = customizeMenu.AddSubMenu(hairMenu);
      subHair.Title = LanguageService.Translate("menu.character.creator.hair");
      Pool.Add(customizeMenu);
      var subCustomize = AddSubMenu(customizeMenu);
      subCustomize.Title = LanguageService.Translate("menu.character.creator.customize");

      var sexMenu = new SexMenu(LanguageService.Translate("menu.character.creator.sex.title"));
      sexMenu.OnSexChanged += async (sender, args) => { Debug.WriteLine(args.Sex.ToString()); };
      Pool.Add(sexMenu);
      AddSubMenu(sexMenu);
    }

    private void OnCharacterChanged()
    {
      CitizenFX.Core.Debug.WriteLine("Character properties changed...");
      CharacterChanged?.Invoke(this, CurrentCharacter);
    }
  }
}