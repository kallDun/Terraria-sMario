using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Logic.Objects;
using Terraria_sMario.Classes.Logic.Objects.Environment.Static_Blocks;

namespace Terraria_sMario.Classes.Logic.Services
{
    class GravitationService
    {
        public double acceler = 0;

        public void updateGravitation(ParentObject self, in List<ParentObject> objects)
        {
            // fix the bag, set block to up if collision it
            checkTheColissionWithFloor(self, objects);


            // set offset
            int offsetY = (int)Math.Round(acceler);


            Predicate<ParentObject> predicate = delegate (ParentObject obj) { return obj is LadderBlock; };
            var ladder = CheckNearObjectByPredicationService.getNearObject(objects, self, predicate);

            if (ladder != null && offsetY < 0)
            {
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
    }
}
