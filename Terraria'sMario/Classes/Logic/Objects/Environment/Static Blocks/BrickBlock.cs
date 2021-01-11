using System.Drawing;
using Terraria_sMario.Images;

namespace Terraria_sMario.Classes.Logic.Objects.Environment.Static_Blocks
{
    class BrickBlock : StaticBlockObject
    {

        public BrickBlock(Point coords)
        {
            this.coords = coords;
            size = new Size(Parameters.blockSize, Parameters.blockSize);
            drawingImage = StaticBlocks_res.brick;
        }

        public BrickBlock(int X, int Y)
        {
            coords = new Point(X, Y);
            size = new Size(Parameters.blockSize, Parameters.blockSize);
            drawingImage = StaticBlocks_res.brick;
        }
    }
}
