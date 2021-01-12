using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Logic.Objects.Environment.Translucent_Blocks;
using Terraria_sMario.Images;

namespace Terraria_sMario.Classes.Logic.Objects.Environment.Static_Blocks
{
    class LadderBlock : TransparentBlockObject
    {
        public LadderBlock(Point coords)
        {
            this.coords = coords;
            size = new Size(Parameters.blockSize, Parameters.blockSize);
            drawingImage = StaticBlocks_res.brick;
        }

        public LadderBlock(int X, int Y)
        {
            coords = new Point(X, Y);
            size = new Size(Parameters.blockSize, Parameters.blockSize);
            drawingImage = StaticBlocks_res.Ladder;
        }
    }
}
