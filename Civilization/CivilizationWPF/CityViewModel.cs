using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Interfaces;
using MVVM;
using Implementation;

namespace CivilizationWPF
{
    class CityViewModel : ObservableObject
    {
        private string _name;

        public CityViewModel(City c)
        {
            c.PropertyChanged += new PropertyChangedEventHandler(delegate(object sender, PropertyChangedEventArgs args)
            {
                Name = ((City)sender).Name;
            });

            Name = c.Name;
        }

        public string Name
        {
            get { return _name; }
            set
            {
                SetAndNotify(ref _name, value, () => Name);
            }
        }
    }
    
}