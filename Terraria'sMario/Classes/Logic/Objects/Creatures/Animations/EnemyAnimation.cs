using System.Collections.Generic;
using System.Drawing;

namespace Terraria_sMario.Classes.Logic.Objects.Features
{
    class EnemyAnimation
    {
        public List<Image> images { get; private set; }

        public EnemyAnimationTypes type { get; private set; }

        public EnemyAnimation(List<Image> images, EnemyAnimationTypes type)
        {
            this.images = images;
            this.type = type;
        }

        public void draw(Graphics g)
        {

        }

    }
}
