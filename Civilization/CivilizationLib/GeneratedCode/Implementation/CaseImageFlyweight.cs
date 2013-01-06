namespace Implementation
{
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using System.IO;
    using System.Drawing;


    public class CaseImageFlyweight : ICaseImageFlyweight
    {
        private Dictionary<int, Image> CaseImages;
        private Dictionary<int, Image> BonusImages;
        private Dictionary<int, Image> CityImages;
        private Dictionary<int, Image> RedUnitsImages;
        private Dictionary<int, Image> BlueUnitsImages;
        private Dictionary<int, Image> OrangeUnitsImages;
        private Dictionary<int, Image> GreenUnitsImages;

        public CaseImageFlyweight() 
        {
            //Lands
            CaseImages = new Dictionary<int, Image>();
            CaseImages.Add(0, Image.FromFile("../../../CivilizationWPF/Resource/map/fields/mountain.png"));
            CaseImages.Add(1, Image.FromFile("../../../CivilizationWPF/Resource/map/fields/plain.png"));
            CaseImages.Add(2, Image.FromFile("../../../CivilizationWPF/Resource/map/fields/desert.png"));

            //Bonuses
            BonusImages = new Dictionary<int, Image>();
            BonusImages.Add(0, Image.FromFile("../../../CivilizationWPF/Resource/map/bonus/food.png"));
            BonusImages.Add(1, Image.FromFile("../../../CivilizationWPF/Resource/map/bonus/iron.png"));

            //Cities
            CityImages = new Dictionary<int, Image>();
            CityImages.Add(0, Image.FromFile("../../../CivilizationWPF/Resource/map/cities/city_red.png"));
            CityImages.Add(1, Image.FromFile("../../../CivilizationWPF/Resource/map/cities/city_blue.png"));
            CityImages.Add(2, Image.FromFile("../../../CivilizationWPF/Resource/map/cities/city_orange.png"));
            CityImages.Add(3, Image.FromFile("../../../CivilizationWPF/Resource/map/cities/city_green.png"));

            //Red Units
            RedUnitsImages = new Dictionary<int, Image>();
            RedUnitsImages.Add(0, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Red/boss.png"));
            RedUnitsImages.Add(1, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Red/student.png"));
            RedUnitsImages.Add(2, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Red/student_alone.png"));
            RedUnitsImages.Add(3, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Red/student_alone_bonus.png"));
            RedUnitsImages.Add(4, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Red/student_bonus.png"));
            RedUnitsImages.Add(5, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Red/teacher.png"));
            RedUnitsImages.Add(6, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Red/teacher_alone.png"));
            RedUnitsImages.Add(7, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Red/teacher_alone_bonus.png"));
            RedUnitsImages.Add(8, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Red/teacher_bonus.png"));

            //Blue Units
            BlueUnitsImages = new Dictionary<int, Image>();
            BlueUnitsImages.Add(0, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Blue/boss.png"));
            BlueUnitsImages.Add(1, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Blue/student.png"));
            BlueUnitsImages.Add(2, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Blue/student_alone.png"));
            BlueUnitsImages.Add(3, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Blue/student_alone_bonus.png"));
            BlueUnitsImages.Add(4, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Blue/student_bonus.png"));
            BlueUnitsImages.Add(5, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Blue/teacher.png"));
            BlueUnitsImages.Add(6, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Blue/teacher_alone.png"));
            BlueUnitsImages.Add(7, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Blue/teacher_alone_bonus.png"));
            BlueUnitsImages.Add(8, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Blue/teacher_bonus.png"));

            //Orange Units
            OrangeUnitsImages = new Dictionary<int, Image>();
            OrangeUnitsImages.Add(0, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Orange/boss.png"));
            OrangeUnitsImages.Add(1, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Orange/student.png"));
            OrangeUnitsImages.Add(2, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Orange/student_alone.png"));
            OrangeUnitsImages.Add(3, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Orange/student_alone_bonus.png"));
            OrangeUnitsImages.Add(4, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Orange/student_bonus.png"));
            OrangeUnitsImages.Add(5, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Orange/teacher.png"));
            OrangeUnitsImages.Add(6, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Orange/teacher_alone.png"));
            OrangeUnitsImages.Add(7, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Orange/teacher_alone_bonus.png"));
            OrangeUnitsImages.Add(8, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Orange/teacher_bonus.png"));

            //Green Units
            GreenUnitsImages = new Dictionary<int, Image>();
            GreenUnitsImages.Add(0, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Green/boss.png"));
            GreenUnitsImages.Add(1, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Green/student.png"));
            GreenUnitsImages.Add(2, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Green/student_alone.png"));
            GreenUnitsImages.Add(3, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Green/student_alone_bonus.png"));
            GreenUnitsImages.Add(4, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Green/student_bonus.png"));
            GreenUnitsImages.Add(5, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Green/teacher.png"));
            GreenUnitsImages.Add(6, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Green/teacher_alone.png"));
            GreenUnitsImages.Add(7, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Green/teacher_alone_bonus.png"));
            GreenUnitsImages.Add(8, Image.FromFile("../../../CivilizationWPF/Resource/map/units/Green/teacher_bonus.png"));

        }

        public Image getCaseImage(int key)  { return (CaseImages[key]); }
        public Image getBonusImage(int key) { return (BonusImages[key]); }
        public Image getCityImage(int key)  { return (CityImages[key]); }
        public Image getUnitImage(int key, PlayerColor col)
        {
            switch (col)
            {
                case PlayerColor.Red:
                    return RedUnitsImages[key];
                case PlayerColor.Blue:
                    return BlueUnitsImages[key];
                case PlayerColor.Orange:
                    return OrangeUnitsImages[key];
                case PlayerColor.Green:
                    return GreenUnitsImages[key];
                default :
                    throw new System.NotImplementedException();
            }
        }
    }

}
