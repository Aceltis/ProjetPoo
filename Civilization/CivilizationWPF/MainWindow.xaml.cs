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

namespace CivilizationWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Valable que quand on parle de pointeur
        unsafe public MainWindow()
        {
            WrapperAlgo algo = new WrapperAlgo();
            int* tab = algo.computeFoo();
            for (int i = 0; i < 5; i++)
            {
                System.Console.WriteLine(tab[i]);
            }
        }

        private void endTurn(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Passage au joueur suivant");
        }
    }
}