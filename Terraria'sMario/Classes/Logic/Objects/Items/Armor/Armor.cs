using System.Collections.Generic;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Items;

namespace Terraria_sMario.Classes.Logic.Objects.Items.Armor
{
    class Armor : ParentItem
    {
        public double armor { get; protected set; } = 0;
        public List<EffectTypes> resistanceEffects { get; private set; } = new List<EffectTypes> { };
    }
}