using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Animations;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures
{
    class Player : Entity
    {
        public List<PlayerAnimation> animations { get; protected set; }

        public override void Draw(Graphics g)
        {
            if (!isRendered) return;

            g.DrawImage(drawingImage, coords);
        }

        public void Jump() => acceler = -20;

        public override void updateProperties()
        {
            
        }
    }
}
