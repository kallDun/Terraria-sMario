using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Items;
using Terraria_sMario.Images;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Players.InventorySystem
{
    class Cell
    {
        public Point coords { get; private set; }
        public ParentItem item;

        public Cell(int X, int Y) => coords = new Point(X, Y);

        public void Draw(Graphics g, Point tableCoord)
        {
            if (item != null)
            {
                g.DrawImage(item.smallImage_inInventory, new Point(tableCoord.X + coords.X, tableCoord.Y + coords.Y));
            }
        }

        public void Update()
        {
            if (item?.opportunUseCount == 0) item = null;
        }

        public void Use() => item?.Use();
    }
}
