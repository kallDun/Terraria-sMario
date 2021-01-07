
using System;
using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects.Features;
using Terraria_sMario.Classes.Logic.Services;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures
{
    abstract class Entity : ParentObject
    {
        public string Name { get; protected set; } = "Entity";
        public float health { get; protected set; }  
        public float maxHealth { get; protected set; }

        public int jumpHeight { get; protected set; } = -15;
        public int speed { get; protected set; } = 3;
        public bool canFly { get; protected set; } = false;
        public bool isRunning = false;

        protected bool isTurnToRight = true;
        protected UI_Entity_Draw uI_Entity_Draw;

        // Effects System

        public List<Effect> effects { get; protected set; } = new List<Effect> { };
        public List<EffectTypes> resistancesEffects { get; protected set; } = new List<EffectTypes> { };

        public void getEffect(Effect effect)
        {
            if (!resistancesEffects.Contains(effect.effectType))
            {
                effects.Add(effect);
            }
        }

        // Damage and Health System

        public void getDamage(float damage)
        {
            if (damage < 0) return;
            if (Effect.isEffectInList(effects, EffectTypes.Poisoning)) damage *= 1.5f;
            health -= damage;
            uI_Entity_Draw.gettingDamage(damage);
        }

        public void getCure(float healing)
        {
            if (healing < 0) return;
            if (Effect.isEffectInList(effects, EffectTypes.Blessing)) healing *= 2;
            if (Effect.isEffectInList(effects, EffectTypes.Curse)) healing /= 2;
            health += healing;
            if (health > maxHealth) health = maxHealth;
        }

        public bool isAlive() => health > 0;

        // Threads

        public void update()
        {
            // update effects duration
            foreach (var effect in effects) effect.updateDurat();

            // update damage timer
            uI_Entity_Draw.updateDamageTimer();

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

        public override void Draw(Graphics g)
        {
            uI_Entity_Draw.Draw(g, this);
        }

        // Gravitation

        protected double acceler = 0;

        public void updateGravitation(in List<ParentObject> objects) 
        {
            if (canFly) return;

            int offsetY = (int) Math.Round(acceler);

            do
            {
                var testCoords = new Point(coords.X, coords.Y + offsetY);

                if (!IntersectionService.isBlockIntersectSomething
                (new AbstractObject(testCoords, size),
                this,
                objects))
                {
                    coords = testCoords;
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

        // Moving System

        public virtual void Jump() 
        {
            if (acceler == 0) acceler = jumpHeight; 
        }

        public virtual int moveRightOrLeft(in List<ParentObject> objects, int direction) 
        {
            int offsetX = direction * (int) Math.Round(isRunning ? speed * 1.5 : speed);

            while (offsetX != 0)
            {
                var testCoords = new Point(coords.X + offsetX, coords.Y);

                if (!IntersectionService.isBlockIntersectSomething
                (new AbstractObject(testCoords, size),
                this,
                objects))
                {
                    coords = testCoords;
                    if (isTurnToRight != (offsetX > 0))
                    {
                        isTurnToRight = (offsetX > 0);
                        drawingImage.RotateFlip(RotateFlipType.Rotate180FlipY);
                    }
                    return offsetX;
                    offsetX = 0;
                }
                else offsetX += offsetX > 0 ? - 1 : 1;
            }
            return 0;
        }

        public virtual int moveUpOrDown(int offsetY, in List<ParentObject> objects)
        {
            if (!canFly) return 0;

            while (offsetY != 0)
            {
                var testCoords = new Point(coords.X, coords.Y + offsetY);

                if (!IntersectionService.isBlockIntersectSomething
                (new AbstractObject(testCoords, size),
                this,
                objects))
                {
                    coords = testCoords;
                    return offsetY;
                    offsetY = 0;
                }
                else offsetY += offsetY > 0 ? -1 : 1;
            }
            return 0;
        }
    }
}
