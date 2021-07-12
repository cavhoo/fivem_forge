namespace FiveMForgeClient.Models.Character
{
  public class NoseTemplate
  {
    public float NoseWidth { get; set; }
    public float NoseCurve { get; set; }
    public float NoseLength { get; set; }
    public float NoseTipLeftRight { get; set; }
    public NoseTemplate(float noseWidth, float noseCurve, float noseTipLeftRight, float noseLength)
    {
      NoseCurve = noseCurve;
      NoseWidth = noseWidth;
      NoseLength = noseLength;
      NoseTipLeftRight = noseTipLeftRight;
    }
  }
}