namespace CityOfMindClient.Models
{
  public class Color
  {
    public int Index { get; set; }
    public int Red { get; set; }
    public int Green { get; set; }
    public int Blue { get; set; }

    public Color(int index, int r, int g, int b)
    {
      Index = index;
      Red = r;
      Green = g;
      Blue = b;
    }
  }
}