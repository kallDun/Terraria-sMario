using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Images;

namespace Terraria_sMario.Classes.Logic.Objects.Items.Weapons.Swords
{
    class Basic_Sword : Weapon
    {

        public Basic_Sword()
        {
            Name = "Базовый меч";
            Description = "Этот меч сгодиться разве чтобы салаты крошить на Новый год.";

            damage = 10;
            canMeleeDamage = true;
            getting_weapon_effects.Add(new Effect(EffectTypes.Poisoning, 2));
            actionRadius = 1 * Parameters.blockSize;
            timerHitMax = 1.5;
            canSplash = true;

            canHeal = true;
            healing = 2.5f;

            opportunUseCount = 10;

            smallImage_inInventory = Items_res.Basic_Sword_Inventory;
            drawingImage = Items_res.Basic_Sword_Inventory;
        }

    }
}
