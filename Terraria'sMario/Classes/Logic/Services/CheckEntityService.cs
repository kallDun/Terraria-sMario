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
            var coord = new Point(entity.coords.X, entity.coords.Y);
            var size = new Size(entity.size.Width + rangeOfMeleeHit, entity.size.Height);
            if (!entity.isTurnToRight) coord.Offset(-rangeOfMeleeHit, 0);
            var entityObject = new AbstractObject(coord, size);

            return getListOfFoundedEntities(objects, entity, entityObject);
        }

        public static List<Entity> getListOfFoundedEntities(List<ParentObject> objects, Entity entity, AbstractObject entityObject)
        {
            var entities = new List<Entity> { };

            foreach (var obj in objects)
            {
                if (getTargetPredicate(entity)(obj) && obj != entity)
                {
                    if (entityObject.coords.X + entityObject.size.Width > obj.coords.X &&
                        entityObject.coords.X < obj.coords.X + obj.size.Width &&
                        entityObject.coords.Y + entityObject.size.Height > obj.coords.Y &&
                        entityObject.coords.Y < obj.coords.Y + obj.size.Height)
                    {
                        if (!(obj as Entity).isDead) entities.Add(obj as Entity);
                    }
                }
            }

            return entities;
        }

        private static Predicate<ParentObject> getTargetPredicate(Entity entity)
            => delegate (ParentObject obj)
            {
                return
                (entity is Player) ? obj is Enemy :
                (entity is Enemy) ? obj is Player :
                false;
            };


        public static List<Entity> searchAllEntities(List<ParentObject> objects, Entity entity, int radius, bool isEverywhere)
        {
            Point coord;
            Size size;

            if (isEverywhere)
            {
                coord = new Point(entity.coords.X, entity.coords.Y);
                size = new Size(entity.size.Width + radius * 2, entity.size.Height + radius * 2);
                coord.Offset(-radius, -radius);
            }
            else
            {
                coord = new Point(entity.coords.X, entity.coords.Y);
                size = new Size(entity.size.Width + radius, entity.size.Height + radius * 2);
                coord.Offset(0, -radius);
                if (!entity.isTurnToRight) coord.Offset(-radius, 0);
            }
            
            var entityObject = new AbstractObject(coord, size);


            return getListOfFoundedEntities(objects, entity, entityObject);
        }

        public static Entity searchTheNearestEntity(List<ParentObject> objects, Entity entity, int radius, bool isEverywhere)
        {
            var list = searchAllEntities(objects, entity, radius, isEverywhere);
            return list.Count > 0 ? list.First() : null;
        }
    }
}
