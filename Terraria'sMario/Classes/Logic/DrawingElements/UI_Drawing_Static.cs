using System;
using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects;
using Terraria_sMario.Images;

namespace Terraria_sMario.Classes.Logic.DrawingElements
{
    static class UI_Drawing_Static
    {

        public static void DrawHealth(Graphics g, Point coord, float health, float maxHealth)
        {
            int count_of_hearts = 0;

            int Health = (int) Math.Round(health);
            int fullHeart = Health / 10;
            int restHeart = Health % 10;

            for (int i = 0; i < fullHeart; i++)
            {
                g.DrawImage(UI.Health_full, coord);
                coord.Offset(15, 0);
                count_of_hearts++;
            }

            var image = restHeart >= 7 ? UI.Health_withoutThird :
                restHeart >= 5 ? UI.Health_withoutHalf :
                restHeart >= 3 ? UI.Health_withLastThird : null;
            
            if (image != null)
            {
                g.DrawImage(image, coord);
                coord.Offset(15, 0);
                count_of_hearts++;
            }  

            for (int i = 0; i < (int)Math.Floor(maxHealth / 10.0) - count_of_hearts; i++)
            {
                g.DrawImage(UI.Health_empty, coord);
                coord.Offset(15, 0);
            }
        }

        public static void DrawEffects(Graphics g, Point coord, List<Effect> effects)
        {
            for (int i = 0; i < effects.Count; i++)
            {
                var newPoint = new Point(coord.X, coord.Y + i * 22);

                g.DrawImage(getEffectImage(effects[i].effectType), newPoint);

                newPoint.Offset(6, 8);
                DrawString(
                    g, newPoint, 
                    effects[i].getIntDurability().ToString(), Brushes.DarkBlue, fontsize: 10);
            }
        }

        public static void DrawResistanceEffects(Graphics g, Point coord, List<EffectTypes> effects)
        {
            for (int i = 0; i < effects.Count; i++)
            {
                var newPoint = new Point(coord.X, coord.Y + i * 15);

                g.DrawImage(UI.Resistance, newPoint);

                newPoint.Offset(2, 2);
                g.DrawImage(getEffectImage(effects[i]), newPoint);
            }
        }

        public static void DrawDamage(Graphics g, Point coord, int damage)
        {
            g.DrawImage(UI.Damage, coord);

            coord.Offset(14, 0);
            DrawString(g, coord, damage.ToString(), Brushes.Red, 12);
        }

        public static void DrawString(Graphics g, Point coord, string str, Brush brush, float fontsize = 14)
        {
            g.DrawString(
                str,
                new Font("Calibri", fontsize, FontStyle.Bold),
                brush, coord);
        }

        private static Image getEffectImage(EffectTypes effect)
        {
            switch (effect)
            {
                case EffectTypes.Fire:
                    return UI.Fire;
                case EffectTypes.Ice:
                    return UI.Ice;
                case EffectTypes.Poisoning:
                    return UI.Poison;
                case EffectTypes.Stunning:
                    return UI.Stunning;
                case EffectTypes.Curse:
                    return UI.Curse;
                case EffectTypes.Blessing:
                    return UI.Bless;
                default:
                    return null;
            }
        }

    }
}
