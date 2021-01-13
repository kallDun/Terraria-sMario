﻿using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Animations;
using Terraria_sMario.Classes.Logic.Objects.Features;
using Terraria_sMario.Classes.Logic.Objects.Items.Weapons.Swords;
using static Terraria_sMario.Images.Players_res;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Players
{
    class Hero : Player
    {
        public Hero(int X, int Y, string Name, int PlayerNumber)
        {
            this.Name = Name;
            coords = new Point(X, Y);
            size = new Size(45, 95);
            maxHealth = 50;
            health = maxHealth;
            jumpHeight = -20;
            speed = 5;
            baseCloseDamage = 6;
            damage_heal_ActionRadius = 30;
            baseTimerHitMax = 1.5;
            uI_Entity_Draw = new UI_Entity_Draw(this, Services.UI_Entity_Draw_Type.OnlyNameAndDamage);
            inventory = new InventorySystem.Inventory(PlayerNumber, this);


            inventory.takeToWeapons(new Basic_Sword()); // ----------- for tests
            resistancesEffects.Add(EffectTypes.Ice); // ------------------------
            resistancesEffects.Add(EffectTypes.Stunning); // -------------------
            resistancesEffects.Add(EffectTypes.Poisoning); // ------------------
            getEffect(new Effect(EffectTypes.Curse, 10)); // -------------------
            getEffect(new Effect(EffectTypes.Fire, 5));  // -------------------
            getEffect(new Effect(EffectTypes.Fire, 5));  // temporary ---------


            drawingImage = sherif_image;
            animations = new List<PlayerAnimation>
            {
                new PlayerAnimation(new List<Image> {
                    sherifwent1, sherifwent2,
                    sherifwent3, sherifwent4,
                    sherifwent3, sherifwent2}, PlayerAnimationTypes.Walking, 6),

                new PlayerAnimation(new List<Image> { 
                    sherifwent1, sherifwent2,
                    sherifwent3, sherifwent4,
                    sherifwent3, sherifwent2 }, PlayerAnimationTypes.Running, 4),

                new PlayerAnimation(new List<Image> {
                    sherif_beat_5, 
                    sherif_beat_3, sherif_beat_2,
                    sherif_beat_1, sherif_beat_2,
                    sherif_beat_3, sherif_beat_4 }, PlayerAnimationTypes.Hitting, 4),

                new PlayerAnimation(new List<Image> { /*animation pictures*/ }, PlayerAnimationTypes.Standing),

                new PlayerAnimation(new List<Image> { /*animation pictures*/ }, PlayerAnimationTypes.Jumping),

                new PlayerAnimation(new List<Image> { /*animation pictures*/ }, PlayerAnimationTypes.Dead)
            };
        }

    }
}
