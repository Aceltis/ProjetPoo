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
            food = 0;
            minerals = 2;
            squareImage = Image.FromFile("C:/Users/msi/Documents/GitHub/ProjetPoo/Civilization/CivilizationWPF/Resource/terrains/desert.png");
        }

        public override void removeUnit(int unit_id)
        {
            throw new System.NotImplementedException();
        }

        public override void afficher(object sender, PaintEventArgs e)
        {
            foreach (int[] tabPos in sqPos)
                e.Graphics.DrawImage(squareImage, 50 * tabPos[0], 50 * tabPos[1], 50, 50);
        }
    }
}

