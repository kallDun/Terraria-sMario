using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Logic.Objects;

namespace Terraria_sMario.Classes.Logic.Services
{
    class GravitationService
    {
        public double acceler = 0;

        public void updateGravitation(ParentObject self, in List<ParentObject> objects)
        {

            int offsetY = (int)Math.Round(acceler);

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
}
