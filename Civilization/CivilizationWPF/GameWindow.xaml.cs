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

namespace CivilizationWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {

        IGame game;
        GameViewModel gvm;
        Dictionary<IPlayer, PlayerViewModel> pToPvm;
        Dictionary<IPlayer, Dictionary<ICity, CityViewModel>> cToCvm;

        public GameWindow(IGameBuilder builder)
        {
            InitializeComponent();
            game = builder.build();
            createPVM();
            createGVM();

            beginTurn();

            drawMap();
        }
        
        private void drawMap()
        {
            System.Windows.Forms.PictureBox pictureBox = new System.Windows.Forms.PictureBox();
            pictureBox.Width = (int)Math.Sqrt((double)game.Map.grid.Count) * 50; pictureBox.Height = (int)Math.Sqrt((double)game.Map.grid.Count) * 50;
            pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(game.Map.afficher);
            System.Windows.Forms.ScrollableControl sc = new System.Windows.Forms.ScrollableControl();
            sc.Controls.Add(pictureBox);
            sc.AutoScroll = true;
            windowsFormsHost1.Child = sc;
        }

        private void beginTurn()
        {
            top.DataContext = pToPvm[game.CurrentPlayer];
            turnsView.DataContext = gvm;

            /*
            if (selectedItem == typeof(ICase))
            {
            }
            else if (selectedItem == typeof(ICity))
            {
            }
            else
            {
                //unit
            } */
        }

        private void nextTurn(object sender, RoutedEventArgs e)
        {
            // Is there a winner among all players ?
            if (game.isWinner())
            {
                endGame();
            }

            game.nextPlayer();

            // Add a turn to the game
            int turns = game.Turns++;
            gvm.Turns = turns.ToString();

            beginTurn();
        }

        private void endGame()
        {
            System.Windows.MessageBox.Show(game.CurrentPlayer.Pseudo + " wins!", "End Game", MessageBoxButton.OK);
        }

        private void createPVM()
        {
            pToPvm = new Dictionary<IPlayer, PlayerViewModel>();
            pToPvm.Add(game.CurrentPlayer, new PlayerViewModel(game.CurrentPlayer));

            IPlayer second = game.Players.Dequeue();
            pToPvm.Add(second, new PlayerViewModel(second));
            game.Players.Enqueue(second);

            if (game.Players.Count() == 2)
            {
                IPlayer third = game.Players.Dequeue();
                pToPvm.Add(third, new PlayerViewModel(third));
                game.Players.Enqueue(third);
            }
            else if (game.Players.Count() == 3)
            {
                IPlayer third = game.Players.Dequeue();
                pToPvm.Add(third, new PlayerViewModel(third));
                game.Players.Enqueue(third);
                IPlayer fourth = game.Players.Dequeue();
                pToPvm.Add(fourth, new PlayerViewModel(fourth));
                game.Players.Enqueue(fourth);
            }
        }

        private void createGVM()
        {
            gvm = new GameViewModel(game);
        }

        private void quitGame(object sender, RoutedEventArgs e)
        {
            Application.Current.Windows[0].Close();
        }

        private void newAction(object sender, RoutedEventArgs e)
        {
        }

        private void prodStudent(object sender, RoutedEventArgs e)
        {
            prodBossView.IsChecked = false;
            prodTeacherView.IsChecked = false;
            prodStudentView.IsChecked = true;
            timerStudentView.Visibility = Visibility.Visible;
            timerTeacherView.Visibility = Visibility.Hidden;
            timerBossView.Visibility = Visibility.Hidden;
        }
        private void prodBoss(object sender, RoutedEventArgs e)
        {
            prodBossView.IsChecked = true;
            prodTeacherView.IsChecked = false;
            prodStudentView.IsChecked = false;
            timerStudentView.Visibility = Visibility.Hidden;
            timerTeacherView.Visibility = Visibility.Hidden;
            timerBossView.Visibility = Visibility.Visible;
        }
        private void prodTeacher(object sender, RoutedEventArgs e)
        {
            prodBossView.IsChecked = false;
            prodTeacherView.IsChecked = true;
            prodStudentView.IsChecked = false;
            timerStudentView.Visibility = Visibility.Hidden;
            timerTeacherView.Visibility = Visibility.Visible;
            timerBossView.Visibility = Visibility.Hidden;
        }

        private void newUnitAction(object sender, RoutedEventArgs e)
        {
        }

        private void newCityAction(object sender, RoutedEventArgs e)
        {
        }
    }
}