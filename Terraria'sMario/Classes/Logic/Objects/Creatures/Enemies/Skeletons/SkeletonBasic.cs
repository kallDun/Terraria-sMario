﻿
using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.AI_Behavior;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.Behavior;
using Terraria_sMario.Classes.Logic.Objects.Features;
using Terraria_sMario.Images;
using static Terraria_sMario.Images.Mobs_res;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies
{
    class SkeletonBasic : Enemy
    {
        public SkeletonBasic(int X, int Y)
        {
            coords = new Point(X, Y);
            size = new Size(45, 90);
            maxHealth = 30;
            health = maxHealth;
            jumpHeight = -14;
            baseCloseDamage = 7;
            baseTimerHitMax = 2; 
            damage_heal_ActionRadius = 20;
            resistancesEffects.Add(EffectTypes.Curse);

            enemy_behavior = new BehaviorControl(
                new List<BehaviorUnitMoving> {
                    new BehaviorUnitMoving(BehaviorTypes.Walking, duration: 6, radius: 10),
                    new BehaviorUnitMoving(BehaviorTypes.Standing, duration: 3)
                },
                new List<BehaviorUnitFinding>
                {
                    new BehaviorUnitFinding(BehaviorTypes.SearchingInFrontOfView, searchEveryone: false, duration: 30, radius: 7)
                },
                new List<BehaviorUnitCombat>
                {
                    new BehaviorUnitCombat(BehaviorTypes.Hitting, duration: 15)
                }
                );

            uI_Entity_Draw = new UI_Entity_Draw(this, Services.UI_Entity_Draw_Type.WithoutName);
            drawingImage = Skeleton_image;
            animations = new List<EnemyAnimation>
            {
                new EnemyAnimation(new List<Image>{
                    Skeleton_walking_3,
                    Skeleton_walking_2, Skeleton_walking_4,
                    Skeleton_walking_2 }, EnemyAnimationTypes.Walking, skipFrames: 3),

                new EnemyAnimation(new List<Image>{ /*animation pictures*/ }, EnemyAnimationTypes.Standing),

                new EnemyAnimation(new List<Image>{ 
                    Skeleton_walking_3,
                    Skeleton_walking_2, Skeleton_walking_4,
                    Skeleton_walking_2 }, EnemyAnimationTypes.Running, skipFrames: 1),

                new EnemyAnimation(new List<Image>{ /*animation pictures*/ }, EnemyAnimationTypes.Jumping),
                new EnemyAnimation(new List<Image>{ /*animation pictures*/ }, EnemyAnimationTypes.Hitting),
                new EnemyAnimation(new List<Image>{ /*animation pictures*/ }, EnemyAnimationTypes.Dead)
            };
        }
    }
}