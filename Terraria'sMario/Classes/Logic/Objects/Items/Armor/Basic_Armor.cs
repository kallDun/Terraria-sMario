using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Players.InventorySystem;
using Terraria_sMario.Images;

namespace Terraria_sMario.Classes.Logic.Objects.Items.Armor
{
    class Basic_Armor : Armor
    {
        public Basic_Armor(int X, int Y)
        {
            Name = "Нагрудник";
            Description = "Защищает на 1 урон и имеет устойчивость к огню .";
            coords = new Point(X, Y);
            size = new Size(60, 60);

            itemType = ItemTypes.Armor;
            opportunUseCount = 20;

            armor = 1;
            resistanceEffects.Add(EffectTypes.Fire);
            resistanceEffects.Add(EffectTypes.Curse);

            drawingImage = Items_res.Basic_Armor;
            smallImage_inInventory = Items_res.Basic_Armor_Inventory;
        }
    }
}
