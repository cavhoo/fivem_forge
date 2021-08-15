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
    public Character()
    {
    }
  
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Gender { get; set; }
    public string Birthdate { get; set; }
    public string AccountUuid { get; }
    public string CharacterUuid { get; }
    public string JobUuid { get; }
    public string LastPos { get; }

    /// <summary>
    /// Parent Data Values
    /// </summary>
    public int Mom { get; set; }

    public int Dad { get; set; }
    public float FaceResemblence { get; set; }
    public float SkinToneResemblence { get; set; }

    /// <summary>
    /// Nose Shape Values
    /// </summary>
    public float NoseLength { get; set; }
    public float NoseWidth { get; set; }
    public float NoseBoneOffset { get; set; }
    public float NoseBoneBend { get; set; }
    public float NoseHeight { get; set; }
    public float NoseTipLowering { get; set; }
    
    /// <summary>
    /// Eye Shape Values
    /// </summary>
    public float EyebrowHeight { get; set; }
    public float EyebrowBulkiness { get; set; }
    public int EyebrowColor { get; set; }
    public int EyebrowShape { get; set; }
    public int EyeColor { get; set; }
    public float EyeOpening { get; set; }
    /// <summary>
    /// Cheek Shape Data
    /// </summary>
    public float CheekWidth { get; set; }
    public float CheekBoneHeight { get; set; }
    public float CheekBoneWidth { get; set; }
    /// <summary>
    /// Lips and Chin Shape Data
    /// </summary>
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
    public int BeardShape { get; set; }
    public int BeardColor { get; set; }
    public int ChestHairShape { get; set; }
    public int ChestHairColor { get; set; }
    
    /// <summary>
    /// Makeup Data
    /// </summary>
    public int MakeUpVariant { get; set; }
    public int BlushVariant { get; set; }
    public int LipstickVariant { get; set; }
    public int MakeUpColor { get; set; }
    public int BlushColor { get; set; }
    public int LipstickColor { get; set; }

    /// <summary>
    /// Tattoo Shape Data
    /// </summary>
    public int TattooVariant { get; set; }
    
    
    public Vector3 GetLastPos()
    {
      var posSplit = LastPos.Split(':');
      return new Vector3(float.Parse(posSplit[0]), float.Parse(posSplit[1]), float.Parse(posSplit[2]));
    }

    public void UpdateParentData(int mom, int dad, float skinToneFactor, float faceFactor)
    {
      Mom = mom;
      Dad = dad;
      SkinToneResemblence = skinToneFactor;
      FaceResemblence = faceFactor;
    }

    public void UpdateNoseData(float width, float height, float boneBend, float boneOffset, float length, float tipLowering)
    {
      NoseHeight = height;
      NoseWidth = width;
      NoseBoneBend = boneBend;
      NoseBoneOffset = boneOffset;
      NoseLength = length;
      NoseTipLowering = tipLowering;
    }

    public void UpdateEyeData(int color, float browHeight, float browBulkiness, float opening, int browStyle, int browColor)
    {
      EyeColor = color;
      EyebrowBulkiness = browBulkiness;
      EyebrowHeight = browHeight;
      EyeOpening = opening;
      EyebrowColor = browColor;
      EyebrowShape = browStyle;
    }

    public void UpdateCheekData(float width, float boneHeight, float boneWidth)
    {
      CheekWidth = width;
      CheekBoneHeight = boneHeight;
      CheekBoneWidth = boneWidth;
    }

    public void UpdateLipData(float thickness)
    {
      LipsThickness = thickness;
    }
    
    public void UpdateChinData(float forward, float height, float gapSize, float width)
    {
      ChinForward = forward;
      ChinHeight = height;
      ChinGapSize = gapSize;
      ChinWidth = width;
    }

    public void UpdateHairData(int hairStyle, int baseColor, int highlightColor)
    {
      HairColor = hairStyle;
      HairHighlightColor = highlightColor;
      HairShape = baseColor;
    }

    public void UpdateMakeUpData(int lipstickColor, int lipStickVariant, int blushColor, int blushVariant, int makeUpVariant, int makeUpColor)
    {
      LipstickColor = lipstickColor;
      LipstickVariant = lipStickVariant;
      BlushColor = blushColor;
      BlushVariant = blushVariant;
      MakeUpVariant = makeUpVariant;
      MakeUpColor = makeUpColor;
    }
  }
}