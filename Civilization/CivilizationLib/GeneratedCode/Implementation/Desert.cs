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

    public class Desert : Case, IDesert
    {
        public Desert()
        {
            Foods = 0;
            Minerals = 2;
        }

        public override void afficher(object sender, PaintEventArgs e, ICaseImageFlyweight fw)
        {
            //Points qui définissent le coin supérieur gauche de la case
            int x = 50 * SqPos[0];
            int y = 50 * SqPos[1];
            if (Visible)
                e.Graphics.DrawImage(fw.getCaseImage(2), x, y, 50, 50);
            else
                e.Graphics.DrawImage(fw.getFoWCaseImage(2), x, y, 50, 50);
        }
    }
}

