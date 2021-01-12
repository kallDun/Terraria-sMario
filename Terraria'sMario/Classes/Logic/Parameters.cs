using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Logic.Objects;
using Terraria_sMario.Classes.Logic.Objects.Creatures;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies;

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
        
    }
}
