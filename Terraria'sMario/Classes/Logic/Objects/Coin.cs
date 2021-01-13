using System;
using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects.Creatures;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Items;
using Terraria_sMario.Classes.Logic.Services;
using Terraria_sMario.Images;

namespace Terraria_sMario.Classes.Logic.Objects
{
    class Coin : ParentObject
    {
        private bool canToGrab = true;

        public Coin(int X, int Y)
        {
            coords = new Point(X, Y);
            size = new Size(25, 25);
            drawingImage = Items_res.Coin;
        }

        public override void Draw(Graphics g)
        {
            if (!isRendered) return;

            g.DrawImage(drawingImage, coords);
        }

        protected GravitationService gravitationService = new GravitationService();

        public override void updateProperties(in List<ParentObject> objects)
        {
            gravitationService.updateGravitation(this, objects);


            if (canToGrab)
            {
                Predicate<ParentObject> predicate = delegate (ParentObject obj) { return obj is Player; };

                var player = CheckNearObjectByPredicationService.getNearObject(objects, this, predicate);

                if (player != null)
                {
                    canToGrab = false;
                    isToDestroy = true;
                    (player as Player).addCoin();
                }
            }
        }
    }
}
