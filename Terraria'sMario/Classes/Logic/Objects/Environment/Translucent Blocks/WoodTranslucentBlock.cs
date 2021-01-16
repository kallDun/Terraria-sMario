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
            constructor();
        }

        public WoodTranslucentBlock(Point coords)
        {
            this.coords = coords;
            constructor();
        }

        private void constructor()
        {
            size = new Size(Parameters.blockSize, Parameters.blockSize);
            transparentImage_80perc = TranslucentBlocks_res.Wood_80_;
            transparentImage_50perc = TranslucentBlocks_res.Wood_50_;
            transparentImage_35perc = TranslucentBlocks_res.Wood_35_;
            transparentImage_20perc = TranslucentBlocks_res.Wood_20_;
        }
    }
}
