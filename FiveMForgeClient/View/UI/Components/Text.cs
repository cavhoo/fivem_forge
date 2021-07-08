extern alias CFX;
using System.Runtime.InteropServices;
using CFX::System.Drawing;
using FiveMForgeClient.View.UI.Hud;

namespace FiveMForgeClient.View.UI.Components
{
  public class Text : Base
  {
    private string _text;
    private readonly CFX::CitizenFX.Core.UI.Text Element;
    public Text(string text, PointF pos, float scale)
    {
      _text = text;
      Element = new CFX::CitizenFX.Core.UI.Text(text, pos, scale);

    }

    public void SetScale(float scale)
    {
      Element.Scale = scale;
    }

    public float GetScale()
    {
      return Element.Scale;
    }

    public void SetText(string text)
    {
      _text = text;
      Element.Caption = text;
    }

    public string GetText()
    {
      return _text;
    }
  }
}