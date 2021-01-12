using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Logic.Objects;
using Terraria_sMario.Classes.Logic.Objects.Environment;
using Terraria_sMario.Classes.Logic.Objects.Environment.Static_Blocks;
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

        public static void BuildBrickHouse(List<ParentObject> levelObjects, int X, int Y, BuildingTypes type)
        {
            int wall_height = 0;
            int roof_height = 0;
            int floor_width = 0;
            var coord = new Point(X, Y);

            if (type == BuildingTypes.Large)
            {
                RemoveBlocks(levelObjects, X, Y, 9, 9);
                wall_height = 2;
                roof_height = 4;
                floor_width = 8;
            }
            else if (type == BuildingTypes.Medium)
            {
                RemoveBlocks(levelObjects, X, Y, 7, 9);
                wall_height = 1;
                roof_height = 3;
                floor_width = 6;
            }
            else if (type == BuildingTypes.Small)
            {
                RemoveBlocks(levelObjects, X, Y, 5, 9);
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

        private static void RemoveBlocks(List<ParentObject> levelObjects, int X, int Y, int width, int height)
        {
            var area = new AbstractObject(new Point(X, Y - height * blockSize), new Size(width * blockSize, (height + 1) * blockSize));
            for (int i = levelObjects.Count - 1; i >= 0; i--)
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
