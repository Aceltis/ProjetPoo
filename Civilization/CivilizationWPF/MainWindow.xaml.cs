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
    struct square
    {
        // 0: Mountain, 1: Plain, 2: Desert
        public int type;
        // 0: No additionnal ressource, 1: Additionnal Iron, 2: Additionnal Food
        public int bonus;
    };

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Valable que quand on parle de pointeur
        public MainWindow()
        {
            List<Case> newMap = createAlgoMap();

        }

        private void endTurn(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Passage au joueur suivant");
        }

        unsafe public List<Case> createAlgoMap ()
        {
            Map newMap = new Map();
            newMap.setMapStrategy(new SmallMapStrategy());

            WrapperAlgo algo = new WrapperAlgo();
            int** Algomap = algo.createMap(25, 25);
            int** bonuses = algo.createBonusesMap(25, 25, 0.2);

            for (int i = 0; i < 25; i++)
            {
                newMap.createMap();
            }
            return new List<Case>();
        }
    }
}