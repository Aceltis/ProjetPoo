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

    public class StudentEII : Unit, IStudent
    {
        public StudentEII(IPlayer p, ICase c)
        {
            MovePoints = 2;
            AttackPoints = 3;
            DefensePoints = 3;
            HP = 10;
            Player = p;
            Case = c;
        }

        public virtual void attack()
        {
            throw new System.NotImplementedException();
        }
    }
}

