using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.AI_Behavior;
using Terraria_sMario.Classes.Logic.Objects.Environment.Static_Blocks;
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
        private List<Entity> found_enemies = new List<Entity> { }; // Враги
        private List<Entity> found_ellies = new List<Entity> { }; // Союзники
        private LadderBlock near_ladder;
        private bool usingLadder = false;
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
            if (lastMove == 0) enemy.setAnimation(Features.EnemyAnimationTypes.Standing);

            if (isFindEnemy)
            {
                CombatSystem(enemy, objects);
                findingBehaviourList[findBehaveList_on].activateForcedType();
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
                EnemyJump(enemy, objects);
            }
        }

        private void EnemyJump(Enemy enemy, List<ParentObject> objects)
        {
            enemy.Jump(objects);
            jump_seconds_now += (1.0 / Parameters.fps);

            if (jump_seconds_now >= jump_seconds_max)
            {
                isJump = false;
                jump_seconds_now = 0;
                lastMove = enemy.moveRightOrLeft(objects, jump_direction);
            }
        }


        private void CombatSystem(Enemy enemy, List<ParentObject> objects)
        {
            if (!combatBehaviourList[combatBehaveList_on].isActiveUnit())
            {
                if (combatBehaviourList[combatBehaveList_on] != combatBehaviourList.Last())
                {
                    combatBehaveList_on++;
                } 
                else
                {
                    combatBehaveList_on = 0;

                    foreach (var item in combatBehaviourList)
                        item.restartUnit();
                }
            }

            int range = enemy.weaponInHand != null ? enemy.weaponInHand.actionRadius : enemy.damage_heal_ActionRadius;

            Entity closestEnemy = CheckDistanceBetweenObjectsService.findClosestObjectToListOfObjects(enemy, found_enemies);
            double? distanceToEnemy = CheckDistanceBetweenObjectsService.FindDistanceBetweenTwoObjects(enemy, closestEnemy);

            Entity closestElly = CheckDistanceBetweenObjectsService.findClosestObjectToListOfObjects(enemy, found_ellies);
            double? distanceToElly = CheckDistanceBetweenObjectsService.FindDistanceBetweenTwoObjects(enemy, closestElly);

            var action = combatBehaviourList[combatBehaveList_on].Update(distanceToEnemy, distanceToElly, enemy, range);

            if (action == ActionType.KeepMovingToEnemy ||
                action == ActionType.KeepMovingToElly ||
                action == ActionType.Retreat)
            {
                Entity needEntity = action == ActionType.KeepMovingToEnemy || action == ActionType.Retreat ?
                    closestEnemy : closestElly;

                var isLadder = (usingLadder && near_ladder != null ||
                    (near_ladder != null &&
                    enemy.coords.Y - needEntity.coords.Y >= 3 * Parameters.blockSize
                    && action != ActionType.Retreat));

                if (isJump)
                {
                    EnemyJump(enemy, objects);
                    CombatAttackUp(enemy, objects, action, needEntity);
                }
                else
                {
                    bool isRight = isLadder ?
                        near_ladder.coords.X + 7 > enemy.coords.X :
                        needEntity.coords.X > enemy.coords.X;
                        
                    int direction = isRight ? 1 : -1;

                    if (!isLadder && action == ActionType.Retreat)
                    {
                        direction = isRight ? -1 : 1; // Идет в обратную от персонажа сторону
                    }

                    if (lastMove == 0)
                    {
                        isJump = true;
                        jump_direction = direction;
                    }
                    else
                    {
                        lastMove = enemy.moveRightOrLeft(objects, direction,
                            run: (!isLadder && (action == ActionType.KeepMovingToEnemy || action == ActionType.Retreat)));

                        CombatAttackUp(enemy, objects, action, needEntity);
                    }

                    if (isLadder)
                    {
                        Predicate<ParentObject> predicate = delegate (ParentObject obj) { return obj is LadderBlock; };
                        var ladder = CheckNearObjectByPredicationService.getNearObject(objects, enemy, predicate);

                        if (ladder != null &&
                            (ladder.coords.X - enemy.coords.X) < -2 && (ladder.coords.X - enemy.coords.X) > -15)
                        {
                            enemy.Jump(objects);
                            usingLadder = action != ActionType.Retreat &&
                                (CheckDistanceBetweenObjectsService.FindDistance_Y_BetweenTwoObjects(enemy, needEntity) >= 5);
                        }
                    }
                }
            }
            else
            if (action == ActionType.Hit)
            {
                enemy.Hit(objects);
            }
            else
            if (action == ActionType.Heal)
            {
                enemy.Heal(objects);
            }
        }

        private static void CombatAttackUp(Enemy enemy, List<ParentObject> objects, ActionType action, Entity needEntity)
        {
            if (CheckDistanceBetweenObjectsService.FindDistance_X_BetweenTwoObjects(enemy, needEntity) <= needEntity.size.Width &&
                CheckDistanceBetweenObjectsService.FindDistance_Y_BetweenTwoObjects(enemy, needEntity) <= needEntity.size.Height)
            {
                if (action == ActionType.KeepMovingToEnemy || action == ActionType.Retreat) enemy.Hit(objects);
                else if (action == ActionType.KeepMovingToElly) enemy.Heal(objects);
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
            found_ellies = findingBehaviourList[findBehaveList_on].UpdateEllies(objects, enemy);
            near_ladder = findingBehaviourList[findBehaveList_on].UpdateLadder(objects, enemy);

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
                {
                    findingBehaviourList[findBehaveList_on].activateForcedType();

                    if (enemy.health <= enemy.maxHealth / 2)
                    {
                        var healing_combat = combatBehaviourList.Where(x => x.behaviorType == BehaviorTypes.Healing);

                        if (healing_combat.Count() > 0)
                        {
                            combatBehaveList_on = combatBehaviourList.IndexOf(healing_combat.First());

                            foreach (var item in combatBehaviourList)
                                item.restartUnit();
                        }

                    }
                }
            }

            health = enemy.health;
        }

    }
}
