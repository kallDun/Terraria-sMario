using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            
            getting_weapon_effects.Add(new Effect(EffectTypes.Stunning, 1));

            canShoot = true;
            bulletUnit = new Basic_arrow();
            bulletCount = 1;
            shootRadius = 10;
            shoot_damage = 5;
            timerHitMax = 2.5;

            drawingImage = Items_res.Basic_Bow_Inventory;
            smallImage_inInventory = Items_res.Basic_Bow_Inventory;
        }

        
    }
}
