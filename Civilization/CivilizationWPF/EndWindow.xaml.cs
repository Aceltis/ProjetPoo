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

namespace CivilizationWPF
{
    /// <summary>
    /// Interaction logic for EndGameWindow.xaml
    /// </summary>
    public partial class EndWindow : Window
    {
        public EndWindow(String name)
        {
            InitializeComponent();

            winnerView.Text = name;
        }

        public void returnMap(Object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void newGame(Object sender, RoutedEventArgs e)
        {
            var newWindow = new HomeWindow();
            Application.Current.Windows[0].Close();
            newWindow.Show();
            this.Close();
        }

        private void exitGame(Object sender, RoutedEventArgs e)
        {
            Application.Current.Windows[0].Close();
            this.Close();
        }
    }
}
