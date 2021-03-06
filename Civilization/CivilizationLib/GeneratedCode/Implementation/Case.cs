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
    using MVVM;

    public class Case : ObservableObject, ICase
    {
        public Case()
        {
            Units = new List<IUnit>();
            Visible = false;
            UnderUnitMoveRange = false;
            UnderUnitAttackRange = false;
        }

        public Case(Case caseToCopy)
        {

        }

        //Attributs
        public virtual int[] SqPos { get; set; }

        private int _minerals;
        public virtual int Minerals
        {
            get { return this._minerals; }
            set { this.SetAndNotify(ref this._minerals, value, () => this._minerals); }
        }

        private int _foods;
        public virtual int Foods
        {
            get { return this._foods; }
            set { this.SetAndNotify(ref this._foods, value, () => this._foods); }
        }

        public virtual List<IUnit> Units { get; set; }
        public virtual ICity City { get; set; }
        public virtual bool Selected { get; set; }
        public virtual bool Visible { get; set; }
        public virtual bool UnderUnitMoveRange { get; set; }
        public virtual bool UnderUnitAttackRange { get; set; }
        public virtual bool EnemyInRange { get; set; }
        public virtual bool CitySuggestion { get; set; }

        //Méthodes
        public virtual void afficher(object sender, PaintEventArgs e, ICaseImageFlyweight fw)
        {
            throw new System.NotImplementedException();
        }

        public virtual void addUnit(IUnit unit)
        {
            //TODO implém ici l'ajout à la base d'unités d'un joueur
            Units.Add(unit);
        }

        public virtual void removeUnit(IUnit unit)
        {
                    Units.Remove(unit);
                    if (unit.Player.Teachers.Find(x => x == unit) != null)
                        unit.Player.Teachers.Remove(unit.Player.Teachers.Find(x => x == unit));
                    if (unit.Player.Students.Find(x => x == unit) != null)
                        unit.Player.Students.Remove(unit.Player.Students.Find(x => x == unit));
                    if (unit.Player.Boss == unit)
                    {
                        unit.Player.Boss = null;
                        foreach (IStudent u in unit.Player.Students)
                        {
                            u.BossBonus = 1;
                        }
                        foreach (ITeacher u in unit.Player.Teachers)
                        {
                            u.BossBonus = 1;
                        }
                    }
        }
    }
}

