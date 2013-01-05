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
        IPlayer currentPlayer;
        Object selectedItem;

        public GameWindow(IGameBuilder builder)
        {
            InitializeComponent();
            game = builder.build();
            createPVM(game);
            createGVM(game);

            beginTurn(pToPvm[game.Players[1]], gvm);

            drawMap(game);
        }
        
        private void drawMap(IGame game)
        {
            System.Windows.Forms.PictureBox pictureBox = new System.Windows.Forms.PictureBox();
            pictureBox.Width = (int)Math.Sqrt((double)game.Map.grid.Count) * 50; pictureBox.Height = (int)Math.Sqrt((double)game.Map.grid.Count) * 50;
            pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(game.Map.afficher);
            System.Windows.Forms.ScrollableControl sc = new System.Windows.Forms.ScrollableControl();
            sc.Controls.Add(pictureBox);
            sc.AutoScroll = true;
            windowsFormsHost1.Child = sc;
        }

        private void beginTurn(PlayerViewModel p, GameViewModel g)
        {
            top.DataContext = p;
            turnsView.DataContext = g;

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

        private void nextTurn()
        {
            
        }

        private void createPVM(IGame g)
        {
            pToPvm = new Dictionary<IPlayer, PlayerViewModel>();
            pToPvm.Add(g.Players[1], new PlayerViewModel(g.Players[1]));
            pToPvm.Add(g.Players[2], new PlayerViewModel(g.Players[2]));

            if (g.Players.Count() == 3)
            {
                pToPvm.Add(g.Players[3], new PlayerViewModel(g.Players[3]));
            }
            else if (g.Players.Count() == 4)
            {
                pToPvm.Add(g.Players[3], new PlayerViewModel(g.Players[3]));
                pToPvm.Add(g.Players[4], new PlayerViewModel(g.Players[4]));
            }
        }

        private void createGVM(IGame g)
        {
            gvm = new GameViewModel(g);
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
    }
}