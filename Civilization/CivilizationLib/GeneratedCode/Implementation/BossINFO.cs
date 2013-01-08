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

	public class BossINFO : Unit, IBoss
	{
        public BossINFO() { }

        public BossINFO(IPlayer p, ICase c)
        {
            AttackRange = 1;
            MaxMovePoints = 2;
            MovePoints = 2;
            AttackPoints = 4;
            DefensePoints = 2;
            HP = 10;
            MaxHP = 10;
            Player = p;
            Case = c;
            CreationTime = 5;
            Cost = 200;
            BossBonus = 1;
        }

        public override int AttackPoints
        {
            get { return (int)(this._attackPoints); }
            set { this.SetAndNotify(ref this._attackPoints, value, () => this._attackPoints); }
        }

        public override int DefensePoints
        {
            get { return (int)(this._defensePoints); }
            set { this.SetAndNotify(ref this._defensePoints, value, () => this._defensePoints); }
        }

        public override void move(ICase destination)
        {
            //Le boss part d'une case, les unités de cette case perdent le bonus
            foreach (IUnit u in destination.Units)
                u.BossBonus = 1;

            MovePoints -= Math.Abs(destination.SqPos[0] - Case.SqPos[0]);
            MovePoints -= Math.Abs(destination.SqPos[1] - Case.SqPos[1]);
            Case.Units.Remove(this);
            Case = destination;
            destination.Units.Add(this);

            //Le boss arrive à une case, les unités gagnent le bonus
            foreach (IUnit u in destination.Units)
                u.BossBonus = 1.5;

            //Si l'unité a pu se déplacer sur une ville, c'est qu'elle est vide -> il la capture
            if (destination.City != null)
                if (destination.City.Player.Color != Player.Color)
                    destination.City.changeOwner(Player);
        }

        public override void afficher(object sender, PaintEventArgs e, ICaseImageFlyweight fw, int x, int y)
        {
            if (Case.Units.OfType<StudentEII>() != null || Case.Units.OfType<BossEII>() != null)
            {
                e.Graphics.DrawImage(fw.getUnitImage(0, Player.Color), x + 6, y + 6, 38, 38);
            }
        }
	}
}

