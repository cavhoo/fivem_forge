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
    public string Name { get; set; }
    public int Age { get; set; }
    public string JobUuid { get; }
    public Vector3 LastPos { get; }
    public int Mom { get; set; }
    public int Dad { get; set; }
    public float Face { get; set; }
    public float SkinTone { get; set; }
    
    /// <summary>
    /// Head Shape Values
    /// </summary>
    public Vector2 NoseShape { get; set; }
    public Vector2 NoseProfile { get; set; }
    public Vector2 NoseTip { get; set; }
    public Vector2 Eyebrows { get; set; }
    public Vector2 CheekBones { get; set; }
    public Vector2 Cheeks { get; set; }
    public Vector2 Eyes { get; set; }
    public Vector2 Lips { get; set; }
    public Vector2 Jaws { get; set; }
    public Vector2 Chin { get; set; }
    public Vector2 ChinShape { get; set; }
    public Vector2 Neck { get; set; }
    
    
  }
}