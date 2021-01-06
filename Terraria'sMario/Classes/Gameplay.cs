using System;
using System.Drawing;
using System.Windows.Forms;
using Terraria_sMario.Classes.Logic.Levels;

namespace Terraria_sMario.Classes
{
    class Gameplay
    {
        private Level activeLevel;

        public Gameplay()
        {
            activeLevel = new TestLevel_1 ();
        }

        public void Update() => activeLevel.Update();

        public void Draw(Graphics g) => activeLevel.Draw(g);

        public void checkCollision() => activeLevel.CheckCollision();

        public void checkField() => activeLevel.updateFieldOfView();

        public void KeyboardListener(KeyEventArgs e) => activeLevel.KeyboardListener(e);
    }
}