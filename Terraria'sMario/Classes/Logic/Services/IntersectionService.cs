
using System;
using System.Collections.Generic;
using Terraria_sMario.Classes.Logic.Objects;
using Terraria_sMario.Classes.Logic.Objects.Creatures;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Items;
using Terraria_sMario.Classes.Logic.Objects.Environment.Translucent_Blocks;

namespace Terraria_sMario.Classes.Logic.Services
{
    static class IntersectionService
    {

        public static bool isBlockIntersectSomething(in ParentObject ourNewBlock, in ParentObject ourLastBlock, in List<ParentObject> blocks) 
        {
            foreach (var block in blocks)
            {
                if (block != ourLastBlock)
                {
                    if (isBlockIntersectBlock(ourNewBlock, ourLastBlock, block)) 
                        return true;
                }
            }
            return false;
        }

        public static bool isBlockIntersectBlock(in ParentObject ourBlock, in ParentObject ourLastBlock, in ParentObject otherBlock)
        {
            if (Rules(ourLastBlock, otherBlock)) return false;

            return 
                ourBlock.coords.X + ourBlock.size.Width > otherBlock.coords.X &&
                ourBlock.coords.X < otherBlock.coords.X + otherBlock.size.Width &&
                ourBlock.coords.Y + ourBlock.size.Height > otherBlock.coords.Y &&
                ourBlock.coords.Y < otherBlock.coords.Y + otherBlock.size.Height;
        }

        private static bool Rules(in ParentObject ourLastBlock, in ParentObject otherBlock)
        {
            if (!ourLastBlock.isHaveCollision || !otherBlock.isHaveCollision) return true;
            if (ourLastBlock is Player && otherBlock is Player) return true;
            if (ourLastBlock is Enemy && otherBlock is Enemy) return true;
            if ((ourLastBlock is Entity && otherBlock is ParentItem) ||
                    (ourLastBlock is ParentItem && otherBlock is Entity)) return true;
            if (ourLastBlock is TransparentBlockObject || otherBlock is TransparentBlockObject) return true;



            return false;
        }

    }
}
