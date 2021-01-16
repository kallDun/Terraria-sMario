using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Items;

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

        public List<ParentObject> getItem(int X, int Y)
        {
            var list = new List<ParentObject> { };

            int n = random.Next(0, 100);
            if (numberOfCoins != 0)
            {
                if (n <= chance)
                {
                    for (int i = 0; i < numberOfCoins; i++)
                    {
                        list.Add(new Coin(X, Y));
                    }
                }
                numberOfCoins = 0;
            }
            else
            if (item != null)
            {
                if (n <= chance)
                {
                    item.dropItem(new System.Drawing.Point(X, Y));
                    list.Add(item);
                }

                item = null;
            }

            return list;
        }
    }
}
