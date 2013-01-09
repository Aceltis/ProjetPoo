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
        //Map
        System.Windows.Forms.PictureBox mapPictureBox;
        IUnit activUnit;
        Queue<IUnit> activUnitsQueue;

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

            //Center the screen when the window is loaded
            this.Loaded+=new RoutedEventHandler(GameWindow_Loaded);
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

        private void endGame()
        {
            var menu = new EndWindow(game.Winner.Name);
            menu.ShowDialog();
        }

        private void quitToMenu(object sender, RoutedEventArgs e)
        {
            var menu = new EndWindow("Still no winner...");
            menu.ShowDialog();
        }

#endregion

#region Game & Turns mecanisms
        private void beginTurn()
        {
            top.DataContext = pToPvm[game.CurrentPlayer];

            //Update cities
            foreach (Case c in game.Map.grid)
            {
                if (c.City != null)
                {
                    if (c.City.Player.Color == game.CurrentPlayer.Color)
                        c.City.updateCity(game.Map);
                    if (c.City.Current_prod == ProductionType.None)
                    {
                        prodBossView.IsChecked = false;
                        prodStudentView.IsChecked = false;
                        prodTeacherView.IsChecked = false;
                    }
                }
            }
        }


        // Add all units of a case waiting for an order
        private void initializeActiveUnits(ICase c)
        {
            activUnitsQueue = new Queue<IUnit>();
            if (c.Units.Count() > 0)
            {
                bool unitNeedOrders = false;
                foreach (IUnit unit in c.Units)
                    if (unit.MovePoints != 0)
                        unitNeedOrders = true;
                if (unitNeedOrders)
                {
                    activUnit = c.Units.First(x => x.MovePoints!=0);
                    foreach (IUnit unit in c.Units)
                    {
                        if (unit.MovePoints != 0)
                            activUnitsQueue.Enqueue(unit);
                    }
                }
            }
        }

        //Fonction appellée à l'appui du bouton
        private void nextTurn(object sender, RoutedEventArgs e)
        {
            callNextTurn();
        }

        //Rend accessible par le code
        private void callNextTurn()
        {

            if ((bool)moveActionView.IsChecked)
                callMoveCancellation();
            if ((bool)attackActionView.IsChecked)
                callAttackCancellation();
            if ((bool)buildActionView.IsChecked)
                callBuildCancellation();

            // Is there a winner among all players ?
            if (game.isWinner())
            {
                //Affichage de la map complète, suppression du fow
                mapPictureBox.Paint -= new System.Windows.Forms.PaintEventHandler(afficherPlayerMap);
                mapPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(game.Map.reveal);
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

                game.Map.SelectedCase = null;
                game.Map.SelectedUnit = null;

                setNeutralInterface();

                game.nextPlayer();

                // Add a turn to the game
                if(game.CurrentPlayer.Color == PlayerColor.Red)
                    game.Turns++;

                //Reset units' move points
                foreach (ITeacher teacher in game.CurrentPlayer.Teachers)
                {
                    if (teacher.MovePoints == teacher.MaxMovePoints && teacher.HP < teacher.MaxHP)
                        teacher.HP ++;
                    teacher.MovePoints = teacher.MaxMovePoints;
                }
                foreach (IStudent student in game.CurrentPlayer.Students)
                {
                    if (student.MovePoints == student.MaxMovePoints && (!student.haveAttacked) && student.HP < student.MaxHP)
                        student.HP ++;
                    student.MovePoints = student.MaxMovePoints;
                    student.haveAttacked = false;
                }
                if (game.CurrentPlayer.Boss != null)
                {
                    if (game.CurrentPlayer.Boss.MovePoints == game.CurrentPlayer.Boss.MaxMovePoints && game.CurrentPlayer.Boss.HP < game.CurrentPlayer.Boss.MaxHP)
                        game.CurrentPlayer.Boss.MovePoints = game.CurrentPlayer.Boss.MaxMovePoints;
                }

                //Masquage de la map
                mapPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(turnBlack);
                windowsFormsHost1.Child.Refresh();
                beginTurn();
                centerScreen();

                //Desactivate interactions and map
                nextTurnButton.Click -= new RoutedEventHandler(nextTurn);
                nextTurnButton.Click += new RoutedEventHandler(nextTurnBlack);

                this.MouseDown += new MouseButtonEventHandler(clickBlack);
                this.KeyDown += new KeyEventHandler(keyDownBlack);
                mapPictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(mapPictureBox_MouseDownBlack);
                mapPictureBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(pb_keyDownBlack);
                windowsFormsHost1.Child.PreviewKeyDown -= new System.Windows.Forms.PreviewKeyDownEventHandler(sc_PreviewKeyDown);
                windowsFormsHost1.Child.MouseDown += new System.Windows.Forms.MouseEventHandler(mapPictureBox_MouseDownBlack);
                windowsFormsHost1.Child.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(pb_keyDownBlack);
                mapPictureBox.MouseClick -= new System.Windows.Forms.MouseEventHandler(pictureBox_Select);
            }
        }
