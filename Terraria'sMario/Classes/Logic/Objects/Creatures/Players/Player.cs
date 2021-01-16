using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Animations;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Animations.Effect_Animations;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Items;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Players.InventorySystem;
using Terraria_sMario.Classes.Logic.Objects.Items.Weapons;
using Terraria_sMario.Classes.Logic.Services;
using static Terraria_sMario.Classes.Logic.Objects.Creatures.Animations.EntityAnimationTypes;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures
{
    abstract class Player : Entity
    {
        protected Inventory inventory;

        // Threads

        public override void Draw(Graphics g)
        {
            if (!isRendered) return;

            if (activeAnimation == null || activeAnimation?.activeImage == null)
                g.DrawImage(drawingImage, coords);
            else
                activeAnimation.Draw(g, coords, isTurnToRight);

            base.Draw(g);
        }

        public void DrawInventory(Graphics g) => inventory.Draw(g);

        public override void updateProperties(in List<ParentObject> objects)
        {
            weaponInHand = inventory.inventory_cells.Last().item as Weapon;
            inventory.Update();

            if (activeAnimation != null && activeAnimation.isLastFrame())
            {
                activeAnimation.setFirstFrame();
                setAnimation(Standing);
            }

        }

        // Control

        public void controlPlayerKeysDown(in List<ParentObject> objects, 
            bool iskeyDown__Inv_right, bool iskeyDown__Inv_left, bool iskeyDown__Inv_start, bool iskeyDown__Inv_end,
            bool iskeyDown__Inv_changePosToWeapon, bool iskeyDown__Inv_changePosToOtherActive, bool iskeyDown__Inv_useActive,
            bool iskeyDown__Inv_takeItem, bool iskeyDown__Inv_dropItem)
        {
            if (isDead) return;
            if (iskeyDown__Inv_right) inventory.takeRightCell();
            if (iskeyDown__Inv_left) inventory.takeLeftCell();
            if (iskeyDown__Inv_start) inventory.goToFirstCell();
            if (iskeyDown__Inv_end) inventory.goToActiveWeaponCell();
            if (iskeyDown__Inv_changePosToWeapon) inventory.setActiveCellToWeaponActiveSlot();
            if (iskeyDown__Inv_changePosToOtherActive) inventory.setActiveCellToBaseActiveSlot();
            if (iskeyDown__Inv_useActive) inventory.tryToUseActiveCell();


            if (iskeyDown__Inv_takeItem)
            {
                Predicate<ParentObject> predicate = delegate (ParentObject obj) { return obj is ParentItem; };

                var item = CheckNearObjectByPredicationService.getNearObject(objects, this, predicate);
                if (item != null && (item as ParentItem).canGrab && inventory.tryToTakeItemToInventory(item as ParentItem)) 
                    (item as ParentItem).takeItem();
            }

            if (iskeyDown__Inv_dropItem)
            {
                var item = inventory.active_cell.item;
                if (item != null)
                {
                    var newCoord = new Point(coords.X + (isTurnToRight ? 50 : -50), coords.Y);

                    if (!IntersectionService.isBlockIntersectSomething(new AbstractObject(newCoord, item.size),
                        item, objects))
                    {
                        inventory.dropItemFromActiveCell();
                        item.dropItem(newCoord);
                        newObjects.Add(item);
                    }
                }
            }
        }

        public void controlPlayerKeysPressed(in List<ParentObject> objects, 
            bool isWentRight, bool isWentLeft, bool isPressedShift,
            bool isJumped, bool isHitting, bool isHealing, bool isShooting)
        {
            if (isDead) return;

            if (isWentRight)
                moveRightOrLeft(objects, 1, isPressedShift);
            if (isWentLeft)
                moveRightOrLeft(objects, -1, isPressedShift);
            if (isJumped)
                Jump(objects);
            if (isHitting)
                Hit(objects);
            if (isHealing)
                Heal(objects);
            if (isShooting)
                Shoot(objects);
        }

        // Actions

        public override int moveRightOrLeft(in List<ParentObject> objects, int direction, bool run = false)
        {
            setAnimation(run ? Running : Walking);
            return base.moveRightOrLeft(objects, direction, run);
        }

        public override void Jump(in List<ParentObject> objects)
        {
            base.Jump(objects);
            setAnimation(Jumping);
        }

        public override bool Hit(in List<ParentObject> objects)
        {
            if (base.Hit(in objects))
            {
                setAnimation(Hitting);
                return true;
            }
            else
                return false;
        }

        public override bool Shoot(in List<ParentObject> objects, float? angle = null)
        {
            if (base.Shoot(objects, angle))
            {
                setAnimation(Shooting);
                return true;
            }
            else
                return false;
        }

        public override bool Heal(in List<ParentObject> objects, int standartHeal = 0)
        {
            if (base.Heal(objects, standartHeal))
            {
                setAnimation(Standing);
                environment__anim.Add(EffectAnimationTypes.Heal);
                return true;
            }
            else
            if (weaponInHand != null && !weaponInHand.canHeal)
            {
                if (inventory.TryToUseBaseActiveSlot() == ItemTypes.Potion)
                {
                    setAnimation(Standing);
                    environment__anim.Add(EffectAnimationTypes.Heal);
                    return true;
                }
                else return false;
            } 
            else return false;
        }

        public void addCoin() => inventory.countOfCoins++;

        // Override

        public override void getDamage(float damage)
        {
            if (damage <= 0) return;

            damage -= (float) inventory.tryToResistAttackWithActiveArmor();
            base.getDamage(damage);
        }

        public override void getEffect(Effect effect)
        {
            if (!inventory.tryToResistEffectWithActiveArmor(effect))
            {
                base.getEffect(effect);
            }
        }
    }
}
