
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

        // field offset

        public void offsetAllObjectsPositionX_Y(int offSetX, int offSetY)
        {
            foreach (var obj in levelObjects)
            {
                obj.offsetPositionX_Y(offSetX, offSetY);
            }
        }

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
                    levelObjects.Concat((item as Entity).updateWorld());

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

            offsetAllObjectsPositionX_Y(offSetX, offSetY);
        } 

        public void updateFieldOfView()
        {
            objectsInTheView = new List<ParentObject> { };

            foreach (var obj in levelObjects)
            {
                if (obj.coords.X >= -2 * blockSize &&
                    obj.coords.X <= (fieldWidth + 4) * blockSize &&
                    obj.coords.Y >= -2 * blockSize &&
                    obj.coords.Y <= (fieldHeight + 4) * blockSize)
                {
                    objectsInTheView.Add(obj);
                }
            }
        }

        // listeners to control player

        private ControlKeyboard controlKeyboard = new ControlKeyboard();

        public void KeyboardListenerPressed(KeyEventArgs e) => controlKeyboard.KeyPress(e);

        public void KeyboardListenerReleased(KeyEventArgs e) => controlKeyboard.KeyUp(e);

        // generate blocks methods

        protected void fillFieldWithGrass(int height, int width0, int width)
        {
            for (int i = width0; i < width; i++)
            {
                GrassBlock grass = new GrassBlock(i * blockSize, height * blockSize);
                levelObjects.Add(grass);

                for (int j = height + 1; j < fieldSize.Height; j++)
                {
                    DirtBlock dirt = new DirtBlock(i * blockSize, j * blockSize);
                    levelObjects.Add(dirt);
                }
            }
        }

        protected void BuildBrickHouse(int X, int Y, BuildingTypes type)
        {
            int wall_height = 0;
            int roof_height = 0;
            int floor_width = 0;
            var coord = new Point(X, Y);
            if (type == BuildingTypes.Large)
            {
                RemovingBlocks(X, Y, 9, 9);
                wall_height = 2;
                roof_height = 4;
                floor_width = 8;
            }
            else if (type == BuildingTypes.Medium)
            {
                RemovingBlocks(X, Y, 7, 9);
                wall_height = 1;
                roof_height = 3;
                floor_width = 6;
            }
            else if (type == BuildingTypes.Small)
            {
                RemovingBlocks(X, Y, 5, 9);
                wall_height = 1;
                roof_height = 2;
                floor_width = 4;
            }
            coord.Offset(0, -3 * blockSize);
            levelObjects.Add(new BrickBlock(coord));
            for (int i = 0; i < wall_height; i++)
            {
                coord.Offset(0, -1 * blockSize);
                levelObjects.Add(new BrickBlock(coord));
            }
            for (int i = 0; i < roof_height; i++)
            {
                coord.Offset(1 * blockSize, -1 * blockSize);
                levelObjects.Add(new BrickBlock(coord));
            }
            for (int i = 0; i < roof_height; i++)
            {
                coord.Offset(1 * blockSize, 1 * blockSize);
                levelObjects.Add(new BrickBlock(coord));
            }
            for (int i = 0; i < wall_height; i++)
            {
                coord.Offset(0, 1 * blockSize);
                levelObjects.Add(new BrickBlock(coord));
            }
            coord.Offset(0, 3 * blockSize);
            levelObjects.Add(new BrickBlock(coord));
            for (int i = 0; i < floor_width; i++)
            {
                coord.Offset(-1 * blockSize, 0);
                levelObjects.Add(new BrickBlock(coord));
            }
            if (type == BuildingTypes.Large)
            {
                coord.Offset(3 * blockSize, -1 * blockSize);
                levelObjects.Add(new BrickBlock(coord));
                coord.Offset(1 * blockSize, -1 * blockSize);
                levelObjects.Add(new BrickBlock(coord));
                coord.Offset(0, 1 * blockSize);
                levelObjects.Add(new BrickBlock(coord));
                coord.Offset(1 * blockSize, 0);
                levelObjects.Add(new BrickBlock(coord));
            }
            else if (type == BuildingTypes.Medium)
            {
                coord.Offset(2 * blockSize, -1 * blockSize);
                levelObjects.Add(new BrickBlock(coord));
                coord.Offset(1 * blockSize, -1 * blockSize);
                levelObjects.Add(new BrickBlock(coord));
                coord.Offset(0, 1 * blockSize);
                levelObjects.Add(new BrickBlock(coord));
                coord.Offset(1 * blockSize, 0);
                levelObjects.Add(new BrickBlock(coord));
            }
        }

        protected void RemovingBlocks(int X, int Y, int width, int height)
        {
            var area = new AbstractObject(new Point(X, Y - height * blockSize), new Size(width * blockSize, (height + 1) * blockSize));
            for (int i = levelObjects.Count - 1 ; i >= 0; i--)
            {
                if (IntersectionService.isBlockIntersectBlock(area, new GrassBlock(20, 12), levelObjects[i]))
                {
                    if (levelObjects[i] is StaticBlockObject)
                    {
                        levelObjects.Remove(levelObjects[i]);
                    }
                }
            }
        }


    }
}
