using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.AI_Behavior;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.Behavior
{
    class BehaviorControl
    {
        private List<BehaviorUnitMoving> movingBehaviourList;
        private int moveBehaveList_on = 0;
        private int lastMove = 0;

        private bool isJump = false;
        private double jump_seconds_final = 0.5;
        private double jump_seconds_now = 0;
        private int jump_direction;

        public BehaviorControl(List<BehaviorUnitMoving> movingBehaviourList)
        {
            this.movingBehaviourList = movingBehaviourList;
        }

        public void update(Enemy enemy, in List<ParentObject> objects)
        {
            if (!movingBehaviourList[moveBehaveList_on].isActiveUnit())
            {
                if (movingBehaviourList[moveBehaveList_on].behaviorType == BehaviorTypes.Standing)
                {
                    lastMove = 1;
                    isJump = false;
                }

                if (movingBehaviourList[moveBehaveList_on] != movingBehaviourList.Last())
                    moveBehaveList_on++;
                else
                {
                    moveBehaveList_on = 0;

                    foreach (var item in movingBehaviourList)
                        item.restartUnit();
                } 
            }

            if (lastMove == 0) enemy.setAnimation(Features.EnemyAnimationTypes.Standing);

            if (!isJump)
            {
                var action_moving = movingBehaviourList[moveBehaveList_on].Update(lastMove);

                if (action_moving == ActionType.MoveRight)
                    lastMove = enemy.moveRightOrLeft(objects, 1);
                else if (action_moving == ActionType.MoveLeft)
                    lastMove = enemy.moveRightOrLeft(objects, -1);
                else if (action_moving == ActionType.JumpRight)
                {
                    isJump = true;
                    jump_direction = 1;
                }
                else if (action_moving == ActionType.JumpLeft)
                {
                    isJump = true;
                    jump_direction = -1;
                }
                else if (action_moving == ActionType.Stand)
                    lastMove = 0;
            }
            else
            {
                enemy.Jump();
                jump_seconds_now += (1.0 / Parameters.fps);

                if (jump_seconds_now >= jump_seconds_final)
                {
                    isJump = false;
                    jump_seconds_now = 0;
                    lastMove = enemy.moveRightOrLeft(objects, jump_direction);
                }
            }
            
        }

    }
}
