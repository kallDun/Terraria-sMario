using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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

        public List<ParentObject> dropAll(Point coords, List<ParentObject> objects)
        {
            var list = new List<ParentObject> { };

            foreach (var item in itemDrops)
            {
                list = list.Concat(item.getItem(coords, objects)).ToList();
            }
            return list;
        }
    }
}