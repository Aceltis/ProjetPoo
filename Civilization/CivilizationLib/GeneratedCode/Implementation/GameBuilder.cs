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

	public class GameBuilder : IGameBuilder
	{
        private Queue<IPlayer> Players;
        public IMap Map { get; set; }

        public GameBuilder(int players, List<String> names, List<String> civs)
        {
            createPlayers(players, names, civs);
            createMap(players);
        }

		public virtual void createMap(int players)
		{
            if (players == 4)
            {
                Map = new Map();
                Map.setMapStrategy(new LargeMapStrategy());
                Map.createMap(this.Players);
            }
            else if (players == 3)
            {
                Map = new Map();
                Map.setMapStrategy(new LargeMapStrategy());
                Map.createMap(this.Players);
            }
            else
            {
                Map = new Map();
                Map.setMapStrategy(new SmallMapStrategy());
                Map.createMap(this.Players);
            }
		}

		public virtual void createPlayers(int players, List<String> names, List<String> civs)
		{
            Players = new Queue<IPlayer>();
            
            //Player-order randomized
            Random rng = new Random();

            if (players == 2)
            {
                int r = rng.Next(2);
                Players.Enqueue(new Player(Map, names[r], civs[r], PlayerColor.Red));
                Players.Enqueue(new Player(Map, names[1 - r], civs[1 - r], PlayerColor.Blue));
            }
            else if (players == 3)
            {
                int r = rng.Next(3);
                Players.Enqueue(new Player(Map, names[r], civs[r], PlayerColor.Red));
                names.RemoveAt(r); civs.RemoveAt(r);
                r = rng.Next(2);
                Players.Enqueue(new Player(Map, names[r], civs[r], PlayerColor.Blue));
                names.RemoveAt(r); civs.RemoveAt(r);
                Players.Enqueue(new Player(Map, names[0], civs[0], PlayerColor.Orange));
            }
            else
            {
                int r = rng.Next(4);
                Players.Enqueue(new Player(Map, names[r], civs[r], PlayerColor.Red));
                names.RemoveAt(r); civs.RemoveAt(r);
                r = rng.Next(3);
                Players.Enqueue(new Player(Map, names[r], civs[r], PlayerColor.Blue));
                names.RemoveAt(r); civs.RemoveAt(r);
                r = rng.Next(3);
                Players.Enqueue(new Player(Map, names[r], civs[r], PlayerColor.Orange));
                names.RemoveAt(r); civs.RemoveAt(r);
                Players.Enqueue(new Player(Map, names[0], civs[0], PlayerColor.Green));
            }
		}

        public virtual IGame build()
        {
            return new Game(Players, Map);
        }

	}
}

