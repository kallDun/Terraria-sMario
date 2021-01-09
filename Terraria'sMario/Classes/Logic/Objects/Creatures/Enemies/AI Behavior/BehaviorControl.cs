using System;
using System.Collections.Generic;
using System.Linq;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.AI_Behavior;
using Terraria_sMario.Classes.Logic.Services;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.Behavior
{
    class BehaviorControl
    {
        private List<BehaviorUnitMoving> movingBehaviourList;
        private int moveBehaveList_on = 0;
        private int lastMove = 0;

        private bool isJump = false;
        private double jump_seconds_now = 0;
        private double jump_seconds_max = 0.5;
        private int jump_direction;


        private List<BehaviorUnitFinding> findingBehaviourList;
        private int findBehaveList_on = 0;
        private List<Entity> found_enemies = new List<Entity> { };
        private bool isFindEnemy = false;
        private double found_seconds_now = 0;
        private double found_seconds_max = 10;


        private List<BehaviorUnitCombat> combatBehaviourList;
        private int combatBehaveList_on = 0;


        public BehaviorControl(
            List<BehaviorUnitMoving> movingBehaviourList,
            List<BehaviorUnitFinding> findingBehaviourList,
            List<BehaviorUnitCombat> combatBehaviourList)
        {
            this.movingBehaviourList = movingBehaviourList;
            this.findingBehaviourList = findingBehaviourList;
            this.combatBehaviourList = combatBehaviourList;
        }

        public void update(Enemy enemy, in List<ParentObject> objects)
        {
            if (isFindEnemy)
            {
                CombatSystem(enemy, objects);
            }
            else
            {
                MovingSystem(enemy, objects);
            }
            
            SearchSystem(enemy, objects);
            TrackingHealthSystem(enemy);
        }

        private void MovingSystem(Enemy enemy, List<ParentObject> objects)
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

                if (jump_seconds_now >= jump_seconds_max)
                {
                    isJump = false;
                    jump_seconds_now = 0;
                    lastMove = enemy.moveRightOrLeft(objects, jump_direction);
                }
            }
        }

        private void CombatSystem(Enemy enemy, List<ParentObject> objects)
        {
            if (!combatBehaviourList[combatBehaveList_on].isActiveUnit())
            {
                if (combatBehaviourList[combatBehaveList_on] != combatBehaviourList.Last())
                    combatBehaveList_on++;
                else
                {
                    combatBehaveList_on = 0;

                    foreach (var item in combatBehaviourList)
                        item.restartUnit();
                }
            }


            Entity closestEntity = CheckDistanceBetweenObjectsService.findClosestObjectToListOfObjects(enemy, found_enemies);
            double distanceToEntity = CheckDistanceBetweenObjectsService.FindDistanceBetweenTwoObjects(enemy, closestEntity);

            var action = combatBehaviourList[combatBehaveList_on].Update(distanceToEntity, enemy);

            if (action == ActionType.KeepMovingToEnemy)
            {
                bool isRight = closestEntity.coords.X > enemy.coords.X;
                int direction = isRight ? 1 : -1;
                enemy.moveRightOrLeft(objects, direction, true);
            }
            else
            if (action == ActionType.Hit)
            {
                enemy.Hit(objects);
            }
        }

        private void SearchSystem(Enemy enemy, List<ParentObject> objects)
        {
            if (!findingBehaviourList[findBehaveList_on].isActiveUnit())
            {
                if (findingBehaviourList[findBehaveList_on] != findingBehaviourList.Last())
                    findBehaveList_on++;
                else
                {
                    findBehaveList_on = 0;

                    foreach (var item in findingBehaviourList)
                        item.restartUnit();
                }
            }

            var list_of_founded_entities = findingBehaviourList[findBehaveList_on].Update(objects, enemy);

            if (list_of_founded_entities.Count == 0 || 
                (list_of_founded_entities.Count == 1 && list_of_founded_entities[0] == null))
            {
                if (isFindEnemy)
                {
                    found_seconds_now += (1.0 / Parameters.fps);

                    if (found_seconds_now >= found_seconds_max)
                    {
                        isFindEnemy = false;
                    }
                }
                else return;
            }
            else
            {
                isFindEnemy = true;
                found_seconds_now = 0;
                found_enemies = list_of_founded_entities;
            }
        }


        // Слежка за показателем здоровья

        private double? health;

        private void TrackingHealthSystem(Enemy enemy)
        {
            if (health != null)
            {
                if (enemy.health < health) 
                    findingBehaviourList[findBehaveList_on].activateForcedType();
            }

            health = enemy.health;
        }

    }
}
