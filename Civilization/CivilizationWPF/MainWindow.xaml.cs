﻿using System;
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
    struct square
    {
        // 0: Mountain, 1: Plain, 2: Desert
        public int type;
        // 0: No additionnal ressource, 1: Additionnal Iron, 2: Additionnal Food
        public int bonus;
        // 0: free, 1: checked, 2: frozen
        public int state;
    };

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Valable que quand on parle de pointeur
        unsafe public MainWindow()
        {
            WrapperAlgo algo = new WrapperAlgo();

            square** map = (square**)algo.computeFoo();
            for (int i = 0; i < 5; i++)
            {
            }
        }
    }
}