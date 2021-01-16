﻿using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Animations;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.Behavior;
using static Terraria_sMario.Classes.Logic.Objects.Creatures.Animations.EntityAnimationTypes;
using Terraria_sMario.Classes.Logic.Objects.Features;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Animations.Effect_Animations;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.Loot_Drop_System;
using System.Linq;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies
{
    abstract class Enemy : Entity
    {
        protected LootSystem lootSystem = new LootSystem(new ItemDropUnit(numberOfCoins: 1, 100));
        protected BehaviorControl enemy_behavior;
        protected int standartHeal_enemy = 6;

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
                if (lootSystem != null)
                    newObjects = newObjects
                        .Concat(lootSystem.dropAll(X: coords.X, Y: coords.Y - 15))
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

    }
}
