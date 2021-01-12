using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Logic.Services;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Items
{
    class ParentItem : ParentObject
    {
        public Image smallImage_inInventory { get; protected set; }

        public int opportunUseCount { get; protected set; } = -1; //  -1 and less -> is infinity
        public string Name { get; protected set; } = "Standart_Name_Item";
        public string Description { get; protected set; } = "Some information";

        public ParentItem takeItem()
        {
            isToDestroy = true;
            return this;
        }

        public ParentItem dropItem(Point coords)
        {
            this.coords = coords;
            isToDestroy = false;
            return this;
        }

        public virtual void Use()
        {
            opportunUseCount--;
        }

        public override void Draw(Graphics g)
        {
            if (!isRendered) return;

            g.DrawImage(drawingImage, coords);
        }

        public override void updateProperties()
        {

        }

        // Gravitation

        protected GravitationService gravitationService = new GravitationService();

        public void updateGravitation(List<ParentObject> objects) =>
            gravitationService.updateGravitation(this, objects);

    }
}
