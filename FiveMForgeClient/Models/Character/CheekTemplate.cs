namespace FiveMForgeClient.Models.Character
{
  public class CheekTemplate
  {
    public float CheekBoneHeight { get; set; }
    public float CheekBoneWidth { get; set; }
    public float CheekWidth { get; set; }

    public CheekTemplate(float cheekBoneHeight, float cheekBoneWidth, float cheekWidth)
    {
      CheekBoneHeight = cheekBoneHeight;
      CheekBoneWidth = cheekBoneWidth;
      CheekWidth = cheekWidth;
    }
  }
}