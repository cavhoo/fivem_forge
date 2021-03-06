extern alias CFX;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml.Serialization;
using CFX::System.Drawing;

namespace FiveMForgeClient.View.UI.Hud
{
  public class Base
  {
    public PointF Position { get; set; }
    public bool Visible { get; set; }
    private List<Base> Children { get; } = new();
    private Base Parent { get; set; }

    public virtual void Update()
    {
      for (var i = 0; i < Children.Count(); i++)
      {
        Children[i].Update();
      }
    }

    public virtual void Draw()
    {
      for (var i = 0; i < Children.Count(); i++)
      {
        if (Children[i].Visible)
        {
          Children[i].Draw();
        }
      }
    }

    public virtual void AddChild(Base child)
    {
      child.Parent.RemoveChild(child);
      child.Parent = this;
      Children.Add(child);
    }

    public virtual void RemoveChild(Base child)
    {
      Children.Remove(child);
      child.Parent = null;
    }
  }
}