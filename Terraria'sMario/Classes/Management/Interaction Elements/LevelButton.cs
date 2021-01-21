using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Logic.DrawingElements;

namespace Terraria_sMario.Classes.Management.Interaction_Elements
{
    class LevelButton : ButtonParent
    {
        public int number { get; private set; }

        public LevelButton(string Name, Point position, int number)
        {
            this.Name = Name;
            this.position = position;
            this.number = number;
            size = new Size(75, 75);
        }


        public override void Draw(Graphics g)
        {
            if (isHovered) g.FillRectangle(Brushes.DimGray, 
                new RectangleF(new Point(position.X + 3, position.Y + 3), size));

            g.FillRectangle(Brushes.DarkCyan, new RectangleF(position, size));

            UI_Drawing_Static.DrawString(g, number.ToString(), Brushes.White, 24, 
                new RectangleF(position, size));
        }
    }
}
