﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace Interfaces
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	public interface IPlayer 
	{
        int[] ScreenPos { get; set; }
        string Name { get; set; }
        Implementation.CivilizationType Civilization { get; set; }
        List<ICity> Cities { get; set; }
        IBoss Boss { get; set; }
        List<IStudent> Students { get; set; }
        List<ITeacher> Teachers { get; set; }
        Implementation.StatusType Status { get; set; }
        Implementation.PlayerColor Color { get; set; }


		void chooseCivilization();

		void endTurn();

		void move();

		void passTurn();

		void attack();

		void changeCityProduction();

		void produceUnits(ICity c, IUnit u);

	}
}

