using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terraria_sMario.Classes.Management.Design_Elements
{
    class Menu_Background
    {

        public void Draw(Graphics g)
        {
            g.FillRectangle(Brushes.LightBlue, new RectangleF(0, 0, Form1.ActiveForm.Width, Form1.ActiveForm.Height));

        }

    }
}
