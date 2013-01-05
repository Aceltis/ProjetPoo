using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Interfaces;

namespace CivilizationWPF
{
    class CityViewModel : INotifyPropertyChanged
    {
        // property changed event
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        private string population;
        private string food;
        private string iron;
        private string neededFood;
        private string currentProd;
        private string currentProdTimer;

        private void OnPropertyChanged(String property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public String Population
        {
            get
            {
                return population;
            }
            set
            {
                population = value;
                OnPropertyChanged("Population");
            }
        }

        public String Food
        {
            get
            {
                return food;
            }
            set
            {
                food = value;
                OnPropertyChanged("Food");
            }
        }

        public String Iron
        {
            get
            {
                return iron;
            }
            set
            {
                iron = value;
                OnPropertyChanged("Iron");
            }
        }

        public String NeededFood
        {
            get
            {
                return neededFood;
            }
            set
            {
                neededFood = value;
                OnPropertyChanged("NeededFood");
            }
        }

        public String CurrentProd
        {
            get
            {
                return currentProd;
            }
            set
            {
                currentProd = value;
                OnPropertyChanged("CurrentProd");
            }
        }

        public String CurrentProdTimer
        {
            get
            {
                return currentProdTimer;
            }
            set
            {
                currentProdTimer = value;
                OnPropertyChanged("CurrentProdTimer");
            }
        }
    }
}
