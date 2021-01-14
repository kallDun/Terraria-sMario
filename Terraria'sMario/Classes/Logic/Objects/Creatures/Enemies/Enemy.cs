using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.Behavior;
using Terraria_sMario.Classes.Logic.Objects.Features;
using static Terraria_sMario.Classes.Logic.Objects.Features.EnemyAnimationTypes;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies
{
    abstract class Enemy : Entity
    {
        public List<EnemyAnimation> animations { get; protected set; }
        public EnemyAnimation activeAnimation { get; protected set; }

        protected BehaviorControl enemy_behavior;
        protected int standartHeal_enemy = 10;


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
                setAnimation(Dead);
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
                setAnimation(Healing);
                return true;
            }
            else
                return false;
        }

        public override bool Shoot(in List<ParentObject> objects)
        {
            if (base.Shoot(objects))
            {
                setAnimation(Shooting);
                return true;
            }
            else
                return false;
        }

        // Animation Set

        public void setAnimation(EnemyAnimationTypes type) // Animations SET
            => activeAnimation = animations.Find(x => x.type == type);
    }
}