#endregion

#region inhibition events
        //If user want to skip his turn
        private void nextTurnBlack(object sender, RoutedEventArgs e)
        {
            revealMapUnderBlack();
            callNextTurn();
        }
        private void mapPictureBox_MouseDownBlack(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            revealMapUnderBlack();
        }

        private void pb_keyDownBlack(object sender, System.Windows.Forms.PreviewKeyDownEventArgs e)
        {
            revealMapUnderBlack();
        }

        private void keyDownBlack(object sender, KeyEventArgs e)
        {
            revealMapUnderBlack();
        }

        private void clickBlack(object sender, EventArgs e)
        {
            revealMapUnderBlack();
        }

        private void revealMapUnderBlack()
        {
            //Re-activate all interactions :
            //Removing events triggers
            this.MouseDown -= new MouseButtonEventHandler(clickBlack);
            this.KeyDown -= new KeyEventHandler(keyDownBlack);
            mapPictureBox.MouseDown -= new System.Windows.Forms.MouseEventHandler(mapPictureBox_MouseDownBlack);
            mapPictureBox.PreviewKeyDown -= new System.Windows.Forms.PreviewKeyDownEventHandler(pb_keyDownBlack);
            windowsFormsHost1.Child.MouseDown -= new System.Windows.Forms.MouseEventHandler(mapPictureBox_MouseDownBlack);
            windowsFormsHost1.Child.PreviewKeyDown -= new System.Windows.Forms.PreviewKeyDownEventHandler(pb_keyDownBlack);
            nextTurnButton.MouseDown -= new MouseButtonEventHandler(clickBlack);
            mapPictureBox.Paint -= new System.Windows.Forms.PaintEventHandler(turnBlack);
            nextTurnButton.Click -= new RoutedEventHandler(nextTurnBlack);

            //Reloading the map
            windowsFormsHost1.Child.Refresh();
            windowsFormsHost1.Child.Focus();

            //Re-activate normal interactions
            mapPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(pictureBox_Select);
            windowsFormsHost1.Child.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(sc_PreviewKeyDown);
            nextTurnButton.Click += new RoutedEventHandler(nextTurn);
        }
#endregion

