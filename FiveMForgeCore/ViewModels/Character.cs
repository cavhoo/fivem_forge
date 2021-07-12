namespace FiveMForge.ViewModels
{
  public class Character
  {
    public string LastPos { get; set; }
    public string CharacterUuid { get; set; }
    public bool InUse { get; set; }
    public string AccountUuid { get; set; }
    public string JobTitle { get; set; }
    public string JobSalary { get; set; }
    public decimal MoneyBank { get; set; }
    public decimal MoneyWallet { get; set; }
    
    public Character()
    {
      
    }
  }
}