using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            heal = 10;
            opportunUseCount = 1;

            drawingImage = Items_res.SmallHealPotion;
            smallImage_inInventory = Items_res.SmallHealPotion_Inventory;
        }
    }
}
