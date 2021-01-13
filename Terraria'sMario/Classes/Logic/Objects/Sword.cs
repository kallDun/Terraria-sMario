using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Images;

namespace Terraria_sMario.Classes.Logic.Objects
{
    class Sword : ParentObject
    {
        public Sword(int X, int Y)
        {
            coords = new Point(X, Y);
            size = new Size(50, 50);
            drawingImage = Items_res.Sword;
        }
        public override void Draw(Graphics g)
        {
            if (!isRendered) return;
            g.DrawImage(drawingImage, coords);
        }
        public override void updateProperties(in List<ParentObject> objects)
        {
        }
    }
}
