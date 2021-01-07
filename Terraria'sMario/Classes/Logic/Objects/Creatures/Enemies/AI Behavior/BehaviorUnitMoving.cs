using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.AI_Behavior
{
    class BehaviorUnitMoving : BehaviorUnitParent
    {
        private int radius;
        private int distancePass = 0;
        private int direction;
        private bool isJumped = false;

        public BehaviorUnitMoving(BehaviorTypes behaviorType, double duration, int radius = 5, int startDirection = 1) 
        {
            this.behaviorType = behaviorType;
            this.radius = radius * Parameters.blockSize;
            direction = startDirection;
            this.duration = duration;
            durationNow = duration;
        }

        public ActionType Update(int lastMove)
        {
            UpdateTimer();

            if (behaviorType == BehaviorTypes.Standing)
                return ActionType.Stand;

            if (behaviorType == BehaviorTypes.Walking ||
                behaviorType == BehaviorTypes.Flying)
            {
                distancePass += lastMove;

                if (lastMove == 0)
                {
                    if (isJumped)
                    {
                        distancePass = 0;
                        direction = direction == 1 ? -1 : 1;
                    }
                    else
                    {
                        isJumped = true;
                        if (direction == 1) return ActionType.JumpRight;
                        else return ActionType.JumpLeft;
                    }
                }
                isJumped = false;

                if (distancePass >= radius / 2) direction = -1;
                else if (distancePass <= -(radius / 2)) direction = 1;

                if (direction == 1) return ActionType.MoveRight;
                else return ActionType.MoveLeft;
            }

            return ActionType.Stand;
        }



    }
}
