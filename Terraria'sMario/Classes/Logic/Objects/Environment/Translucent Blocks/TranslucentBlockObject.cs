using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Logic.Objects.Environment.Translucent_Blocks;

namespace Terraria_sMario.Classes.Logic.Objects.Environment.Transparent_Blocks.Translucent_Blocks
{
    class TranslucentBlockObject : ParentObject
    {
        protected Image transparentImage;
        public bool isPlayerIn = false;

        public override void Draw(Graphics g)
        {
            if (!isRendered) return;

            g.DrawImage(isPlayerIn? transparentImage : drawingImage,
                coords);
            

        }

        public override void updateProperties(in List<ParentObject> objects)
        {
            isPlayerIn = false;
        }

    }
}
