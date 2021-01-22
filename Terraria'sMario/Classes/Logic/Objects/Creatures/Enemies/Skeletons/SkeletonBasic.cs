
using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Animations;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.AI_Behavior;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.Behavior;
using Terraria_sMario.Classes.Logic.Objects.Features;
using Terraria_sMario.Images;
using static Terraria_sMario.Images.Mobs_res;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies
{
    class SkeletonBasic : Enemy
    {
        public SkeletonBasic(int X, int Y, int level = 1)
        {
            Name = "Skeleton";
            EntityType = EntityTypes.Skeleton;

            coords = new Point(X, Y);
            standing_size = new Size(40, 90);
            dead_size = new Size(40, 90);
            maxHealth = 30;
            health = maxHealth;
            jumpHeight = -14;
            baseCloseDamage = 7;
            baseTimerHitMax = 2; 
            damage_heal_ActionRadius = 20;
            resistancesEffects.Add(EffectTypes.Curse);

            this.level = level;
            updatePropertiesToLevel();

            enemy_behavior = new BehaviorControl(
                new List<BehaviorUnitMoving> {
                    new BehaviorUnitMoving(BehaviorTypes.Walking, duration: 6, radius: 10),
                    new BehaviorUnitMoving(BehaviorTypes.Standing, duration: 3)
                },
                new List<BehaviorUnitFinding>
                {
                    new BehaviorUnitFinding(BehaviorTypes.SearchingInFrontOfView, searchEveryone: false, duration: 30, radius: 6)
                },
                new List<BehaviorUnitCombat>
                {
                    new BehaviorUnitCombat(BehaviorTypes.Hitting, duration: 15)
                }
                );

            uI_Entity_Draw = new UI_Entity_Draw(this, Services.UI_Entity_Draw_Type.WithoutName);
            drawingImage = Skeleton_image;
            standingImage = Skeleton_image;
            deadImage = Skeleton_image;
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

                new EntityAnimation(new List<Image>{ /*animation pictures*/ }, EntityAnimationTypes.Hitting),
                new EntityAnimation(new List<Image>{ /*animation pictures*/ }, EntityAnimationTypes.Dead)
            };
        }
    }
}
