using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Animations
{
    abstract class ParentAnimation
    {
        public List<Image> images { get; protected set; }
        public Image activeImage { get; protected set; }
        public bool isLoop { get; protected set; } = true;
        public Point coord { get; protected set; } = new Point(0, 0);

        protected int skipFrames;
        protected int skipedFrames = 1;

        public bool isTurnToRight { get; protected set; } = true;

        public void Draw(Graphics g, Point coord, bool isTurnToRight)
        {
            coord.Offset(this.coord);

            if (this.isTurnToRight != isTurnToRight)
            {
                flipImages();
                this.isTurnToRight = isTurnToRight;
            }
            g.DrawImage(activeImage, coord);

            if (skipFrames == skipedFrames)
            {
                skipedFrames = 1;

                if (images.Last() == activeImage)
                {
                    if (isLoop)
                    {
                        activeImage = images.First();
                    }
                }
                else 
                    activeImage = images[images.IndexOf(activeImage) + 1];
            }
            else
            {
                skipedFrames++;
            }
        }

        private void flipImages()
        {
            foreach (var image in images)
            {
                image.RotateFlip(RotateFlipType.Rotate180FlipY);
            }
        }

        public bool isLastFrame() => images.Count() > 0 && activeImage == images.Last();

        public void setFirstFrame()
        {
            if (images.Count() > 0) activeImage = images.First();
        }
    }
}