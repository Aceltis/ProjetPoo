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
using Implementation;
using Interfaces;

namespace CivilizationWPF
{
    /// <summary>
    /// Interaction logic for CityNameWindow.xaml
    /// </summary>
    public partial class CityNameWindow : Window
    {
        public CityNameWindow(IPlayer p)
        {
            InitializeComponent();

            newCityName.Text = p.Name + "City" + p.Cities.Count().ToString();
        }

        private void cancel(Object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void ok(Object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
