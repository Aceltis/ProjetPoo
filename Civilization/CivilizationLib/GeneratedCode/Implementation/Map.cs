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
        public virtual IMapStrategy mapStrategy { get; set; }
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


        public void createMap(Queue<IPlayer> playersqueue)
        {
            mapStrategy.createMap(grid, playersqueue);
        }

        //Change la propriété Visible suivant le joueur qui est en train de jouer
        private void updateVisibility(IPlayer currPlayer)
        {
            foreach (Case square in grid)
                square.Visible = false;

            for (int i = 0; i < grid.Count; i++)
            {
                if (grid[i].City != null)
                    if (grid[i].City.Player.Color == currPlayer.Color)
                    {
                        for (int k = -3; k < 4; k++)
                        {
                            //Tests en cas de bord de map horizontal
                            bool cond1 = ((k <= 0) || ((i % mapStrategy.width) + k) < mapStrategy.width);
                            bool cond2 = ((k >= 0) || ((i % mapStrategy.width) + k) >= 0);
                            if (cond1 && cond2)
                            {
                                for (int l = -3 + Math.Abs(k); l < 4 - Math.Abs(k); l++)
                                {
                                    if ((i + k + mapStrategy.width * l >= 0) && (i + k + mapStrategy.width * l < grid.Count))
                                        grid[i + k + mapStrategy.width * l].Visible = true;
                                }
                            }
                        }
                    }
                    else grid[i].Visible = false;

                if (grid[i].Units.Count != 0)
                    if (grid[i].Units.First().Player.Color == currPlayer.Color)
                    {
                        for (int k = -2; k < 3; k++)
                        {
                            //Tests en cas de bord de map horizontal
                            bool cond1 = ((k <= 0) || ((i % mapStrategy.width) + k) < mapStrategy.width);
                            bool cond2 = ((k >= 0) || ((i % mapStrategy.width) + k) >= 0);
                            if (cond1 && cond2)
                            {
                                for (int l = -2 + Math.Abs(k); l < 3 - Math.Abs(k); l++)
                                {
                                    //Test en cas de bord de map vertical, la case à éclairer est-elle dans le tableau ?
                                    if ((i + k + mapStrategy.width * l >= 0) && (i + k + mapStrategy.width * l<grid.Count))
                                        grid[i + k + mapStrategy.width * l].Visible = true;
                                }
                            }
                        }
                    }
                    else grid[i].Visible = false;
            }
        }

        public void afficher(object sender, PaintEventArgs e, IPlayer currPlayer)
        {
            updateVisibility(currPlayer);
            
            //Affichage par case, l'ordre d'appel définit la priorité d'affichage des différents logos
            for (int i = 0; i < grid.Count; i++)
            {
                int x = 50 * grid[i].SqPos[0];
                int y = 50 * grid[i].SqPos[1];
                grid[i].afficher(sender, e, FWimages);

                //Affichage de la ville
                if (grid[i].City != null && grid[i].Visible)
                    grid[i].City.afficher(sender, e, FWimages, x, y);

                //Affichage des unités
                if (grid[i].Visible)
                    foreach (IUnit unit in grid[i].Units)
                        unit.afficher(sender, e, FWimages, x, y);

                //Surligne la case
                if (grid[i].Selected)
                {
                    Pen brown = new Pen(Color.SaddleBrown, 2);
                    e.Graphics.DrawRectangle(brown, x + 1, y + 1, 48, 48);
                }
            }
        }

        public void select(int x, int y)
        {
            foreach (Case c in grid)
                c.Selected = false;
            int x_pos = x / 50;
            int y_pos = y / 50;
            grid[x_pos + mapStrategy.width * y_pos].Selected = true;
        }

        //Affiche la map complète : fin de partie
        public virtual void reveal(object sender, PaintEventArgs e)
        {
            foreach (Case square in grid)
                square.Visible = true;

            //Affichage par case, l'ordre d'appel définit la priorité d'affichage des différents logos
            for (int i = 0; i < grid.Count; i++)
            {
                int x = 50 * grid[i].SqPos[0];
                int y = 50 * grid[i].SqPos[1];
                grid[i].afficher(sender, e, FWimages);

                //Affichage de la ville
                if (grid[i].City != null)
                    grid[i].City.afficher(sender, e, FWimages, x, y);

                //Affichage des unités
                foreach (IUnit unit in grid[i].Units)
                    unit.afficher(sender, e, FWimages, x, y);

                //Surligne la case
                if (grid[i].Selected)
                {
                    Pen brown = new Pen(Color.SaddleBrown, 2);
                    e.Graphics.DrawRectangle(brown, x + 1, y + 1, 48, 48);
                }
            }
        }
    }
}

