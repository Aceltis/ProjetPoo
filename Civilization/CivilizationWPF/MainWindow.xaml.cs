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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Map newMap = new Map();
            newMap.setMapStrategy(new SmallMapStrategy());
            newMap.createMap();
            System.Windows.Forms.PictureBox pictureBox = new System.Windows.Forms.PictureBox();
            pictureBox.Width = newMap.grid.Count * 2; pictureBox.Height = newMap.grid.Count * 2;
            newMap.afficher(pictureBox);
            windowsFormsHost1.Width = newMap.grid.Count * 2; windowsFormsHost1.Height = newMap.grid.Count * 2;
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
    }
}