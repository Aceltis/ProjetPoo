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

	public class Desert : Case, IDesert
	{
        public Desert()
        {
            food = 0;
            minerals = 2;

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

