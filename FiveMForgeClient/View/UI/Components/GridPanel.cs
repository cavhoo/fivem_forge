extern alias CFX;
using System;
using System.Drawing;
using CFX::CitizenFX.Core.UI;
using LemonUI.Menus;
using PointF = CFX::System.Drawing.PointF;
using SizeF = CFX::System.Drawing.SizeF;

namespace FiveMForgeClient.View.UI.Components
{
  public delegate void GridValueChangedEventHandler(object sender, EventArgs args);

  public class GridValueChangedEventArgs
  {
    public float X { get; set; }
    public float Y { get; set; }

    internal GridValueChangedEventArgs(float x, float y)
    {
      X = x;
      Y = y;
    }
  }
  public class GridPanel : NativePanel
  {
    public event GridValueChangedEventHandler GridPositonCHanged;
    private float GridWidth;
    private float GridHeight;
    private NativePanel InternalPanel;
    private Sprite GridSprite;
    public GridPanel(float width, float height)
    {
      GridWidth = width;
      GridHeight = height;
      Initialize();
    }

    private void Initialize()
    {
      GridSprite = new Sprite("pause_menu_pages_char_mom_dad", "nose_grid", new SizeF(0,0), new PointF(0, 0));
    }

    public override void Process()
    {
      base.Process();
      GridSprite.Draw();
    }
  }
}