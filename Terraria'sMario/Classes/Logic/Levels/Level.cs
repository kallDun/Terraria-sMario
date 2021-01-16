using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Terraria_sMario.Classes.Control;
using Terraria_sMario.Classes.Logic.Objects;
using Terraria_sMario.Classes.Logic.Objects.Creatures;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Items;
using Terraria_sMario.Classes.Logic.Objects.Environment;
using Terraria_sMario.Classes.Logic.Objects.Environment.Background_Blocks;
using Terraria_sMario.Classes.Logic.Objects.Environment.Translucent_Blocks;
using Terraria_sMario.Classes.Logic.Objects.Environment.Transparent_Blocks.Translucent_Blocks;
using Terraria_sMario.Classes.Logic.Objects.Items.Weapons.Bullets;
using static Terraria_sMario.Classes.Logic.Parameters;

namespace Terraria_sMario.Classes.Logic.Levels
{
    abstract class Level
    {
        // fields

        public List<ParentObject> levelObjects { get; protected set; } = new List<ParentObject> { };
        
        public List<ParentObject> objectsInTheView { get; protected set; } = new List<ParentObject> { };

        public List<Player> players { get; protected set; } = new List<Player> { };

        public Size fieldSize { get; protected set; }

        // threads

        public void Draw(Graphics g)
        {
            // Background blocks
            foreach (var item in objectsInTheView)
            {
                if (item is BackgroundBlockObject) item.Draw(g);
            }
            // Static blocks
            foreach (var item in objectsInTheView)
            { 
                if (item is StaticBlockObject) item.Draw(g);
            }
            // Transparent Blocks
            foreach (var item in objectsInTheView)
            {
                if (item is TransparentBlockObject) item.Draw(g);
            }
            // Items & Coins
            foreach (var item in objectsInTheView)
            {
                if (item is ParentItem || item is Coin || item is Sword) item.Draw(g);
            }
            // NPC's
            foreach (var item in objectsInTheView)
            {
                if (item is Entity && !(item is Player)) item.Draw(g);
            }
            // Bullets
            foreach (var item in objectsInTheView)
            {
                if (item is BulletParent) item.Draw(g);
            }
            // Players
            foreach (var player in players)
            {
                player.Draw(g);
            }
            // Translucent objects
            foreach (var item in objectsInTheView)
            {
                if (item is TranslucentBlockObject) item.Draw(g);
            }
            // Inventory
            foreach (var player in players)
            {
                player.DrawInventory(g);
            }
        }

        public void Update()
        {
            foreach (var item in objectsInTheView)
            {
                item.updateProperties(objectsInTheView);

                if (item is Entity)
                {
                    (item as Entity).update();
                    (item as Entity).updateGravitation(objectsInTheView);
                    levelObjects = levelObjects.Concat((item as Entity).updateWorld()).ToList();
                }
            }

            controlKeyboard.updateMove(players, objectsInTheView);

            // Destroy check
            foreach (var item in objectsInTheView)
            {
                if (item.isToDestroy)
                {
                    levelObjects.Remove(item);
                }
            }

        }

        public void KeepMainObjectInTheCenter()
        {
            var playerPoint = players[0].coords;
            var offSetX = centerPosition.X - playerPoint.X;
            var offSetY = centerPosition.Y - playerPoint.Y;

            offsetAllObjectsInPosition(offSetX, offSetY);
        } 

        public void updateFieldOfView()
        {
            objectsInTheView = new List<ParentObject> { };

            foreach (var obj in levelObjects)
            {
                if (obj.coords.X >= -8 * blockSize &&
                obj.coords.X <= (fieldWidth + 8) * blockSize &&
                obj.coords.Y >= -4 * blockSize &&
                obj.coords.Y <= (fieldHeight + 4) * blockSize)
                {
                    objectsInTheView.Add(obj);
                }
            }
        }

        // field offset

        private void offsetAllObjectsInPosition(int offSetX, int offSetY)
        {
            foreach (var obj in levelObjects)
            {
                obj.offsetPositionX_Y(offSetX, offSetY);
            }
        }

        // listeners to control player

        private ControlKeyboard controlKeyboard = new ControlKeyboard();

        public void KeyboardListenerPressed(KeyEventArgs e) => controlKeyboard.KeyPress(e, players, objectsInTheView);

        public void KeyboardListenerReleased(KeyEventArgs e) => controlKeyboard.KeyUp(e);

    }
}
