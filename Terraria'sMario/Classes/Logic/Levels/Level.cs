
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Terraria_sMario.Classes.Control;
using Terraria_sMario.Classes.Logic.Objects;
using Terraria_sMario.Classes.Logic.Objects.Creatures;
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

        public Player player { get; protected set; }

        public Size fieldSize { get; protected set; }

        // character offset

        public void offsetPositionX(int offSetX)
        {
            foreach (var obj in levelObjects)
            {
                if (obj != player) obj.offsetPositionX(offSetX);
            }
        }

        public void offsetPositionY(int offSetY)
        {
            foreach (var obj in levelObjects)
            {
                obj.offsetPositionY(offSetY);
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
                if (item is Entity && item != player) item.Draw(g);
            }

            player.Draw(g);
        }

        public void Update()
        {
            foreach (var item in objectsInTheView)
            {
                item.updateProperties();

                if (item is Entity)
                {
                    (item as Entity).update();
                } 
            }
        }

        public void CheckCollision()
        {
            foreach (var item in objectsInTheView)
            {
                foreach (var block in objectsInTheView)
                {
                    if (item is Entity && block != item)
                    {
                        if (IntersectionService.isBlockIntersectBlock(item, block))
                        {
                            string type = IntersectionService.getTypeOfIntersectingBlock(item, block);

                            switch (type)
                            {
                                case "down":
                                    (item as Entity).setAccelerationToZero();
                                    item.setUpToTheBlock(block);
                                    break;

                                case "up":
                                    (item as Entity).setAccelerationToZero();
                                    item.setDownToTheBlock(block);
                                    break;

                                case "left":
                                    break;

                                case "right":
                                    break;
                            }
                        }
                    }                    
                }
            }
        }

        public void updateFieldOfView()
        {
            objectsInTheView = new List<ParentObject> { };

            foreach (var obj in levelObjects)
            {
                if (obj.coords.X >= -blockSize &&
                    obj.coords.X <= fieldWidth * blockSize &&
                    obj.coords.Y >= 0 &&
                    obj.coords.Y <= fieldHeight * blockSize)
                {
                    objectsInTheView.Add(obj);
                }
            }
        }

        // listeners

        public void KeyboardListener(KeyEventArgs e)
        {
            if (ControlKeyboard.checkOnPressedSpace(e))
            {
                player.Jump();
            }
            if (ControlKeyboard.checkOnPressedRight(e))
            {
                offsetPositionX(-5);
            }
            if (ControlKeyboard.checkOnPressedLeft(e))
            {
                offsetPositionX(5);
            }
            if (ControlKeyboard.checkOnPressedTop(e))
            {
                player.Jump();
            }
        }

        // generate blocks methods

        protected void fillFieldWithGrass(int height, int width0, int width)
        {
            for (int i = width0; i < width; i++)
            {
                var grass = new GrassBlock(i * blockSize, height * blockSize);
                levelObjects.Add(grass);

                for (int j = height + 1; j < fieldSize.Height; j++)
                {
                    var dirt = new DirtBlock(i * blockSize, j * blockSize);
                    levelObjects.Add(dirt);
                }
            }
        }

        
    }
}
