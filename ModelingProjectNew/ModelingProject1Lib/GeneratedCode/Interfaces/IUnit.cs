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

	public interface IUnit 
	{
        //Getters et Setters d'attributs
        int HP {get;set;}
        int attackPoints {get;set;}
        int defensePoints {get;set;}
        int pos_x {get;set;}
        int pos_y {get;set;}
        int movePoints {get;set;}
        int cost {get;set;}
        int id {get;set;}
        int creationTime {get;set;}

        //Méthodes
		void move(int x, int y);
		void passTurn();
		void defend();
	}
}
