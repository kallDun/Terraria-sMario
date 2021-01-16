using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terraria_sMario.Classes.Logic.Objects.Environment.Background_Blocks
{
    class BackgroundBlockObject : ParentObject
    {
        public override void Draw(Graphics g)
        {
            if (!isRendered) return;
            g.DrawImage(drawingImage, coords);
        }

        public override void updateProperties(in List<ParentObject> objects)
        {

        }
    }
}
