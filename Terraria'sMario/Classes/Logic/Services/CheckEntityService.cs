using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Logic.Objects;
using Terraria_sMario.Classes.Logic.Objects.Creatures;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies;

namespace Terraria_sMario.Classes.Logic.Services
{
    static class CheckEntityService
    {
        static public Enemy getNearEnemy(in List<ParentObject> objects, in Entity entity, int rangeOfMeleeHit, bool isTurnToRight)
        {
            var coord = new Point(entity.coords.X, entity.coords.Y);
            var size = new Size(entity.size.Width + rangeOfMeleeHit, entity.size.Height);

            if (!isTurnToRight)
                coord.Offset(-rangeOfMeleeHit, 0);

            var entityObject = new AbstractObject(coord , size);
            foreach (var obj in objects)
            {
                if (obj is Enemy)
                { 
                    return 
                        ourBlock.coords.X + ourBlock.size.Width > otherBlock.coords.X &&
                        ourBlock.coords.X < otherBlock.coords.X + otherBlock.size.Width &&
                        ourBlock.coords.Y + ourBlock.size.Height > otherBlock.coords.Y &&
                        ourBlock.coords.Y < otherBlock.coords.Y + otherBlock.size.Height;
                }
            }
            return null;
        }
    }
}
