using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terraria_sMario.Classes.Logic.Objects
{
    class AbstractObject : ParentObject
    {
        public AbstractObject(Point coords, Size size)
        {
            this.coords = coords;
            this.size = size;
        }

        public override void Draw(Graphics g)
        {
            
        }

        public override void updateProperties(in List<ParentObject> objects)
        {
            
        }
    }
}
