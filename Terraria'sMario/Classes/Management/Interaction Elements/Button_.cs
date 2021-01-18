using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terraria_sMario.Classes.Management.Interaction_Elements
{
    class Button_
    {
        private Image image;
        private Image hovered_image;
        private Image clicked_image;
        private Size size;
        private Point position;

        public bool isCLicked { get; private set; }
        public bool isHovered { get; private set; }

        public Button_(Image image, Image hovered_image, Image clicked_image, Size size, Point position)
        {
            this.image = image;
            this.hovered_image = hovered_image;
            this.clicked_image = clicked_image;
            this.size = size;
            this.position = position;
        }

        public void Draw(Graphics g) =>
            g.DrawImage(isCLicked ? clicked_image : isHovered ? hovered_image : image, position);

        public void MouseOn(int X, int Y) => isHovered = isCoordInButton(X, Y);

        public void MouseClick(int X, int Y) => isCLicked = isCoordInButton(X, Y);

        private bool isCoordInButton(int X, int Y) =>
            X >= position.X && Y >= position.Y && X <= position.X + size.Width && Y <= position.Y + size.Height;
    }
}
