using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Items;
using Terraria_sMario.Classes.Logic.Services;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.Loot_Drop_System
{
    class ItemDropUnit
    {
        Random random = new Random();

        public int numberOfCoins { get; private set; } = 0;

        public ParentItem item { get; private set; }
        public double chance { get; private set; }

        public ItemDropUnit(ParentItem item, double chance)
        {
            this.item = item;
            this.chance = chance;
        }

        public ItemDropUnit(int numberOfCoins, double chance)
        {
            this.numberOfCoins = numberOfCoins;
            this.chance = chance;
        }

        public List<ParentObject> getItem(Point coords, List<ParentObject> objects)
        {
            var list = new List<ParentObject> { };

            int n = random.Next(0, 100);
            if (numberOfCoins != 0)
            {
                if (n <= chance)
                {
                    for (int i = 0; i < numberOfCoins; i++)
                    {
                        list.Add(tryToSetItem(new Coin(coords.X, coords.Y), objects));
                    }
                }
                numberOfCoins = 0;
            }
            else
            if (item != null)
            {
                if (n <= chance)
                {
                    item.dropItem(new Point(coords.X, coords.Y));
                    list.Add(tryToSetItem(item, objects));
                }

                item = null;
            }

            return list;
        }


        // set item in the world

        private int radius = Parameters.blockSize; // block to the right & block to the left

        private ParentObject tryToSetItem(ParentObject item, List<ParentObject> objects)
        {
            var area = new AbstractObject(item.coords, item.size);

            bool isMinus = true;
            for (int i = 0; i <= radius; i++, isMinus = !isMinus)
            {
                var offset_X = i;
                if (isMinus) offset_X *= -1;

                area.offsetPositionX_Y(offset_X, 0);

                if (!IntersectionService.isBlockIntersectSomething(area, item, objects))
                {
                    item.setNewCoords(area.coords);
                    return item;
                }
            }

            return item;
        }
    }
}
