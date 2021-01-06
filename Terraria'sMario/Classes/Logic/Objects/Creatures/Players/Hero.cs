using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Animations;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Players
{
    class Hero : Player
    {
        public Hero(int X, int Y)
        {
            coords = new Point(X, Y);
            size = new Size(55, 100);
            health = 100;

            drawingImage = Resources.sherifreworked;
            animations = new List<PlayerAnimation>
            {
                new PlayerAnimation(new List<Image> { /*animation pictures*/ }, PlayerAnimationTypes.Walking),
                new PlayerAnimation(new List<Image> { /*animation pictures*/ }, PlayerAnimationTypes.Standing),
                new PlayerAnimation(new List<Image> { /*animation pictures*/ }, PlayerAnimationTypes.Running),
                new PlayerAnimation(new List<Image> { /*animation pictures*/ }, PlayerAnimationTypes.Jumping),
                new PlayerAnimation(new List<Image> { /*animation pictures*/ }, PlayerAnimationTypes.Hitting),
                new PlayerAnimation(new List<Image> { /*animation pictures*/ }, PlayerAnimationTypes.Dead)
            };  
        }


    }
}
