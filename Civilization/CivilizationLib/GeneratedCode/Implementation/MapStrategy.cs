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

	public abstract class MapStrategy : IMapStrategy
	{
        public int height { get; set; }
        public int width { get; set; }

        public virtual void createMap(List<ICase> map, Queue<IPlayer> players)
		{
			throw new System.NotImplementedException();
		}

	}
}

