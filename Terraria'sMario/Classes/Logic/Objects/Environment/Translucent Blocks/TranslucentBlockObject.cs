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
        protected Image transparentImage_20perc; // 3
        protected Image transparentImage_35perc; // 2
        protected Image transparentImage_50perc; // 1
        protected Image transparentImage_80perc; // usual, 0
        public bool isPlayerIn { get; private set; } = false;

        public int translucentNow { get; private set; } = 0;

        public override void Draw(Graphics g)
        {
            if (!isRendered) return;

            g.DrawImage(
                translucentNow == 0 ? transparentImage_80perc :
                translucentNow == 1 ? transparentImage_50perc :
                translucentNow == 2 ? transparentImage_35perc : transparentImage_20perc,
                coords);
        }

        public override void updateProperties(in List<ParentObject> objects)
        {
            // player check
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

            // block translucent check
            Predicate<ParentObject> predicateTranslucentBlock = delegate (ParentObject obj) { return obj is TranslucentBlockObject; };
            var verySmall_radius = 2 * Parameters.blockSize; // 1 
            var small_radius = 4 * Parameters.blockSize; // 2 
            var medium_radius = 5 * Parameters.blockSize; // 2.5 
            var large_radius = 7 * Parameters.blockSize; // 3.5 
            // blocks to up, down, left & right

            if (setTranslucent(objects, predicateTranslucentBlock, small_radius, 3)) return;
            if (setTranslucent(objects, predicateTranslucentBlock, medium_radius, 2)) return;
            if (setTranslucent(objects, predicateTranslucentBlock, large_radius, 1)) return;

            translucentNow = 0;
        }

        private bool setTranslucent(in List<ParentObject> objects, Predicate<ParentObject> predicateTranslucentBlock, int radius, int translucent)
        {
            var blocks = getNearBlocks(objects, predicateTranslucentBlock, radius);
            foreach (var item in blocks)
            {
                if ((item as TranslucentBlockObject).isPlayerIn)
                {
                    translucentNow = translucent;
                    return true;
                }
            }
            return false;
        }

        private List<ParentObject> getNearBlocks(in List<ParentObject> objects, Predicate<ParentObject> predicateTranslucentBlock, int radius)
        {
            AbstractObject area = new AbstractObject(
                new Point(coords.X - radius / 2, coords.Y - radius / 2),
                new Size(size.Width + radius, size.Height + radius));

            return CheckNearObjectByPredicationService.getNearObjects(objects, area, predicateTranslucentBlock);
        }
    }
}
