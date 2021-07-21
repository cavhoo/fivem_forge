namespace CityOfMindClient.Models.Character
{
  public class CharacterPreset
  {
    public int FaceMom { get; set; }
    public int FaceDad { get; set; }
    public int SkinMom { get; set; }
    public int SkinDad { get; set; }
    public float SkinBlend { get; set; }
    public float ShapeBlend { get; set; }
    public float NoseWidth { get; set; }
    public float NoseTipLength { get; set; }
    public float NoseTipHeight { get; set; }
    public float NoseTipLowering { get; set; }
    public float NoseBoneBend { get; set; }
    public float NoseBoneOffset { get; set; }
    public float CheekBoneWidth { get; set; }
    public float CheekBoneHeight { get; set; }
    public float CheekWidth { get;  set; }
    public float ChinWidth { get; set; }
    public float ChinForward { get;  set; }
    public float ChinHeight { get; set; }
    public float ChinGapSize { get; set; }
    public int EyeColor { get; set; }
    public float EyeBrowHeight { get; set; }
    public float EyeBrowBulkiness { get; set; }
    public float EyeOpening { get; set; }
    public float Thickness { get; set; }
    public int Sex { get; set; }

    public CharacterPreset()
    {
    }

    public CharacterPreset(int faceMom, int faceDad, int skinMom, int skinDad, float skinBlend, float shapeBlend)
    {
      FaceDad = faceDad;
      FaceMom = faceMom;
    }
  }
}