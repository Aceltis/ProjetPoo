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

    public class Player : IPlayer
    {
        public virtual string pseudo
        {
            get;
            set;
        }

        public virtual CivilizationType civilization
        {
            get;
            set;
        }

        public virtual int score
        {
            get;
            set;
        }

        public virtual List<City> cities
        {
            get;
            set;
        }

        public virtual StatusType status
        {
            get;
            set;
        }

        public virtual void chooseCivilization()
        {
            throw new System.NotImplementedException();
        }

        public virtual void endTurn()
        {
            throw new System.NotImplementedException();
        }

        public virtual void move()
        {
            throw new System.NotImplementedException();
        }

        public virtual void passTurn()
        {
            throw new System.NotImplementedException();
        }

        public virtual void attack()
        {
            throw new System.NotImplementedException();
        }

        public virtual void changeCityProduction()
        {
            throw new System.NotImplementedException();
        }

        public virtual void produceUnits(City city, Unit unit)
        {
            throw new System.NotImplementedException();
        }

    }
}
