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
using System.Windows.Shapes;
using Wrapper;
using Interfaces;
using Implementation;

namespace CivilizationWPF
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        int players = 0;
        GameBuilder gameBuilder;
        List<String> names;
        List<String> civs;

        public HomeWindow()
        {
            InitializeComponent();
        }

        public void quitGame(Object sender, RoutedEventArgs e)
        {
            Application.Current.Windows[0].Close();
        }

        public void play(Object sender, RoutedEventArgs e)
        {
            names = new List<String>();
            civs = new List<String>();

            if (players == 2)
            {
                names.Add(playerOne.Text);
                civs.Add(civ1.Text);

                names.Add(playerTwo.Text);
                civs.Add(civ2.Text);
            }
            else if (players == 3)
            {
                names.Add(playerOne.Text);
                civs.Add(civ1.Text);

                names.Add(playerTwo.Text);
                civs.Add(civ2.Text);

                names.Add(playerThree.Text);
                civs.Add(civ3.Text);
            }
            else
            {
                names.Add(playerOne.Text);
                civs.Add(civ1.Text);

                names.Add(playerTwo.Text);
                civs.Add(civ2.Text);

                names.Add(playerThree.Text);
                civs.Add(civ3.Text);

                names.Add(playerFour.Text);
                civs.Add(civ4.Text);
            }

            gameBuilder = new GameBuilder(players, names, civs);
            var newWindow = new GameWindow(gameBuilder);
            Application.Current.Windows[0].Close();
            newWindow.Show();
        }

        public void twoPlayers(Object sender, RoutedEventArgs e)
        {
            first_player.Visibility = Visibility.Visible;
            second_player.Visibility = Visibility.Visible;
            third_player.Visibility = Visibility.Hidden;
            fourth_player.Visibility = Visibility.Hidden;
            players = 2;
        }

        public void threePlayers(Object sender, RoutedEventArgs e)
        {
            first_player.Visibility = Visibility.Visible;
            second_player.Visibility = Visibility.Visible;
            third_player.Visibility = Visibility.Visible;
            fourth_player.Visibility = Visibility.Hidden;
            players = 3;
        }

        public void fourPlayers(Object sender, RoutedEventArgs e)
        {
            first_player.Visibility = Visibility.Visible;
            second_player.Visibility = Visibility.Visible;
            third_player.Visibility = Visibility.Visible;
            fourth_player.Visibility = Visibility.Visible;
            players = 4;
        }
    }
}
