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

	public class Unit : IUnit
	{
        //Attribut nécessitant une condition ici HP positifs, mais en fait noneed dans le sens où HP nég = mort)
        private int _HP;
        public virtual int HP
        {
            get { return _HP; }
            set { if(value>=0) _HP=value; }
        }

        //Attribut ne nécessitant pas de condition (=> constructeurs C#)
        //private int _attackPoints;
		public virtual int attackPoints
		{
			get;
			set;
		}

        //private int _defense;
        public virtual int defensePoints
		{
			get;
			set;
		}

        //private int _pos_x;
		public virtual int pos_x
		{
			get;
		    set;
		}

        //private int _pos_y;
		public virtual int pos_y
		{
			get;
			set;
		}

        //private int _movePoints;
		public virtual int movePoints
		{
			get;
			set;
		}

        //private int _cost;
		public virtual int cost
		{
			get;
			set;
		}

        //private int _id;
		public virtual int id
		{
			get;
			set;
		}

        //private int _creationTime;
		public virtual int creationTime
		{
			get;
			set;
		}
        
		public virtual void move(int x, int y)
		{
            if (movePoints >= pos_x + pos_y - x - y)
            {
                pos_x = x;
                pos_y = y;
            }
		}

		public virtual void passTurn()
		{
			throw new System.NotImplementedException();
		}

        public virtual void defend()
        {
            throw new System.NotImplementedException();
        }
	}
}

