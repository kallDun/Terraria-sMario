using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Animations
{
    class PlayerAnimation : ParentAnimation
    {
        public PlayerAnimationTypes type { get; private set; }

        public PlayerAnimation(List<Image> images, PlayerAnimationTypes type, int skipFrames = 1)
        {
            this.images = images;
            this.type = type;
            this.skipFrames = skipFrames;

            activeImage = images.Count() > 0 ? images.First() : null;
        }
    }
}
