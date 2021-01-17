using System;
using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.DrawingElements;
using Terraria_sMario.Classes.Logic.Objects.Creatures;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Animations.Effect_Animations;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Items;
using Terraria_sMario.Classes.Logic.Services;

namespace Terraria_sMario.Classes.Logic.Objects.Items.Bombs
{
    class Bomb : ParentItem
    {
        public List<Effect> effects { get; protected set; } = new List<Effect> { };
        public int damage { get; protected set; } = 0;
        public double timerMax { get; protected set; }
        public int radius { get; protected set; }

        public bool isTimerStarted { get; protected set; } = false;
        private bool isExploded = false;
        private double timerNow = 0;

        private EffectAnimationControl environment__anim = new EffectAnimationControl();

        public override void Draw(Graphics g)
        {
            // Draw object
            if (!isExploded) base.Draw(g);

            
            if (isTimerStarted)
            {
                // Draw timer
                UI_Drawing_Static.DrawString(g,
                    new Point(coords.X + size.Width - 10, coords.Y - 10),
                    string.Format("{0 : 0.0}", timerMax - timerNow),
                    Brushes.Red, 16);

                // Draw dashed red line
                float[] dashValues = { 3, 4, 3, 4 };
                Pen pen = new Pen(Color.Red, 3);
                pen.DashPattern = dashValues;

                g.DrawLine(pen, 
                    new Point(coords.X + size.Width / 2 - (radius + 10), coords.Y + size.Height), 
                    new Point(coords.X + size.Width / 2 + (radius + 10), coords.Y + size.Height));
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
            if (isExploded)
            {
                if (environment__anim.isEmpty()) 
                    isToDestroy = true;
            }
        }

        private void explode(in List<ParentObject> objects)
        {
            var list = CheckEntityService.searchAllEntities(objects, new AbstractEntity(coords, size), radius, true);

            foreach (var item in list)
            {
                item.getDamage(damage);

                foreach (var effect in effects)
                {
                    item.getEffect(new Effect(effect));
                }
            }

            environment__anim.Add(EffectAnimationTypes.Explosion);

            isTimerStarted = false;
            isExploded = true;
        }

        public override void Use(in Entity entity)
        {
            var newCoord = new Point(coords.X, coords.Y);
            var explosionBomb = new BombConstructor(this, true);
            explosionBomb.dropItem(newCoord);
            entity.newObjects.Add(explosionBomb);

            base.Use(entity);
        }

    }
}
