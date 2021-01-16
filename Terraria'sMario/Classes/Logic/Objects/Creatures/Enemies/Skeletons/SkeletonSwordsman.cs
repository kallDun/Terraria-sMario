using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies
{
    class SkeletonSwordsman : Enemy
    {
        public SkeletonSwordsman()
        {
            Name = "Skeleton Swordsman";
            EntityType = EntityTypes.Skeleton;
            standing_size = new Size(40, 90);
            dead_size = new Size(40, 90);



        }

    }
}
