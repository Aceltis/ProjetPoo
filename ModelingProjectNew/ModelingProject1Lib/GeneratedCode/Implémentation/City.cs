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

	public class City : ICity
	{
		public virtual int population
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

		public virtual Player player
		{
			get;
			set;
		}

		public virtual ProductionType current_prod
		{
			get;
			set;
		}

		public virtual int owned_minerals
		{
			get;
			set;
		}

		public virtual int owned_food
		{
			get;
			set;
		}

		public virtual void produceUnits()
		{
			throw new System.NotImplementedException();
		}

		public virtual void changeProduction()
		{
			throw new System.NotImplementedException();
		}

		public virtual void upgradePopulation()
		{
			throw new System.NotImplementedException();
		}

		public virtual void spawnUnit()
		{
			throw new System.NotImplementedException();
		}

	}
}

