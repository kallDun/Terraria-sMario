using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Images;

namespace Terraria_sMario.Classes.Logic.Objects.Items.Weapons.Bullets
{
    class Basic_arrow : BulletParent
    {
        public Basic_arrow()
        {
            size = new System.Drawing.Size(20, 5);

            base_damage = 1;
            isRicochet = false;

            drawingImage = Items_res.Arrow;
        }
    }
}
