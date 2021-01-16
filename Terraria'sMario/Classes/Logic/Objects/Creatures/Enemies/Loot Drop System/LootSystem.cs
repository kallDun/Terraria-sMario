using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.Loot_Drop_System
{
    class LootSystem
    {
        public List<ItemDropUnit> itemDrops { get; private set; }

        public LootSystem(List<ItemDropUnit> itemDrops)
        {
            this.itemDrops = itemDrops;
        }

        public LootSystem(ItemDropUnit itemDrop)
        {
            this.itemDrops = new List<ItemDropUnit> {itemDrop};
        }

        public List<ParentObject> dropAll(int X, int Y)
        {
            var list = new List<ParentObject> { };

            foreach (var item in itemDrops)
            {
                list = list.Concat(item.getItem(X, Y)).ToList();
            }

            return list;
        }

    }
}
