using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Interfaces;
using Implementation;
using MVVM;

namespace CivilizationWPF
{
    class GameViewModel : ObservableObject
    {
        private int _turns;

        public GameViewModel(Game g)
        {
            g.PropertyChanged += new PropertyChangedEventHandler(delegate(object sender, PropertyChangedEventArgs args)
            {
                Turns = ((Game)sender).Turns;
            });
            Turns = g.Turns;
        }

        public int Turns
        {
            get { return _turns; }
            set
            {
                SetAndNotify(ref _turns, value, () => Turns);
            }
        }
    }
}
