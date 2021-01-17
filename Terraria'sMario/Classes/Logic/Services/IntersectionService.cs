
using System;
using System.Collections.Generic;
using Terraria_sMario.Classes.Logic.Objects;
using Terraria_sMario.Classes.Logic.Objects.Creatures;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Items;
using Terraria_sMario.Classes.Logic.Objects.Environment.Background_Blocks;
using Terraria_sMario.Classes.Logic.Objects.Environment.Translucent_Blocks;
using Terraria_sMario.Classes.Logic.Objects.Environment.Transparent_Blocks.Translucent_Blocks;
using Terraria_sMario.Classes.Logic.Objects.Items.Weapons.Bullets;

namespace Terraria_sMario.Classes.Logic.Services
{
    static class IntersectionService
    {

        public static bool isBlockIntersectSomething(in ParentObject ourNewBlock, in ParentObject ourLastBlock, in List<ParentObject> blocks, bool useRules = true) 
            => GetIntersectionBlockIfIntersectSomething(ourNewBlock, ourLastBlock, blocks, useRules) != null;

        public static bool isBlockIntersectBlock(in ParentObject ourBlock, in ParentObject ourLastBlock, in ParentObject otherBlock, bool useRules = true) 
            => GetIntersectionBlockIfIntersectBlock(ourBlock, ourLastBlock, otherBlock, useRules) != null;


        public static ParentObject GetIntersectionBlockIfIntersectSomething(in ParentObject ourNewBlock, in ParentObject ourLastBlock, in List<ParentObject> blocks, bool useRules = true)
        {
            foreach (var block in blocks)
            {
                if (block != ourLastBlock)
                {
                    var obj = GetIntersectionBlockIfIntersectBlock(ourNewBlock, ourLastBlock, block, useRules);
                    if (obj != null)
                        return obj;
                }
            }
            return null;
        }

        public static ParentObject GetIntersectionBlockIfIntersectBlock(in ParentObject ourBlock, in ParentObject ourLastBlock, in ParentObject otherBlock, bool useRules = true)
        {
            if (useRules && Rules(ourLastBlock, otherBlock)) return null;

            if (ourBlock.coords.X + ourBlock.size.Width > otherBlock.coords.X &&
                ourBlock.coords.X < otherBlock.coords.X + otherBlock.size.Width &&
                ourBlock.coords.Y + ourBlock.size.Height > otherBlock.coords.Y &&
                ourBlock.coords.Y < otherBlock.coords.Y + otherBlock.size.Height)

                return otherBlock;
            else
                return null;
        }


        // return true if not intersect 
        private static bool Rules(in ParentObject ourBlock, in ParentObject otherBlock)
        {
            if (!ourBlock.isHaveCollision || !otherBlock.isHaveCollision) return true;

            if (ourBlock is Player && otherBlock is Player) return true;
            if (ourBlock is Enemy && otherBlock is Enemy) return true;

            if ((ourBlock is Entity && (otherBlock is ParentItem || otherBlock is Coin)) ||
                    ((ourBlock is ParentItem || ourBlock is Coin) && otherBlock is Entity)) return true;

            if (ourBlock is BackgroundBlockObject || otherBlock is BackgroundBlockObject) return true;
            if (ourBlock is TransparentBlockObject || otherBlock is TransparentBlockObject) return true;
            if (ourBlock is TranslucentBlockObject || otherBlock is TranslucentBlockObject) return true;

            if ((ourBlock is BulletParent && otherBlock is Entity) ||
                    (ourBlock is Entity && otherBlock is BulletParent)) return true;
            if (ourBlock is BulletParent && otherBlock is BulletParent) return true;

            if ((ourBlock is Entity && otherBlock is Entity) && 
                (!(ourBlock as Entity).isColisionWithOtherEntitiesOn ||
                !(otherBlock as Entity).isColisionWithOtherEntitiesOn)) return true;

            return false;
        }

    }
}
