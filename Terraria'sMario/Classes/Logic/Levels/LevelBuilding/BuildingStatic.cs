using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects;
using Terraria_sMario.Classes.Logic.Objects.Environment;
using Terraria_sMario.Classes.Logic.Objects.Environment.Static_Blocks;
using Terraria_sMario.Classes.Logic.Objects.Environment.Translucent_Blocks;
using Terraria_sMario.Classes.Logic.Objects.Environment.Transparent_Blocks;
using Terraria_sMario.Classes.Logic.Objects.Environment.Transparent_Blocks.Translucent_Blocks;
using Terraria_sMario.Classes.Logic.Services;
using static Terraria_sMario.Classes.Logic.Parameters;

namespace Terraria_sMario.Classes.Logic.Levels.LevelBuilding
{
    static class BuildingStatic
    {

        public static void fillFieldWithGrass(List<ParentObject> levelObjects, int height, int height0, int width0, int width)
        {
            for (int i = width0; i < width; i++)
            {
                GrassBlock grass = new GrassBlock(i * blockSize, height0 * blockSize);
                levelObjects.Add(grass);

                for (int j = height0 + 1; j < height; j++)
                {
                    DirtBlock dirt = new DirtBlock(i * blockSize, j * blockSize);
                    levelObjects.Add(dirt);
                }
            }
        }

        //Building Brick House method
        public static void BuildBrickHouse(List<ParentObject> levelObjects, int X, int Y, BuildingTypes type)
        {
            // Parameters
            int wall_height = 0;
            int roof_height = 0;
            int floor_width = 0;
            int door_height = 0;

            var coord = new Point(X, Y);
            if (type == BuildingTypes.Large)
            {
                RemoveBlocks(levelObjects, X, Y, 9, 9);
                wall_height = 2;
                roof_height = 4;
                floor_width = 8;
                door_height = 3;
            }
            else if (type == BuildingTypes.Medium)
            {
                RemoveBlocks(levelObjects, X, Y, 7, 9);
                wall_height = 1;
                roof_height = 3;
                floor_width = 6;
                door_height = 3;
            }
            else if (type == BuildingTypes.Small)
            {
                RemoveBlocks(levelObjects, X, Y, 5, 9);
                wall_height = 1;
                roof_height = 2;
                floor_width = 4;
                door_height = 2;
            }

            // Static blocks
            coord.Offset(0, -(1 + door_height) * blockSize);
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
            coord.Offset(0, (1 + door_height) * blockSize);
            levelObjects.Add(new BrickBlock(coord));
            for (int i = 0; i < floor_width; i++)
            {
                coord.Offset(-1 * blockSize, 0);
                levelObjects.Add(new BrickBlock(coord));
            }
            if (type == BuildingTypes.Large || type == BuildingTypes.Medium)
            {
                var y_offset = floor_width / 2 - 1;
                coord.Offset(y_offset * blockSize, -1 * blockSize);
                levelObjects.Add(new BrickBlock(coord));
                coord.Offset(1 * blockSize, -1 * blockSize);
                levelObjects.Add(new BrickBlock(coord));
                coord.Offset(0, 1 * blockSize);
                levelObjects.Add(new BrickBlock(coord));
                coord.Offset(1 * blockSize, 0);
                levelObjects.Add(new BrickBlock(coord));
            }

            // Translucent Coating
            var _coord = new Point(X, Y);

            for (int i = 0; i < door_height; i++)
            {
                _coord.Offset(0, -blockSize);
                var newCoord = _coord;
                for (int j = 0; j <= floor_width; j++)
                {
                    levelObjects.Add(new WoodTranslucentBlock(newCoord));
                    newCoord.Offset(blockSize, 0);
                }
            }
            _coord.Offset(blockSize, 0);
            for (int i = 0; i <= wall_height; i++)
            {
                _coord.Offset(0, -blockSize);
                var newCoord = _coord;
                for (int j = 0; j <= floor_width - 2; j++)
                {
                    levelObjects.Add(new WoodTranslucentBlock(newCoord));
                    newCoord.Offset(blockSize, 0);
                }
            }
            for (int i = 0; i <= roof_height - 1; i++)
            {
                _coord.Offset(blockSize, -blockSize);
                var newCoord = _coord;
                for (int j = 0; j <= floor_width - (2 * (i + 2)); j++)
                {
                    levelObjects.Add(new WoodTranslucentBlock(newCoord));
                    newCoord.Offset(blockSize, 0);
                }
            }

        }

