namespace Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Drawing;

    public interface ICaseImageFlyweight
    {
        Image getCaseImage(int key);
        Image getFoWCaseImage(int key);
        Image getBonusImage(int key);
        Image getFoWBonusImage(int key);
        Image getCityImage(int key);
        Image getUnitImage(int key, Implementation.PlayerColor col);
    }
}
