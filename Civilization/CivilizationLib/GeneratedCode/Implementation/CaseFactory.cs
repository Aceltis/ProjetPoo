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
		public virtual string errors
		{
			get;
			set;
		}

		public virtual Dictionary<int, Case> mapCases
		{
			get;
			set;
		}

		public virtual IEnumerable<Case> Case
		{
			get;
			set;
		}

		public virtual void createCase()
		{
			throw new System.NotImplementedException();
		}

	}
}
