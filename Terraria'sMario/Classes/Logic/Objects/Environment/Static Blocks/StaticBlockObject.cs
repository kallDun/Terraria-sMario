using System.Collections.Generic;
using System.Drawing;

namespace Terraria_sMario.Classes.Logic.Objects.Environment
{
    class StaticBlockObject : ParentObject
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