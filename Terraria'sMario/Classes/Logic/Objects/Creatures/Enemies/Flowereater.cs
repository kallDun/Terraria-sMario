using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Logic.Objects.Features;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies
{
    class Flovereater : Enemy
    {
        public Flovereater(int X, int Y)
        {
            coords = new Point(X, Y);
            size = new Size(50, 50);
            health = 500;
            resistancesEffects.Add(EffectTypes.Stunning);

            //drawingImage = Resources.flovereater_image;  <== needs to add

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
