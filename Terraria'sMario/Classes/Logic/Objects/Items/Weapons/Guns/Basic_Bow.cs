
using Terraria_sMario.Classes.Logic.Objects.Creatures.Players.InventorySystem;
using Terraria_sMario.Classes.Logic.Objects.Items.Weapons.Bullets;
using Terraria_sMario.Images;

namespace Terraria_sMario.Classes.Logic.Objects.Items.Weapons.Guns
{
    class Basic_Bow : Weapon
    {
        public Basic_Bow(int X = 0, int Y = 0)
        {
            coords = new System.Drawing.Point(X, Y);
            size = new System.Drawing.Size(40, 40);

            Name = "Базовый лук";
            Description = "Вы перепутали детский и боевой лук и теперь очень жалеете .";

            itemType = ItemTypes.Weapon;

            getting_weapon_effects.Add(new Effect(EffectTypes.Ice, 2));

            canShoot = true;
            bulletUnit = new Basic_arrow();
            bulletCount = 1;
            shootRadius = 10 * Parameters.blockSize;
            shoot_damage = 5;
            timerHitMax = 2.5;

            drawingImage = Items_res.Basic_Bow_World;
            smallImage_inInventory = Items_res.Basic_Bow_Inventory;
        }

        
    }
}
