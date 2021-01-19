using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terraria_sMario.Classes.Management.Design_Elements
{
    static class DesignElementsStatic
    {

        public static void FillScreenWithColor(Graphics g, Brush brush)
        {
            g.FillRectangle(brush, new RectangleF(x: 0, y: 0, 1300, 700));
        }


    }
}
