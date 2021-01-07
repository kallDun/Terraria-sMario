using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Animations;
using Terraria_sMario.Classes.Logic.Objects.Features;
using static Terraria_sMario.Images.Players_res;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Players
{
    class Hero : Player
    {
        public Hero(int X, int Y, string Name)
        {
            this.Name = Name;
            coords = new Point(X, Y);
            size = new Size(55, 100);
            maxHealth = 50;
            health = maxHealth;
            jumpHeight = -20;
            speed = 5;
            uI_Entity_Draw = new UI_Entity_Draw(this, Services.UI_Entity_Draw_Type.All);


            effects.Add(new Effect(EffectTypes.Curse, 10)); // temporary ---------
            effects.Add(new Effect(EffectTypes.Fire, 20));  // -------------------
            getDamage(15); // ------------------------------------------- for tests


            drawingImage = sherif_image;
            animations = new List<PlayerAnimation>
            {
                new PlayerAnimation(new List<Image> {
                    sherifwent1,sherifwent2,sherifwent3,sherifwent4, sherifwent3 ,sherifwent2}, PlayerAnimationTypes.Walking, 3),

                new PlayerAnimation(new List<Image> { /*animation pictures*/ }, PlayerAnimationTypes.Standing),
                new PlayerAnimation(new List<Image> { /*animation pictures*/ }, PlayerAnimationTypes.Running),
                new PlayerAnimation(new List<Image> { /*animation pictures*/ }, PlayerAnimationTypes.Jumping),
                new PlayerAnimation(new List<Image> { /*animation pictures*/ }, PlayerAnimationTypes.Hitting),
                new PlayerAnimation(new List<Image> { /*animation pictures*/ }, PlayerAnimationTypes.Dead)
            };
        }


    }
}
