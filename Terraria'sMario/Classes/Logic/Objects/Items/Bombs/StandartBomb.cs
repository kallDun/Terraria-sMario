using System.Drawing;
using Terraria_sMario.Images;

namespace Terraria_sMario.Classes.Logic.Objects.Items.Bombs
{
    class StandartBomb : Bomb
    {
        public StandartBomb(int X, int Y)
        {
            Name = "Обычная бомба";
            Description = "Взорвет врагов в радиусе 6 блоков .";
            coords = new Point(X, Y);
            size = new Size(40, 40);

            opportunUseCount = 1;

            damage = 15;
            timerMax = 5;
            radius = 6 * Parameters.blockSize;

            drawingImage = Items_res.Bomb;
            smallImage_inInventory = Items_res.Bomb_InInventory;
        }
    }
}
