﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Logic.Objects.Items.Weapons;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.AI_Behavior
{
    class BehaviorUnitCombat : BehaviorUnitParent
    {
        private int healingDistance;
        private double? last_distanceToEntity;

        public BehaviorUnitCombat(BehaviorTypes behaviorType, double duration, int healingDistance = 200)
        {
            this.healingDistance = healingDistance;
            this.behaviorType = behaviorType;
            this.duration = duration;
            durationNow = duration;
        }

        public ActionType Update(double? distanceToEntity, double? distanceToElly, in Entity entity, int range)
        {
            UpdateTimer();

            if (behaviorType == BehaviorTypes.Hitting)
            {
                if (entity.weaponInHand != null && !entity.weaponInHand.canMeleeHit)
                {
                    durationNow = 0;
                    return ActionType.None;
                }

                if (distanceToEntity == null) return ActionType.None;

                if (distanceToEntity > range)
                {
                    return ActionType.KeepMovingToEnemy;
                }
                else
                    return ActionType.Hit;
            }

            if (behaviorType == BehaviorTypes.Shooting)
            {
                if (entity.weaponInHand == null || !entity.weaponInHand.canShoot)
                {
                    durationNow = 0;
                    return ActionType.None;
                }

                if (distanceToEntity == null) return ActionType.None;

                var shooting_radius = entity.weaponInHand?.shootRadius;

                if (distanceToEntity > shooting_radius - 40)
                {
                    return ActionType.KeepMovingToEnemy;
                }
                else
                if (distanceToEntity < shooting_radius - 80)
                {
                    if (distanceToEntity < Parameters.blockSize)
                    {
                        if (last_distanceToEntity != null)
                        {
                            if (last_distanceToEntity < Parameters.blockSize)
                            {
                                last_distanceToEntity = distanceToEntity;
                                return ActionType.Retreat;
                            }
                            else
                            {
                                last_distanceToEntity = distanceToEntity;
                                return ActionType.Shoot;
                            }
                        }
                        else
                        {
                            last_distanceToEntity = distanceToEntity;
                            return ActionType.Shoot;
                        }
                    }
                    else
                    {
                        last_distanceToEntity = distanceToEntity;
                        return ActionType.Retreat;
                    }  
                }                    
                else
                    return ActionType.Shoot;
            }

            if (behaviorType == BehaviorTypes.Healing)
            {

                if (!entity.isFullHealth())
                {

                    if (distanceToEntity == null) return ActionType.Heal;

                    if (distanceToEntity < healingDistance)
                    {
                        if (last_distanceToEntity != null)
                        {
                            if (last_distanceToEntity <= distanceToEntity)
                            {
                                last_distanceToEntity = distanceToEntity;
                                return ActionType.Retreat;
                            }
                            else
                            {
                                last_distanceToEntity = distanceToEntity;
                                return ActionType.Heal;
                            }
                        }
                        else
                        {
                            last_distanceToEntity = distanceToEntity;
                            return ActionType.Retreat;
                        }               
                    }
                    else
                    {
                        last_distanceToEntity = distanceToEntity;
                        return ActionType.Heal;
                    }
                }
                else
                {
                    if (distanceToElly == null)
                    {
                        if (distanceToEntity != null)
                        {
                            durationNow = 0;
                            return ActionType.Retreat;
                        }
                        else return ActionType.None;
                    }

                    if (distanceToElly > range)
                    {
                        return ActionType.KeepMovingToElly;
                    }
                    else
                        return ActionType.Heal;
                }
            }
            return ActionType.None;
        }
    }
}