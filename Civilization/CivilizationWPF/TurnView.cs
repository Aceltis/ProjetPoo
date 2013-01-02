using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace CivilizationWPF
{
    class TurnView : INotifyPropertyChanged
    {
        // property changed event
        public event PropertyChangedEventHandler PropertyChanged;

        private string _turnNumber;

        private void OnPropertyChanged(String property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public TurnView()
        {
            _turnNumber = "1";
        }

        public String TurnNumber
        {
            get
            {
                return _turnNumber;
            }
            set
            {
                _turnNumber = value;
                OnPropertyChanged("TurnNumber");
            }
        }
    }
}
