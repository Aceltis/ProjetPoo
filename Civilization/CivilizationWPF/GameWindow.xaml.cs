using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms.Integration;
using Wrapper;
using Interfaces;
using Implementation;

namespace CivilizationWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        Game game;

        public GameWindow(GameBuilder builder)
        {
            InitializeComponent();

            game = (Game)builder.build();

            System.Windows.Forms.PictureBox pictureBox = new System.Windows.Forms.PictureBox();
            pictureBox.Width = game.map.grid.Count * 2; pictureBox.Height = game.map.grid.Count * 2;
            game.map.afficher(pictureBox);
            windowsFormsHost1.Width = game.map.grid.Count * 2; windowsFormsHost1.Height = game.map.grid.Count * 2;
            windowsFormsHost1.Child = pictureBox;
        }

        private void endTurn(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Passage au joueur suivant");
        }

        private void openMenu(object sender, RoutedEventArgs e)
        {
        }

        private void newAction(object sender, RoutedEventArgs e)
        {
        }

        private void prodStudent(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Enabled");
        }
        private void prodBoss(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Enabled");
        }
        private void prodTeacher(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Enabled");
        }
    }
}