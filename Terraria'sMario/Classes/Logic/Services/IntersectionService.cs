
using System;
using Terraria_sMario.Classes.Logic.Objects;
using Terraria_sMario.Classes.Logic.Objects.Creatures;

namespace Terraria_sMario.Classes.Logic.Services
{
    static class IntersectionService
    {

        public static bool isBlockIntersectBlock(ParentObject ourBlock, ParentObject otherBlock)
        {
            return 
                ourBlock.coords.X + ourBlock.size.Width > otherBlock.coords.X &&
                ourBlock.coords.X < otherBlock.coords.X + otherBlock.size.Width &&
                ourBlock.coords.Y + ourBlock.size.Height > otherBlock.coords.Y &&
                ourBlock.coords.Y < otherBlock.coords.Y + otherBlock.size.Height;
        }

        public static string getTypeOfIntersectingBlock(ParentObject ourBlock, ParentObject otherBlock)
        {
            if (Math.Abs((ourBlock.coords.Y + ourBlock.size.Height) - (otherBlock.coords.Y + otherBlock.size.Height)) 
                < otherBlock.size.Height / 4)
            {
                if (ourBlock.coords.X + ourBlock.size.Width < otherBlock.coords.X + otherBlock.size.Width)
                    return "left";
                else
                    return "right";
            }
            else
            {
                if (ourBlock.coords.Y + ourBlock.size.Height < otherBlock.coords.Y + otherBlock.size.Height)
                    return "down";
                else
                    return "up";
            }
        }
    }
}
