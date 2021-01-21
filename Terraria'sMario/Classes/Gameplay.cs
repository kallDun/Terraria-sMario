using System;
using System.Drawing;
using System.Windows.Forms;
using Terraria_sMario.Classes.Logic.Levels;
using Terraria_sMario.Classes.Save_System;

namespace Terraria_sMario.Classes
{
    class Gameplay
    {
        // Fields & Costrustor

        public Level activeLevel { get; private set; }

        public Gameplay(int levelNumber)
        {
            activeLevel = getActiveLevelByNumber(levelNumber);
        }

        // Level calls

        public Level getActiveLevelByNumber(int levelNumber)
        {
            switch (levelNumber)
            {
                case 1:
                    return new TestLevel_1(Saves.activeSaveData);

                default:
                    return null;
            }
        }

        // Threads

        public void Update() => activeLevel?.Update();

        public void Draw(Graphics g) => activeLevel?.Draw(g);

        public void KeepMainPlayerInTheCenter() => activeLevel?.KeepMainObjectInTheCenter();

        public void checkField() => activeLevel?.updateFieldOfView();

        public void KeyboardListenerKeyDown(KeyEventArgs e) => activeLevel?.KeyboardListenerPressed(e);

        public void KeyboardListenerKeyUp(KeyEventArgs e) => activeLevel?.KeyboardListenerReleased(e);
    }
}