using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Items
{
    class ParentItem : ParentObject
    {
        protected bool isLayOnTheGround = true;
        public Image smallImage { get; protected set; }

        public ParentItem takeItem()
        {
            if (isLayOnTheGround)
            {
                coords = new Point(0, 0);
                isToDestroy = true;
                return this;
            }
            return null;
        }

        public ParentItem dropItem(Point coords)
        {
            if (!isLayOnTheGround)
            {
                this.coords = coords;
                isToDestroy = false;
                return this;
            }
            return null;
        }


        public override void Draw(Graphics g)
        {
            if (!isRendered) return;
            if (!isLayOnTheGround) return;

            g.DrawImage(drawingImage, coords);
        }


        public override void updateProperties()
        {

        }
    }
}
