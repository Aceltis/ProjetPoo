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

    public class Iron : CaseDecorator, IIron
    {
        public virtual int additional_minerals
        {
            get;
            set;
        }

        public virtual void addMinerals(int additional_minerals)
        {
            throw new System.NotImplementedException();
        }

        public override void removeUnit(int unit_id)
        {
            throw new System.NotImplementedException();
        }

        public override void afficher(int x, int y)
        {
            throw new System.NotImplementedException();
        }

    }
}
