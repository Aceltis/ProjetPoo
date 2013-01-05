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
    }
}
