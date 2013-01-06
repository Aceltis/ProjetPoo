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
    using System.Windows.Forms;
    using System.Drawing;

    public class Map : IMap
    {

        public virtual List<ICase> grid { get; set; }
        private IMapStrategy mapStrategy;
        private CaseImageFlyweight FWimages;

        public Map()
        {
            grid = new List<ICase>();
            FWimages = new CaseImageFlyweight();
        }

        public void setMapStrategy(IMapStrategy mapStrategy)
        {
            this.mapStrategy = mapStrategy;
        }


        public void createMap()
        {
            mapStrategy.createMap(grid);
        }

        public void afficher(object sender, PaintEventArgs e)
        {
            for (int i = 0; i < grid.Count; i++)
                grid[i].afficher(sender, e, FWimages);
        }

        public void select(int x, int y)
        {
            foreach (Case c in grid)
                c.Selected = false;
            int x_pos = x / 50;
            int y_pos = y / 50;
            grid[x_pos + 25 * y_pos].Selected = true;
        }
    }
}