#region Buttons events
        private void callMove()
        {
            moveActionView.Click -= new RoutedEventHandler(moveAction);

            // Uncheck others actions
            moveActionView.IsChecked = true;
            attackActionView.IsChecked = false;
            buildActionView.IsChecked = false;

            mapPictureBox.MouseClick -= new System.Windows.Forms.MouseEventHandler(pictureBox_Select);
            mapPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(pictureBox_MoveUnit);
            game.Map.drawMoveBorders(game.CurrentPlayer);
            windowsFormsHost1.Child.Refresh();

            moveActionView.Click += new RoutedEventHandler(cancellMove);
            buildActionView.Click += new RoutedEventHandler(cancellMove);
            attackActionView.Click += new RoutedEventHandler(cancellMove);
        }

        private void callAttack()
        {
            if (!game.Map.SelectedUnit.haveAttacked)
            {
                attackActionView.Click -= new RoutedEventHandler(attackAction);

                // Uncheck others actions
                moveActionView.IsChecked = false;
                attackActionView.IsChecked = true;
                buildActionView.IsChecked = false;

                mapPictureBox.MouseClick -= new System.Windows.Forms.MouseEventHandler(pictureBox_Select);
                mapPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(pictureBox_AttackUnit);
                game.Map.drawAttackBorders();
                game.Map.circleEnemies(game.CurrentPlayer);
                windowsFormsHost1.Child.Refresh();

                moveActionView.Click += new RoutedEventHandler(cancellAttack);
                buildActionView.Click += new RoutedEventHandler(cancellAttack);
                attackActionView.Click += new RoutedEventHandler(cancellAttack);
            }
            else
            {
                attackActionView.IsChecked = false;
            }
        }

        private void callBuild()
        {
            buildActionView.Click -= new RoutedEventHandler(buildAction);

            // Uncheck others actions
            moveActionView.IsChecked = false;
            attackActionView.IsChecked = false;
            buildActionView.IsChecked = true;

            mapPictureBox.MouseClick -= new System.Windows.Forms.MouseEventHandler(pictureBox_Select);
            mapPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(pictureBox_BuildCity);
            game.Map.drawMoveBorders(game.CurrentPlayer);
            game.Map.drawCityPossibilities(game.CurrentPlayer);
            windowsFormsHost1.Child.Refresh();

            moveActionView.Click += new RoutedEventHandler(cancellBuild);
            buildActionView.Click += new RoutedEventHandler(cancellBuild);
            attackActionView.Click += new RoutedEventHandler(cancellBuild);
        }

        private void callMoveCancellation()
        {
            moveActionView.Click -= new RoutedEventHandler(cancellMove);
            buildActionView.Click -= new RoutedEventHandler(cancellMove);
            attackActionView.Click -= new RoutedEventHandler(cancellMove);

            //Uncheck other buttons
            moveActionView.IsChecked = false;

            mapPictureBox.MouseClick -= new System.Windows.Forms.MouseEventHandler(pictureBox_MoveUnit);
            mapPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(pictureBox_Select);

            //Si Build pressé, pas besoin d'effacer ça
            if (!(bool)buildActionView.IsChecked)
            {
                foreach (ICase c in game.Map.grid)
                    c.UnderUnitMoveRange = false;
                windowsFormsHost1.Child.Refresh();
            }

            moveActionView.Click += new RoutedEventHandler(moveAction);
        }

        private void callAttackCancellation()
        {
            moveActionView.Click -= new RoutedEventHandler(cancellAttack);
            buildActionView.Click -= new RoutedEventHandler(cancellAttack);
            attackActionView.Click -= new RoutedEventHandler(cancellAttack);

            attackActionView.IsChecked = false;

            mapPictureBox.MouseClick -= new System.Windows.Forms.MouseEventHandler(pictureBox_AttackUnit);
            mapPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(pictureBox_Select);

            foreach (ICase c in game.Map.grid)
            {
                c.UnderUnitMoveRange = false;
                c.UnderUnitAttackRange = false;
                c.EnemyInRange = false;
            }
            windowsFormsHost1.Child.Refresh();

            attackActionView.Click += new RoutedEventHandler(attackAction);
        }

        private void callBuildCancellation()
        {
            moveActionView.Click -= new RoutedEventHandler(cancellBuild);
            buildActionView.Click -= new RoutedEventHandler(cancellBuild);
            attackActionView.Click -= new RoutedEventHandler(cancellBuild);

            //Uncheck other buttons
            buildActionView.IsChecked = false;

            mapPictureBox.MouseClick -= new System.Windows.Forms.MouseEventHandler(pictureBox_BuildCity);
            mapPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(pictureBox_Select);

            //Si Move pressé, pas besoin d'effacer ça
            if (!(bool)moveActionView.IsChecked)
            {
                foreach (ICase c in game.Map.grid)
                {
                    c.UnderUnitMoveRange = false;
                    c.CitySuggestion = false;
                }
                windowsFormsHost1.Child.Refresh();
            }

            buildActionView.Click += new RoutedEventHandler(buildAction);
        }

        //action is wrapped into a function to access it from anywhere else into the C# code
        private void moveAction(object sender, RoutedEventArgs e)
        {
            callMove();
        }

        private void cancellMove(object sender, RoutedEventArgs e)
        {
            callMoveCancellation();
        }

        private void attackAction(object sender, RoutedEventArgs e)
        {
            callAttack();
        }

        private void cancellAttack(object sender, RoutedEventArgs e)
        {
            callAttackCancellation();
        }

        private void buildAction(object sender, RoutedEventArgs e)
        {
            callBuild();
        }

        private void cancellBuild(object sender, RoutedEventArgs e)
        {
            callBuildCancellation();
        }

        private void prodStudent(object sender, RoutedEventArgs e)
        {
            // Uncheck others actions
            prodBossView.IsChecked = false;
            prodTeacherView.IsChecked = false;
            prodStudentView.IsChecked = true;

            game.Map.SelectedCase.City.produceStudent();

            // Timer visible for the production
            timerStudentView.Visibility = Visibility.Visible;
            timerTeacherView.Visibility = Visibility.Hidden;
            timerBossView.Visibility = Visibility.Hidden;
        }

        private void prodBoss(object sender, RoutedEventArgs e)
        {
            // Uncheck others actions
            prodBossView.IsChecked = true;
            prodTeacherView.IsChecked = false;
            prodStudentView.IsChecked = false;

            game.Map.SelectedCase.City.produceBoss();

            // Timer visible for the production
            timerStudentView.Visibility = Visibility.Hidden;
            timerTeacherView.Visibility = Visibility.Hidden;
            timerBossView.Visibility = Visibility.Visible;
        }

        private void prodTeacher(object sender, RoutedEventArgs e)
        {
            // Uncheck others actions
            prodBossView.IsChecked = false;
            prodTeacherView.IsChecked = true;
            prodStudentView.IsChecked = false;

            game.Map.SelectedCase.City.produceTeacher();

            // Timer visible for the production
            timerStudentView.Visibility = Visibility.Hidden;
            timerTeacherView.Visibility = Visibility.Visible;
            timerBossView.Visibility = Visibility.Hidden;
        }

        private void nextSelectionnable(object sender, RoutedEventArgs e)
        {
            if (game.Map.SelectedCase != null)
            {
                if (game.Map.SelectedCase.Units.Count > 1)
                {
                    IUnit nextUnit = game.Map.SelectedCase.Units.First(x => x != game.Map.SelectedUnit);
                    game.Map.SelectedUnit = nextUnit;
                    updateInterface(nextUnit);
                }
                else
                    nextSelectionnableButton.Visibility = Visibility.Hidden;
            }
        }

        private void nextAction(object sender, RoutedEventArgs e)
        {
            bool unitNeedOrder = false;
            foreach (IUnit u in activUnitsQueue)
                if (u.MovePoints != 0)
                    unitNeedOrder = true;

            if (unitNeedOrder)
            {
                ImageBrush unitDrawing = new ImageBrush();

                activUnitsQueue.Enqueue(activUnit);
                activUnit = activUnitsQueue.Dequeue();
                if (activUnit.MovePoints != 0)
                {
                    if (activUnit is ITeacher)
                    {
                        unitDrawing.ImageSource = (BitmapImage)FindResource("Teacher");
                        attackActionView.Visibility = Visibility.Hidden;
                        buildActionView.Visibility = Visibility.Visible;
                    }
                    else if (activUnit is IStudent)
                    {
                        unitDrawing.ImageSource = (BitmapImage)FindResource("Student");
                        buildActionView.Visibility = Visibility.Hidden;
                        attackActionView.Visibility = Visibility.Visible;
                    }
                    else if (activUnit is IBoss)
                    {
                        unitDrawing.ImageSource = (BitmapImage)FindResource("Boss");
                        attackActionView.Visibility = Visibility.Hidden;
                        buildActionView.Visibility = Visibility.Hidden;
                    }

                    field.Visibility = Visibility.Hidden;
                    unit.Visibility = Visibility.Visible;
                    city.Visibility = Visibility.Hidden;
                    neutral.Visibility = Visibility.Hidden;

                    action.Background = unitDrawing;
                    unit.DataContext = new UnitViewModel((Unit)activUnit);
                    game.Map.SelectedUnit = activUnit;
                }
                else nextAction(sender, e);
            }
        }

        private void focusCity(object sender, RoutedEventArgs e)
        {
            field.Visibility = Visibility.Hidden;
            unit.Visibility = Visibility.Hidden;
            city.Visibility = Visibility.Visible;
            neutral.Visibility = Visibility.Hidden;

            ImageBrush unitDrawing = new ImageBrush();
            unitDrawing.ImageSource = (BitmapImage)FindResource("City");
            action.Background = unitDrawing;
            city.DataContext = new CityViewModel((City)game.Map.SelectedCase.City);
        }

