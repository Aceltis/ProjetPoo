﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Implémentation
{
	using Interfaces;
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public class Case : ICase
	{
		public virtual int minerals
		{
			get;
			set;
		}

		public virtual int pos_x
		{
			get;
			set;
		}

		public virtual int pos_y
		{
			get;
			set;
		}

		public virtual int food
		{
			get;
			set;
		}

		public virtual List<Unit> units
		{
			get;
			set;
		}

		public virtual City city
		{
			get;
			set;
		}

		public virtual void removeUnit(int unit_id)
		{
            foreach (Unit unit in units)
            {
                if (unit.id == unit_id)
                    units.Remove(unit);
            }
		}
        
        public virtual void addUnit(Unit unit)
        {
            units.Add(unit);
        }


		public virtual void afficher(int x, int y)
		{
			throw new System.NotImplementedException();
		}

	}
}

