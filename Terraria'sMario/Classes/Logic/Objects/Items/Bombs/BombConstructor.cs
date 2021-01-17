using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terraria_sMario.Classes.Logic.Objects.Items.Bombs
{
    class BombConstructor : Bomb
    {
        public BombConstructor(Bomb bomb, bool isTimerStarted = false)
        {
            Name = bomb.Name;
            Description = bomb.Description;
            coords = bomb.coords;
            size = bomb.size;

            itemType = bomb.itemType;
            opportunUseCount = 1;

            damage = bomb.damage;
            timerMax = bomb.timerMax;
            radius = bomb.radius;
            effects = bomb.effects;

            this.isTimerStarted = isTimerStarted;
            canGrab = !isTimerStarted;

            drawingImage = bomb.drawingImage;
            smallImage_inInventory = bomb.smallImage_inInventory;
        }
    }
}
