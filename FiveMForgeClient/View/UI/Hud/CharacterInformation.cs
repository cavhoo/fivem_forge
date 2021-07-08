﻿extern alias CFX;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using CFX::CitizenFX.Core.UI;
using CFX::System.Drawing;
using Sprite = NativeUI.Sprite;

namespace FiveMForgeClient.View.UI.Hud
{
  public class CharacterInformation : Base
  {
    private Sprite Background;
    private List<Text> Lines = new();

    public CharacterInformation()
    {
      Visible = false;
      Initialize();
    }

    private void Initialize()
    {
      var name = new Text($"Name Hans", PointF.Empty, 1.0f);
      Lines.Add(name);
      var age = new Text("Age 52", PointF.Empty, 1.0f);
      Lines.Add(age);
      var job = new Text("Job: ", PointF.Empty, 1.0f);
      Lines.Add(job);
      var walletMoney = new Text("Wallet Amount", PointF.Empty, 1.0f);
      Lines.Add(walletMoney);
      var bankMoney = new Text("Bank Amount", PointF.Empty, 1.0f);
      Lines.Add(bankMoney);
    }

    public override void Update()
    {
      throw new System.NotImplementedException();
    }

    public override void Draw()
    {
      if (Visible)
      {
        for (int i = 0; i < Lines.Count(); i++)
        {
          Lines[i].Draw();
        }
      }
    }
  }
}