using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.AI_Behavior
{
    enum ActionType
    {
        // Walking not battle actions :
        Stand, MoveRight, MoveLeft, FlyRight, FlyLeft, JumpRight, JumpLeft,


        // Battle actions :
        KeepMovingToEnemy, KeepMovingToElly,

        Retreat, Hit, Heal, Shoot,

        None
    }
}
