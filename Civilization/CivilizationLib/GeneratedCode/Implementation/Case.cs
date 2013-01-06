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

    public class Case : ICase
    {
        

        public Case()
        {
            units = new List<IUnit>();
            Visible = true;
        }

        public Case(Case caseToCopy)
        {

        }

        //Attributs
        public virtual int[] sqPos { get; set; }
        public virtual int minerals { get; set; }
        public virtual int food { get; set; }
        public virtual List<IUnit> units { get; set; }
        public virtual ICity city { get; set; }
        public virtual bool Selected { get; set; }
        public virtual bool Visible { get; set; }

        //Méthodes
        public virtual void afficher(object sender, PaintEventArgs e, ICaseImageFlyweight fw)
        {
            throw new System.NotImplementedException();
        }

        public virtual void addUnit(IUnit unit)
        {
            units.Add(unit);
        }

        public virtual void removeUnit(int unit_id)
        {
            foreach (IUnit unit in units)
            {
                if (unit.Id == unit_id)
                    units.Remove(unit);
            }
        }
    }
}

