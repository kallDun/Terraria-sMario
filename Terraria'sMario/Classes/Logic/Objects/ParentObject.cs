using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terraria_sMario.Classes.Logic.Objects
{
    abstract class ParentObject
    {
        public Point coords { get; protected set; }
        public Size size { get; protected set; }

        public bool isRendered = true; 

        public Image drawingImage { get; protected set; }

        public abstract void Draw(Graphics g);

        abstract public void updateProperties();

        // change position methods 

        public void offsetPositionX(int offsetX) => coords = new Point(coords.X + offsetX, coords.Y);

        public void offsetPositionY(int offsetY) => coords = new Point(coords.X, coords.Y + offsetY);

        public void setUpToTheBlock(ParentObject block)
        {
            coords = new Point(coords.X, block.coords.Y - size.Height);
        }

        internal void setDownToTheBlock(ParentObject block)
        {
            coords = new Point(coords.X, block.coords.Y + size.Height);
        }
    }
}