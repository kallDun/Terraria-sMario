using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Animations;
using Terraria_sMario.Classes.Logic.Objects.Features;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies
{
    class FlowerEater : Enemy
    {
        public FlowerEater(int X, int Y)
        {
            Name = "FlowerEater";
            EntityType = EntityTypes.FlowerEater;

            coords = new Point(X, Y);
            standing_size = new Size(40, 90);
            dead_size = new Size(40, 90);
            maxHealth = 500;
            health = maxHealth;
            jumpHeight = -14;
            resistancesEffects.Add(EffectTypes.Stunning);
            uI_Entity_Draw = new UI_Entity_Draw(this, Services.UI_Entity_Draw_Type.WithoutName);

            //drawingImage = Resources.flowerEater_image;  <== needs to add

            animations = new List<EntityAnimation>
            {
                new EntityAnimation(new List<Image>{ /*animation pictures*/ }, EntityAnimationTypes.Walking),
                new EntityAnimation(new List<Image>{ /*animation pictures*/ }, EntityAnimationTypes.Standing),
                new EntityAnimation(new List<Image>{ /*animation pictures*/ }, EntityAnimationTypes.Running),
                new EntityAnimation(new List<Image>{ /*animation pictures*/ }, EntityAnimationTypes.Jumping),
                new EntityAnimation(new List<Image>{ /*animation pictures*/ }, EntityAnimationTypes.Hitting),
                new EntityAnimation(new List<Image>{ /*animation pictures*/ }, EntityAnimationTypes.Dead)
            };
        }
    }
}
