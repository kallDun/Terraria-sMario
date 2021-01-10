using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Logic.DrawingElements;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Items;
using Terraria_sMario.Classes.Logic.Objects.Items.Weapons;
using Terraria_sMario.Images;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Players.InventorySystem
{
    class Inventory
    {
        private Player player;
        private Image drawingImage;

        private Point coords;

        private Cell active_cell;
        private Cell[] inventory_cells = new Cell[11] {
            new Cell(17, 163), new Cell(71, 163), new Cell(125, 163), 
            new Cell(180, 163), new Cell(237, 163), new Cell(293, 163),
            new Cell(350, 163), new Cell(407, 163), new Cell(465, 163),
            new Cell(26, 225), new Cell(95, 225) };

        private Point health_coord = new Point(16, 58);
        private Point name_coord = new Point(14, 8);
        private Point coinsCount_coord = new Point(220, 45);


        private int countOfCoins = 0;


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

            this.player = player;
            active_cell = inventory_cells[10];
        }


        public void Draw(Graphics g)
        {
            // Draw cells
            g.DrawImage(drawingImage, coords);
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
            UI_Drawing_Static.DrawString(g, new Point(coinsCount_coord.X + coords.X, coinsCount_coord.Y + coords.Y),
                countOfCoins.ToString(), Brushes.Yellow, 24, 
                new RectangleF(coinsCount_coord.X + coords.X, coinsCount_coord.Y + coords.Y, 70, 25));

        }

        public void Update()
        {
            foreach (var cell in inventory_cells)
            {
                cell.Update();
            }
        }


        // Controls

        public bool tryToTakeItemToInventory(ParentItem item)
        {
            for (int i = 0; i < inventory_cells.Length; i++)
            {
                if (inventory_cells[i].item != null)
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
                if (active_cell.item is Weapon) return;

                var temporary_cell = active_cell;
                active_cell = inventory_cells[9];
                inventory_cells[9] = temporary_cell;
            }
        }
        public void setActiveCellToWeaponActiveSlot()
        {
            if (active_cell != inventory_cells[9] && active_cell != inventory_cells[10])
            {
                if (!(active_cell.item is Weapon)) return;

                var temporary_cell = active_cell;
                active_cell = inventory_cells[9];
                inventory_cells[9] = temporary_cell;
            }
        }

        public void useCell() => active_cell.Use();
        public void goToFirstCell() => active_cell = inventory_cells[0];
        public void goToActiveWeaponCell() => active_cell = inventory_cells[10];
        public void takeLeftCell()
        {
            active_cell =
                active_cell != inventory_cells.First() ?
                inventory_cells[inventory_cells.Length - 1] :
                inventory_cells[Array.IndexOf(inventory_cells, active_cell) - 1];
        }
        public void takeRightCell()
        {
            active_cell =
                active_cell != inventory_cells.Last() ?
                inventory_cells[0] :
                inventory_cells[Array.IndexOf(inventory_cells, active_cell) + 1];
        }
    }
}
