using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terraria_sMario.Classes.Logic.Objects.Environment.Static_Blocks
{
    class DirtBlock : StaticBlockObject
    {
        public DirtBlock(int X, int Y)
        {
            coords = new Point(X, Y);
            size = new Size(Parameters.blockSize, Parameters.blockSize);
            drawingImage = Resources.Dirt;
        }

    }
}

