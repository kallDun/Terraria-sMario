using System;
using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects;
using Terraria_sMario.Classes.Logic.Objects.Environment.Static_Blocks;

namespace Terraria_sMario.Classes.Logic.Services
{
    class GravitationService
    {
        public double acceler { get; private set; } = 0;

        public void updateGravitation(ParentObject self, in List<ParentObject> objects)
        {
            // fix the bag, set block to up if collision it
            checkTheColissionWithFloor(self, objects);


            // set offset
            int offsetY = (int)Math.Round(acceler);

            

            if (isLadder(objects, self) && offsetY < 0)
            {
                // use ladder
                var offset_temp = -5;
                var testCoords = new Point(self.coords.X, self.coords.Y + offset_temp);
                if (!IntersectionService.isBlockIntersectSomething
                    (new AbstractObject(testCoords, self.size),
                    self, objects))
                {
                    self.offsetPositionX_Y(0, offset_temp);
                }

                acceler = 0;
            }
            else
            {
                // jump
                do
                {
                    var testCoords = new Point(self.coords.X, self.coords.Y + offsetY);

                    if (!IntersectionService.isBlockIntersectSomething
                    (new AbstractObject(testCoords, self.size),
                    self,
                    objects))
                    {
                        self.setNewCoords(testCoords);
                        offsetY = 0;
                        acceler += 1.5;
                    }
                    else
                    {
                        acceler = 0;
                        offsetY = offsetY > 0 ? offsetY - 1 : offsetY + 1;
                    }

                } while (offsetY != 0);

            }
        }

        private void checkTheColissionWithFloor(ParentObject self, in List<ParentObject> objects)
        {
            var test_coord = self.coords;
            while (IntersectionService.isBlockIntersectSomething(new AbstractObject(test_coord, self.size), self, objects))
            {
                test_coord.Offset(0, -2);
            }
            self.setNewCoords(test_coord);
        }

        public void tryToJump(in List<ParentObject> objects, ParentObject self,  int jumpHeight)
        {
            if (canJump(objects, self))
                acceler = jumpHeight;

            if (isLadder(objects, self)) 
                acceler = jumpHeight;
        }

        public bool canJump(in List<ParentObject> objects, ParentObject self)
        {
            var area = new AbstractObject(
                self.coords,
                new Size(self.size.Width, self.size.Height + 1));

            return IntersectionService.isBlockIntersectSomething(area, self, objects);
        }

        public bool isLadder(in List<ParentObject> objects, ParentObject self)
        {
            var area = new AbstractObject(
                self.coords,
                new Size(self.size.Width, self.size.Height + 1));

            Predicate<ParentObject> predicate = delegate (ParentObject obj) {
                return obj is LadderBlock; // jump ladder rule
            };
            var blockDown = CheckNearObjectByPredicationService.getNearObject(objects, area, predicate);

            return (blockDown != null);
        }
    }
}
