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
            game = (Game)builder.build();
            /*
            System.Windows.Forms.PictureBox pictureBox1 = new System.Windows.Forms.PictureBox();
            pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(newMap.grid[3].afficher);

            Image myImage = new Image();
            myImage.Width = 50;

            BitmapImage myBitmapImage = new BitmapImage();

            myBitmapImage.BeginInit();
            myBitmapImage.UriSource = new Uri(@"C:\Users\msi\Documents\GitHub\ProjetPoo\Civilization\CivilizationWPF\Resource\terrains\desert.png");

            myBitmapImage.DecodePixelWidth = 50;
            myBitmapImage.EndInit();

            */
            InitializeComponent();

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