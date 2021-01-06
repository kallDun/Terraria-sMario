using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Animations;

namespace Terraria_sMario.Classes.Logic.Objects.Features
{
    class EnemyAnimation : ParentAnimation
    {
        public EnemyAnimationTypes type { get; private set; }

        public EnemyAnimation(List<Image> images, EnemyAnimationTypes type, int skipFrames = 1)
        {
            this.images = images;
            this.type = type;
            this.skipFrames = skipFrames;

            activeImage = images.Count() > 0 ? images.First() : null;
        }
    }
}
