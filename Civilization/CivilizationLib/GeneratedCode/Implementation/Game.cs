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
    using MVVM;

    public class Game : ObservableObject, IGame
    {
        public virtual Queue<IPlayer> Players { get; set; }
        public virtual IPlayer CurrentPlayer { get; set; }
        public virtual IPlayer Winner { get; set; }
        public virtual List<IPlayer> Loosers { get; set; }
        public virtual IMap Map { get; set; }

        private int _turns;
        public virtual int Turns
        {
            get { return this._turns; }
            set { this.SetAndNotify(ref this._turns, value, () => this._turns); }
        }

        public Game(Queue<IPlayer> joueurs, IMap carte)
        {
            Players = joueurs;
            Winner = null;
            Loosers = new List<IPlayer>();
            CurrentPlayer = Players.Dequeue();
            Map = carte;
            Turns = 1;
        }

        public virtual void addLooser(IPlayer player)
        {
            player.Status = StatusType.Spectator;
            Loosers.Add(player);
        }

        public virtual bool isWinner()
        {
            return Winner != null;
        }

        public virtual bool isLooser()
        {
            if ((!CurrentPlayer.builtHisFirstCity) && CurrentPlayer.Cities.Count() == 0 && Players.Count() > 0)
            {
                if (CurrentPlayer.Teachers.Count == 0 && CurrentPlayer.Students.Count == 0 && CurrentPlayer.Boss == null)
                {
                    addLooser(CurrentPlayer);
                    if (Players.Count() == 1)
                        Winner = Players.First();
                    return true;
                }
            }
            if (CurrentPlayer.builtHisFirstCity && CurrentPlayer.Cities.Count() == 0 && Players.Count() > 0)
            {
                addLooser(CurrentPlayer);
                if (Players.Count() == 1)
                    Winner = Players.First();
                return true;
            }
            return false;
        }

        public virtual bool isSpectator(IPlayer p)
        {
            return (p.Status == StatusType.Spectator);
        }

        public virtual void nextPlayer()
        {
            if (isLooser())
            {
                CurrentPlayer = Players.Dequeue();

            }
            else if (isWinner())
            {
                Winner = CurrentPlayer;
            }
            else
            {
                Players.Enqueue(CurrentPlayer);
                CurrentPlayer = Players.Dequeue();
            }      
        }

    }
}

