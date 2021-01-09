using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Logic.Services;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.AI_Behavior
{
    class BehaviorUnitFinding : BehaviorUnitParent
    {
        private int radius;
        private bool searchEveryone;

        public BehaviorUnitFinding(BehaviorTypes behaviorType, bool searchEveryone, double duration, int radius = 5)
        {
            this.behaviorType = behaviorType;
            this.searchEveryone = searchEveryone;
            this.radius = radius * Parameters.blockSize;
            this.duration = duration;
            durationNow = duration;
        }

        public List<Entity> Update(List<ParentObject> objects, Entity thisEntity)
        {
            UpdateTimer();
            forcedTypeUpdateTimer();

            if (behaviorType == BehaviorTypes.SearchingInFrontOfView)
            {
                if (searchEveryone)
                    return CheckEntityService.searchAllEntities(objects, thisEntity, radius, false);
                else
                    return new List<Entity>
                    {
                        CheckEntityService.searchTheNearestEntity(objects, thisEntity, radius, false)
                    };     
            }
            else
            if (behaviorType == BehaviorTypes.SearchingEverywhere)
            {
                if (searchEveryone)
                    return CheckEntityService.searchAllEntities(objects, thisEntity, radius, true);
                else
                    return new List<Entity>
                    {
                        CheckEntityService.searchTheNearestEntity(objects, thisEntity, radius, true)
                    };   
            }

            return new List<Entity>{};
        }


        public List<Entity> UpdateEllies(List<ParentObject> objects, Enemy enemy) =>
            CheckEntityService.searchAllEntities(objects, enemy, 20 * Parameters.blockSize, isEverywhere: true, isEnemy: false);


        // Усиленный тип поиска в случае нанесения урона

        private double forcedType_seconds_Max = 5;
        private double forcedType_seconds_Now = 0;
        private bool isForcedTypeActive = false;

        BehaviorTypes last_behaviorType;
        private int last_radius;


        private void forcedTypeUpdateTimer()
        {
            if (isForcedTypeActive)
            {
                forcedType_seconds_Now += (1.0 / Parameters.fps);

                if (forcedType_seconds_Now >= forcedType_seconds_Max)
                {
                    disactivateForcedType();
                }
            }
        }

        private void disactivateForcedType()
        {
            behaviorType = last_behaviorType;
            radius = last_radius;
        }

        public void activateForcedType()
        {
            last_behaviorType = behaviorType;
            last_radius = radius;

            forcedType_seconds_Now = 0;
            durationNow = 0;
            behaviorType = BehaviorTypes.SearchingEverywhere;
            radius = 15;
        }

    }
}
