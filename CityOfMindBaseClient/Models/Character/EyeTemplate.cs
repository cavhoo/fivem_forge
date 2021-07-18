namespace FiveMForgeClient.Models.Character
{
  public class EyeTemplate
  {
    public float EyeBrowHeight { get; set; }
    public float EyeBrowWidth { get; set; }
    public float EyeOpening { get; set; }

    public EyeTemplate(float eyeBrowHeight, float eyeBrowWidth, float eyeOpening)
    {
      EyeBrowHeight = eyeBrowHeight;
      EyeBrowWidth = eyeBrowWidth;
      EyeOpening = eyeOpening;
    }
  }
}