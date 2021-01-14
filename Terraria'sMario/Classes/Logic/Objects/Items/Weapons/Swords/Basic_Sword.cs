﻿using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Animations;
using Terraria_sMario.Images;
using static Terraria_sMario.Classes.Logic.Objects.Creatures.EntityTypes;
using static Terraria_sMario.Classes.Logic.Objects.Creatures.Animations.EntityAnimationTypes;

namespace Terraria_sMario.Classes.Logic.Objects.Items.Weapons.Swords
{
    class Basic_Sword : Weapon
    {

        public Basic_Sword(int X = 0, int Y = 0)
        {
            coords = new System.Drawing.Point(X, Y);
            size = new System.Drawing.Size(50, 50);

            Name = "Базовый меч";
            Description = "Этот меч сгодиться разве чтобы салаты на Новый год крошить .";

            damage = 10;
            canMeleeDamage = true;
            getting_weapon_effects.Add(new Effect(EffectTypes.Poisoning, 2));
            actionRadius = 1 * Parameters.blockSize;
            timerHitMax = 1.5;
            canSplash = true;

            canHeal = true;
            healing = 2.5f;

            opportunUseCount = 10;

            drawingImage = Items_res.Sword;
            smallImage_inInventory = Items_res.Basic_Sword_Inventory;

            // animations

            entityWeaponAnimations = new List<EntityWeaponAnimation> 
            {
                // for Hero
                new EntityWeaponAnimation(Hero, new List<EntityAnimation>
                {
                    new EntityAnimation(new List<Image>{ }, Standing, skipFrames: 4),
                    new EntityAnimation(new List<Image>{ }, Hitting, skipFrames: 4),
                    new EntityAnimation(new List<Image>{ }, Healing, skipFrames: 4)
                })
            };
    }

    }
}
