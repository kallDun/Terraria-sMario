using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terraria_sMario.Classes.Logic.Objects.Items.Weapons.Bullets
{
    class BulletConstructor : BulletParent
    {
        public BulletConstructor(BulletParent bullet)
        {
            coords = bullet.coords;
            size = bullet.size;
            drawingImage = bullet.drawingImage;

            base_damage = bullet.base_damage;
            effects = bullet.effects;
            isRicochet = bullet.isRicochet;
            damage = bullet.damage;
            angle = bullet.angle;
            maxDistance = bullet.maxDistance;
            distanceNow = bullet.distanceNow;
        }
    }
}
