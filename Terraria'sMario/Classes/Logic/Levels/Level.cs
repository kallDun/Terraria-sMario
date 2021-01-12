
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Terraria_sMario.Classes.Control;
using Terraria_sMario.Classes.Logic.Objects;
using Terraria_sMario.Classes.Logic.Objects.Creatures;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Animations;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies;
using Terraria_sMario.Classes.Logic.Objects.Environment;
using Terraria_sMario.Classes.Logic.Objects.Environment.Static_Blocks;
using Terraria_sMario.Classes.Logic.Services;
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
            foreach (var item in objectsInTheView)
            { 
                if (item is StaticBlockObject) item.Draw(g);
            }

            foreach (var item in objectsInTheView)
            {
                if (item is Entity && !(item is Player)) item.Draw(g);
            }

            foreach (var player in players)
            {
                player.Draw(g);
            }
        }

        public void Update()
        {
            foreach (var item in objectsInTheView)
            {
                item.updateProperties();

                if (item is Entity)
                {
                    (item as Entity).update();
                    (item as Entity).updateGravitation(objectsInTheView);
                    levelObjects = levelObjects.Concat((item as Entity).updateWorld()).ToList();

                    if (item is Enemy)
                    {
                        (item as Enemy).updateBehavior(objectsInTheView);
                    }
                } 
            }

            controlKeyboard.updateMove(players, objectsInTheView);
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

        public void KeyboardListenerPressed(KeyEventArgs e) => controlKeyboard.KeyPress(e, players);

        public void KeyboardListenerReleased(KeyEventArgs e) => controlKeyboard.KeyUp(e);


    }
}
