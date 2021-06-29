using System;

namespace FiveMForgeClient.Models.Character
{
  public class Character
  {
    public Character(string name, int age, string accountUuid, string jobUuid, string characterUuid)
    {
      AccountUuid = accountUuid;
      CharacterUuid = characterUuid;
      Age = age;
      Name = name;
      JobUuid = jobUuid;
    }


    public string AccountUuid { get; set; }
    public string CharacterUuid { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string JobUuid { get; set; }
  }
}