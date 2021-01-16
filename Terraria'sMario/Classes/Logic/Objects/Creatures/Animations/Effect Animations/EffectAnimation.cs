using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Animations.Effect_Animations;
using static Terraria_sMario.Images.EnvironmentEffects;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Animations
{
    class EffectAnimation : ParentAnimation
    {
        public EffectAnimationTypes type { get; protected set; }

        public EffectAnimation(EffectAnimationTypes type)
        {
            this.type = type;
            isLoop = false;
            setParameters();
            activeImage = images.Count() > 0 ? images.First() : null;
        }

        private void setParameters()
        {
            switch (type)
            {
                case EffectAnimationTypes.Heal:
                    coord = new Point(0, -30);
                    images = new List<Image> {
                        Heal_Effect_1, Heal_Effect_2,
                        Heal_Effect_3, Heal_Effect_4,
                        Heal_Effect_5, Heal_Effect_6};
                    skipFrames = 3;
                    break;

                case EffectAnimationTypes.MeleeHit:

                    break;

                case EffectAnimationTypes.Shoot:

                    break;

                case EffectAnimationTypes.LittleExplosion:

                    break;

                case EffectAnimationTypes.Explosion:

                    break;

                case EffectAnimationTypes.BigExplosion:

                    break;

                case EffectAnimationTypes.Killed:

                    break;
            }
        }
    }
}
