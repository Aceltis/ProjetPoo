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
    class CaseViewModel : ObservableObject
    {
        private int _minerals;
        private int _foods;

        public CaseViewModel(Case c)
        {
            c.PropertyChanged += new PropertyChangedEventHandler(delegate(object sender, PropertyChangedEventArgs args)
            {
                Minerals = ((Case)sender).Minerals;
                Foods = ((Case)sender).Foods;
            });

            Minerals = c.Minerals;
            Foods = c.Foods;
        }

        public int Minerals
        {
            get { return _minerals; }
            set
            {
                SetAndNotify(ref _minerals, value, () => Minerals);
            }
        }

        public int Foods
        {
            get { return _foods; }
            set
            {
                SetAndNotify(ref _foods, value, () => Foods);
            }
        }
    }

}