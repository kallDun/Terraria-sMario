using System;
using System.Collections.Generic;
using System.Linq;
using Terraria_sMario.Classes.Logic.Objects.Creatures;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Items;
using Terraria_sMario.Classes.Logic.Services;

namespace Terraria_sMario.Classes.Logic.Objects.Items.Weapons
{
    class Weapon : ParentItem
    {
        // Для дальнего оружия
        public bool canShoot { get; protected set; } = false;
        public int bulletCount { get; protected set; } = 1;
        // экземпляр патрона для стрелкового оружия

        //  Для ближнего оружия
        public int rangeOfMeleeHit { get; protected set; } 
        public bool splashDamage { get; protected set; } 

        // Обязательные поля
        public float damage { get; protected set; }
        public List<Effect> getting_weapon_effects { get; protected set; } = new List<Effect> { };
        public double timerHitMax { get; protected set; }

        public bool MakeMeleeDamage(in List<ParentObject> objects, in Entity self)
        {
            if (splashDamage)
            {
                List<Entity> entities = CheckEntityService.getAllNearEntities(objects, self, rangeOfMeleeHit);
                foreach (var entity in entities)
                {
                    HitEntity(entity);
                }
                return entities.Count > 0;
            }
            else
            {
                var entity = CheckEntityService.getNearEntity(objects, self, rangeOfMeleeHit);
                if (entity != null)
                {
                    HitEntity(entity);

                    return true;
                }
                else
                    return false;
            }
        }

        private void HitEntity(Entity entity)
        {
            entity.getDamage(damage);
            foreach (var effect in getting_weapon_effects)
            {
                entity.getEffect(effect);
            }
        }


        public List<ParentObject> Shoot() // EMPTY
        {
            return new List<ParentObject> { }; // Возвращает список из выпущенных патронов
        }
    }
}
