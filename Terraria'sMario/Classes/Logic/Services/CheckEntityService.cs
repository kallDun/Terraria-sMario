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
        public static Entity getNearEntity(in List<ParentObject> objects, Entity entity, int rangeOfMeleeHit)
        {
            var list = getAllNearEntities(objects, entity, rangeOfMeleeHit);
            return list.Count > 0 ? list.First() : null;
        }

        public static List<Entity> getAllNearEntities(List<ParentObject> objects, Entity entity, int rangeOfMeleeHit)
        {
            var entities = new List<Entity> { };

            Predicate<ParentObject> predicate =
                delegate (ParentObject obj) { return
                (entity is Player) ? obj is Enemy :
                (entity is Enemy) ? obj is Player :
                false;
                };


            var coord = new Point(entity.coords.X, entity.coords.Y);
            var size = new Size(entity.size.Width + rangeOfMeleeHit, entity.size.Height);
            if (!entity.isTurnToRight) coord.Offset(-rangeOfMeleeHit, 0);
            var entityObject = new AbstractObject(coord, size);


            foreach (var obj in objects)
            {
                if (predicate(obj) && obj != entity)
                {
                    if (entityObject.coords.X + entityObject.size.Width > obj.coords.X &&
                        entityObject.coords.X < obj.coords.X + obj.size.Width &&
                        entityObject.coords.Y + entityObject.size.Height > obj.coords.Y &&
                        entityObject.coords.Y < obj.coords.Y + obj.size.Height)
                    {
                        entities.Add(obj as Entity);
                    }
                }
            }

            return entities;
        }
    }
}
