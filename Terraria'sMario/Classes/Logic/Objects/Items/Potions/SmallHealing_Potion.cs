using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Players.InventorySystem;
using Terraria_sMario.Images;

namespace Terraria_sMario.Classes.Logic.Objects.Items.Potions
{
    class SmallHealing_Potion : Potion
    {
        public SmallHealing_Potion(int X, int Y)
        {
            Name = "Малое зелье";
            Description = "Восстановит тебе 10 здоровья.";
            coords = new Point(X, Y);
            size = new Size(40, 40);

            itemType = ItemTypes.Potion;

            heal = 10;
            opportunUseCount = 1;

            drawingImage = Items_res.SmallHealPotion;
            smallImage_inInventory = Items_res.SmallHealPotion_Inventory;
        }
    }
}
