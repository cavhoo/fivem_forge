using System.ComponentModel.Design;
using LemonUI.Menus;

namespace FiveMForgeClient.View.UI.Directory
{
  /// <summary>
  /// Convenience class for storing SliderRange values 
  /// </summary>
  public class SliderRange
  {
    public int Max { get; private set; }
    public int Min { get; private set; }
    internal SliderRange(int min, int max)
    {
      Max = max;
      Min = min;
    }
  }
  
  
  public class MenuItemSlider : NativeItem
  {
    /// <summary>
    /// Basic data of slider
    /// </summary>
    private SliderRange Range;
    private float Steps;
    
    /// <summary>
    /// Make a new slider MenuElement
    /// </summary>
    /// <param name="title">Title of the slider menu Entry</param>
    /// <param name="min">Minimum value of the slider</param>
    /// <param name="max">Maximum value fo the slider</param>
    /// <param name="steps">Steps how the slider is incremented</param>
    
    public MenuItemSlider(string title, int min, int max, float steps) : base(title)
    {
      Range = new SliderRange(min, max);
      Steps = steps;
    }
    
    
    
  }
}