﻿using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Animations;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.AI_Behavior;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.Behavior;
using Terraria_sMario.Classes.Logic.Objects.Features;
using Terraria_sMario.Classes.Logic.Objects.Items.Weapons.Guns;
using static Terraria_sMario.Images.Mobs_res;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.Skeletons
{
    class SkeletonArcher : Enemy
    {
        public SkeletonArcher(int X, int Y)
        {
            Name = "Skeleton Archer";
            EntityType = EntityTypes.Skeleton;

            coords = new Point(X, Y);
            size = new Size(40, 90);
            maxHealth = 25;
            health = maxHealth;
            jumpHeight = -14;
            baseCloseDamage = 7;
            baseTimerHitMax = 2;
            damage_heal_ActionRadius = 20;

            weaponInHand = new Basic_Bow();


            enemy_behavior = new BehaviorControl(
                new List<BehaviorUnitMoving> {
                    new BehaviorUnitMoving(BehaviorTypes.Walking, duration: 6, radius: 15),
                    new BehaviorUnitMoving(BehaviorTypes.Standing, duration: 5)
                },
                new List<BehaviorUnitFinding>
                {
                    new BehaviorUnitFinding(BehaviorTypes.SearchingEverywhere, searchEveryone: true, duration: 30, radius: 5)
                },
                new List<BehaviorUnitCombat>
                {
                    new BehaviorUnitCombat(BehaviorTypes.Shooting, duration: 15),
                    new BehaviorUnitCombat(BehaviorTypes.Hitting, duration: 15)
                }
                );

            uI_Entity_Draw = new UI_Entity_Draw(this, Services.UI_Entity_Draw_Type.WithoutName);
            drawingImage = Skeleton_image;
            animations = new List<EntityAnimation>
            {
                new EntityAnimation(new List<Image>{
                    Skeleton_walking_3,
                    Skeleton_walking_2, Skeleton_walking_4,
                    Skeleton_walking_2 }, EntityAnimationTypes.Walking, skipFrames: 6),

                new EntityAnimation(new List<Image>{ /*animation pictures*/ }, EntityAnimationTypes.Standing),

                new EntityAnimation(new List<Image>{
                    Skeleton_walking_3,
                    Skeleton_walking_2, Skeleton_walking_4,
                    Skeleton_walking_2 }, EntityAnimationTypes.Running, skipFrames: 2),

                new EntityAnimation(new List<Image>{ /*animation pictures*/ }, EntityAnimationTypes.Jumping),
                new EntityAnimation(new List<Image>{ /*animation pictures*/ }, EntityAnimationTypes.Hitting),
                new EntityAnimation(new List<Image>{ /*animation pictures*/ }, EntityAnimationTypes.Dead)
            };
        }
    }
}
