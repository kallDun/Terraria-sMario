﻿using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Terraria_sMario.Classes.Logic.Objects.Features;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures
{
    abstract class Entity : ParentObject
    {
        public float health { get; protected set; }

        public Image drawingImage { get; protected set; }

        public List<Effect> effects { get; protected set; } = new List<Effect> { };
        public List<EffectTypes> resistancesEffects { get; protected set; } = new List<EffectTypes> { };

        public bool isAlive() => health > 0;

        public void getEffect(Effect effect)
        {
            if (!resistancesEffects.Contains(effect.effectType))
            {
                effects.Add(effect);
            }
        }

        public void getDamage(float damage)
        {
            if (Effect.isEffectInList(effects, EffectTypes.Poisoning)) damage *= 1.5f;
            health -= damage;
        }

        public void getCure(float healing)
        {
            if (Effect.isEffectInList(effects, EffectTypes.Blessing)) healing *= 2;
            if (Effect.isEffectInList(effects, EffectTypes.Curse)) healing /= 2;
            health += healing;
        }

        public void update()
        {
            // update effects duration
            foreach (var effect in effects) effect.reduceDurat();

            // delete effect if duration <= 0
            for (int i = 0; i < effects.Count; i++)
            {
                if (!effects[i].isExist())
                {
                    effects.Remove(effects[i]);
                    break;
                }
            }

        }
    }
}