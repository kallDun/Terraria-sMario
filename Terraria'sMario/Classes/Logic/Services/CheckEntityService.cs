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
        private const int overheadDistance = 50;

        public static Entity getNearEntity(in List<ParentObject> objects, Entity entity, int rangeOfMeleeHit, bool isEnemy = true)
        {
            var list = getAllNearEntities(objects, entity, rangeOfMeleeHit, isEnemy);
            return list.Count > 0 ? list.First() : null;
        }

        public static List<Entity> getAllNearEntities(List<ParentObject> objects, Entity entity, int rangeOfMeleeHit, bool isEnemy = true)
        {
            var coord = new Point(entity.coords.X, entity.coords.Y - overheadDistance);
            var size = new Size(entity.size.Width + rangeOfMeleeHit, entity.size.Height + overheadDistance);
            if (!entity.isTurnToRight) coord.Offset(-rangeOfMeleeHit, 0);
            var entityObject = new AbstractObject(coord, size);

            return getListOfFoundedEntities(objects, entity, entityObject, isEnemy);
        }

        private static List<Entity> getListOfFoundedEntities(List<ParentObject> objects, Entity entity, AbstractObject entityObject, bool isEnemy)
        {
            var predicate = isEnemy ? getEnemyTargetPredicate(entity) : getAllyTargetPredicate(entity);

            var entities = new List<Entity> { };

            foreach (var obj in objects)
            {
                if (predicate(obj) && obj != entity)
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

        public static List<Entity> searchAllEntities(List<ParentObject> objects, Entity entity, ParentObject obj, int radius, bool isEverywhere, bool isEnemy = true)
        {
            Point coord;
            Size size;

            if (isEverywhere)
            {
                coord = new Point(obj.coords.X, obj.coords.Y);
                size = new Size(obj.size.Width + radius * 2, obj.size.Height + radius * 2);
                coord.Offset(-radius, -radius);
            }
            else
            {
                coord = new Point(obj.coords.X, obj.coords.Y);
                size = new Size(obj.size.Width + radius, obj.size.Height + radius * 2);
                coord.Offset(0, -radius);
                if (!entity.isTurnToRight) coord.Offset(-radius, 0);
            }
            
            var entityObject = new AbstractObject(coord, size);


            return getListOfFoundedEntities(objects, entity, entityObject, isEnemy);
        }

        public static Entity searchTheNearestEntity(List<ParentObject> objects, Entity entity, int radius, bool isEverywhere, bool isEnemy = true)
        {
            var list = searchAllEntities(objects, entity, entity, radius, isEverywhere);
            return list.Count > 0 ? list.First() : null;
        }

        public static Entity getShootedEntity(List<ParentObject> objects, Entity entity, AbstractObject bullet)
        {
            var list = getListOfFoundedEntities(objects, entity, bullet, true);

            if (list.Count > 0) return list.First();
            else return null;
        }

        private static Predicate<ParentObject> getEnemyTargetPredicate(Entity entity)
            => delegate (ParentObject obj)
            {
                return
                (entity is Player) ? obj is Enemy :
                (entity is Enemy) ? obj is Player :
                false;
            };

        private static Predicate<ParentObject> getAllyTargetPredicate(Entity entity)
            => delegate (ParentObject obj)
            {
                return
                (entity is Player) ? obj is Player :
                (entity is Enemy) ? obj is Enemy :
                false;
            };
    }
}
