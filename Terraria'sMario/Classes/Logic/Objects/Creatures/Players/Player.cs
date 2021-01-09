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

        public override void updateProperties()
        {
            if (isDead)
            {
                setAnimation(Dead);
                return;
            }

            if (activeAnimation != null && activeAnimation.isLastFrame())
            {
                activeAnimation.setFirstFrame();
                setAnimation(Standing);
            }

        }

        public void controlPlayer(in List<ParentObject> objects, 
            bool isWentRight, bool isWentLeft, bool isPressedShift,
            bool isJumped, bool isHitting, bool isHealing, bool isShooting)
        {
            if (isDead) return;

            if (isWentRight)
                moveRightOrLeft(objects, 1, isPressedShift);
            if (isWentLeft)
                moveRightOrLeft(objects, -1, isPressedShift);
            if (isJumped)
                Jump();
            if (isHitting)
                Hit(objects);
            if (isHealing)
                Heal(objects);
            if (isShooting)
                Shoot(objects);
        }

        // Actions

        public override int moveRightOrLeft(in List<ParentObject> objects, int direction, bool run = false)
        {
            setAnimation(run ? Running : Walking);
            return base.moveRightOrLeft(objects, direction, run);
        }

        public override void Jump()
        {
            base.Jump();
            setAnimation(Jumping);
        }

        public override bool Hit(in List<ParentObject> objects)
        {
            if (base.Hit(in objects))
            {
                setAnimation(Hitting);
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

        public override bool Heal(in List<ParentObject> objects, int standartHeal = 0)
        {
            if (base.Heal(objects, standartHeal))
            {
                setAnimation(Healing);
                return true;
            }
            else
                return false;
        }

        

        public void setAnimation(PlayerAnimationTypes type) // Animations SET
        {
            activeAnimation = animations.Find(x => x.type == type);
        }
    }
}
