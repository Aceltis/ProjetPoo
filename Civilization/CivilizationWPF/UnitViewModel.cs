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
    class UnitViewModel : ObservableObject
    {
        private string _hp;
        private string _attackPoints;
        private string _defensePoints;
        private string _movePoints;
        
        public UnitViewModel(Unit u)
        {
            u.PropertyChanged += new PropertyChangedEventHandler(delegate(object sender, PropertyChangedEventArgs args)
            {
                HP = ((Unit)sender).HP.ToString();
                AttackPoints = ((Unit)sender).AttackPoints.ToString();
                DefensePoints = ((Unit)sender).DefensePoints.ToString();
                MovePoints = ((Unit)sender).MovePoints.ToString();
            });

            HP = u.HP.ToString();
            AttackPoints = u.AttackPoints.ToString();
            DefensePoints = u.DefensePoints.ToString();
            MovePoints = u.MovePoints.ToString();
        }

        public string HP
        {
            get { return _hp; }
            set
            {
                SetAndNotify(ref _hp, value, () => HP);
            }
        }

        public string AttackPoints
        {
            get { return _attackPoints; }
            set
            {
                SetAndNotify(ref _attackPoints, value, () => AttackPoints);
            }
        }

        public string DefensePoints
        {
            get { return _defensePoints; }
            set
            {
                SetAndNotify(ref _defensePoints, value, () => DefensePoints);
            }
        }

        public string MovePoints
        {
            get { return _movePoints; }
            set
            {
                SetAndNotify(ref _movePoints, value, () => MovePoints);
            }
        }
    }
    
}
