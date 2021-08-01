extern alias CFX;
using System;
using CityOfMindClient.View.UI.Menu.CharacterCreate.Menus;
using FiveMForgeClient.View.UI.Menu.CharacterCreate;
using Vector2 = CFX::CitizenFX.Core.Vector2;
using Vector3 = CFX::CitizenFX.Core.Vector3;

namespace CityOfMindClient.Models.Character
{
  public class Character
  {
    public Character(string name, int? age, string accountUuid, string jobUuid, string characterUuid, string lastPos)
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
    public int? Age { get; set; }
    public string JobUuid { get; }
    public string LastPos { get; }

    /// <summary>
    /// Parent Data Values
    /// </summary>
    public int? Mom { get; set; }

    public int? Dad { get; set; }
    public float? FaceResemblence { get; set; }
    public float? SkinToneResemblence { get; set; }

    /// <summary>
    /// Head Shape Values
    /// </summary>
    public float NoseLength { get; set; }

    public float NoseBoneOffset { get; set; }
    public float NoseBoneBend { get; set; }
    public float EyebrowHeight { get; set; }
    public float EyebrowBulkiness { get; set; }
    public int EyeColor { get; set; }
    public float EyeOpening { get; set; }
    public float CheekWidth { get; set; }
    public float CheekBoneHeight { get; set; }
    public float CheekBoneWidth { get; set; }
    public float LipsThickness { get; set; }
    public float ChinWidth { get; set; }
    public float ChinForward { get; set; }
    public float ChinHeight { get; set; }
    public float ChinGapSize { get; set; }

    /// <summary>
    /// Hair Information
    /// </summary>
    public int HairShape { get; set; }

    public int HairColor { get; set; }
    public int HairHighlightColor { get; set; }

    /// <summary>
    /// Makeup Data
    /// </summary>
    public int MakeUpVariant { get; set; }
    public int BlushVariant { get; set; }
    public int LipstickVariant { get; set; }
    public int MakeUpColor { get; set; }
    public int BlushColor { get; set; }
    public int LipstickColor { get; set; }

    public Vector3 GetLastPos()
    {
      var posSplit = LastPos.Split(':');
      return new Vector3(float.Parse(posSplit[0]), float.Parse(posSplit[1]), float.Parse(posSplit[2]));
    }

    public void UpdateParentData(ParentsChangedEventArgs parentData)
    {
      Mom = parentData.Mom;
      Dad = parentData.Dad;
      SkinToneResemblence = parentData.SkinToneFactor;
      FaceResemblence = parentData.ResemblanceFactor;
    }

    public void UpdateFaceData(FaceChangedEventArgs faceData)
    {
      NoseLength = faceData.NoseTipLength;
      NoseBoneBend = faceData.NoseBoneBend;
      NoseBoneOffset = faceData.NoseBoneOffset;
      EyebrowHeight = faceData.EyeBrowHeight;
      EyebrowBulkiness = faceData.EyeBrowBulkiness;
      EyeColor = faceData.EyeColor;
      EyeOpening = faceData.EyeOpening;
      CheekWidth = faceData.CheekWidth;
      CheekBoneHeight = faceData.CheekBoneHeight;
      CheekBoneWidth = faceData.CheekBoneWidth;
      LipsThickness = faceData.LipThickness;
      ChinForward = faceData.ChinForward;
      ChinHeight = faceData.ChinHeight;
      ChinGapSize = faceData.ChinGapSize;
      ChinWidth = faceData.ChinWidth;
    }

    public void UpdateHairData(HairChangedEventArgs hairData)
    {
      HairColor = hairData.BaseColor;
      HairHighlightColor = hairData.HighlightColor;
      HairShape = hairData.HairShape;
    }

    public void UpdateMakeUpData(MakeUpChangedEventArgs makeUpData)
    {
      LipstickColor = makeUpData.LipstickColor;
      LipstickVariant = makeUpData.LipstickVariant;
      BlushColor = makeUpData.BlushColor;
      BlushVariant = makeUpData.BlushVariant;
      MakeUpVariant = makeUpData.MakeUpVariant;
      MakeUpColor = makeUpData.MakeUpColor;
    }
  }
}