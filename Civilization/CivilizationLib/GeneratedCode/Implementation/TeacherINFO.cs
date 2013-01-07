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

    public class TeacherINFO : Unit, ITeacher
    {
        public TeacherINFO(IPlayer p, ICase c)
        {
            MovePoints = 3;
            AttackPoints = 0;
            DefensePoints = 1;
            HP = 1;
            Player = p;
            Case = c;
        }

        public virtual void createCity()
        {
            throw new System.NotImplementedException();
        }

        public override void afficher(object sender, PaintEventArgs e, ICaseImageFlyweight fw, int x, int y)
        {
            if (Case.units.OfType<StudentINFO>().Count() != 0)
            {
                if (Case.units.OfType<BossINFO>().Count() != 0)
                    e.Graphics.DrawImage(fw.getUnitImage(8, Player.Color), x + 23, y + 23, 25, 25);
                else
                    e.Graphics.DrawImage(fw.getUnitImage(5, Player.Color), x + 23, y + 23, 25, 25);

                //Affichage du nombre d'unités du même type
                if (Case.units.OfType<TeacherINFO>().Count() > 1)
                {
                    e.Graphics.DrawString(Case.units.OfType<TeacherINFO>().Count().ToString(), new Font("Arial", 7, FontStyle.Bold), new SolidBrush(Color.Black), x + 37, y + 37);
                    e.Graphics.DrawString(Case.units.OfType<TeacherINFO>().Count().ToString(), new Font("Arial", 6), new SolidBrush(Color.White), x + 37, y + 37);
                }
            }
            else
            {
                if (Case.units.OfType<BossINFO>().Count() != 0)
                    e.Graphics.DrawImage(fw.getUnitImage(7, Player.Color), x + 6, y + 6, 38, 38);
                else
                    e.Graphics.DrawImage(fw.getUnitImage(6, Player.Color), x + 6, y + 6, 38, 38);

                //Affichage du nombre d'unités du même type
                if (Case.units.OfType<TeacherINFO>().Count() > 1)
                {
                    e.Graphics.DrawString(Case.units.OfType<TeacherINFO>().Count().ToString(), new Font("Arial", 11, FontStyle.Bold), new SolidBrush(Color.Black), x + 28, y + 28);
                    e.Graphics.DrawString(Case.units.OfType<TeacherINFO>().Count().ToString(), new Font("Arial", 10), new SolidBrush(Color.White), x + 28, y + 28);
                }
            }
        }
    }
}

