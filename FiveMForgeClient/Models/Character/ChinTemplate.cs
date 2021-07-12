namespace FiveMForgeClient.Models.Character
{
  public class ChinTemplate
  {
    public float ChinHeight { get; set; }
    public float ChinLength { get; set; }
    public float ChinWidth { get; set; }
    public float ChinGapSize { get; set; }
    
    public ChinTemplate(float chinHeight, float chinLength, float chinWidth, float chinGapSize)
    {
      ChinHeight = chinHeight;
      ChinLength = chinLength;
      ChinWidth = chinWidth;
      ChinGapSize = chinGapSize;
    }
  }
}