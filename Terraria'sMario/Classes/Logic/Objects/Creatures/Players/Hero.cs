using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Players
{
    class Hero : Player
    {
        public Hero(int X, int Y)
        {
            coords = new Point(X, Y);
            size = new Size(55, 100);
            health = 100;

            drawingImage = Resources.sherifreworked;
        }

    }
}
