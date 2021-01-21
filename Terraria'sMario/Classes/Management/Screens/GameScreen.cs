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

        List<LevelButton> levelButtons = new List<LevelButton> { };

        public GameScreen()
        {
            buttons = new List<ButtonParent> {
                new Button_("Exit", Management_res.Exit, new Size(100, 40), new Point(135, 505))
            };
        }


        public override void Draw(Graphics g)
        {
            DesignElementsStatic.FillScreenWithColor(g, Brushes.LightBlue);
            buttons.ForEach(x => x.Draw(g));

            gameplay?.Draw(g);
        }
        
        // Mouse Control

        public override void MouseClick(MouseEventArgs e)
        {
            buttons.ForEach(x => x.MouseClick(e.X, e.Y));

            if (isButtonClicked("Exit"))
                ScreenControl.ChangeScreen(new ChooseSaveScreen());
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
