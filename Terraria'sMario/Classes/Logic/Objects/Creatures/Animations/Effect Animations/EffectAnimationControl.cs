using System.Collections.Generic;
using System.Drawing;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Animations.Effect_Animations
{
    class EffectAnimationControl
    {
        private List<EffectAnimation> environment_effects_anim = new List<EffectAnimation> { };

        public void Draw(Graphics g, Point coords)
        {
            foreach (var animat in environment_effects_anim)
            {
                animat.Draw(g, coords, true);
            }
        }

        public void Update()
        {
            // check the end of Environment Animations
            for (int i = environment_effects_anim.Count - 1; i >= 0; i--)
            {
                if (environment_effects_anim[i].isLastFrame())
                    environment_effects_anim.RemoveAt(i);
            }
        }

        public void Add(EffectAnimationTypes type) => environment_effects_anim.Add(new EffectAnimation(type));

        public bool isEmpty() => environment_effects_anim.Count == 0;
    }
}