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
    using System.Drawing.Imaging;
    using Wrapper;

    public class Map : IMap
    {

        public virtual List<ICase> grid { get; set; }
        public virtual IMapStrategy mapStrategy { get; set; }
        public virtual ICase SelectedCase { get; set; }
        public virtual IUnit SelectedUnit { get; set; }
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

        #region MapDisplay
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

                if (grid[i].UnderUnitMoveRange)
                {
                    Pen green = new Pen(Color.Green, 2);
                    if (i + 1 < grid.Count)
                        if (!grid[i + 1].UnderUnitMoveRange)
                            e.Graphics.DrawLine(green, x + 49, y, x + 49, y + 50);
                    if (i - 1 >= 0)
                        if (!grid[i - 1].UnderUnitMoveRange)
                            e.Graphics.DrawLine(green, x+1, y, x+1, y + 50);
                    if (i + mapStrategy.width < grid.Count)
                        if (!grid[i + mapStrategy.width].UnderUnitMoveRange)
                            e.Graphics.DrawLine(green, x, y + 49, x + 50, y + 49);
                    if (i - mapStrategy.width >= 0)
                        if (!grid[i - mapStrategy.width].UnderUnitMoveRange)
                            e.Graphics.DrawLine(green, x, y+1, x + 50, y+1);
                }

                if (grid[i].CitySuggestion)
                {
                    Pen gold = new Pen(Color.Gold, 2);
                    e.Graphics.DrawRectangle(gold, x + 1, y + 1, 48, 48);

                    //Affichage de la proposition de ville
                    ColorMatrix matrix = new ColorMatrix();
                    matrix.Matrix33 = 0.5f; //opacity 0 = completely transparent, 1 = completely opaque

                    ImageAttributes attributes = new ImageAttributes();
                    attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

                    e.Graphics.DrawImage(FWimages.getCityImage((int)currPlayer.Color), new Rectangle(x, y, 50, 50), 0, 0, 50, 50, GraphicsUnit.Pixel, attributes);
                }

                //Surligne une case ennemie
                if (grid[i].UnderUnitAttackRange)
                {
                    Pen royalBlue = new Pen(Color.RoyalBlue, 2);
                    e.Graphics.DrawRectangle(royalBlue, x + 1, y + 1, 48, 48);
                }

                //Surligne une case ennemie
                if (grid[i].EnemyInRange)
                {
                    Pen red = new Pen(Color.Red, 2);
                    e.Graphics.DrawRectangle(red, x + 1, y + 1, 48, 48);
                }
            }
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
        #endregion

        #region Actions
        public void select(int x, int y)
        {
            if (SelectedUnit != null)
                SelectedUnit = null;

            if (SelectedCase != null)
            {
                SelectedCase.Selected = false;
                SelectedCase = null;
            }


            int x_pos = x / 50;
            int y_pos = mapStrategy.width * (y / 50);
            grid[x_pos + y_pos].Selected = true;
            SelectedCase = grid[x_pos + y_pos];

            if (SelectedCase.Units.Count != 0)
                SelectedUnit = SelectedCase.Units.First();
            else
                SelectedUnit = null;
        }

        //Cheks if the clicked square is within unit range
        //if yes, move unit to it
        //then selects the square
        public void moveTo(int x, int y)
        {
            //Position de la case cliquée sur la map
            int x_pos = x / 50;
            int y_pos = mapStrategy.width * (y / 50);

            if (grid[x_pos + y_pos].UnderUnitMoveRange)
                SelectedUnit.move(grid[x_pos + y_pos]);
            
            foreach (ICase c in grid)
                c.UnderUnitMoveRange = false;

            //S'il y a une case de sélectionnée, on la désélectionne
            if (SelectedCase != null)
                SelectedCase.Selected = false;

            //On séléctionne la nouvelle case
            grid[x_pos + y_pos].Selected = true;
            SelectedCase = grid[x_pos + y_pos];

            if (SelectedCase.Units.Count != 0)
                SelectedUnit = SelectedCase.Units.First();
            else
                SelectedUnit = null;
        }

        public void attack(int x, int y)
        {
            //Position de la case cliquée sur la map
            int x_pos = x / 50;
            int y_pos = mapStrategy.width * (y / 50);

            //TODO
            if (grid[x_pos + y_pos].EnemyInRange)
                ((IStudent)SelectedUnit).attack(grid[x_pos + y_pos]);

            foreach (ICase c in grid)
            {
                c.UnderUnitMoveRange = false;
                c.UnderUnitAttackRange = false;
                c.EnemyInRange = false;
            }

            //S'il y a une case de sélectionnée, on la désélectionne
            if (SelectedCase != null)
                SelectedCase.Selected = false;

            //On séléctionne la nouvelle case
            grid[x_pos + y_pos].Selected = true;
            SelectedCase = grid[x_pos + y_pos];

            if (SelectedCase.Units.Count != 0)
                SelectedUnit = SelectedCase.Units.First();
        }

        //Cheks if the clicked square is within unit range and if a city can be built in it
        //if yes, move unit to it and build City
        //then selects the square
        public void buildCity(int x, int y, IPlayer currPlayer, String name)
        {
            //Position de la case cliquée sur la map
            int x_pos = x / 50;
            int y_pos = mapStrategy.width * (y / 50);

            if (grid[x_pos + y_pos].UnderUnitMoveRange)
            {
                if (grid[x_pos + y_pos].Units.Count != 0)
                {
                    if (grid[x_pos + y_pos].Units.First().Player.Color == currPlayer.Color)
                    {
                        SelectedUnit.move(grid[x_pos + y_pos]);
                        ((ITeacher)SelectedUnit).createCity(currPlayer, name);
                    }
                }
                else
                {
                    SelectedUnit.move(grid[x_pos + y_pos]);
                    ((ITeacher)SelectedUnit).createCity(currPlayer, name);
                }
            }


            foreach (ICase c in grid)
            {
                c.UnderUnitMoveRange = false;
                c.CitySuggestion = false;
            }

            //S'il y a une case de sélectionnée, on la désélectionne
            if (SelectedCase != null)
                SelectedCase.Selected = false;
            
            //On séléctionne la nouvelle case
            grid[x_pos + y_pos].Selected = true;
            SelectedCase = grid[x_pos + y_pos];

            if (SelectedCase.Units.Count != 0)
                SelectedUnit = SelectedCase.Units.First();
            else
                SelectedUnit = null;
        }
        #endregion

        #region UnitsZones

        //Affiche la distance de parcours possible de l'unité
        public virtual void drawMoveBorders(IPlayer currPlayer)
        {
            int i = SelectedCase.SqPos[0] + mapStrategy.width * SelectedCase.SqPos[1];
            for (int k = -SelectedUnit.MovePoints; k <= SelectedUnit.MovePoints; k++)
            {
                //Tests en cas de bord de map horizontal
                bool cond1 = ((k <= 0) || ((i % mapStrategy.width) + k) < mapStrategy.width);
                bool cond2 = ((k >= 0) || ((i % mapStrategy.width) + k) >= 0);
                if (cond1 && cond2)
                {
                    for (int l = -SelectedUnit.MovePoints + Math.Abs(k); l <= SelectedUnit.MovePoints - Math.Abs(k); l++)
                    {
                        //Test en cas de bord de map vertical, la case à éclairer est-elle dans le tableau ?
                        if ((i + k + mapStrategy.width * l >= 0) && (i + k + mapStrategy.width * l < grid.Count))
                            if (grid[i + k + mapStrategy.width * l].Units.Count != 0)
                            {
                                if (grid[i + k + mapStrategy.width * l].Units.First().Player.Color == currPlayer.Color)
                                    grid[i + k + mapStrategy.width * l].UnderUnitMoveRange =
                                        pathFinder(SelectedCase.SqPos[0], SelectedCase.SqPos[1], SelectedUnit.MovePoints, SelectedCase.SqPos[0] + k,SelectedCase.SqPos[1]+ l, currPlayer);
                            }
                            else
                                grid[i + k + mapStrategy.width * l].UnderUnitMoveRange =
                                    pathFinder(SelectedCase.SqPos[0], SelectedCase.SqPos[1], SelectedUnit.MovePoints, SelectedCase.SqPos[0] + k,SelectedCase.SqPos[1]+ l, currPlayer);
                    }
                }
            }
        }

        //Affiche la portée de l'unité
        public virtual void drawAttackBorders()
        {
            int i = SelectedCase.SqPos[0] + mapStrategy.width * SelectedCase.SqPos[1];
            for (int k = -SelectedUnit.AttackRange; k <= SelectedUnit.AttackRange; k++)
            {
                //Tests en cas de bord de map horizontal
                bool cond1 = ((k <= 0) || ((i % mapStrategy.width) + k) < mapStrategy.width);
                bool cond2 = ((k >= 0) || ((i % mapStrategy.width) + k) >= 0);
                if (cond1 && cond2)
                {
                    for (int l = -SelectedUnit.AttackRange + Math.Abs(k); l <= SelectedUnit.AttackRange - Math.Abs(k); l++)
                    {
                        //Test en cas de bord de map vertical, la case à éclairer est-elle dans le tableau ?
                        if ((i + k + mapStrategy.width * l >= 0) && (i + k + mapStrategy.width * l < grid.Count))
                            grid[i + k + mapStrategy.width * l].UnderUnitAttackRange = true;
                    }
                }
            }
        }

        //Entoure les ennemis à portée
        public virtual void circleEnemies(IPlayer currPlayer)
        {
            int i = SelectedCase.SqPos[0] + mapStrategy.width * SelectedCase.SqPos[1];
            for (int k = -SelectedUnit.AttackRange; k <= SelectedUnit.AttackRange; k++)
            {
                //Tests en cas de bord de map horizontal
                bool cond1 = ((k <= 0) || ((i % mapStrategy.width) + k) < mapStrategy.width);
                bool cond2 = ((k >= 0) || ((i % mapStrategy.width) + k) >= 0);
                if (cond1 && cond2)
                {
                    for (int l = -SelectedUnit.AttackRange + Math.Abs(k); l <= SelectedUnit.AttackRange - Math.Abs(k); l++)
                    {
                        //Test en cas de bord de map vertical + comparaison unité
                        if ((i + k + mapStrategy.width * l >= 0) && (i + k + mapStrategy.width * l < grid.Count) && (grid[i + k + mapStrategy.width * l].Units.Count != 0))
                            if (grid[i + k + mapStrategy.width * l].Units.First().Player.Color != currPlayer.Color)
                                grid[i + k + mapStrategy.width * l].EnemyInRange = true;
                    }
                }
            }
        }

        //Affiche les endroits proposés pour construire une ville
        public virtual void drawCityPossibilities(IPlayer currPlayer)
        {
            //Variables food/minerals du tour
            Dictionary<int, int> caseValue = new Dictionary<int, int>();

            int i = SelectedCase.SqPos[0] + mapStrategy.width * SelectedCase.SqPos[1];
            for (int k = -5; k <= 5; k++)
            {
                //Tests en cas de bord de map horizontal
                bool cond1 = ((k <= 0) || ((i % mapStrategy.width) + k) < mapStrategy.width);
                bool cond2 = ((k >= 0) || ((i % mapStrategy.width) + k) >= 0);
                if (cond1 && cond2)
                {
                    for (int l = -5 + Math.Abs(k); l <= 5 - Math.Abs(k); l++)
                    {
                        //Test en cas de bord de map vertical + comparaison unité
                        if ((i + k + mapStrategy.width * l >= 0) && (i + k + mapStrategy.width * l < grid.Count))
                        {
                            //Calcul du nombre de points de la case
                            int value = 0;
                            int pos = i + k + mapStrategy.width * l;
                            if (grid[pos].Visible)
                            {
                                if (grid[pos].City == null)
                                {
                                    if (grid[pos].Units.Count != 0)
                                    {
                                        if (grid[pos].Units.First().Player.Color == currPlayer.Color)
                                            value += 25 * grid[pos].Units.Count;
                                    }
                                    else
                                    {
                                        //Check de la valeur des ressources aux alentours
                                        for (int k2 = -3; k2 <= 3; k2++)
                                        {
                                            //Tests en cas de bord de map horizontal
                                            bool cond11 = ((k2 <= 0) || ((i % mapStrategy.width) + k2) < mapStrategy.width);
                                            bool cond21 = ((k2 >= 0) || ((i % mapStrategy.width) + k2) >= 0);
                                            if (cond1 && cond2)
                                            {
                                                for (int l2 = -3 + Math.Abs(k2); l2 <= 3 - Math.Abs(k2); l2++)
                                                {
                                                    //Test en cas de bord de map vertical, la case à éclairer est-elle dans le tableau ?
                                                    if ((i + k2 + mapStrategy.width * l2 >= 0) && (i + k2 + mapStrategy.width * l2 < grid.Count))
                                                    {
                                                        value += 10 * grid[pos].Minerals;
                                                        value += 10 * grid[pos].Foods;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                value += 10 * grid[pos].Minerals;
                                value += 10 * grid[pos].Foods;
                            }
                            //Ajout au dictionnaire de points
                            caseValue.Add(pos, value);
                        }
                    }
                }
            }
            for (int nb = 0; nb < Math.Min(3, caseValue.Count); nb++)
            {
                int maxValue = caseValue.Values.Max();
                foreach (var c in caseValue)
                {
                    if (c.Value >= maxValue)
                    {
                        grid[c.Key].CitySuggestion = true;
                        caseValue.Remove(c.Key);
                        break;
                    }

                }
            }
        }
        #endregion

        //TODO supprimer si non fonctionnel
        public bool pathFinder(int posX, int posY, int mp, int destX, int destY, IPlayer currPlayer)
        {
            if (grid[posX + mapStrategy.width * posY].Units.Count != 0)
                if (grid[posX + mapStrategy.width * posY].Units.First().Player.Color != currPlayer.Color)
                    return false;
            if (mp <= 0 && ((destX != posX) || (destY != posY)))
                return false;
            if (mp >= 0 && (destX == posX) && (destY == posY))
                return true;
            if (mp >= 0 && ((destX != posX) || (destY != posY)))
            {
                bool way1 = (pathFinder(posX + 1, posY, mp-1, destX, destY, currPlayer));
                bool way2 = (pathFinder(posX, posY + 1, mp-1, destX, destY, currPlayer));
                bool way3 = (pathFinder(posX - 1, posY, mp-1, destX, destY, currPlayer));
                bool way4 = (pathFinder(posX, posY - 1, mp-1, destX, destY, currPlayer));
                return (way1 || way2 || way3 || way4);
            }
            return false;
        }
    }
}

