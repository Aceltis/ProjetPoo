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
    using System.Windows.Forms;
    using System.Drawing;

    public class Iron : CaseDecorator, IIron
    {
        public Iron(ICase caseToDecorate) : base(caseToDecorate)
        {
            caseToDecorate.Minerals += 2;
        }

        public override void afficher(object sender, PaintEventArgs e, ICaseImageFlyweight fw)
        {
            if (Visible)
            {
                Case.Visible = true;
                Case.afficher(sender, e, fw);
                e.Graphics.DrawImage(fw.getBonusImage(1), 50 * Case.SqPos[0], 50 * Case.SqPos[1], 50, 50);
            }
            else
            {
                Case.Visible = false;
                Case.afficher(sender, e, fw);
                e.Graphics.DrawImage(fw.getFoWBonusImage(1), 50 * Case.SqPos[0], 50 * Case.SqPos[1], 50, 50);
            }
        }

    }
}

