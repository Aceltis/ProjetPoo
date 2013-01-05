namespace Implementation
{
    using Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using System.Drawing;


    public class CaseImageFlyweight : ICaseImageFlyweight
    {
        private Dictionary<int, Image> CaseImages;

        public CaseImageFlyweight()
        {
            CaseImages = new Dictionary<int, Image>();
            CaseImages.Add(0, Image.FromFile("../../../CivilizationWPF/Resource/map/fields/mountain.png"));
            CaseImages.Add(1, Image.FromFile("../../../CivilizationWPF/Resource/map/fields/plain.png"));
            CaseImages.Add(2, Image.FromFile("../../../CivilizationWPF/Resource/map/fields/desert.png"));
        }

        public Image getCaseImage(int key)
        {
            return (CaseImages[key]);
        }
    }

}
