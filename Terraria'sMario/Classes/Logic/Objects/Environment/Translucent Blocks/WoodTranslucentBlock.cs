using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects.Environment.Transparent_Blocks.Translucent_Blocks;
using Terraria_sMario.Images;

namespace Terraria_sMario.Classes.Logic.Objects.Environment.Translucent_Blocks
{
    class WoodTranslucentBlock : TranslucentBlockObject
    {
        public WoodTranslucentBlock(int X, int Y)
        {
            coords = new Point(X, Y);
            size = new Size(Parameters.blockSize, Parameters.blockSize);
            drawingImage = TranslucentBlocks_res.Wood_80_;
            transparentImage = TranslucentBlocks_res.Wood_35_;
        }

        public WoodTranslucentBlock(Point coords)
        {
            this.coords = coords;
            size = new Size(Parameters.blockSize, Parameters.blockSize);
            drawingImage = TranslucentBlocks_res.Wood_80_;
            transparentImage = TranslucentBlocks_res.Wood_35_;
        }
    }
}
