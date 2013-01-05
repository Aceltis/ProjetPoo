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
        PlayerViewModel pvm1;
        PlayerViewModel pvm2;
        PlayerViewModel pvm3;
        PlayerViewModel pvm4;

        public GameWindow(GameBuilder builder)
        {
            InitializeComponent();
            game = (Game)builder.build();
            createPVM(game);

            beginTurn(pvm1);
            
            System.Windows.Forms.PictureBox pictureBox = new System.Windows.Forms.PictureBox();
            pictureBox.Width = (int)Math.Sqrt((double)game.Map.grid.Count) * 50; pictureBox.Height = (int)Math.Sqrt((double)game.Map.grid.Count) * 50;
            game.Map.afficher(pictureBox);
            System.Windows.Forms.ScrollableControl sc = new System.Windows.Forms.ScrollableControl();
            sc.Controls.Add(pictureBox);
            sc.AutoScroll = true;
            windowsFormsHost1.Child = sc;
        }

        private void beginTurn(PlayerViewModel p)
        {
            top.DataContext = p;
        }

        private void afficherTop(PlayerViewModel pvm)
        {
        }

        private void createPVM(Game g)
        {
            pvm1 = new PlayerViewModel(g.Players[1]);
            pvm2 = new PlayerViewModel(g.Players[2]);

            if (g.Players.Count() == 3)
            {
                pvm3 = new PlayerViewModel(g.Players[3]);
            }
            else if (g.Players.Count() == 4)
            {
                pvm3 = new PlayerViewModel(g.Players[3]);
                pvm4 = new PlayerViewModel(g.Players[4]);
            }
        }

        private void endTurn(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Passage au joueur suivant");
        }

        private void endGame(object sender, RoutedEventArgs e)
        {
            Application.Current.Windows[0].Close();
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