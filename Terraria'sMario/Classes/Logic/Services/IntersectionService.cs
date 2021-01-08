
using System;
using System.Collections.Generic;
using Terraria_sMario.Classes.Logic.Objects;

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
                    if (isBlockIntersectBlock(ourNewBlock, block)) 
                        return true;
                }
            }
            return false;
        }

        public static bool isBlockIntersectBlock(in ParentObject ourBlock, in ParentObject otherBlock)
        {
            if (!ourBlock.isHaveCollision || !otherBlock.isHaveCollision) return false;
            return 
                ourBlock.coords.X + ourBlock.size.Width > otherBlock.coords.X &&
                ourBlock.coords.X < otherBlock.coords.X + otherBlock.size.Width &&
                ourBlock.coords.Y + ourBlock.size.Height > otherBlock.coords.Y &&
                ourBlock.coords.Y < otherBlock.coords.Y + otherBlock.size.Height;
        }

    }
}
