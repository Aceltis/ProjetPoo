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
        public virtual string Pseudo { get; set; }
        public virtual CivilizationType Civilization { get; set; }
        public virtual List<ICity> Cities { get; set; }
        public virtual IBoss Boss { get; set; }
        public virtual List<IStudent> Students { get; set; }
        public virtual List<ITeacher> Teachers { get; set; }
        public virtual StatusType Status { get; set; }
        public virtual Implementation.PlayerColor Color { get; set; }

        public Player(String name, String Civ, Implementation.PlayerColor col)
        {
            if (Civ == "INFO")
            {
                Civilization = CivilizationType.INFO;

                // Il me faut les positions de départ ! :)
                Teachers = new List<ITeacher>();
                Teachers.Add(new TeacherINFO(this, new Case()));

                Students = new List<IStudent>();
                Students.Add(new StudentINFO(this, new Case()));
            }
            else
            {
                Civilization = CivilizationType.EII;

                Teachers = new List<ITeacher>();
                Teachers.Add(new TeacherEII(this, new Case()));

                Students = new List<IStudent>();
                Students.Add(new StudentEII(this, new Case()));
            }

            Pseudo = name;
            Cities = new List<ICity>();
            Status = StatusType.InGame;
            Color = col;
            Boss = null;
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

        public virtual void produceUnits(ICity city, IUnit unit)
        {
            throw new System.NotImplementedException();
        }

    }
}

