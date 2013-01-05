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

    public class Game : IGame
    {
        public virtual Dictionary<int,IPlayer> Players { get; set; }
        public virtual IPlayer Winner { get; set; }
        public virtual Dictionary<int,IPlayer> Loosers { get; set; }
        public virtual IMap Map { get; set; }
        public virtual int Turns { get; set; }

        public Game(Dictionary<int, IPlayer> joueurs, IMap carte)
        {
            Players = joueurs;
            Winner = null;
            Loosers = new Dictionary<int, IPlayer>();
            Map = carte;
            Turns = 1;
        }

        public virtual void addLooser(IPlayer p)
        {
            throw new System.NotImplementedException();
        }

        public virtual void endGame()
        {
            throw new System.NotImplementedException();
        }

        public virtual void isWinner()
        {
            throw new System.NotImplementedException();
        }

        public virtual void isSpectator(IPlayer p)
        {
            throw new System.NotImplementedException();
        }

        public virtual void startNewTurn()
        {
            throw new System.NotImplementedException();
        }

    }
}

