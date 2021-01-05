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

        public abstract void Draw(Graphics g);
    }
}
