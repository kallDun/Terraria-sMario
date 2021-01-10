using System;
using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects;
using Terraria_sMario.Images;

namespace Terraria_sMario.Classes.Logic.DrawingElements
{
    static class UI_Drawing_Static
    {

        public static void DrawHealth(Graphics g, Point coord, float health, 
            float maxHealth, int heartsInRow, bool y_dir = true, bool isBig = false) // y_dir = true is up, false is down 
        {
            Image imageHealth_full = isBig ? UI.Health_full_big : UI.Health_full;
            Image imageHealth_withoutThird = isBig ? UI.Health_withoutThird_big : UI.Health_withoutThird;
            Image imageHealth_withoutHalf = isBig ? UI.Health_withoutHalf_big : UI.Health_withoutHalf;
            Image imageHealth_withLastThird = isBig ? UI.Health_withLastThird_big : UI.Health_withLastThird;
            Image imageHealth_empty = isBig ? UI.Health_empty_big : UI.Health_empty;
            int basicOffset = isBig ? 30 : 15;

            Point start_coord = coord;
            int count_of_hearts = 0;
            int y_offset = y_dir? -basicOffset : basicOffset;

            int Health = (int) Math.Round(health);
            int fullHeart = Health / 10;
            int restHeart = Health % 10;

            for (int i = 0; i < fullHeart; i++)
            {
                g.DrawImage(imageHealth_full, coord);
                coord.Offset(basicOffset, 0);
                count_of_hearts++;

                if (count_of_hearts % heartsInRow == 0)
                    coord = new Point(start_coord.X, start_coord.Y + (count_of_hearts / heartsInRow) * y_offset);
            }

            var image = restHeart >= 7 ? imageHealth_withoutThird :
                restHeart >= 5 ? imageHealth_withoutHalf :
                restHeart >= 3 ? imageHealth_withLastThird : null;
            
            if (image != null)
            {
                g.DrawImage(image, coord);
                coord.Offset(basicOffset, 0);
                count_of_hearts++;

                if (count_of_hearts % heartsInRow == 0)
                    coord = new Point(start_coord.X, start_coord.Y + (count_of_hearts / heartsInRow) * y_offset);
            }

            var count = (int)Math.Floor(maxHealth / 10.0) - count_of_hearts;
            for (int i = 0; i < count; i++)
            {
                g.DrawImage(imageHealth_empty, coord);
                coord.Offset(basicOffset, 0);
                count_of_hearts++;

                if (count_of_hearts % heartsInRow == 0)
                    coord = new Point(start_coord.X, start_coord.Y + (count_of_hearts / heartsInRow) * y_offset);
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

        public static void DrawString(Graphics g, Point coord, string str, Brush brush, float fontsize = 14, RectangleF? rect = null)
        {
            if (rect == null)
            {
                g.DrawString(
                str,
                new Font("Calibri", fontsize, FontStyle.Bold, GraphicsUnit.Point),
                brush, coord);
            }
            else
            {
                StringFormat stringFormat = new StringFormat();
                stringFormat.Alignment = StringAlignment.Center;
                stringFormat.LineAlignment = StringAlignment.Center;

                g.DrawString(
                str,
                new Font("Calibri", fontsize, FontStyle.Bold, GraphicsUnit.Point),
                brush, (RectangleF) rect, stringFormat);
            }
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