#endregion

#region Graphics mecanisms

        // Comments
        private void drawMap()
        {
            int height = (int)Math.Sqrt((double)game.Map.grid.Count) * 50;
            int width = (int)Math.Sqrt((double)game.Map.grid.Count) * 50;
            mapPictureBox = new System.Windows.Forms.PictureBox();
            mapPictureBox.Width = width; mapPictureBox.Height = height;
            mapPictureBox.Paint += new System.Windows.Forms.PaintEventHandler(afficherPlayerMap);
            mapPictureBox.MouseEnter += pictureBox_giveFocus;
            mapPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(pictureBox_Select);

            //Setting up scrollableContent and adapting it to the game
            System.Windows.Forms.ScrollableControl sc = new System.Windows.Forms.ScrollableControl();
            sc.Controls.Add(mapPictureBox);
            sc.AutoScroll = true;
            //sc.HorizontalScroll.Maximum = pictureBox.Width;
            //sc.VerticalScroll.Maximum = pictureBox.Height;
            sc.HorizontalScroll.SmallChange = 50; sc.VerticalScroll.SmallChange = 50;
            sc.HorizontalScroll.LargeChange = 500; sc.VerticalScroll.LargeChange = 500;

            sc.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(sc_PreviewKeyDown);

            windowsFormsHost1.Child = sc;
        }


        // Calls the function that centers the screen for the first player
        private void GameWindow_Loaded(object sender, RoutedEventArgs e)
        {
            centerScreen();
        }

        // Comments
        public void afficherPlayerMap(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            game.Map.afficher(sender, e, game.CurrentPlayer);
        }

        // Comments
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
                    if (game.CurrentPlayer.Teachers.First().Case.SqPos[0] * 50 - (int)windowsFormsHost1.ActualWidth / 2 < ((System.Windows.Forms.ScrollableControl)windowsFormsHost1.Child).DisplayRectangle.Width)
                        x = game.CurrentPlayer.Teachers.First().Case.SqPos[0] * 50 - (int)windowsFormsHost1.ActualWidth / 2;
                    else x = ((System.Windows.Forms.ScrollableControl)windowsFormsHost1.Child).DisplayRectangle.Width;
                }
                else x = 0;
                if (game.CurrentPlayer.Teachers.First().Case.SqPos[1] * 50 - (int)windowsFormsHost1.ActualHeight / 2 > 0)
                {
                    if (game.CurrentPlayer.Teachers.First().Case.SqPos[1] * 50 - (int)windowsFormsHost1.ActualHeight / 2 < ((System.Windows.Forms.ScrollableControl)windowsFormsHost1.Child).DisplayRectangle.Height)
                        y = game.CurrentPlayer.Teachers.First().Case.SqPos[1] * 50 - (int)windowsFormsHost1.ActualHeight / 2;
                    else y = ((System.Windows.Forms.ScrollableControl)windowsFormsHost1.Child).DisplayRectangle.Height;
                }
                else y = 0;
                game.CurrentPlayer.ScreenPos = new int[2] { x, y };
            }
            ((System.Windows.Forms.ScrollableControl)windowsFormsHost1.Child).HorizontalScroll.Value = x;
            ((System.Windows.Forms.ScrollableControl)windowsFormsHost1.Child).VerticalScroll.Value = y;
            windowsFormsHost1.Child.PerformLayout();
        }

        // Comments
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
        private void pictureBox_Select(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            windowsFormsHost1.Child.Focus();
            game.Map.select(e.X, e.Y);

            if (game.Map.SelectedCase != null)
            {
                if (game.Map.SelectedCase.Units.Count > 1)
                {
                    nextSelectionnableButton.Visibility = Visibility.Visible;
                }
                else nextSelectionnableButton.Visibility = Visibility.Hidden;
            }

            mapPictureBox.Refresh();
            updateInterface();
            
        }

        //Move selected unit, Move button have been pressed
        private void pictureBox_MoveUnit(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            moveActionView.IsChecked = false;

            moveActionView.Click -= new RoutedEventHandler(cancellMove);
            buildActionView.Click -= new RoutedEventHandler(cancellMove);
            attackActionView.Click -= new RoutedEventHandler(cancellMove);

            windowsFormsHost1.Child.Focus();
            game.Map.moveTo(e.X, e.Y);

            mapPictureBox.MouseClick -= new System.Windows.Forms.MouseEventHandler(pictureBox_MoveUnit);
            mapPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(pictureBox_Select);
            mapPictureBox.Refresh();

            updateInterface();

            //Re-activate button
            moveActionView.Click += new RoutedEventHandler(moveAction);

            foreach (IPlayer player in game.Players)
            {
                if (player.Teachers.Count + player.Students.Count + player.Cities.Count == 0)
                {
                    if (player.Boss == null)
                    {
                        game.Loosers.Add(player);
                        if (game.Players.Count == 1)
                            game.Winner=game.Players.First();
                    }
                }
            }
        }

        //Attack unit with selected unit, Attack button have been pressed
        private void pictureBox_AttackUnit(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            attackActionView.IsChecked = false;

            moveActionView.Click -= new RoutedEventHandler(cancellAttack);
            buildActionView.Click -= new RoutedEventHandler(cancellAttack);
            attackActionView.Click -= new RoutedEventHandler(cancellAttack);

            windowsFormsHost1.Child.Focus();
            game.Map.attack(e.X, e.Y);

            //Mode attaque -> mode sélection
            mapPictureBox.MouseClick -= new System.Windows.Forms.MouseEventHandler(pictureBox_AttackUnit);
            mapPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(pictureBox_Select);
            mapPictureBox.Refresh();

            updateInterface();

            //Re-activate button
            attackActionView.Click += new RoutedEventHandler(attackAction);

            foreach (IPlayer player in game.Players)
            {
                if (player.Teachers.Count + player.Students.Count + player.Cities.Count == 0)
                {
                    if (player.Boss == null)
                    {
                        game.Loosers.Add(player);
                        if (game.Players.Count == 1)
                            game.Winner=game.Players.First();
                    }
                }
            }
        }

        //Build City with selected unit, Build button have been pressed, map have been clicked
        private void pictureBox_BuildCity(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            buildActionView.IsChecked = false;

            moveActionView.Click -= new RoutedEventHandler(cancellBuild);
            buildActionView.Click -= new RoutedEventHandler(cancellBuild);
            attackActionView.Click -= new RoutedEventHandler(cancellBuild);

            String cityName = "";
            if (game.Map.grid[e.X / 50 + game.Map.mapStrategy.width * (e.Y / 50)].UnderUnitMoveRange)
            {
                var newWindow = new CityNameWindow(game.CurrentPlayer);
                newWindow.ShowDialog();

                if (newWindow.DialogResult == true)
                {
                    cityName = newWindow.newCityName.Text;
                }
                else
                {
                    callBuildCancellation();
                }
            }

            windowsFormsHost1.Child.Focus();
            game.Map.buildCity(e.X, e.Y, game.CurrentPlayer, cityName);

            //Cast du sender en l'objet approprié. Mode attaque -> mode sélection
            if (cityName != "")
            {
                mapPictureBox.MouseClick -= new System.Windows.Forms.MouseEventHandler(pictureBox_BuildCity);
                mapPictureBox.MouseClick += new System.Windows.Forms.MouseEventHandler(pictureBox_Select);
            }

            mapPictureBox.Refresh();
            updateInterface();

            //Re-activate button
            buildActionView.Click += new RoutedEventHandler(buildAction);
        }

        private void setNeutralInterface()
        {
            showUnitInterface(null);
        }

        // Selects the interface linked to the type of the square
        private void updateInterface()
        {
            ICase selectedCase = game.Map.grid.Find(x => x.Selected == true);

            if (game.Map.SelectedCase != null)
            {
                initializeActiveUnits(game.Map.SelectedCase);

                if (game.Map.SelectedCase.Units.Count > 1)
                    nextSelectionnableButton.Visibility = Visibility.Visible;
                else nextSelectionnableButton.Visibility = Visibility.Hidden;
            }

            if (this.activUnitsQueue.Count != 0)
            {
                orderUnit.Visibility = Visibility.Visible;
                //game.Map.SelectedUnit = this.activUnitsQueue.Dequeue();
            }
            else orderUnit.Visibility = Visibility.Hidden;

            if (selectedCase != null)
            {
                if (selectedCase.City != null && selectedCase.Visible == true)
                {
                    field.Visibility = Visibility.Hidden;
                    unit.Visibility = Visibility.Hidden;
                    city.Visibility = Visibility.Visible;
                    neutral.Visibility = Visibility.Hidden;

                    city.DataContext = new CityViewModel((City)selectedCase.City);
                    showUnitInterface(selectedCase);

                }
                else if (selectedCase.Units.Count() > 0 && selectedCase.Visible == true)
                {
                    field.Visibility = Visibility.Hidden;
                    unit.Visibility = Visibility.Visible;
                    city.Visibility = Visibility.Hidden;
                    neutral.Visibility = Visibility.Hidden;

                    unit.DataContext = new UnitViewModel((Unit)game.Map.SelectedUnit);
                    showUnitInterface(selectedCase);
                }
                else
                {
                    field.Visibility = Visibility.Visible;
                    unit.Visibility = Visibility.Hidden;
                    city.Visibility = Visibility.Hidden;
                    neutral.Visibility = Visibility.Hidden;

                    field.DataContext = new CaseViewModel((Case)selectedCase);
                    showUnitInterface(selectedCase);
                }

                if (selectedCase.Units.Count() > 1)
                {
                    if (selectedCase.Units.Find(x => x.MovePoints != 0) != null)
                    {
                        orderCity.Visibility = Visibility.Hidden;
                        orderUnit.Visibility = Visibility.Visible;
                        orderUnitSmall.Visibility = Visibility.Hidden;
                    }
                }
                else if (selectedCase.Units.Count() > 0 && selectedCase.City != null)
                {
                    orderCity.Visibility = Visibility.Visible;
                    if (selectedCase.Units.Find(x => x.MovePoints != 0) != null)
                    {
                        orderUnit.Visibility = Visibility.Hidden;
                        orderUnitSmall.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    orderCity.Visibility = Visibility.Hidden;
                    orderUnit.Visibility = Visibility.Hidden;
                    orderUnitSmall.Visibility = Visibility.Hidden;
                }
            }
            else showUnitInterface(null);
        }

        //Surcharge pour ignorer la queue des unités actives
        private void updateInterface(IUnit nextUnit)
        {            
            unit.DataContext = new UnitViewModel((Unit)nextUnit);

            ImageBrush unitDrawing = new ImageBrush();
            if (nextUnit is ITeacher)
            {
                unitDrawing.ImageSource = (BitmapImage)FindResource("Teacher");
                attackActionView.Visibility = Visibility.Hidden;
                buildActionView.Visibility = Visibility.Visible;
            }
            else if (nextUnit is IStudent)
            {
                unitDrawing.ImageSource = (BitmapImage)FindResource("Student");
                buildActionView.Visibility = Visibility.Hidden;
                attackActionView.Visibility = Visibility.Visible;
            }
            else if (nextUnit is IBoss)
            {
                unitDrawing.ImageSource = (BitmapImage)FindResource("Boss");
                attackActionView.Visibility = Visibility.Hidden;
                buildActionView.Visibility = Visibility.Hidden;
            }
            action.Background = unitDrawing;
        }

        // Draw the bottom interface according to the selected Case
        private void showUnitInterface(ICase c)
        {
            ImageBrush unitDrawing = new ImageBrush();
            if (c != null)
            {
                if (c.Units.Count() > 0 && c.Visible && c.City == null)
                {
                    if (game.Map.SelectedUnit is ITeacher)
                    {
                        unitDrawing.ImageSource = (BitmapImage)FindResource("Teacher");
                        attackActionView.Visibility = Visibility.Hidden;
                        buildActionView.Visibility = Visibility.Visible;
                    }
                    else if (game.Map.SelectedUnit is IStudent)
                    {
                        unitDrawing.ImageSource = (BitmapImage)FindResource("Student");
                        buildActionView.Visibility = Visibility.Hidden;
                        attackActionView.Visibility = Visibility.Visible;
                    }
                    else if (game.Map.SelectedUnit is IBoss)
                    {
                        unitDrawing.ImageSource = (BitmapImage)FindResource("Boss");
                        attackActionView.Visibility = Visibility.Hidden;
                        buildActionView.Visibility = Visibility.Hidden;
                    }
                }
                else if (c.City != null && c.Visible)
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
            }
            else
            {
                neutral.Visibility = Visibility.Visible;
                field.Visibility = Visibility.Hidden;
                unit.Visibility = Visibility.Hidden;
                city.Visibility = Visibility.Hidden;
                unitDrawing.ImageSource = (BitmapImage)FindResource("Accueil");
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

                //Touche permettant de faire passer le tour d'une unité
                case (int)System.Windows.Forms.Keys.Space :
                    e.IsInputKey = true;

                    if (game.Map.SelectedUnit != null)
                    {
                        //On la rend inactive
                        game.Map.SelectedUnit.MovePoints = 0;
                        foreach (ICase square in game.Map.grid)
                        {
                            square.UnderUnitAttackRange = false;
                            square.EnemyInRange = false;
                            square.CitySuggestion = false;
                            square.UnderUnitMoveRange = false;
                        }
                        moveActionView.IsChecked = false;
                        attackActionView.IsChecked = false;
                        buildActionView.IsChecked = false;

                        if(game.Map.SelectedCase!=null)
                            if (game.Map.SelectedCase.Units.Count != 0)
                            {
                                foreach (IUnit unit in game.Map.SelectedCase.Units)
                                {
                                    if (unit.MovePoints != 0)
                                    {
                                        game.Map.SelectedUnit = unit;
                                        break;
                                    }
                                }
                            }

                        sc.Refresh();
                        updateInterface();
                        break; //TODO
                    }
                    break;

                case (int)System.Windows.Forms.Keys.M:
                    e.IsInputKey = true;
                    if (game.Map.SelectedUnit != null && (game.Map.SelectedUnit.Player.Color == game.CurrentPlayer.Color))
                    {
                        if ((bool)buildActionView.IsChecked)
                            callBuildCancellation();
                        if ((bool)attackActionView.IsChecked)
                            callAttackCancellation();
                        if (!(bool)moveActionView.IsChecked)
                            callMove();
                    }
                    break;
                case (int)System.Windows.Forms.Keys.A:
                    e.IsInputKey = true;
                    if (game.Map.SelectedUnit != null && (game.Map.SelectedUnit.Player.Color == game.CurrentPlayer.Color))
                    {
                        if ((game.Map.SelectedUnit.GetType() == (new StudentINFO()).GetType())||(game.Map.SelectedUnit.GetType() == (new StudentEII()).GetType()))
                        {
                            if ((bool)moveActionView.IsChecked)
                                callMoveCancellation();
                            if ((bool)buildActionView.IsChecked)
                                callBuildCancellation();
                            if (game.Map.SelectedUnit != null)
                                if (!(bool)attackActionView.IsChecked)
                                    callAttack();
                        }
                    }
                    break;
                case (int)System.Windows.Forms.Keys.B:
                    e.IsInputKey = true;
                    if (game.Map.SelectedUnit != null && (game.Map.SelectedUnit.Player.Color == game.CurrentPlayer.Color))
                    {
                        if ((game.Map.SelectedUnit.GetType() == (new TeacherINFO()).GetType()) || (game.Map.SelectedUnit.GetType() == (new TeacherEII()).GetType()))
                        {
                            if ((bool)moveActionView.IsChecked)
                                callMoveCancellation();
                            if ((bool)attackActionView.IsChecked)
                                callAttackCancellation();
                            if (!(bool)buildActionView.IsChecked)
                                callBuild();
                        }
                    }
                    break;
                case (int)System.Windows.Forms.Keys.Escape:
                    e.IsInputKey = true;
                    if ((game.Map.SelectedUnit != null)&&(moveActionView.IsChecked != false || attackActionView.IsChecked != false || buildActionView.IsChecked != false))
                    {
                        if ((bool)moveActionView.IsChecked)
                            callMoveCancellation();
                        if ((bool)attackActionView.IsChecked)
                            callAttackCancellation();
                        if ((bool)buildActionView.IsChecked)
                            callBuildCancellation();

                        sc.Refresh();
                        updateInterface();
                        break;
                    }
                    else if (game.Map.SelectedCase != null)
                    {
                        game.Map.SelectedUnit = null;
                        game.Map.SelectedCase = null;
                        foreach (ICase square in game.Map.grid)
                        {
                            square.Selected = false;
                            square.UnderUnitAttackRange = false;
                            square.EnemyInRange = false;
                            square.CitySuggestion = false;
                            square.UnderUnitMoveRange = false;
                        }
                        moveActionView.IsChecked = false;
                        attackActionView.IsChecked = false;
                        buildActionView.IsChecked = false;
                        sc.Refresh();
                        updateInterface();
                        break;
                    }
                    break;
                case (int)System.Windows.Forms.Keys.Enter:
                    e.IsInputKey = true;
                    callNextTurn();
                    break;
            }
            sc.PerformLayout();
        }
#endregion
    }
}