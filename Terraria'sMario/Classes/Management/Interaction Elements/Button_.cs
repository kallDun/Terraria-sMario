using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Images;

namespace Terraria_sMario.Classes.Management.Interaction_Elements
{
    class Button_
    {
        public string Name { get; private set; }

        private int version;

        private Image image;
        private Image hovered_image;
        private Image clicked_image;
        private Size size;
        private Point position;

        public bool isCLicked { get; private set; }
        public bool isHovered { get; private set; }

        public Button_(string Name, Image image, Image hovered_image, Image clicked_image, Size size, Point position)
        {
            this.Name = Name;
            this.image = image;
            this.hovered_image = hovered_image;
            this.clicked_image = clicked_image;
            this.size = size;
            this.position = position;
            version = 1;
        }

        public Button_(string Name, Image image, Size size, Point position)
        {
            this.Name = Name;
            this.image = image;
            this.hovered_image = Management_res.Hover;
            this.clicked_image = Management_res.Click;
            this.size = size;
            this.position = position;
            version = 2;
        }

        public void Draw(Graphics g)
        {
            if (version == 1)
            {
                g.DrawImage(isCLicked ? clicked_image : isHovered ? hovered_image : image, position);
            }
            else
            if (version == 2)
            {
                g.DrawImage(image, position);
                if (isHovered || isCLicked)
                {
                    g.DrawImage(isCLicked ? clicked_image : hovered_image, new Point(position.X + size.Width, position.Y));
                }
            }
        }

        public void MouseOn(int X, int Y) => isHovered = isCoordInButton(X, Y);

        public void MouseClick(int X, int Y) => isCLicked = isCoordInButton(X, Y);

        private bool isCoordInButton(int X, int Y) =>
            X >= position.X && Y >= position.Y && X <= position.X + size.Width && Y <= position.Y + size.Height;
    }
}
