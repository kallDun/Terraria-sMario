using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Terraria_sMario.Classes.Management.Design_Elements;
using Terraria_sMario.Classes.Management.Interaction_Elements;
using Terraria_sMario.Classes.Save_System;
using Terraria_sMario.Images;

namespace Terraria_sMario.Classes.Management.Screens
{
    class GameScreen : ScreenParent
    {
        Gameplay gameplay;

        public GameScreen()
        {
            buttons = new List<ButtonParent> {
                new Button_("Exit", Management_res.Exit, new Size(100, 40), new Point(135, 505))
            };
            FillScreenWithLevelButtons(new Point(135, 130));
        }
        // 1050 x 450

        private void FillScreenWithLevelButtons(Point position)
        {
            var levels_count = 30; // hardcode, need to rework !!!
            var inRow = 10;

            // button 75 x 75

            for (int i = 0; i < levels_count; i++)
            {
                var lvlBut = new LevelButton("Level_Button",
                        new Point(position.X + (i % inRow) * 100, position.Y + (i / inRow) * 100), i + 1);

                buttons.Add(lvlBut);
            }
        }

        public override void Draw(Graphics g)
        {
            if (gameplay == null)
            {
                DesignElementsStatic.FillScreenWithColor(g, Brushes.LightBlue);
                buttons.ForEach(x => x.Draw(g));
            }
            else gameplay?.Draw(g);
        }
        
        // Mouse Control

        public override void MouseClick(MouseEventArgs e)
        {
            if (gameplay != null) return;

            buttons.ForEach(x => x.MouseClick(e.X, e.Y));

            if (isButtonClicked("Exit"))
                ScreenControl.ChangeScreen(new ChooseSaveScreen());
            else if (isButtonClicked("Level_Button"))
            {
                var list = buttons.Where(x => x.isClicked && x.Name == "Level_Button").ToList();
                if (list.Count > 0) gameplay = new Gameplay((list.First() as LevelButton).number);
            }
        }

        public override void MouseMove(MouseEventArgs e) => buttons.ForEach(x => x.MouseOn(e.X, e.Y));


        public override void Update()
        {
            if (Saves.activeSaveData == null) ScreenControl.ChangeScreen(new ChooseSaveScreen());
            if (gameplay?.activeLevel == null) gameplay = null;
            gameplay?.Update();
        }

        // Only Gameplay's Voids

        public override void checkField() => gameplay?.checkField();

        public override void KeepMainPlayerInTheCenter() => gameplay?.KeepMainPlayerInTheCenter();

        public override void KeyboardListenerKeyDown(KeyEventArgs e) => gameplay?.KeyboardListenerKeyDown(e);

        public override void KeyboardListenerKeyUp(KeyEventArgs e) => gameplay?.KeyboardListenerKeyUp(e);
    }
}
