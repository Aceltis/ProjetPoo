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

	public class CaseFactory : ICaseFactory
	{

        private Dictionary<int, Case> mapCases;

        public CaseFactory()
        {
            mapCases.Add(0, new Mountain());
            mapCases.Add(1, new Plain());
            mapCases.Add(2, new Desert());
        }

        public Case getCase(int key)
        {
            return (mapCases[key]);
        }
	}
}

