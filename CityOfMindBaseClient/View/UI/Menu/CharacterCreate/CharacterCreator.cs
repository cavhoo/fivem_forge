extern alias CFX;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Remoting.Channels;
using System.Threading.Tasks;
using CFX::CitizenFX.Core;
using CityOfMindClient.View.UI.Menu.CharacterCreate.Menus;
using FiveMForgeClient.Enums;
using FiveMForgeClient.Services.Language;
using FiveMForgeClient.Models.Character;
using LemonUI;
using LemonUI.Menus;
using static CFX::CitizenFX.Core.Native.API;

namespace FiveMForgeClient.View.UI.Menu.CharacterCreate
{
  public delegate void CharacterChangedEventHandler(object sender, CharacterChangedEventArgs args);

  public class CharacterChangedEventArgs
  {
    public int Sex { get; set; }
    public ChinChangedEventArgs ChinData { get; set; }
    public EyeMenuChangedEventArgs EyeData { get; set; }
    public NoseChangedEventArgs NoseData { get; set; }
    public CheekChangedEventArgs CheekData { get; set; }
    public LipsChangedEventArgs LipData { get; set; }
  }
  
  public class CharacterCreator : NativeMenu
  {
    private Character createdCharacter;
    private int CreatedPedId;
    private ObjectPool Pool;
    private int SelectedSex;
    private bool IsSwitching;

    private NativeSubmenuItem _femalePresets;
    private NativeSubmenuItem _malePresets;
    

    public CharacterCreator(string title, string subtitle) : base(title, subtitle)
    {
      Visible = false;
    }

    public void SetPedId(int pedId)
    {
      Debug.WriteLine($"Setting ped id to: {pedId}");
      CreatedPedId = pedId;
    }

    public void SetMenuPool(ref ObjectPool pool)
    {
      if (Pool != null)
      {
        Debug.WriteLine("Can only register one MenuPool for character creator.");
      }

      Pool = pool;
      Initialize();
    }

    public void SetPresets()
    {
      
    }

    private void Initialize()
    {
      createdCharacter =
        new Character("", 30, "", "", "", new Vector3(100, 100, 100)); // TODO: Replace position with airport location.
      
      var faceMenu = new FaceMenu(LanguageService.Translate("menu.character.creator.face.title"), ref Pool);
      faceMenu.FaceChanged += (sender, args) => { };
      Pool.Add(faceMenu);

      var presetList = new NativeListItem<string>(LanguageService.Translate("menu.character.creator.presets"));
      Add(presetList);

      var parentsMenu = new ParentsMenu(LanguageService.Translate("menu.character.creator.parents.title"));
      parentsMenu.ParentsChanged += (sender, args) =>
      {
        Debug.WriteLine($"Changing Parents...{args.Dad} | {args.Mom} | {args.ResemblanceFactor}");
        SetPedHeadBlendData(CreatedPedId, args.Dad, args.Mom, 0, args.Mom, args.Dad, 0, args.ResemblanceFactor,
          args.SkinToneFactor, 0, false);
      };
      Pool.Add(parentsMenu);

      var customizeMenu = new NativeMenu(LanguageService.Translate("menu.character.creator.customize"));
      customizeMenu.AddSubMenu(faceMenu);
      customizeMenu.AddSubMenu(parentsMenu);
      var customizeSubmenu = AddSubMenu(customizeMenu);
      
      var sexMenu = new SexMenu(LanguageService.Translate("menu.character.creator.sex.title"));
      sexMenu.OnSexChanged += async (sender, args) =>
      {
        SelectedSex = args.Sex;
        Debug.WriteLine(args.Sex.ToString());
      };
      Pool.Add(sexMenu);
      AddSubMenu(sexMenu);
    }
  }
}