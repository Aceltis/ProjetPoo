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
    public class PlayerViewModel : ObservableObject, INotifyPropertyChanged
    {
        private string _name;
        private string _color;
        private string _civilization;
        private string _cities;
        private string _boss;
        private string _students;
        private string _teachers;

        public PlayerViewModel(Player p)
        {
            p.PropertyChanged += new PropertyChangedEventHandler(delegate(object sender, PropertyChangedEventArgs args)
            {
                Name = ((Player)sender).Name;
                Color = colorLabel(((Player)sender).Color);
                Civilization = civilizationLabel(((Player)sender).Civilization);
                Cities = citiesLabel(((Player)sender).Cities);
                Boss = bossLabel(((Player)sender).Boss);
                Students = studentsLabel(((Player)sender).Students);
                Teachers = teachersLabel(((Player)sender).Teachers);
            });

            Name = p.Name;
            Color = colorLabel(p.Color);
            Civilization = civilizationLabel(p.Civilization);
            Cities = citiesLabel(p.Cities);
            Boss = bossLabel(p.Boss);
            Students = studentsLabel(p.Students);
            Teachers = teachersLabel(p.Teachers);
        }

        public string Name
        {
            get { return _name; }
            set
            {
                SetAndNotify(ref _name, value, () => Name);
            }
        }

        public string Color
        {
            get { return _color; }
            set
            {
                SetAndNotify(ref _color, value, () => Color);
            }
        }

        public string colorLabel(PlayerColor c)
        {
            return c.ToString();
        }

        public string Civilization
        {
            get { return _civilization; }
            set
            {
                SetAndNotify(ref _civilization, value, () => Civilization);
            }
        }

        public string civilizationLabel(CivilizationType c)
        {
            return c.ToString();
        }

        public string Cities
        {
            get { return _cities; }
            set
            {
                SetAndNotify(ref _cities, value, () => Cities);
            }
        }

        public string citiesLabel(List<ICity> cities)
        {
            return cities.Count().ToString();
        }

        public string Boss
        {
            get { return _boss; }
            set
            {
                SetAndNotify(ref _boss, value, () => Boss);
            }
        }

        public string bossLabel(IBoss b)
        {
            if (b == null)
                return "./Resource/interface/icons/dead.png";
            else
                return "./Resource/interface/icons/alive.png";
        }

        public string Students
        {
            get { return _students; }
            set
            {
                SetAndNotify(ref _students, value, () => Students);
            }
        }

        public string studentsLabel(List<IStudent> s)
        {
            return s.Count().ToString();
        }

        public string Teachers
        {
            get { return _teachers; }
            set
            {
                SetAndNotify(ref _teachers, value, () => Teachers);
            }
        }

        public string teachersLabel(List<ITeacher> t)
        {
            return t.Count().ToString();
        }
    }
}
