﻿
using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.Skeletons;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Players;
using static Terraria_sMario.Classes.Logic.Parameters;

namespace Terraria_sMario.Classes.Logic.Levels
{
    class TestLevel_1 : Level
    {

        public TestLevel_1 ()
        {
            var player = new Hero(480, 100, "Player_1");
            levelObjects.Add(player);
            players.Add(player);

            var skelet = new SkeletonBasic(1100, 100);
            levelObjects.Add(skelet);

            var skelet2 = new SkeletonHealer(1400, 100);
            levelObjects.Add(skelet2);

            fieldSize = new Size(100, 100);

            int height_1 = 8;
            int height_2 = 7;
            int height_3 = 8;
            int height_4 = 10;
            int height_5 = 11;
            int width_1 = 7;
            int width_2 = 14;
            int width_3 = 19;
            int width_4 = 40;
            int width_5 = 60;

            fillFieldWithGrass(height_1, 0, width_1);
            fillFieldWithGrass(height_2, width_1, width_2);
            fillFieldWithGrass(height_3, width_2, width_3);
            fillFieldWithGrass(height_4, width_3, width_4);
            fillFieldWithGrass(height_5, width_4, width_5);
        }

        
    }
}
