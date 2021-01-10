using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.AI_Behavior;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.Behavior;
using Terraria_sMario.Classes.Logic.Objects.Features;
using static Terraria_sMario.Images.Mobs_res;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.Skeletons
{
    class SkeletonHealer : Enemy
    {
        public SkeletonHealer(int X, int Y)
        {
            coords = new Point(X, Y);
            size = new Size(45, 90);
            maxHealth = 20;
            health = maxHealth;
            jumpHeight = -14;
            baseCloseDamage = 7;
            baseTimerHitMax = 1.5;
            damage_heal_ActionRadius = 30;
            standartHeal_enemy = 8;
            resistancesEffects.Add(EffectTypes.Curse);
            resistancesEffects.Add(EffectTypes.Blessing);

            enemy_behavior = new BehaviorControl(
                new List<BehaviorUnitMoving> {
                    new BehaviorUnitMoving(BehaviorTypes.Walking, duration: 8, radius: 12),
                    new BehaviorUnitMoving(BehaviorTypes.Standing, duration: 2)
                },
                new List<BehaviorUnitFinding>
                {
                    new BehaviorUnitFinding(BehaviorTypes.SearchingInFrontOfView, searchEveryone: false, duration: 30, radius: 15)
                },
                new List<BehaviorUnitCombat>
                {
                    new BehaviorUnitCombat(BehaviorTypes.Healing, duration: 15, healingDistance: 150)
                }
                );

            uI_Entity_Draw = new UI_Entity_Draw(this, Services.UI_Entity_Draw_Type.WithoutName);
            drawingImage = Skeleton_image;
            animations = new List<EnemyAnimation>
            {
                new EnemyAnimation(new List<Image>{
                    Skeleton_walking_3,
                    Skeleton_walking_2, Skeleton_walking_4,
                    Skeleton_walking_2 }, EnemyAnimationTypes.Walking, skipFrames: 6),

                new EnemyAnimation(new List<Image>{ /*animation pictures*/ }, EnemyAnimationTypes.Standing),

                new EnemyAnimation(new List<Image>{
                    Skeleton_walking_3,
                    Skeleton_walking_2, Skeleton_walking_4,
                    Skeleton_walking_2 }, EnemyAnimationTypes.Running, skipFrames: 2),

                new EnemyAnimation(new List<Image>{ /*animation pictures*/ }, EnemyAnimationTypes.Jumping),
                new EnemyAnimation(new List<Image>{ /*animation pictures*/ }, EnemyAnimationTypes.Hitting),
                new EnemyAnimation(new List<Image>{ /*animation pictures*/ }, EnemyAnimationTypes.Dead)
            };
        }
    }
}
