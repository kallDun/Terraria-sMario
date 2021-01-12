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
        public bool isHaveCollision = true;
        public bool isToDestroy = false;

        public Image drawingImage { get; protected set; }

        public abstract void Draw(Graphics g);

        abstract public void updateProperties();

        public void offsetPositionX_Y(int offSetX, int offSetY) => 
            coords = new Point(coords.X + offSetX, coords.Y + offSetY); // change position method 
        public void setNewCoords(Point coords) => this.coords = coords;
    }
}