extern alias CFX;
using static CFX::CitizenFX.Core.Native.API;
using BaseScript = CFX::CitizenFX.Core.BaseScript;
using Debug = CFX::CitizenFX.Core.Debug;

namespace Client.Controller.Character
{
  public class Character : BaseScript
  {
    public static void UpdateProperties(int pedId, Models.Character.Character characterData)
    {
      SetPedHeadBlendData(
        pedId,
        characterData.Dad,
        characterData.Mom,
        0,
        characterData.Dad,
        characterData.Mom,
        0,
        characterData.FaceResemblence,
        characterData.SkinToneResemblence,
        0,
        false
      );
      SetPedFaceFeature(pedId, 1, characterData.NoseWidth);
      SetPedFaceFeature(pedId, 2, characterData.NoseHeight);
      SetPedFaceFeature(pedId, 3, characterData.NoseLength);
      SetPedFaceFeature(pedId, 4, characterData.NoseBoneBend);
      SetPedFaceFeature(pedId, 5, characterData.NoseBoneOffset);
      SetPedFaceFeature(pedId, 6, 0.5f);

      SetPedFaceFeature(pedId, 7, characterData.EyebrowHeight);
      SetPedFaceFeature(pedId, 8, characterData.EyebrowBulkiness);
      SetPedFaceFeature(pedId, 12, characterData.EyeOpening);
      SetPedEyeColor(pedId, characterData.EyeColor);
      SetPedHeadOverlay(pedId, 2, characterData.EyebrowShape, 1.0f);
      SetPedHeadOverlayColor(pedId, 2, 1, characterData.EyebrowColor, 0);

      SetPedFaceFeature(pedId, 9, characterData.CheekBoneWidth);
      SetPedFaceFeature(pedId, 10, characterData.CheekBoneHeight);
      SetPedFaceFeature(pedId, 11, characterData.CheekWidth);

      SetPedFaceFeature(pedId, 12, characterData.LipsThickness);

      // Update Makeup
      SetPedHeadOverlay(pedId, 4, characterData.MakeUpVariant, 1.0f);
      SetPedHeadOverlayColor(pedId, 4, 0, characterData.MakeUpColor, 0);

      // Update Hairstyle
      Debug.WriteLine($"Chaing Hairstyle {characterData.HairShape}");
      SetPedComponentVariation(pedId, 2, characterData.HairShape, 0, 2);
      SetPedHairColor(pedId, characterData.HairColor, characterData.HairHighlightColor);

      // Update Beard
      SetPedHeadOverlay(pedId, 1, characterData.BeardShape, 1.0f);
      SetPedHeadOverlayColor(pedId, 1, 1, characterData.BeardColor, 0);

      // Update Blush
      SetPedHeadOverlay(pedId, 5, characterData.BlushVariant, 1.0f);
      SetPedHeadOverlayColor(pedId, 5, 2, characterData.BlushColor, 0);

      // Update Lipstick
      SetPedHeadOverlay(pedId, 8, characterData.LipstickVariant, 1.0f);
      SetPedHeadOverlayColor(pedId, 8, 2, characterData.LipstickColor, 0);

      // Update Chest Hair
      SetPedHeadOverlay(pedId, 10, characterData.ChestHairShape, 1.0f);
      SetPedHeadOverlayColor(pedId, 10, 1, characterData.ChestHairColor, 0);
    }
  }
}