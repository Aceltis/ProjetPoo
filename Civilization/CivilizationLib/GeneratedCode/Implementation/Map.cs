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

    public class Map : IMap
    {
        public Map(Game g)
        {
        }

        public virtual int height
        {
            get;
            set;
        }

        public virtual int width
        {
            get;
            set;
        }

        public virtual MapType type
        {
            get;
            set;
        }

        public virtual List<Case> cases
        {
            get;
            set;
        }

        public virtual int map_id
        {
            get;
            set;
        }

        public virtual CaseFactory CaseFactory
        {
            get;
            set;
        }

        public virtual IEnumerable<Case> Case
        {
            get;
            set;
        }

    }
}

