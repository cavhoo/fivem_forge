extern alias CFX;
using System;
using CFX::CitizenFX.Core;

namespace FiveMForgeClient.Models.Character
{
  public class Character
  {
    public Character(string name, int age, string accountUuid, string jobUuid, string characterUuid, Vector3 lastPos)
    {
      AccountUuid = accountUuid;
      CharacterUuid = characterUuid;
      Age = age;
      Name = name;
      JobUuid = jobUuid;
      LastPos = lastPos;
    }


    public string AccountUuid { get; }
    public string CharacterUuid { get; }
    public string Name { get; }
    public int Age { get; }
    public string JobUuid { get; }
    public Vector3 LastPos { get; }
  }
}