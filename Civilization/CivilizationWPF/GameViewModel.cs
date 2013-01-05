using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Implementation;
using Interfaces;

namespace CivilizationWPF
{
    class GameViewModel : INotifyPropertyChanged
    {
        // property changed event
        public event PropertyChangedEventHandler PropertyChanged;

        private String turns;

        private void OnPropertyChanged(String property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public GameViewModel(Game g)
        {
            turns = g.Turns.ToString();
        }

        public String Turns
        {
            get
            {
                return turns;
            }
            set
            {
                turns = value;
                OnPropertyChanged("Name");
            }
        }
    }
}
