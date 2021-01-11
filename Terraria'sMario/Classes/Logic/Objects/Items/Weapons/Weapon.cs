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
        public int shootRadius { get; protected set; }
        // экземпляр патрона для стрелкового оружия

        //  Для ближнего оружия
        public bool canMeleeDamage { get; protected set; } = false;
        public bool canSplashDamage { get; protected set; }
        public int ClosePushing { get; protected set; }

        // Для ближнего и дальнего оружия
        public float damage { get; protected set; }
        public List<Effect> getting_weapon_effects { get; protected set; } = new List<Effect> { };

        // Для лечебного и ближнего оружия
        public int actionRadius { get; protected set; }

        // Для лечебного оружия
        public float healing { get; protected set; }
        public bool canHeal { get; protected set; } = false;
        public bool canSplashHeal { get; protected set; }

        // Обязательные поля
        public double timerHitMax { get; protected set; }


        public bool MakeMeleeDamage(in List<ParentObject> objects, in Entity self)
        {
            if (canSplashDamage)
            {
                List<Entity> entities = CheckEntityService.getAllNearEntities(objects, self, actionRadius);
                foreach (var entity in entities)
                {
                    HitEntity(entity, objects);
                }
                return entities.Count > 0;
            }
            else
            {
                var entity = CheckEntityService.getNearEntity(objects, self, actionRadius);
                if (entity != null)
                {
                    HitEntity(entity, objects);

                    return true;
                }
                else
                    return false;
            }
        }

        public bool MakeHealing(in List<ParentObject> objects, in Entity self)
        {
            if (canSplashHeal)
            {
                List<Entity> allies = CheckEntityService.getAllNearEntities(objects, self, actionRadius, isEnemy: false);
                HealEntity(self);

                foreach (var ally in allies)
                {
                    HealEntity(ally);
                }

                return true;
            }
            else
            {
                if (!self.isFullHealth())
                {
                    HealEntity(self);
                    return true;
                }
                else
                {
                    var ally = CheckEntityService.getNearEntity(objects, self, actionRadius, isEnemy: false);
                    if (ally != null)
                    {
                        HealEntity(ally);
                        return true;
                    }
                    else
                        return false;
                }
                
            }
        }

        public List<ParentObject> Shoot() // EMPTY
        {
            return new List<ParentObject> { }; // Возвращает список из выпущенных патронов
        }




        private void HealEntity(Entity entity) => entity?.getCure(healing);

        private void HitEntity(Entity entity, in List<ParentObject> objects)
        {
            entity?.getDamage(damage);
            foreach (var effect in getting_weapon_effects)
            {
                entity?.getEffect(new Effect(effect));
            }
        }
    }
}
