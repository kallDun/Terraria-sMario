using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Logic.Objects.Creatures;
using Terraria_sMario.Classes.Logic.Services;

namespace Terraria_sMario.Classes.Logic.Objects.Items.Weapons.Bullets
{
    class BulletParent : ParentObject
    {
        public Entity owner;

        public float base_damage { get; protected set; }
        public List<Effect> effects { get; protected set; } = new List<Effect> { };
        public bool isRicochet { get; protected set; } = false;

        public float damage { get; protected set; }
        public int velocity { get; protected set; }
        public float angle { get; protected set; } // 0 is to right, 180 is to left
        public int maxDistance { get; protected set; }
        public int distanceNow { get; protected set; }

        public void Shoot(in Weapon weapon, float angle, Entity owner, int offsetY = 0)
        {
            damage = base_damage * weapon.shoot_damage;
            effects = effects.Concat(weapon.getting_weapon_effects).ToList();
            maxDistance = weapon.shootRadius;
            distanceNow = 0;
            velocity = weapon.shootRadius / Parameters.blockSize * 2; 

            this.angle = angle;
            this.owner = owner;
            coords = new Point(
                owner.isTurnToRight ? owner.coords.X + 15 : owner.coords.X - 15, 
                (owner.coords.Y + owner.size.Height / 2) + offsetY);
        }

        public override void Draw(Graphics g)
        {
            if (!isRendered) return;

            var image = drawingImage;

            /*if (angle > 0 && angle <= 90) image.RotateFlip(RotateFlipType.Rotate90FlipNone);
            if (angle > 90 && angle <= 180) image.RotateFlip(RotateFlipType.Rotate180FlipNone);
            if (angle > 180 && angle <= 270) image.RotateFlip(RotateFlipType.Rotate270FlipNone);*/

            g.DrawImage(image, coords);
        }

        public override void updateProperties(in List<ParentObject> objects)
        {
            var newCoords = new Point(
                (int) Math.Round(coords.X + velocity * Math.Cos(angle * (Math.PI / 180.0))),
                (int) Math.Round(coords.Y + velocity * Math.Sin(angle * (Math.PI / 180.0))));

            var bulletArea = new AbstractObject(newCoords, size);
            var enemy = CheckEntityService.getShootedEntity(objects, owner, bulletArea);

            if (enemy == null)
            {
                if (!IntersectionService.isBlockIntersectSomething(bulletArea, this, objects))
                {
                    coords = newCoords;
                    distanceNow += velocity;
                }
                else isToDestroy = true;
            }
            else
            {
                enemy.getDamage(damage);
                foreach (var effect in effects)
                {
                    enemy.getEffect(new Effect(effect));
                }

                isToDestroy = true;
            }

            if (distanceNow >= maxDistance) isToDestroy = true;
        }
    }
}
