﻿
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies;
using Terraria_sMario.Classes.Logic.Objects.Environment.Static_Blocks;
using Terraria_sMario.Classes.Logic.Objects.Features;
using Terraria_sMario.Classes.Logic.Objects.Items.Weapons;
using Terraria_sMario.Classes.Logic.Services;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures
{
    abstract class Entity : ParentObject
    {
        // Fields

        public string Name { get; protected set; } = "Entity";
        public float health { get; protected set; }  
        public float maxHealth { get; protected set; }

        public int jumpHeight { get; protected set; } = -15;
        public int speed { get; protected set; } = 3;
        public bool canFly { get; protected set; } = false;
        public bool isTurnToRight { get; protected set; } = true;

        protected UI_Entity_Draw uI_Entity_Draw;

        // Effects System

        public List<Effect> effects { get; protected set; } = new List<Effect> { };
        public List<EffectTypes> resistancesEffects { get; protected set; } = new List<EffectTypes> { };

        public void getEffect(Effect effect)
        {
            if (!resistancesEffects.Contains(effect.effectType))
            {
                var eff = effects.Where(x => effect.effectType == x.effectType);
                if (eff.Count() > 0)
                {
                    eff.First().addDuration(effect.duration);
                }
                else
                {
                    effects.Add(effect);
                }
            }
        }

        // Damage and Health System

        public bool isFullHealth() => health == maxHealth;

        public bool isDead { get; private set; } = false;

        public void getDamage(float damage)
        {
            if (damage < 0 || isDead) return;
            if (Effect.isEffectInList(effects, EffectTypes.Poisoning)) damage *= 1.5f;
            health -= damage;
            uI_Entity_Draw.gettingDamage(damage);

            if (health <= 0) isDead = true;
        }

        public void getCure(float healing)
        {
            if (healing < 0 || isDead) return;
            if (Effect.isEffectInList(effects, EffectTypes.Blessing)) healing *= 2;
            if (Effect.isEffectInList(effects, EffectTypes.Curse)) healing /= 2;

            if (Effect.isEffectInList(effects, EffectTypes.Fire))
            {
                effects.Remove(effects.Where(x => x.effectType == EffectTypes.Fire).First());
            }
            else
            if (Effect.isEffectInList(effects, EffectTypes.Poisoning))
            {
                effects.Remove(effects.Where(x => x.effectType == EffectTypes.Poisoning).First());
            }
            else
            {
                health += healing;
                if (health > maxHealth) health = maxHealth;
            }            
        }

        // Hit & Weapon System

        public float baseCloseDamage { get; protected set; } = 5;
        public double baseTimerHitMax { get; protected set; } = 1.5; // in seconds
        public int damage_heal_ActionRadius { get; protected set; } = 15;
        public Weapon weaponInHand { get; protected set; }

        public virtual bool Hit(in List<ParentObject> objects)
        {
            if (!isReadyToHit) return false;

            if (weaponInHand != null && weaponInHand.canMeleeDamage)
            {
                weaponInHand.MakeMeleeDamage(objects, this);
                restartHitTimer();
                return true;
            }
            else
            {
                CheckEntityService.getNearEntity(objects, this, damage_heal_ActionRadius)
                    ?.getDamage(baseCloseDamage);

                restartHitTimer();
                return true;
            }
        }

        public virtual bool Shoot(in List<ParentObject> objects)
        {
            if (!isReadyToHit) return false;

            if (weaponInHand != null && weaponInHand.canShoot)
            {
                newObjects.Concat(weaponInHand.Shoot());
                restartHitTimer();
                return true;
            }
            else
                return false;
        }

        public virtual bool Heal(in List<ParentObject> objects, int standartHeal = 0)
        {
            if (!isReadyToHit) return false;

            if (weaponInHand != null && weaponInHand.canHeal)
            {
                weaponInHand.MakeHealing(objects, this);
                restartHitTimer();
                return true;
            }
            else
            {
                if (standartHeal != 0)
                {
                    if (!isFullHealth()) getCure(standartHeal);
                    else
                    {
                        CheckEntityService.getNearEntity(objects, this, damage_heal_ActionRadius, isEnemy: false)
                            ?.getCure(standartHeal);
                    }

                    restartHitTimer();
                    return true;
                }
                else
                    return false;
            }
        }


        public Stopwatch timerHitNow { get; protected set; } = new Stopwatch(); // HIT TIMER
        public bool isReadyToHit { get; protected set; } = true;

        public void updateTimerForHit()
        {
            var max = weaponInHand != null ? weaponInHand.timerHitMax : baseTimerHitMax;
            if (timerHitNow.ElapsedMilliseconds >= max * 1000)
            {
                timerHitNow.Stop();
                isReadyToHit = true;
            }
        }
        private void restartHitTimer()
        {
            timerHitNow.Restart();
            timerHitNow.Start();
            isReadyToHit = false;
        }

        // Threads

        protected List<ParentObject> newObjects = new List<ParentObject> { }; //-------------------------
        public List<ParentObject> updateWorld() // ----------------------------- add new objects to world
        {
            var list = newObjects;
            newObjects.Clear();
            return list;
        }

        public void update()
        {
            // update effects duration
            foreach (var effect in effects) effect.updateDurat();

            // update damage timer
            uI_Entity_Draw.updateDamageTimer();

            // update hit timer
            updateTimerForHit();

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
            if (!isDead) uI_Entity_Draw.Draw(g, this);
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

        public virtual int moveRightOrLeft(in List<ParentObject> objects, int direction, bool run = false) 
        {
            int offsetX = direction * (int) Math.Round(run ? speed * 1.5 : speed);

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
