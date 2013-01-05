using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Interfaces;

namespace CivilizationWPF
{
    public class PlayerViewModel : INotifyPropertyChanged
    {
        // property changed event
        public event PropertyChangedEventHandler PropertyChanged;

        private string name;
        private string color;
        private string civilization;
        private string cityNumber;
        private string teacherNumber;
        private string studentNumber;
        private string bossStatus;

        private void OnPropertyChanged(String property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public PlayerViewModel()
        {
        }

        public PlayerViewModel(IPlayer p)
        {
            name = p.Pseudo;
            color = p.Color;
            civilization = p.Civilization.ToString();
            teacherNumber = p.Teachers.Count().ToString();
            studentNumber = p.Students.Count().ToString();
            cityNumber = p.Cities.Count().ToString();
            bossStatus = "./Resource/interface/icons/dead.png";
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

        public String Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
                OnPropertyChanged("Color");
            }
        }

        public String Civilization
        {
            get
            {
                return civilization;
            }
            set
            {
                civilization = value;
                OnPropertyChanged("Civilization");
            }
        }

        public String CityNumber
        {
            get
            {
                return cityNumber;
            }
            set
            {
                cityNumber = value;
                OnPropertyChanged("CityNumber");
            }
        }

        public String TeacherNumber
        {
            get
            {
                return teacherNumber;
            }
            set
            {
                teacherNumber = value;
                OnPropertyChanged("TeacherNumber");
            }
        }

        public String StudentNumber
        {
            get
            {
                return studentNumber;
            }
            set
            {
                studentNumber = value;
                OnPropertyChanged("StudentNumber");
            }
        }

        public String BossStatus
        {
            get
            {
                return bossStatus;
            }
            set
            {
                bossStatus = value;
                OnPropertyChanged("BossStatus");
            }
        }
    }
}
