using System.Collections.Generic;
using System.Drawing;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Animations
{
    class PlayerAnimation
    {
        public List<Image> images { get; private set; }

        public PlayerAnimationTypes type { get; private set; }

        public PlayerAnimation(List<Image> images, PlayerAnimationTypes type)
        {
            this.images = images;
            this.type = type;
        }
        public void draw(Graphics g)
        {

        }
    }
}
