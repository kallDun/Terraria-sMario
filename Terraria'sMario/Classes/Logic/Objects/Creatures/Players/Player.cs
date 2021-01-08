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

        public override int moveRightOrLeft(in List<ParentObject> objects, int direction)
        {
            setAnimation(Walking);
            return base.moveRightOrLeft(objects, direction);
        }

        public override void Jump()
        {
            base.Jump();
            setAnimation(Jumping);
        }

        public override void Hit(in List<ParentObject> objects)
        {
            base.Hit(objects);
            setAnimation(Hitting);
        }

        public override void updateProperties()
        {
            if (activeAnimation != null && activeAnimation.isLastFrame())
                setAnimation(Standing);
        }

        public void setAnimation(PlayerAnimationTypes type) // Animations SET
        {
            activeAnimation = animations.Find(x => x.type == type);
        }
    }
}
