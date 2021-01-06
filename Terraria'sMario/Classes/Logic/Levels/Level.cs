
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Terraria_sMario.Classes.Control;
using Terraria_sMario.Classes.Logic.Objects;
using Terraria_sMario.Classes.Logic.Objects.Creatures;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Animations;
using Terraria_sMario.Classes.Logic.Objects.Environment;
using Terraria_sMario.Classes.Logic.Objects.Environment.Static_Blocks;
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
                } 
            }
        }

        public void KeepMainPlayerInTheCenter()
        {
            var playerPoint = players[0].coords;
            var offSetX = mainChacterPosition.X - playerPoint.X;
            var offSetY = mainChacterPosition.Y - playerPoint.Y;

            offsetAllObjectsPositionX_Y(offSetX, offSetY);
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

        // listeners to control player

        public void KeyboardListener(KeyEventArgs e)
        {
            if (ControlKeyboard.checkOnPressedSpace(e))
            {
                players[0].Jump();
            }
            else
            if (ControlKeyboard.checkOnPressedRight(e))
            {
                players[0].moveRightOrLeft(5, objectsInTheView);
                players[0].setAnimation(PlayerAnimationTypes.Walking);
            }
            else
            if (ControlKeyboard.checkOnPressedLeft(e))
            {
                players[0].moveRightOrLeft(-5, objectsInTheView);
                players[0].setAnimation(PlayerAnimationTypes.Walking);
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
