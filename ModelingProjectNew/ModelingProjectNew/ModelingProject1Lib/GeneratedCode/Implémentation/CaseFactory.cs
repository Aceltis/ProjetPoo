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

	public class CaseFactory : CaseFactory
	{
		private string errors
		{
			get;
			set;
		}

		private Dictionary<int, Case> mapCases
		{
			get;
			set;
		}

		public virtual void createCase()
		{
			throw new System.NotImplementedException();
		}

		public CaseFactory()
		{
		}

	}
}

