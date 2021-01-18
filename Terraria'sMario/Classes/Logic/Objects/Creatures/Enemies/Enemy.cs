using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.Behavior;
using static Terraria_sMario.Classes.Logic.Objects.Creatures.Animations.EntityAnimationTypes;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Animations.Effect_Animations;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.Loot_Drop_System;
using System.Linq;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies
{
    abstract class Enemy : Entity
    {
        // Fields

        protected LootSystem lootSystem = new LootSystem(new ItemDropUnit(numberOfCoins: 1, 100));
        protected BehaviorControl enemy_behavior;
        protected int standartHeal_enemy = 6;

        // Score & Level System

        protected int scoreForHit = 1;
        protected int scoreForKilling = 3;

        public void updatePropertiesToLevel()
        {
            for (int i = 1; i <= level; i++)
            {
                maxHealth *= (float)Parameters.HealthMultiplierEnemy[i];
                health = maxHealth;
                damage_amplification *= (float)Parameters.DamageMultiplierEnemy[i];
            }
        }

        // Threads

        public override void Draw(Graphics g)
        {
            if (!isRendered) return;            

            if (activeAnimation == null || activeAnimation?.activeImage == null)
                g.DrawImage(drawingImage, coords);
            else
                activeAnimation.Draw(g, coords, isTurnToRight);

            base.Draw(g);
        }

        public override void updateProperties(in List<ParentObject> objects)
        {
            if (isDead)
            {
                newObjects = newObjects
                    .Concat(lootSystem.dropAll(coords, objects))
                    .ToList();

                return;
            }

            enemy_behavior?.update(this, objects); // Enemy behavior
        }

        // Actions

        public override void Jump(in List<ParentObject> objects)
        {
            setAnimation(Jumping);
            base.Jump(objects);
        }

        public override int moveRightOrLeft(in List<ParentObject> objects, int direction, bool run = false)
        {
            setAnimation(run ? Running : Walking);
            return base.moveRightOrLeft(objects, direction, run);
        }

        public override bool Hit(in List<ParentObject> objects)
        {
            if (base.Hit(objects))
            {
                setAnimation(Hitting);
                return true;
            }
            return false;
        }

        public override bool Heal(in List<ParentObject> objects, int standartHeal = 0)
        {
            if (base.Heal(objects, standartHeal_enemy))
            {
                setAnimation(Standing);
                environment__anim.Add(EffectAnimationTypes.Heal);
                return true;
            }
            else
                return false;
        }

        public override bool Shoot(in List<ParentObject> objects, float? angle = null)
        {
            if (base.Shoot(objects, angle))
            {
                setAnimation(Shooting);
                return true;
            }
            else
                return false;
        }

        // Override

        public override void getDamage(float damage, Entity entity = null)
        {
            if (isDead) return;
            base.getDamage(damage);

            // add scores:

            if (entity is Player)
            {
                if (isDead) (entity as Player).getScores(scoreForKilling);
                else (entity as Player).getScores(scoreForHit);
            }
        }

    }
}
