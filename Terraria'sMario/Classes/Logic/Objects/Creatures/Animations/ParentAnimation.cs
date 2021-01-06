using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Animations
{
    abstract class ParentAnimation
    {
        public List<Image> images { get; protected set; }
        public Image activeImage { get; protected set; }

        protected int skipFrames;
        protected int skipedFrames = 1;

        public void Draw(Graphics g, Point playerCoord)
        {
            g.DrawImage(activeImage, playerCoord);

            if (skipFrames == skipedFrames)
            {
                skipedFrames = 1;

                activeImage = (images.Last() == activeImage) ?
                images.First() :
                images[images.IndexOf(activeImage) + 1];
            }
            else
            {
                skipedFrames++;
            }
        }
    }

}
