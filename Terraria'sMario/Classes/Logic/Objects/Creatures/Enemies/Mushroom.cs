using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Logic.Objects.Features;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies
{
    class Mushroom : Enemy
    {
        public Mushroom(int X, int Y)
        {
            coords = new Point(X, Y);
            size = new Size(40, 50);
            maxHealth = 20;
            health = maxHealth;
            jumpHeight = -14;
            resistancesEffects.Add(EffectTypes.Poisoning);
            uI_Entity_Draw = new UI_Entity_Draw(this, Services.UI_Entity_Draw_Type.WithoutName);

            //drawingImage = Resources.mushroom_image;  <== needs to add

            animations = new List<EnemyAnimation> 
            {
                new EnemyAnimation(new List<Image>{ /*animation pictures*/ }, EnemyAnimationTypes.Walking),
                new EnemyAnimation(new List<Image>{ /*animation pictures*/ }, EnemyAnimationTypes.Standing),
                new EnemyAnimation(new List<Image>{ /*animation pictures*/ }, EnemyAnimationTypes.Running),
                new EnemyAnimation(new List<Image>{ /*animation pictures*/ }, EnemyAnimationTypes.Jumping),
                new EnemyAnimation(new List<Image>{ /*animation pictures*/ }, EnemyAnimationTypes.Hitting),
                new EnemyAnimation(new List<Image>{ /*animation pictures*/ }, EnemyAnimationTypes.Dead)            
            };
        }
        
    }
}
