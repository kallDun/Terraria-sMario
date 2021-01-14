using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Animations
{
    class EntityAnimation : ParentAnimation
    {
        public EntityAnimationTypes type { get; protected set; }

        public EntityAnimation(List<Image> images, EntityAnimationTypes type, int skipFrames = 1)
        {
            this.images = images;
            this.type = type;
            this.skipFrames = skipFrames;

            activeImage = images.Count() > 0 ? images.First() : null;
        }
    }
}
