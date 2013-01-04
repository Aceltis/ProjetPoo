using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace CivilizationWPF
{
    class PlayerViewModel : INotifyPropertyChanged
    {
        // property changed event
        public event PropertyChangedEventHandler PropertyChanged;

        private string _name;
        private string _color;
        private string _civilization;
        private string _cityNumber;
        private string _teacherNumber;
        private string _studentNumber;
        private string _bossStatus;

        private void OnPropertyChanged(String property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public PlayerViewModel(String name, String color, String civ)
        {
            _name = name;
            _color = color;
            _civilization = civ;
            _teacherNumber = "1";
            _studentNumber = "1";
            _cityNumber = "0";
            _bossStatus = "./Resource/interface/icons/dead.png";
        }

        public String Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public String Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                OnPropertyChanged("Color");
            }
        }

        public String Civilization
        {
            get
            {
                return _civilization;
            }
            set
            {
                _civilization = value;
                OnPropertyChanged("Civilization");
            }
        }

        public String CityNumber
        {
            get
            {
                return _cityNumber;
            }
            set
            {
                _cityNumber = value;
                OnPropertyChanged("CityNumber");
            }
        }

        public String TeacherNumber
        {
            get
            {
                return _teacherNumber;
            }
            set
            {
                _teacherNumber = value;
                OnPropertyChanged("TeacherNumber");
            }
        }

        public String StudentNumber
        {
            get
            {
                return _studentNumber;
            }
            set
            {
                _studentNumber = value;
                OnPropertyChanged("StudentNumber");
            }
        }

        public String BossStatus
        {
            get
            {
                return _bossStatus;
            }
            set
            {
                _bossStatus = value;
                OnPropertyChanged("BossStatus");
            }
        }
    }
}
