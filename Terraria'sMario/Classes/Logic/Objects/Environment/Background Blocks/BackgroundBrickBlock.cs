using System.Drawing;
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