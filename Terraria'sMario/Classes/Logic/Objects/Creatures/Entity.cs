
using System;
using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.Services;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures
{
    abstract class Entity : ParentObject
    {
        public float health { get; protected set; }        

        public List<Effect> effects { get; protected set; } = new List<Effect> { };
        public List<EffectTypes> resistancesEffects { get; protected set; } = new List<EffectTypes> { };

        public bool isAlive() => health > 0;

        public void getEffect(Effect effect)
        {
            if (!resistancesEffects.Contains(effect.effectType))
            {
                effects.Add(effect);
            }
        }

        public void getDamage(float damage)
        {
            if (Effect.isEffectInList(effects, EffectTypes.Poisoning)) damage *= 1.5f;
            health -= damage;
        }

        public void getCure(float healing)
        {
            if (Effect.isEffectInList(effects, EffectTypes.Blessing)) healing *= 2;
            if (Effect.isEffectInList(effects, EffectTypes.Curse)) healing /= 2;
            health += healing;
        }

        public void update()
        {
            // update effects duration
            foreach (var effect in effects) effect.reduceDurat();

            // delete effect if duration <= 0
            for (int i = 0; i < effects.Count; i++)
            {
                if (!effects[i].isExist())
                {
                    effects.Remove(effects[i]);
                    break;
                }
            }
        }

        // Gravitation

        protected double acceler = 0;

        public void updateGravitation(in List<ParentObject> objects) 
        {
            int offsetY = (int) Math.Round(acceler);

            while (offsetY != 0)
            {
                var testCoords = new Point(coords.X, coords.Y + offsetY);

                if (!IntersectionService.isBlockIntersectSomething
                (new AbstractObject(testCoords, size),
                this,
                objects))
                {
                    coords = testCoords;
                    offsetY = 0;
                }
                else
                {
                    acceler = 0;
                    offsetY = offsetY > 0 ? offsetY - 1 : offsetY + 1;
                }
            }

            acceler += 1.5;
        }

        // Moving System

        public void Jump() 
        {
            acceler = -20; 
        }

        public void moveRightOrLeft(int offsetX, in List<ParentObject> objects) 
        {
            while (offsetX != 0)
            {
                var testCoords = new Point(coords.X + offsetX, coords.Y);

                if (!IntersectionService.isBlockIntersectSomething
                (new AbstractObject(testCoords, size),
                this,
                objects))
                {
                    coords = testCoords;
                    offsetX = 0;
                }
                else offsetX += offsetX > 0 ? - 1 : 1;
            }
        }

    }
}
