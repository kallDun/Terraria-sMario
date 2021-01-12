
using System;
using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.DrawingElements;
using Terraria_sMario.Classes.Logic.Objects.Creatures;
using Terraria_sMario.Classes.Logic.Services;
using static Terraria_sMario.Images.UI;

namespace Terraria_sMario.Classes.Logic.Objects.Features
{
    class UI_Entity_Draw
    {
        private UI_Entity_Draw_Type type;
        private Point name_coord;
        private Point health_coord;
        private Point effects_coord;
        private Point resistEffects_coord;
        private Point damage_coord;

        private string name;
        private float health;
        private float maxHealth;
        private List<Effect> effects;
        private List<EffectTypes> resistancesEffects;

        private float damage;
        private double damageTimer = 0;
        private double damageTimerMax = 2; // duration + 1 (in seconds) of showing damage 

        public UI_Entity_Draw(in Entity entity, UI_Entity_Draw_Type type)
        {
            updateEntityDraw(entity);
            this.type = type;
            this.name = entity.Name;
            this.maxHealth = entity.maxHealth;
        }

        public void updateDamageTimer()
        {
            if (damageTimer > 0)
            {
                damageTimer -= (1.0 / Parameters.fps);
            }
        }

        public void updateEntityDraw(in Entity entity)
        {
            health = entity.health;
            effects = entity.effects;
            resistancesEffects = entity.resistancesEffects;
            updateCoords(entity.coords, entity.size);
        }

        public void updateCoords(Point coords, Size size)
        {
            if (type == UI_Entity_Draw_Type.WithoutName || 
                type == UI_Entity_Draw_Type.WithoutNameAndHP)
            {
                health_coord = new Point(coords.X - 10, coords.Y - 15);
            }
            else
            {
                health_coord = new Point(coords.X - 10, coords.Y - 32);
            }
            
            name_coord = new Point(coords.X - 10, coords.Y - 22);
            effects_coord = new Point(coords.X + size.Width, coords.Y + 20);
            resistEffects_coord = new Point(coords.X - 15, coords.Y + 0);
            damage_coord = new Point(coords.X + size.Width, coords.Y);
        }

        public void gettingDamage(float damage)
        {
            damageTimer = damageTimerMax;
            this.damage = damage;
        }

        public void Draw(Graphics g, in Entity entity)
        {
            updateEntityDraw(entity);

            switch (type)
            {
                case UI_Entity_Draw_Type.All:
                    DrawName(g);
                    DrawHealth(g);
                    DrawEffects(g);
                    DrawDamage(g);
                    DrawResistEffects(g);
                    break;

                case UI_Entity_Draw_Type.WithoutName:
                    DrawHealth(g);
                    DrawEffects(g);
                    DrawDamage(g);
                    DrawResistEffects(g);
                    break;

                case UI_Entity_Draw_Type.WithoutNameAndHP:
                    DrawEffects(g);
                    DrawDamage(g);
                    DrawResistEffects(g);
                    break;

                case UI_Entity_Draw_Type.WithoutHP:
                    DrawName(g);
                    DrawEffects(g);
                    DrawDamage(g);
                    DrawResistEffects(g);
                    break;

                case UI_Entity_Draw_Type.WithoutResistance:
                    DrawName(g);
                    DrawHealth(g);
                    DrawEffects(g);
                    DrawDamage(g);
                    break;

                case UI_Entity_Draw_Type.WithoutGettingDamage:
                    DrawName(g);
                    DrawHealth(g);
                    DrawEffects(g);
                    DrawResistEffects(g);
                    break;

                case UI_Entity_Draw_Type.OnlyNameAndDamage:
                    DrawName(g);
                    DrawDamage(g);
                    break;
            }
        }

        private void DrawName(Graphics g) => UI_Drawing_Static.DrawString(g, name_coord, name, Brushes.Black);

        private void DrawHealth(Graphics g) => UI_Drawing_Static.DrawHealth(g, health_coord, health, maxHealth, 5);

        private void DrawEffects(Graphics g) => UI_Drawing_Static.DrawEffects(g, effects_coord, effects, false, 22);

        private void DrawResistEffects(Graphics g) => UI_Drawing_Static.DrawResistanceEffects(g, resistEffects_coord, resistancesEffects);
        
        private void DrawDamage(Graphics g)
        {
            var Damage = (int) Math.Round(damage);

            if (Damage > 0 && damageTimer > 0)
            {
                UI_Drawing_Static.DrawDamage(g, damage_coord, Damage);
            }
        }
    }
}
