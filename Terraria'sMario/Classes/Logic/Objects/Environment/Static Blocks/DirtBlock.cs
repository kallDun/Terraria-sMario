using System.Drawing;
using Terraria_sMario.Images;

namespace Terraria_sMario.Classes.Logic.Objects.Environment.Static_Blocks
{
    class DirtBlock : StaticBlockObject
    {
        public DirtBlock(int X, int Y)
        {
            coords = new Point(X, Y);
            size = new Size(Parameters.blockSize, Parameters.blockSize);
            drawingImage = StaticBlocks_res.Dirt;
        }
    }
}