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
using Implementation;
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

        #region Game Initialization
        public GameWindow(IGameBuilder builder)
        {
            InitializeComponent();
            game = builder.build();
            createPVM();
            createGVM();

            initializeDataContext();
            drawMap();
            beginTurn();
        }

        private void initializeDataContext()
        {
            turnsView.DataContext = gvm;
        }

        private void createPVM()
        {
            pToPvm = new Dictionary<IPlayer, PlayerViewModel>();
            pToPvm.Add(game.CurrentPlayer, new PlayerViewModel((Player)game.CurrentPlayer));

            IPlayer second = game.Players.Dequeue();
            pToPvm.Add(second, new PlayerViewModel((Player)second));
            game.Players.Enqueue(second);

            if (game.Players.Count() == 2)
            {
                IPlayer third = game.Players.Dequeue();
                pToPvm.Add(third, new PlayerViewModel((Player)third));
                game.Players.Enqueue(third);
            }
            else if (game.Players.Count() == 3)
            {
                IPlayer third = game.Players.Dequeue();
                pToPvm.Add(third, new PlayerViewModel((Player)third));
                game.Players.Enqueue(third);
                IPlayer fourth = game.Players.Dequeue();
                pToPvm.Add(fourth, new PlayerViewModel((Player)fourth));
                game.Players.Enqueue(fourth);
            }
        }

        private void createGVM()
        {
            gvm = new GameViewModel((Game)game);
        }

        private void quitGame(object sender, RoutedEventArgs e)
        {
            Application.Current.Windows[0].Close();
        }
        #endregion

        #region Game & Turns mecanisms
        private void beginTurn()
        {
            top.DataContext = pToPvm[game.CurrentPlayer];
        }

        private void nextTurn(object sender, RoutedEventArgs e)
        {
            // Is there a winner among all players ?
            if (game.isWinner())
            {
                //Affichage de la map complète, suppression du fow
                windowsFormsHost1.Child.Controls.OfType<System.Windows.Forms.PictureBox>().First().Paint -= new System.Windows.Forms.PaintEventHandler(afficherPlayerMap);
                windowsFormsHost1.Child.Controls.OfType<System.Windows.Forms.PictureBox>().First().Paint += new System.Windows.Forms.PaintEventHandler(game.Map.reveal);
                windowsFormsHost1.Child.Refresh();
                endGame();
            }
            else
            {
                //Saves the current player's screen location
                game.CurrentPlayer.ScreenPos[0] = ((System.Windows.Forms.ScrollableControl)windowsFormsHost1.Child).HorizontalScroll.Value;
                game.CurrentPlayer.ScreenPos[1] = ((System.Windows.Forms.ScrollableControl)windowsFormsHost1.Child).VerticalScroll.Value;

                //Reset selection
                foreach (Case c in game.Map.grid)
                    c.Selected = false;

                game.nextPlayer();

                // Add a turn to the game
                game.Turns++;

                //Masquage de la map
                windowsFormsHost1.Child.Controls.OfType<System.Windows.Forms.PictureBox>().First().Paint += new System.Windows.Forms.PaintEventHandler(turnBlack);
                windowsFormsHost1.Child.Refresh();
                beginTurn();
                centerScreen();

                //Le siège accueille un nouveau joueur
                //System.Windows.MessageBox.Show("Have a seat " + game.CurrentPlayer.Name + " !", "CiviliZation : Hotseat", MessageBoxButton.OK);

                //Affichage de la map du nouveau joueur
                windowsFormsHost1.Child.Controls.OfType<System.Windows.Forms.PictureBox>().First().Paint -= new System.Windows.Forms.PaintEventHandler(turnBlack);
                windowsFormsHost1.Child.Refresh();
                windowsFormsHost1.Child.Focus();
            }
        }
        
        private void endGame()
        {
            System.Windows.MessageBox.Show(game.CurrentPlayer.Name + " wins!", "End Game", MessageBoxButton.OK);
        }
        #endregion

        #region Buttons events
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
        #endregion

        #region Graphics mecanisms
        private void drawMap()
        {
            int height = (int)Math.Sqrt((double)game.Map.grid.Count) * 50;
            int width = (int)Math.Sqrt((double)game.Map.grid.Count) * 50;
            System.Windows.Forms.PictureBox pictureBox = new System.Windows.Forms.PictureBox();
            pictureBox.Width = width; pictureBox.Height = height;
            pictureBox.Paint += new System.Windows.Forms.PaintEventHandler(afficherPlayerMap);
            pictureBox.MouseEnter += pictureBox_giveFocus;
            pictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(pictureBox_MouseClick);

            //Setting up scrollableContent and adapting it to the game
            System.Windows.Forms.ScrollableControl sc = new System.Windows.Forms.ScrollableControl();
            sc.Controls.Add(pictureBox);
            sc.AutoScroll = true;
            sc.HorizontalScroll.Maximum = pictureBox.Width;
            sc.VerticalScroll.Maximum = pictureBox.Height;
            sc.HorizontalScroll.SmallChange = 50;
            sc.VerticalScroll.SmallChange = 50;
            sc.HorizontalScroll.LargeChange = 500;
            sc.VerticalScroll.LargeChange = 500;

            sc.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(sc_PreviewKeyDown);

            windowsFormsHost1.Child = sc;
            centerScreen();

            //TODO !
            ////Dimensions de windowsFormsHost1, pas encore disponibles car non-affiché
            ////On centre l'écran pour le joueur 1
            //List<int> maxValues = centerScreen(500, 1002, pictureBox.Width, pictureBox.Height);
            //sc.HorizontalScroll.Maximum = maxValues[0];
            //sc.VerticalScroll.Maximum = maxValues[1];
            //windowsFormsHost1.Child.PerformLayout();
        }

        public void afficherPlayerMap(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            game.Map.afficher(sender, e, game.CurrentPlayer);
        }

        private void centerScreen()
        {
            int x, y;
            if (game.CurrentPlayer.ScreenPos != null)
            {
                x = game.CurrentPlayer.ScreenPos[0];
                y = game.CurrentPlayer.ScreenPos[1];
            }
            else
            {
                if (game.CurrentPlayer.Teachers.First().Case.SqPos[0] * 50 - (int)windowsFormsHost1.ActualWidth / 2 > 0)
                {
                    if (game.CurrentPlayer.Teachers.First().Case.SqPos[0] * 50 - (int)windowsFormsHost1.ActualWidth / 2 < ((System.Windows.Forms.ScrollableControl)windowsFormsHost1.Child).HorizontalScroll.Maximum)
                        x = game.CurrentPlayer.Teachers.First().Case.SqPos[0] * 50 - (int)windowsFormsHost1.ActualWidth / 2;
                    else x = ((System.Windows.Forms.ScrollableControl)windowsFormsHost1.Child).HorizontalScroll.Maximum;
                }
                else x = 0;
                if (game.CurrentPlayer.Teachers.First().Case.SqPos[1] * 50 - (int)windowsFormsHost1.ActualHeight / 2 > 0)
                {
                    if (game.CurrentPlayer.Teachers.First().Case.SqPos[1] * 50 - (int)windowsFormsHost1.ActualHeight / 2 < ((System.Windows.Forms.ScrollableControl)windowsFormsHost1.Child).VerticalScroll.Maximum)
                        y = game.CurrentPlayer.Teachers.First().Case.SqPos[1] * 50 - (int)windowsFormsHost1.ActualHeight / 2;
                    else y = ((System.Windows.Forms.ScrollableControl)windowsFormsHost1.Child).VerticalScroll.Maximum;
                }
                else y = 0;
                game.CurrentPlayer.ScreenPos = new int[2] { x, y };
            }
            ((System.Windows.Forms.ScrollableControl)windowsFormsHost1.Child).HorizontalScroll.Value = x;
            ((System.Windows.Forms.ScrollableControl)windowsFormsHost1.Child).VerticalScroll.Value = y;
            windowsFormsHost1.Child.PerformLayout();
        }

        private List<int> centerScreen(int height, int width, int schmax, int scvmax)
        {
            int x, y;
            if (game.CurrentPlayer.ScreenPos != null)
            {
                x = game.CurrentPlayer.ScreenPos[0];
                y = game.CurrentPlayer.ScreenPos[1];
            }
            else
            {
                if (game.CurrentPlayer.Teachers.First().Case.SqPos[0] * 50 - width / 2 > 0)
                {
                    if (game.CurrentPlayer.Teachers.First().Case.SqPos[0] * 50 - width / 2 < schmax)
                        x = game.CurrentPlayer.Teachers.First().Case.SqPos[0] * 50 - width / 2;
                    else x = schmax;
                }
                else x = 0;
                if (game.CurrentPlayer.Teachers.First().Case.SqPos[1] * 50 - height / 2 > 0)
                {
                    if (game.CurrentPlayer.Teachers.First().Case.SqPos[1] * 50 - height / 2 < scvmax)
                        y = game.CurrentPlayer.Teachers.First().Case.SqPos[1] * 50 - height / 2;
                    else y = scvmax;
                }
                else y = 0;
                game.CurrentPlayer.ScreenPos = new int[2] { x, y };
            }
            List<int> values = new List<int>();
            values.Add(x);
            values.Add(y);
            return values;
        }

        private void turnBlack(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.Black), 0, 0, game.Map.mapStrategy.height * 50, game.Map.mapStrategy.width * 50);
        }

        //Give focus to the map when mouse enters map's area
        private void pictureBox_giveFocus(object sender, EventArgs e)
        {
            windowsFormsHost1.Child.Focus();
        }

        //Give focus to the map when mouse clicks map's area
        //+ Select appropriate Square
        private void pictureBox_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            windowsFormsHost1.Child.Focus();
            game.Map.select(e.X, e.Y);

            //Cast du sender en l'objet approprié
            System.Windows.Forms.PictureBox pb = (System.Windows.Forms.PictureBox)sender;
            pb.Refresh();

            // Select the interface linked to the type of the square
            ICase selectedCase = game.Map.grid.Find(x => x.Selected == true);
            if (selectedCase.City != null && selectedCase.Visible == true)
            {
                field.Visibility = Visibility.Hidden;
                unit.Visibility = Visibility.Hidden;
                city.Visibility = Visibility.Visible;
                city.DataContext = new CityViewModel((City)selectedCase.City);
                showUnitInterface(selectedCase);

            }
            else if (selectedCase.Units.Count() > 0 && selectedCase.Visible == true)
            {
                field.Visibility = Visibility.Hidden;
                unit.Visibility = Visibility.Visible;
                city.Visibility = Visibility.Hidden;
                unit.DataContext = new UnitViewModel((Unit)selectedCase.Units[0]);
                showUnitInterface(selectedCase);
            }
            else
            {
                field.Visibility = Visibility.Visible;
                unit.Visibility = Visibility.Hidden;
                city.Visibility = Visibility.Hidden;
                field.DataContext = new CaseViewModel((Case)selectedCase);
                showUnitInterface(selectedCase);
            }
        }

        // Draw the bottom interface according to the selected Case
        private void showUnitInterface(ICase c)
        {
            ImageBrush unitDrawing = new ImageBrush();
            if (c.Units.Count() > 0)
            {
                if (c.Units[0] is ITeacher)
                    unitDrawing.ImageSource = (BitmapImage)FindResource("Teacher");
                else if (c.Units[0] is IStudent)
                    unitDrawing.ImageSource = (BitmapImage)FindResource("Student");
                else if (c.Units[0] is IBoss)
                    unitDrawing.ImageSource = (BitmapImage)FindResource("Boss");
            }
            else if (c.City != null)
            {
                unitDrawing.ImageSource = (BitmapImage)FindResource("City");
            }
            else
            {
                unitDrawing.ImageSource = (BitmapImage)FindResource("Field");
                if (c is IPlain || (c.Foods == 5 && c.Minerals == 1) || (c.Foods == 3 && c.Minerals == 3))
                    fieldType.Text = "Plain";
                else if (c is IMountain || (c.Foods == 0 && c.Minerals == 5) || (c.Foods == 2 && c.Minerals == 3))
                    fieldType.Text = "Mountain";
                else
                    fieldType.Text = "Desert";
            }

            action.Background = unitDrawing;
        }

        // Comments
        private void sc_PreviewKeyDown(object sender, System.Windows.Forms.PreviewKeyDownEventArgs e)
        {
            System.Windows.Forms.ScrollableControl sc = (System.Windows.Forms.ScrollableControl)sender;
            switch (e.KeyValue)
            {
                case (int)System.Windows.Forms.Keys.Down:
                    e.IsInputKey = true;
                    sc.VerticalScroll.Value += 50;
                    break;
                case (int)System.Windows.Forms.Keys.Up:
                    e.IsInputKey = true;
                    if (sc.VerticalScroll.Value - 50 < 0)
                        sc.VerticalScroll.Value = 0;
                    else sc.VerticalScroll.Value -= 50;
                    break;
                case (int)System.Windows.Forms.Keys.Right:
                    e.IsInputKey = true;
                    sc.HorizontalScroll.Value += 50;
                    break;
                case (int)System.Windows.Forms.Keys.Left:
                    e.IsInputKey = true;
                    if (sc.HorizontalScroll.Value - 50 < 0)
                        sc.HorizontalScroll.Value = 0;
                    else
                        sc.HorizontalScroll.Value -= 50;
                    break;
            }
            sc.PerformLayout();
        }
        #endregion
    }
}