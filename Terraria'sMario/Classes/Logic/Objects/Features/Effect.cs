using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terraria_sMario.Classes.Logic.Objects
{
    class Effect
    {
        public EffectTypes effectType { get; private set; }
        public double duration { get; private set; }

        public int getIntDurability() => (int) Math.Round(duration);

        public Effect(EffectTypes effectType, float durability)
        {
            this.effectType = effectType;
            this.duration = durability;
        }

        public bool isExist() => duration > 0;

        public void updateDurat() => duration -= ( 1.0 / Parameters.fps);

        public void addDuration(double duration) => this.duration += duration;


        public static bool isEffectInList(List<Effect> effects, EffectTypes effectType)
        {
            foreach (var effect in effects)
            {
                if (effect.effectType == effectType) return true;
            }
            return false;
        }

    }
}
