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
        private string _population;
        private string _ownedMinerals;
        private string _ownedFoods;
        
        public CityViewModel(City c)
        {
            c.PropertyChanged += new PropertyChangedEventHandler(delegate(object sender, PropertyChangedEventArgs args)
            {
                Name = ((City)sender).Name;
                Population = populationLabel(((City)sender).Population);
                OwnedMinerals = ownedMineralsLabel(((City)sender).OwnedMinerals);
                OwnedFoods = ownedMineralsLabel(((City)sender).OwnedFoods);
            });

            Name = c.Name;
            Population = populationLabel(c.Population);
            OwnedMinerals = ownedMineralsLabel(c.OwnedMinerals);
            OwnedFoods = ownedMineralsLabel(c.OwnedFoods);
        }

        public string Name
        {
            get { return _name; }
            set
            {
                SetAndNotify(ref _name, value, () => Name);
            }
        }

        public string Population
        {
            get { return _population; }
            set
            {
                SetAndNotify(ref _population, value, () => Population);
            }
        }

        public string populationLabel(int p)
        {
            return p.ToString();
        }

        public string OwnedMinerals
        {
            get { return _ownedMinerals; }
            set
            {
                SetAndNotify(ref _ownedMinerals, value, () => OwnedMinerals);
            }
        }

        public string ownedMineralsLabel(int o)
        {
            return o.ToString();
        }

        public string OwnedFoods
        {
            get { return _ownedFoods; }
            set
            {
                SetAndNotify(ref _ownedFoods, value, () => OwnedFoods);
            }
        }

        public string ownedFoodsLabel(int f)
        {
            return f.ToString();
        }
    }
    
}