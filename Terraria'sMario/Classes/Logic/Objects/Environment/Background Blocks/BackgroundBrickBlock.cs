using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Images;

namespace Terraria_sMario.Classes.Logic.Objects.Environment.Background_Blocks
{
    class BackgroundBrickBlock : BackgroundBlockObject
    {
        public BackgroundBrickBlock(Point coords)
        {
            this.coords = coords;
            size = new Size(Parameters.blockSize, Parameters.blockSize);
            drawingImage = TranslucentBlocks_res.Background_Brick;
        }
    }
}