        public static void BuildBlockOfFlats(List<ParentObject> levelObjects, int X, int Y, BuildingTypes type)
        {
            int floor = 0;
            var coord = new Point(X, Y);
            if (type == BuildingTypes.Large)
            {
                RemoveBlocks(levelObjects, X, Y, 9, 26);
                floor = 5;
            }
            else if (type == BuildingTypes.Medium)
            {
                floor = 3;
            }
            else if (type == BuildingTypes.Small)
            {
                floor = 2;
            }

            coord.Offset(3 * blockSize, 1 * blockSize);
            for (int i = 0; i < (floor - 1) * 5; i++)
            {
                coord.Offset(0, -1 * blockSize);
                levelObjects.Add(new LadderBlock(coord));
            }
            coord.Offset(-3 * blockSize, (floor - 1) * 5 * blockSize + 3);

            //first floor
            coord.Offset(0, -3 * blockSize);
            levelObjects.Add(new ConcreteBlock(coord));
            for (int i = 0; i < 2; i++)
            {
                coord.Offset(0, -1 * blockSize);
                levelObjects.Add(new ConcreteBlock(coord));
            }
            for (int i = 0; i < 2; i++)
            {
                coord.Offset(1 * blockSize, 0);
                levelObjects.Add(new ConcreteBlock(coord));
            }
            coord.Offset(1 * blockSize, 0);
            for (int i = 0; i < 5; i++)
            {
                coord.Offset(1 * blockSize, 0);
                levelObjects.Add(new ConcreteBlock(coord));
            }
            for (int i = 0; i < 5; i++)
            {
                coord.Offset(0, 1 * blockSize);
                levelObjects.Add(new ConcreteBlock(coord));
            }
            for (int i = 0; i < 8; i++)
            {
                coord.Offset(-1 * blockSize, 0);
                levelObjects.Add(new ConcreteBlock(coord));
            }

            //second floor
            coord.Offset(0, -5 * blockSize);
            for (int i = 0; i < 5; i++)
            {
                coord.Offset(0, -1 * blockSize);
                levelObjects.Add(new ConcreteBlock(coord));
            }
            for (int i = 0; i < 2; i++)
            {
                coord.Offset(1 * blockSize, 0);
                levelObjects.Add(new ConcreteBlock(coord));
            }
            coord.Offset(1 * blockSize, 0);
            for (int i = 0; i < 5; i++)
            {
                coord.Offset(1 * blockSize, 0);
                levelObjects.Add(new ConcreteBlock(coord));
            }
            for (int i = 0; i < 2; i++)
            {
                coord.Offset(0, 1 * blockSize);
                levelObjects.Add(new ConcreteBlock(coord));
            }

            // another floors
            for (int j = 0; j < floor; j++)
            {
                coord.Offset(-8 * blockSize, -2 * blockSize);
                for (int i = 0; i < 5; i++)
                {
                    coord.Offset(0, -1 * blockSize);
                    levelObjects.Add(new ConcreteBlock(coord));
                }
                for (int i = 0; i < 2; i++)
                {
                    coord.Offset(1 * blockSize, 0);
                    levelObjects.Add(new ConcreteBlock(coord));
                }
                coord.Offset(1 * blockSize, 0);
                for (int i = 0; i < 5; i++)
                {
                    coord.Offset(1 * blockSize, 0);
                    levelObjects.Add(new ConcreteBlock(coord));
                }
                for (int i = 0; i < 4; i++)
                {
                    coord.Offset(0, 1 * blockSize);
                    levelObjects.Add(new ConcreteBlock(coord));
                }
            }
        }

        private static void RemoveBlocks(List<ParentObject> levelObjects, int X, int Y, int width, int height)
        {
            var area = new AbstractObject(new Point(X, Y - height * blockSize), new Size(width * blockSize, (height + 1) * blockSize));
            for (int i = levelObjects.Count - 1; i >= 0; i--)
            {
                if (IntersectionService.isBlockIntersectBlock(area, new GrassBlock(20, 12), levelObjects[i], useRules: false))
                {
                    if (levelObjects[i] is StaticBlockObject ||
                        levelObjects[i] is TranslucentBlockObject ||
                        levelObjects[i] is TransparentBlockObject)
                    {
                        levelObjects.Remove(levelObjects[i]);
                    }
                }
            }
        }
    }
}
