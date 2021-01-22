using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Animations;
using Terraria_sMario.Classes.Logic.Objects.Features;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies
{
    class Mushroom : Enemy
    {
        public Mushroom(int X, int Y, int level = 1)
        {
            Name = "Mushroom";
            EntityType = EntityTypes.Mushroom;

            coords = new Point(X, Y);
            standing_size = new Size(40, 90);
            dead_size = new Size(40, 90);
            maxHealth = 20;
            health = maxHealth;
            jumpHeight = -14;
            resistancesEffects.Add(EffectTypes.Poisoning);
            uI_Entity_Draw = new UI_Entity_Draw(this, Services.UI_Entity_Draw_Type.WithoutName);

            this.level = level;
            updatePropertiesToLevel();

            //drawingImage = Resources.mushroom_image;  <== needs to add

            animations = new List<EntityAnimation> 
            {
                new EntityAnimation(new List<Image>{ /*animation pictures*/ }, EntityAnimationTypes.Walking),
                new EntityAnimation(new List<Image>{ /*animation pictures*/ }, EntityAnimationTypes.Standing),
                new EntityAnimation(new List<Image>{ /*animation pictures*/ }, EntityAnimationTypes.Running),
                new EntityAnimation(new List<Image>{ /*animation pictures*/ }, EntityAnimationTypes.Hitting),
                new EntityAnimation(new List<Image>{ /*animation pictures*/ }, EntityAnimationTypes.Dead)            
            };
        }
        
    }
}
