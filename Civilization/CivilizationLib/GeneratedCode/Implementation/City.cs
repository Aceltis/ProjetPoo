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

    public class City : ICity
    {
        public City(Player p, Case c)
        {
            population = 1;
            position.pos_x = c.pos_x;
            position.pos_y = c.pos_y;
            player = p;
            current_prod = ProductionType.None;
            owned_food = c.food;
            owned_minerals = c.minerals;
        }

        public virtual int population
        {
            get;
            set;
        }

        public virtual Case position
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

        public virtual void produceBoss(Unit unit)
        {
            if (current_prod == ProductionType.None && owned_minerals >= 200)
            {
                current_prod = ProductionType.Boss;

                if (unit.cost == 0)
                {
                    spawnUnit(unit);
                }
            }
        }

        public virtual void produceStudent()
        {
            throw new System.NotImplementedException();
        }

        public virtual void produceTeacher()
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

        public virtual void spawnUnit(Unit unit)
        {
            throw new System.NotImplementedException();
        }

    }
}
