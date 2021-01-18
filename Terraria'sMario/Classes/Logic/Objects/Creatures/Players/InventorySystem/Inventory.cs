using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Logic.DrawingElements;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Items;
using Terraria_sMario.Classes.Logic.Objects.Items.Armor;
using Terraria_sMario.Classes.Logic.Objects.Items.Bombs;
using Terraria_sMario.Classes.Logic.Objects.Items.Potions;
using Terraria_sMario.Classes.Logic.Objects.Items.Weapons;
using Terraria_sMario.Images;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Players.InventorySystem
{
    class Inventory
    {
        // fields

        private Player player;
        private Image drawingImage;

        private Point global_coords;
        public Point coords { get; private set; }

        public Cell active_cell { get; private set; }
        public int countOfCoins = 0;

        private bool isShow = true;
        private int belowY_coords = 720;

        // coords

        public Cell[] inventory_cells { get; private set; } = new Cell[11] {
            new Cell(17, 163), new Cell(71, 163), new Cell(125, 163),
            new Cell(180, 163), new Cell(237, 163), new Cell(293, 163),
            new Cell(350, 163), new Cell(407, 163), new Cell(465, 163),
            new Cell(26, 225), new Cell(95, 225) };

        private Point health_coord = new Point(16, 58);
        private Point name_coord = new Point(14, 8);
        private Point coinsCount_coord = new Point(220, 45);
        private Point effects_coord = new Point(300, 52);
        private Point resistance_coord = new Point(300, 106);
        private Point reloadWeaponSec_coord = new Point(134, 212);

        private Point level_coord = new Point(390, 12);
        private Point scores_coord = new Point(460, 14);

        // item stats coords
        private Point weaponStat_name__coord = new Point(198, 220);
        private Point weaponStat_description__coord = new Point(162, 246);
        private Point weaponStat_damage__coord = new Point(352, 217);
        private Point weaponStat_range__coord = new Point(416, 217);
        private Point weaponStat_splash__coord = new Point(479, 216);
        private Point weaponStat_effects__coord = new Point(339, 233);
        private Point weaponStat_resist__coord = new Point(412, 231);
        private Point weaponStat_reload__coord = new Point(482, 231);
        private Point weaponStat_shootDM__coord = new Point(357, 243);
        private Point weaponStat_heal__coord = new Point(410, 243);
        private Point weaponStat_bullets__coord = new Point(482, 243);
        private Point weaponStat_shield__coord = new Point(477, 255);
        private Point weaponStat_type__coord = new Point(332, 255);


        // constructor
        public Inventory(int playerNumber, Player player)
        {
            if (playerNumber == 1)
            {
                coords = Parameters.inventoryPlayer1_Position;
                drawingImage = UI.Inventory_player1;
            }
            else
            if (playerNumber == 2)
            {
                coords = Parameters.inventoryPlayer2_Position;
                drawingImage = UI.Inventory_player1;
            }

            global_coords = coords;
            this.player = player;
            active_cell = inventory_cells[10];
        }


        // threads

        public void Draw(Graphics g)
        {
            // Draw main image
            g.DrawImage(drawingImage, coords);

            // Draw cells
            foreach (var cell in inventory_cells)
            {
                cell.Draw(g, coords);
            }

            // Draw active cell
            if (active_cell != null)
                g.DrawImage(UI.Cell_outline_var2, new Point(coords.X + active_cell.coords.X, coords.Y + active_cell.coords.Y));

            // Draw HP
            UI_Drawing_Static.DrawHealth(g, new Point(health_coord.X + coords.X, health_coord.Y + coords.Y),
                player.health, player.maxHealth, 7, false, true);

            // Draw Name
            UI_Drawing_Static.DrawString(g, new Point(name_coord.X + coords.X, name_coord.Y + coords.Y),
                player.Name, Brushes.White, 24);

            // Coins Count
            UI_Drawing_Static.DrawString(g,
                countOfCoins.ToString(), Brushes.Yellow, 24,
                new RectangleF(coinsCount_coord.X + coords.X, coinsCount_coord.Y + coords.Y, 70, 25));

            // Draw effects
            UI_Drawing_Static.DrawEffects(g, new Point(coords.X + effects_coord.X, coords.Y + effects_coord.Y),
                player.effects, true);

            // Draw resistance effects
            var armor = inventory_cells[9].item;
            var resistance_list_armor =
                armor != null && armor is Armor ? (armor as Armor).resistanceEffects : new List<EffectTypes> { };
            var resistances = player.resistancesEffects.Union(resistance_list_armor).ToList();
            UI_Drawing_Static.DrawResistanceEffects(g, new Point(coords.X + resistance_coord.X, coords.Y + resistance_coord.Y),
                resistances, true, 30, false);

            // Draw level 
            UI_Drawing_Static.DrawString(g, new Point(level_coord.X + coords.X, level_coord.Y + coords.Y),
                $"lv: {player.level}", Brushes.CadetBlue, 20);
            // Draw scores
            UI_Drawing_Static.DrawString(g, new Point(scores_coord.X + coords.X, scores_coord.Y + coords.Y),
                $"({player.scores})", Brushes.Goldenrod, 14);


            // Draw reload weapon seconds
            var seconds_max = (inventory_cells[10].item == null) ? player.baseTimerHitMax :
                (inventory_cells[10].item as Weapon).timerHitMax;
            var secondsToHit = seconds_max - (player.timerHitNow.ElapsedMilliseconds / 1000.0);
            if (secondsToHit < 0 || secondsToHit == seconds_max) secondsToHit = 0;

            UI_Drawing_Static.DrawString(g, new Point(reloadWeaponSec_coord.X + coords.X, reloadWeaponSec_coord.Y + coords.Y),
                secondsToHit == 0 ? "0" : string.Format("{0:0.0}", secondsToHit),
                secondsToHit == 0 ? Brushes.Green : Brushes.Red,
                12);

            //-------------------------- Draw active item statistic
            var item = active_cell.item;
            if (item != null)
            {
                // name:
                UI_Drawing_Static.DrawString(g, new Point(weaponStat_name__coord.X + coords.X, weaponStat_name__coord.Y + coords.Y),
                    item.Name, Brushes.DarkOrange, 8);
                // description :
                UI_Drawing_Static.DrawString(g,
                    item.Description, Brushes.DarkOrange, 6f,
                    new RectangleF(
                        new PointF(weaponStat_description__coord.X + coords.X, weaponStat_description__coord.Y + coords.Y),
                        new SizeF(125, 20)), true);


                // ----------------------------------- Weapon
                if (item is Weapon)
                {
                    if ((item as Weapon).canHeal || (item as Weapon).canMeleeHit)
                    {
                        // splash:
                        UI_Drawing_Static.DrawString(g, new Point(weaponStat_splash__coord.X + coords.X, weaponStat_splash__coord.Y + coords.Y),
                        (item as Weapon).canSplash ? "+" : "-",
                        Brushes.DarkOrange, 12);

                        // range:
                        UI_Drawing_Static.DrawString(g, new Point(weaponStat_range__coord.X + coords.X, weaponStat_range__coord.Y + coords.Y),
                        string.Format("{0: 0.0}", (item as Weapon).actionRadius / Parameters.blockSize),
                        Brushes.DarkOrange, 10);
                    }
                    if ((item as Weapon).canMeleeHit)
                    {
                        // damage:
                        UI_Drawing_Static.DrawString(g, new Point(weaponStat_damage__coord.X + coords.X, weaponStat_damage__coord.Y + coords.Y),
                        (item as Weapon).damage.ToString(), Brushes.DarkOrange, 10);
                    }
                    if ((item as Weapon).canShoot)
                    {
                        // shoot damage:
                        UI_Drawing_Static.DrawString(g, new Point(weaponStat_shootDM__coord.X + coords.X, weaponStat_shootDM__coord.Y + coords.Y),
                        (item as Weapon).shoot_damage.ToString(), Brushes.DarkOrange, 10);

                        // bullets:
                        UI_Drawing_Static.DrawString(g, new Point(weaponStat_bullets__coord.X + coords.X, weaponStat_bullets__coord.Y + coords.Y),
                        (item as Weapon).bulletCount.ToString(), Brushes.DarkOrange, 10);
                    }
                    if ((item as Weapon).canHeal)
                    {
                        // heal
                        UI_Drawing_Static.DrawString(g, new Point(weaponStat_heal__coord.X + coords.X, weaponStat_heal__coord.Y + coords.Y),
                        (item as Weapon).healing.ToString(), Brushes.DarkOrange, 10);
                    }

                    // effects:
                    UI_Drawing_Static.DrawEffects(g, new Point(coords.X + weaponStat_effects__coord.X, coords.Y + weaponStat_effects__coord.Y),
                    (item as Weapon).getting_weapon_effects, isToRight: true, distance: 16);

                    // reload:
                    UI_Drawing_Static.DrawString(g, new Point(weaponStat_reload__coord.X + coords.X, weaponStat_reload__coord.Y + coords.Y),
                    (item as Weapon).timerHitMax.ToString(), Brushes.DarkOrange, 10);

                }
                //------------------------------------ Potion
                else if (item is Potion)
                {
                    // effects:
                    UI_Drawing_Static.DrawEffects(g, new Point(coords.X + weaponStat_effects__coord.X, coords.Y + weaponStat_effects__coord.Y),
                    (item as Potion).effects, isToRight: true, distance: 16);

                    // heal
                    UI_Drawing_Static.DrawString(g, new Point(weaponStat_heal__coord.X + coords.X, weaponStat_heal__coord.Y + coords.Y),
                    (item as Potion).heal.ToString(), Brushes.DarkOrange, 10);

                }
                //------------------------------------ Bomb
                else if (item is Bomb)
                {
                    // range:
                    UI_Drawing_Static.DrawString(g, new Point(weaponStat_range__coord.X + coords.X, weaponStat_range__coord.Y + coords.Y),
                    string.Format("{0: 0.0}", (item as Bomb).radius / Parameters.blockSize),
                    Brushes.DarkOrange, 10);

                    // damage:
                    UI_Drawing_Static.DrawString(g, new Point(weaponStat_damage__coord.X + coords.X, weaponStat_damage__coord.Y + coords.Y),
                    (item as Bomb).damage.ToString(), Brushes.DarkOrange, 10);

                }
                //------------------------------------ Armor
                else if (item is Armor)
                {

                    // resistance effects:
                    UI_Drawing_Static.DrawResistanceEffects(g, new Point(coords.X + weaponStat_resist__coord.X, coords.Y + weaponStat_resist__coord.Y),
                    (item as Armor).resistanceEffects, isToRight: true, 9, withBorder: false);

                    // armor:
                    UI_Drawing_Static.DrawString(g, new Point(coords.X + weaponStat_shield__coord.X, coords.Y + weaponStat_shield__coord.Y),
                    (item as Armor).armor.ToString(), Brushes.DarkOrange, 10);

                }

                //------------------------------------ Type:
                UI_Drawing_Static.DrawString(g, new Point(weaponStat_type__coord.X + coords.X, weaponStat_type__coord.Y + coords.Y),
                item.opportunUseCount > 0 ? $"{item.itemType} [{item.opportunUseCount}]" : $"{item.itemType}",
                Brushes.DarkTurquoise, 10);
            }

        }

        public void Update()
        {
            // Update cells
            foreach (var cell in inventory_cells)
            {
                cell.Update();
            }

            // Visibility
            if (isShow)
            {
                if (coords.Y != global_coords.Y)
                {
                    coords = new Point(coords.X, coords.Y - 20); 

                    if (coords.Y < global_coords.Y) 
                        coords = new Point(coords.X, global_coords.Y);
                }
            }
            else
            {
                if (coords.Y != belowY_coords)
                {
                    coords = new Point(coords.X, coords.Y + 20);

                    if (coords.Y > belowY_coords)
                        coords = new Point(coords.X, belowY_coords);
                }
            }

        }


        // Controls & Actions

        public void changeVisibility() => isShow = !isShow;

        public void takeToWeapons(ParentItem item) // temporary void
        { 
            if (item != null && item is Weapon)
            {
                inventory_cells[10].item = item;
            }
        }

        public void dropItemFromActiveCell() => active_cell.item = null;
        public bool tryToTakeItemToInventory(ParentItem item)
        {
            for (int i = 0; i < inventory_cells.Length; i++)
            {
                var inv_item = inventory_cells[i].item;
                if (inv_item != null)
                {
                    if (inv_item.GetType() == item.GetType() &&
                        inv_item.opportunUseCount > 0 && item.opportunUseCount > 0)
                    {
                        inv_item.addItems(item);
                        return true;
                    }
                }
            }

            for (int i = 0; i < inventory_cells.Length; i++)
            {
                if (inventory_cells[i].item == null)
                {
                    if (i == 10 && !(item is Weapon)) return false;
                    if (i == 9 && (item is Weapon)) return false;

                    inventory_cells[i].item = item;
                    active_cell = inventory_cells[i];
                    return true;
                }
            }
            return false;
        }
        
        public void setActiveCellToBaseActiveSlot()
        {
            if (active_cell != inventory_cells[9] && active_cell != inventory_cells[10])
            {
                if (active_cell.item != null && active_cell.item is Weapon) return;

                var temporary_cell = active_cell.item;
                active_cell.item = inventory_cells[9].item;
                inventory_cells[9].item = temporary_cell;
            }
        }
        public void setActiveCellToWeaponActiveSlot()
        {
            if (active_cell != inventory_cells[9] && active_cell != inventory_cells[10])
            {
                if (active_cell.item != null && !(active_cell.item is Weapon)) return;

                var temporary_cell = active_cell.item;
                active_cell.item = inventory_cells[10].item;
                inventory_cells[10].item = temporary_cell;
            }
        }

        public void goToFirstCell() => active_cell = inventory_cells[0];
        public void goToActiveWeaponCell() => active_cell = inventory_cells[10];
        public void takeLeftCell()
        {
            active_cell =
                active_cell == inventory_cells.First() ?
                inventory_cells[inventory_cells.Length - 1] :
                inventory_cells[Array.IndexOf(inventory_cells, active_cell) - 1];
        }
        public void takeRightCell()
        {
            active_cell =
                active_cell == inventory_cells.Last() ?
                inventory_cells[0] :
                inventory_cells[Array.IndexOf(inventory_cells, active_cell) + 1];
        }

        public ItemTypes tryToUseActiveCell() => active_cell.Use(player);
        public ItemTypes TryToUseBaseActiveSlot() => inventory_cells[9].Use(player);

        // Armor Actions

        public double tryToResistAttackWithActiveArmor()
        {
            var item = inventory_cells[9].item;

            if (item != null && item is Armor)
            {
                item.Use(player);
                return (item as Armor).armor;
            }
            else return 0;
        }

        public bool tryToResistEffectWithActiveArmor(Effect effect)
        {
            var item = inventory_cells[9].item;

            if (item != null && item is Armor &&
                (item as Armor).resistanceEffects.Contains(effect.effectType))
            {
                item.Use(player);
                return true;
            }
            else 
                return false;
        }
    }
}
