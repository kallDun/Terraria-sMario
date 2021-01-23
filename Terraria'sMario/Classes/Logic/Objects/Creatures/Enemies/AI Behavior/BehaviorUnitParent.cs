using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies.AI_Behavior
{
    abstract class BehaviorUnitParent
    {
        public BehaviorTypes behaviorType { get; protected set; }
        public double duration { get; protected set; } // duration in seconds
        protected double durationNow;

        protected void UpdateTimer() => durationNow -= (1.0 / Parameters.fps);

        public void restartUnit() => durationNow = duration;

        public void turnOffUnit() => durationNow = 0;

        public bool isActiveUnit() => (durationNow > 0);
    }
}