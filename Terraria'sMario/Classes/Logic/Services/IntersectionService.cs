
using Terraria_sMario.Classes.Logic.Objects;
using Terraria_sMario.Classes.Logic.Objects.Creatures;

namespace Terraria_sMario.Classes.Logic.Services
{
    static class IntersectionService
    {

        public static bool isEntityIntersectOtherEntity(Entity mainCreature, Entity otherCreature)
        {
            return false;
        }

        public static bool isEntityIntersectBlockUpOrDown(Entity creature, ParentObject block)
        {
            var flag = creature.coords.X + creature.size.Width > block.coords.X &&
                       creature.coords.X < block.coords.X + block.size.Width &&
                       creature.coords.Y + creature.size.Height > block.coords.Y &&
                       creature.coords.Y < block.coords.Y + block.size.Height;
            return flag;
        }

    }
}
