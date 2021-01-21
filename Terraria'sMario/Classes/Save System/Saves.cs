using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Logic.Levels;
using Terraria_sMario.Classes.Logic.Objects.Creatures;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Players;

namespace Terraria_sMario.Classes.Save_System
{
    static class Saves
    {
        public static List<SaveData> game_Saves = new List<SaveData> 
        {
            new SaveData("Test Save", DifficultTypes.Hard, new List<Player>{ new Hero(480, 100, "kallDun", 1) })
        };

        public static int lastGame = 0; // index in list of saves about last game


        public static SaveData activeSaveData;
        
        
        //public static int activeLevelNumber;
        //public static Level activeLevel;

    }
}
