using System.Drawing;
using Terraria_sMario.Classes.Logic.Levels.LevelBuilding;
using Terraria_sMario.Classes.Logic.Objects;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.Skeletons;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Players;
using Terraria_sMario.Classes.Logic.Objects.Environment.Translucent_Blocks;
using Terraria_sMario.Classes.Logic.Objects.Items.Armor;
using Terraria_sMario.Classes.Logic.Objects.Items.Bombs;
using Terraria_sMario.Classes.Logic.Objects.Items.Potions;
using Terraria_sMario.Classes.Logic.Objects.Items.Weapons.Guns;
using Terraria_sMario.Classes.Logic.Objects.Items.Weapons.Swords;
using static Terraria_sMario.Classes.Logic.Parameters;

namespace Terraria_sMario.Classes.Logic.Levels
{
    class TestLevel_1 : Level
    {
        public TestLevel_1 ()
        {
            var player = new Hero(480, 100, "Player_1", 1);
            levelObjects.Add(player);
            players.Add(player);


            /*var skelet2 = new SkeletonBasic(800, 100);
            levelObjects.Add(skelet2);

            var skelet3 = new SkeletonHealer(1400, 100);
            levelObjects.Add(skelet3);*/

            var skelet = new SkeletonArcher(900, 100);
            levelObjects.Add(skelet);

            var item = new Basic_Sword(520, 250);
            levelObjects.Add(item);

            var bow = new Basic_Bow(520, 100);
            levelObjects.Add(bow);

            var potion_heal = new SmallHealing_Potion(520, 100);
            levelObjects.Add(potion_heal);

            var potion_heal2 = new SmallHealing_Potion(520, 100);
            levelObjects.Add(potion_heal2);

            var bomb = new StandartBomb(520, 100);
            levelObjects.Add(bomb);

            var bomb2 = new StandartBomb(520, 100);
            levelObjects.Add(bomb2);

            var armor = new Basic_Armor(520, 100);
            levelObjects.Add(armor);

            var coin = new Coin(1750, 350);
            levelObjects.Add(coin);

            /*var sword = new Sword(580, 100);
            levelObjects.Add(sword);*/

            fieldSize = new Size(100, 100);

            int height_down = fieldSize.Height;
            int height_1 = 8;
            int height_2 = 7;
            int height_3 = 8;
            int height_4 = 10;
            int height_5 = 11;
            int width_1 = 7;
            int width_2 = 14;
            int width_3 = 16;
            int width_4 = 40;
            int width_5 = 60;

            BuildingStatic.fillFieldWithGrass(levelObjects, height_down, height_1, 0,        width_1);
            BuildingStatic.fillFieldWithGrass(levelObjects, height_down, height_2, width_1,  width_2);
            BuildingStatic.fillFieldWithGrass(levelObjects, height_down, height_3, width_2,  width_3);
            BuildingStatic.fillFieldWithGrass(levelObjects, height_down, height_4, width_3,  width_4);
            BuildingStatic.fillFieldWithGrass(levelObjects, height_down, height_5, width_4,  width_5);

            BuildingStatic.BuildBrickHouse(levelObjects, 1750, 500, BuildingTypes.Medium);

            BuildingStatic.BuildBlockOfFlats(levelObjects, 1100, 450, BuildingTypes.Large);
        }
    }
}
