using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Logic.DrawingElements;
using Terraria_sMario.Classes.Logic.Objects.Creatures;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Animations;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Animations.Effect_Animations;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Items;
using Terraria_sMario.Classes.Logic.Services;

namespace Terraria_sMario.Classes.Logic.Objects.Items.Bombs
{
    class Bomb : ParentItem
    {
        public Entity entity;
        public List<Effect> effects { get; protected set; } = new List<Effect> { };
        public int damage { get; protected set; } = 0;
        protected double timerMax;
        protected int radius;

        private bool isTimerStarted = false;
        private double timerNow = 0;

        private EffectAnimationControl environment__anim = new EffectAnimationControl();

        public override void Draw(Graphics g)
        {
            // Draw object
            base.Draw(g);

            // Draw timer
            if (isTimerStarted)
            {
                UI_Drawing_Static.DrawString(g,
                    new Point(coords.X + size.Width - 20, coords.Y - 10),
                    string.Format("{0 : 0.0}", timerMax - timerNow),
                    Brushes.Red, 16);
            }

            // Draw explosion animation
            environment__anim.Draw(g, coords);
        }

        public override void updateProperties(in List<ParentObject> objects)
        {
            base.updateProperties(objects);
            environment__anim.Update();

            if (isTimerStarted)
            {
                timerNow += (1.0 / Parameters.fps);
                if (timerNow >= timerMax)
                {
                    explode(objects);
                }
            }
        }

        private void explode(in List<ParentObject> objects)
        {
            var list = CheckEntityService.searchAllEntities(objects, entity, radius, true);

            foreach (var item in list)
            {
                item.getDamage(damage);

                foreach (var effect in effects)
                {
                    item.getEffect(new Effect(effect));
                }
            }

            isToDestroy = true;
            //environment__anim.Add(EffectAnimationTypes.Explosion);
        }

        public override void Use(in Entity entity)
        {
            var newCoord = new Point(coords.X, coords.Y);

            dropItem(newCoord);
            entity.newObjects.Add(this);
            isTimerStarted = true;
            canGrab = false;

            this.entity = entity;
            base.Use(entity);
        }

    }
}
