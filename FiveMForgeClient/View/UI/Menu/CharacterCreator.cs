extern alias CFX;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Remoting.Channels;
using CFX::CitizenFX.Core;
using FiveMForgeClient.Enums;
using FiveMForgeClient.Services.Language;
using FiveMForgeClient.Models.Character;
using FiveMForgeClient.View.UI.Components;
using LemonUI;
using LemonUI.Menus;
using static CFX::CitizenFX.Core.Native.API;

namespace FiveMForgeClient.View.UI.Menu
{
  public class CharacterCreator : NativeMenu
  {
    private Character createdCharacter;
    private int CreatedPedId;
    private ObjectPool Pool;
    private NativeMenu Inheritance;
    private NativeMenu Face;
    private NativeMenu Appearance;
    private NativeMenu Clothes;

    public CharacterCreator(string title, string subtitle, bool glare = false) : base(title, subtitle)
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
        Debug.WriteLine("Can only register one MenuPool for character selector.");
      }

      Pool = pool;
      Initialize();
    }

    private void Initialize()
    {
      createdCharacter =
        new Character("", 30, "", "", "", new Vector3(100, 100, 100)); // TODO: Replace position with airport location.
      var parentsMenu = new ParentsMenu(LanguageService.Translate("menu.character.creator.parents.title"));
      parentsMenu.ParentsChanged += (sender, args) =>
      {
        Debug.WriteLine($"Changing Parents...{args.Dad} | {args.Mom} | {args.ResemblanceFactor}");
        SetPedHeadBlendData(CreatedPedId, args.Dad, args.Mom, 0, args.Mom, args.Dad, 0, args.ResemblanceFactor,
          args.SkinToneFactor, 0, false);
      };
      Pool.Add(parentsMenu);
      AddSubMenu(parentsMenu).Title = LanguageService.Translate("menu.character.creator.parents.title");

      var faceMenu = new FaceMenu(LanguageService.Translate("menu.character.creator.face.title"));
      faceMenu.FaceChanged += (sender, args) =>
      {
        
      };
      Pool.Add(faceMenu);
      AddSubMenu(faceMenu).Title = LanguageService.Translate("menu.character.creator.face.title");
    }
  }
}