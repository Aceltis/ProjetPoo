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

	public class TeacherINFO : Unit, ITeacher
	{
        public TeacherINFO()
        {
            movePoints = 3;
            attackPoints = 0;
            defensePoints = 1;
            HP = 1;
        }

		public virtual void createCity()
		{
			throw new System.NotImplementedException();
		}

		public override void move()
		{
			throw new System.NotImplementedException();
		}

		public override void passTurn()
		{
			throw new System.NotImplementedException();
		}

		public override void defend()
		{
			throw new System.NotImplementedException();
		}

	}
}

