﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Implementation
{
	using Interfaces;
	using System;
	using System.Collections.Generic;
	using System.Linq;
    using System.Text;
    using System.Drawing;
    using System.Windows.Forms;

    public class Plain : Case, IPlain
    {
        public Plain()
        {
            food = 3;
            minerals = 1;
        }

        public override void removeUnit(int unit_id)
        {
            throw new System.NotImplementedException();
        }

        public override void afficher(object sender, PaintEventArgs e, ICaseImageFlyweight fw)
        {
            //Points qui définissent le coin supérieur gauche de la case
            int x = 50 * sqPos[0];
            int y = 50 * sqPos[1];
            e.Graphics.DrawImage(fw.getCaseImage(1), x, y, 50, 50);
            if (Selected)
            {
                e.Graphics.DrawLine(new Pen(Color.SaddleBrown, 1), x, y, x + 49, y);
                e.Graphics.DrawLine(new Pen(Color.SaddleBrown, 1), x + 49, y, x + 49, y + 49);
                e.Graphics.DrawLine(new Pen(Color.SaddleBrown, 1), x + 49, y + 49, x, y + 49);
                e.Graphics.DrawLine(new Pen(Color.SaddleBrown, 1), x, y + 49, x, y);
            }
        }
    }
}

