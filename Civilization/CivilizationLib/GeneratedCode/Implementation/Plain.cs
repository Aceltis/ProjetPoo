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
            e.Graphics.DrawImage(fw.getCaseImage(1), 50 * sqPos[0], 50 * sqPos[1], 50, 50);
        }
    }
}

