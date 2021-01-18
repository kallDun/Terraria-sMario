
using System.Collections.Generic;
using System.Drawing;

namespace Terraria_sMario.Classes.Logic
{
    static class Parameters
    {
        public static readonly int fps = 25;

        public static readonly int fieldWidth = 26;
        public static readonly int fieldHeight = 14;
        public static readonly int blockSize = 50;

        public static readonly Point centerPosition = new Point(610, 250);
        public static readonly Point inventoryPlayer1_Position = new Point(0, 415);
        public static readonly Point inventoryPlayer2_Position = new Point(775, 415);


        // --------------------- level system

        // player

        public static readonly Dictionary<int, int> ScoresToGetLevel 
            = new Dictionary<int, int>
            {
                {1, 0}, {2, 120}, {3, 250}, {4, 500}, {5, 800}, {6, 1200}, {7, 1600}, {8, 2100}, {9, 2700}, {10, 3500}
            };

        public static readonly Dictionary<int, double> HealthMultiplierPlayer
            = new Dictionary<int, double>
            {
                {1, 1}, {2, 1.25}, {3, 1.35}, {4, 1.2}, {5, 1.15}, {6, 1.1}, {7, 1.05}, {8, 1.025}, {9, 1.1}, {10, 1.15}
            };

        public static readonly Dictionary<int, double> DamageMultiplierPlayer
            = new Dictionary<int, double>
            {
                {1, 1}, {2, 1.1}, {3, 1.15}, {4, 1.2}, {5, 1.1}, {6, 1.05}, {7, 1.025}, {8, 1.05}, {9, 1.05}, {10, 1.1}
            };


        // enemy

        public static readonly Dictionary<int, double> HealthMultiplierEnemy
            = new Dictionary<int, double>
            {
                {1, 1}, {2, 1.15}, {3, 1.2}, {4, 1.1}, {5, 1.05}, {6, 1.05}, {7, 1.025}, {8, 1}, {9, 1.05}, {10, 1.1}
            };

        public static readonly Dictionary<int, double> DamageMultiplierEnemy
            = new Dictionary<int, double>
            {
                {1, 1}, {2, 1.05}, {3, 1.1}, {4, 1.15}, {5, 1.1}, {6, 1.05}, {7, 1.05}, {8, 1.025}, {9, 1.025}, {10, 1.05}
            };

    }
}
