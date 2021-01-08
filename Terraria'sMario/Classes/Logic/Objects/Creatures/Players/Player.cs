using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Animations;
using static Terraria_sMario.Classes.Logic.Objects.Creatures.Animations.PlayerAnimationTypes;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures
{
    class Player : Entity
    {
        public List<PlayerAnimation> animations { get; protected set; }
        public PlayerAnimation activeAnimation { get; protected set; }

        public override void Draw(Graphics g)
        {
            if (!isRendered) return;

            if (activeAnimation == null || activeAnimation?.activeImage == null)
                g.DrawImage(drawingImage, coords);
            else
                activeAnimation.Draw(g, coords, isTurnToRight);

            base.Draw(g);
        }

        public override int moveRightOrLeft(in List<ParentObject> objects, int direction, bool run = false)
        {
            if (isDead) return 0;

            setAnimation(Walking);
            return base.moveRightOrLeft(objects, direction);
        }

        public override void Jump()
        {
            if (isDead) return;

            base.Jump();
            setAnimation(Jumping);
        }

        public override bool Hit(in List<ParentObject> objects)
        {
            if (!isDead && base.Hit(in objects))
            {
                setAnimation(Hitting);
                return true;
            }
            else
                return false;
        }

        public override void updateProperties()
        {
            if (isDead)
            {
                setAnimation(Dead);
                return;
            }

            if (activeAnimation != null && activeAnimation.isLastFrame())
                setAnimation(Standing);
        }

        public void setAnimation(PlayerAnimationTypes type) // Animations SET
        {
            activeAnimation = animations.Find(x => x.type == type);
        }
    }
}
