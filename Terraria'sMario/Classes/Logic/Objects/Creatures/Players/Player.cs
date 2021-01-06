using System;
using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Animations;

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
                activeAnimation.Draw(g, coords);
        }

        public override void updateProperties()
        {
            setAnimation(PlayerAnimationTypes.Standing);
        }

        

        internal void setAnimation(PlayerAnimationTypes type) // Animations SET
        {
            activeAnimation = animations.Find(x => x.type == type);
        }
    }
}
