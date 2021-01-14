using System.Collections.Generic;
using Terraria_sMario.Classes.Logic.Objects.Creatures;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Items;

namespace Terraria_sMario.Classes.Logic.Objects.Items.Potions
{
    class Potion : ParentItem
    {
        public int heal { get; protected set; } = 0;
        public List<Effect> effects { get; protected set; } = new List<Effect> { };

        public override void Use(in Entity entity)
        {
            entity.getCure(heal);

            foreach (var effect in effects)
            {
                entity.getEffect(new Effect(effect));
            }

            base.Use(entity);
        }

    }
}
