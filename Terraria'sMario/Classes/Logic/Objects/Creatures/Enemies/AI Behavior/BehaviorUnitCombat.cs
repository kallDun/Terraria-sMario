using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.AI_Behavior
{
    class BehaviorUnitCombat : BehaviorUnitParent
    {

        public BehaviorUnitCombat(BehaviorTypes behaviorType, double duration)
        {
            this.behaviorType = behaviorType;
            this.duration = duration;
            durationNow = duration;
        }

        public ActionType Update(double distanceToEntity, in Entity entity)
        {
            if (behaviorType == BehaviorTypes.None) return ActionType.None;

            if (behaviorType == BehaviorTypes.Hitting)
            {
                var range = entity.weaponInHand != null ?
                    entity.weaponInHand.rangeOfMeleeHit :
                    entity.rangeOfMeleeHit;

                if (distanceToEntity > range)
                {
                    return ActionType.KeepMovingToEnemy;
                }
                else
                    return ActionType.Hit;

            }

            return ActionType.None;
        }
    }
}
