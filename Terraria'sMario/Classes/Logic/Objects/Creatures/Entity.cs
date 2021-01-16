using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Animations;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Animations.Effect_Animations;
using Terraria_sMario.Classes.Logic.Objects.Features;
using Terraria_sMario.Classes.Logic.Objects.Items.Weapons;
using Terraria_sMario.Classes.Logic.Services;
using static Terraria_sMario.Classes.Logic.Objects.Creatures.Animations.EntityAnimationTypes;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures
{
    abstract class Entity : ParentObject
    {
        // Fields

        public EntityTypes EntityType { get; protected set; }

        public string Name { get; protected set; } = "Entity";
        public float health { get; protected set; }  
        public float maxHealth { get; protected set; }

        public int jumpHeight { get; protected set; } = -15;
        public int speed { get; protected set; } = 3;
        public bool canFly { get; protected set; } = false;
        public bool isTurnToRight { get; protected set; } = true;

        public bool isColisionWithOtherEntitiesOn { get; protected set; } = true;

        // Drawing UI & Animations

        protected UI_Entity_Draw uI_Entity_Draw;
        public List<EntityAnimation> animations { get; protected set; }
        public EntityAnimation activeAnimation { get; protected set; }
        public List<EffectAnimation> environment_effects_anim { get; protected set; }
            = new List<EffectAnimation> { };

        public void setAnimation(EntityAnimationTypes type) // Animation SET
        {
            if (weaponInHand == null) setStandartAnimation(type);
            else
            {
                var animObject = EntityWeaponAnimation.getAnimationsToEntity
                    (weaponInHand.entityWeaponAnimations, EntityType);

                if (animObject != null)
                {
                    activeAnimation = animObject.animations.Find(x => x.type == type);

                    if (activeAnimation == null || activeAnimation.images.Count == 0) setStandartAnimation(type);
                }
                else 
                    setStandartAnimation(type);
            }
        }

        private void setStandartAnimation(EntityAnimationTypes type)
        {
            if (type == Walking && EntityState == EntityStates.Sitting) type = WalkingSquat;

            activeAnimation = animations.Find(x => x.type == type);

            if (type == Standing && EntityState != EntityStates.Standing) activeAnimation = null;
        }

        // Character states (Stand, Sit, Dead)

        public EntityStates EntityState { get; protected set; } = EntityStates.Standing;

        public Image standingImage { get; protected set; }
        public Image sittingImage { get; protected set; }
        public Image deadImage { get; protected set; }

        public Size standing_size { get; protected set; }
        public Size sitting_size { get; protected set; }
        public Size dead_size { get; protected set; }

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

            if (health <= 0) 
            {
                isDead = true;
                setAnimation(Dead);
                EntityState = EntityStates.Dead;
            }
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
            if (EntityState != EntityStates.Standing) return false;
            if (Effect.isEffectInList(effects, EffectTypes.Stunning)) return false;

            if (weaponInHand != null)
            {
                if (!weaponInHand.canMeleeHit) return false;

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

        public virtual bool Shoot(in List<ParentObject> objects, float? angle = null)
        {
            if (!isReadyToHit) return false;
            if (EntityState != EntityStates.Standing) return false;
            if (Effect.isEffectInList(effects, EffectTypes.Stunning)) return false;

            if (weaponInHand != null && weaponInHand.canShoot)
            {
                var bullets_list = weaponInHand.Shoot(this, angle);
                newObjects = newObjects.Concat(bullets_list).ToList();
                restartHitTimer();
                return true;
            }
            else
                return false;
        }

        public virtual bool Heal(in List<ParentObject> objects, int standartHeal = 0)
        {
            if (!isReadyToHit) return false;
            if (Effect.isEffectInList(effects, EffectTypes.Stunning)) return false;

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
            var list = new List<ParentObject>(newObjects);
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

            // update Weapon
            if (weaponInHand?.opportunUseCount == 0) weaponInHand = null;

            // update Effects Statements
            if (Effect.isEffectInList(effects, EffectTypes.Fire)) getDamage(1.0f / Parameters.fps);
            if (Effect.isEffectInList(effects, EffectTypes.Blessing)) getCure(0.5f / Parameters.fps);

            // update base states
            drawingImage = 
                EntityState == EntityStates.Standing ? standingImage :
                EntityState == EntityStates.Sitting ? sittingImage :
                EntityState == EntityStates.Dead ? deadImage : standingImage;
            size =
                EntityState == EntityStates.Standing ? standing_size :
                EntityState == EntityStates.Sitting ? sitting_size :
                EntityState == EntityStates.Dead ? dead_size : standing_size;
            isColisionWithOtherEntitiesOn =
                EntityState == EntityStates.Standing ? true :
                EntityState == EntityStates.Sitting ? true :
                EntityState == EntityStates.Dead ? false : true;

            // check the end of Environment Animations
            for (int i = environment_effects_anim.Count - 1; i >= 0; i--)
            {
                if (environment_effects_anim[i].isLastFrame()) 
                    environment_effects_anim.RemoveAt(i);
            }
        }

        public override void Draw(Graphics g)
        {
            if (!isDead) uI_Entity_Draw.Draw(g, this);

            foreach (var animat in environment_effects_anim)
            {
                animat.Draw(g, coords, true);
            }
        }

        // Gravitation

        protected GravitationService gravitationService = new GravitationService();

        public void updateGravitation(in List<ParentObject> objects) 
        {
            if (!canFly)
            {
                gravitationService.updateGravitation(this, objects);
            }
        }

        // Moving System

        public virtual void Jump(in List<ParentObject> objects) 
        {
            if (EntityState != EntityStates.Standing) return;
            if (Effect.isEffectInList(effects, EffectTypes.Stunning)) return;

            gravitationService.tryToJump(objects, this, jumpHeight);
        }

        public virtual int moveRightOrLeft(in List<ParentObject> objects, int direction, bool run = false) 
        {
            if (Effect.isEffectInList(effects, EffectTypes.Stunning)) return 0;
            if (EntityState != EntityStates.Standing) run = false;

            int offsetX = direction * (int) Math.Round(run ? speed * 1.5 : speed);
            if (Effect.isEffectInList(effects, EffectTypes.Ice)) offsetX /= 2;

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

        public virtual int moveUpOrDown(int offsetY, in List<ParentObject> objects, int direction)
        {
            if (!canFly) return 0;
            if (Effect.isEffectInList(effects, EffectTypes.Stunning)) return 0;

            if (Effect.isEffectInList(effects, EffectTypes.Ice)) offsetY /= 2;

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
