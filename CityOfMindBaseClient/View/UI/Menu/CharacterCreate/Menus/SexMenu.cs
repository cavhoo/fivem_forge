using System.ComponentModel;
using FiveMForgeClient.Services.Language;
using LemonUI.Menus;

namespace FiveMForgeClient.View.UI.Menu.CharacterCreate
{
  public delegate void SexChangedHandler(object sender, SexChangedEventArgs args);

  public class SexChangedEventArgs
  {
    public int Sex { get; set; }

    internal SexChangedEventArgs(int sex)
    {
      Sex = sex;
    }
  }

  public class SexMenu : NativeMenu
  {
    public event SexChangedHandler OnSexChanged;
    private string[] AvailableGenders; 
    
    public SexMenu(string title) : base(title)
    {
      AvailableGenders = new[] {LanguageService.Translate("male"), LanguageService.Translate("female")};
      Initialize();
    }

    private void Initialize()
    {
      var sexList = new NativeListItem<string>(LanguageService.Translate("menu.character.creator.sex.sex"), AvailableGenders);
      sexList.ItemChanged += (sender, args) =>
      {
        OnSexChanged?.Invoke(this, new SexChangedEventArgs(sexList.SelectedIndex));
      };
      Add(sexList);
    }
  }
}