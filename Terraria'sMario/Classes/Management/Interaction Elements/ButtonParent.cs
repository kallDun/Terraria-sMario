using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terraria_sMario.Classes.Management.Interaction_Elements
{
    abstract class ButtonParent
    {
        public string Name { get; protected set; }
        protected Size size;
        protected Point position;

        public bool isCLicked { get; protected set; }
        public bool isHovered { get; protected set; }

        public void MouseOn(int X, int Y) => isHovered = isCoordInButton(X, Y);

        public void MouseClick(int X, int Y) => isCLicked = isCoordInButton(X, Y);

        private bool isCoordInButton(int X, int Y) =>
            X >= position.X && Y >= position.Y && X <= position.X + size.Width && Y <= position.Y + size.Height;

        abstract public void Draw(Graphics g);
    }
}
