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
        public float durability { get; private set; }

        public Effect(EffectTypes effectType, float durability)
        {
            this.effectType = effectType;
            this.durability = durability;
        }

        public bool isExist() => durability > 0;

        public void reduceDurat() => durability -= (1 / Parameters.fps);

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
