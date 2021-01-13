using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Logic.Objects.Creatures;
using Terraria_sMario.Classes.Logic.Objects.Environment.Translucent_Blocks;
using Terraria_sMario.Classes.Logic.Services;

namespace Terraria_sMario.Classes.Logic.Objects.Environment.Transparent_Blocks.Translucent_Blocks
{
    class TranslucentBlockObject : ParentObject
    {
        protected Image transparentImage;
        public bool isPlayerIn { get; private set; } = false;
        public bool isTranslucentNow { get; private set; } = false;

        public override void Draw(Graphics g)
        {
            if (!isRendered) return;

            g.DrawImage(isTranslucentNow ? transparentImage : drawingImage,
                coords);
        }

        public override void updateProperties(in List<ParentObject> objects)
        {
            Predicate<ParentObject> predicatePlayer = delegate (ParentObject obj) { return obj is Player; };
            var player = CheckNearObjectByPredicationService.getNearObject(objects, this, predicatePlayer);
            if (player != null)
            {
                isPlayerIn = true;
            }
            else 
            { 
                isPlayerIn = false;
            }


            Predicate<ParentObject> predicateTranslucentBlock = delegate (ParentObject obj) { return obj is TranslucentBlockObject; };
            var radius = 6 * Parameters.blockSize; // 3 block to up, down, left & right
            AbstractObject area = new AbstractObject(
                new Point(coords.X - radius / 2, coords.Y - radius / 2), 
                new Size(size.Width + radius, size.Height + radius));
            
            var blocks = CheckNearObjectByPredicationService.getNearObjects(objects, area, predicateTranslucentBlock);

            foreach (var item in blocks)
            {
                if ((item as TranslucentBlockObject).isPlayerIn)
                {
                    isTranslucentNow = true;
                    return;
                }
            }
            isTranslucentNow = false;

        }

    }
}
