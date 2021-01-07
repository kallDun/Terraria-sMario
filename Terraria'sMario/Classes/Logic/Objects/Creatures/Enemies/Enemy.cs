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

        public override int moveRightOrLeft(in List<ParentObject> objects, int direction)
        {
            setAnimation(Walking);
            return base.moveRightOrLeft(objects, direction);
        }

        public void updateBehavior(in List<ParentObject> objects) =>
            // Enemy behavior
            enemy_behavior?.update(this, objects);

        public override void updateProperties() // EMPTY 
        {
            
        }

        public void setAnimation(EnemyAnimationTypes type) // Animations SET
            => activeAnimation = animations.Find(x => x.type == type);
    }
}
