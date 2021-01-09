using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.Behavior;
using Terraria_sMario.Classes.Logic.Objects.Features;
using static Terraria_sMario.Classes.Logic.Objects.Features.EnemyAnimationTypes;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies
{
    class Enemy : Entity
    {
        public List<EnemyAnimation> animations { get; protected set; }
        public EnemyAnimation activeAnimation { get; protected set; }

        protected BehaviorControl enemy_behavior;

        public override void Draw(Graphics g)
        {
            if (!isRendered) return;            

            if (activeAnimation == null || activeAnimation?.activeImage == null)
                g.DrawImage(drawingImage, coords);
            else
                activeAnimation.Draw(g, coords, isTurnToRight);

            base.Draw(g);
        }

        public override void Jump()
        {
            setAnimation(Jumping);
            base.Jump();
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

        public void updateBehavior(in List<ParentObject> objects) {
            if (isDead) return;

            enemy_behavior?.update(this, objects); // Enemy behavior
        }

        public override void updateProperties() 
        {
            if (isDead)
            {
                setAnimation(Dead);
                return;
            }
        }

        public void setAnimation(EnemyAnimationTypes type) // Animations SET
            => activeAnimation = animations.Find(x => x.type == type);
    }
}
