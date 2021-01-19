using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Terraria_sMario.Classes.Management.Design_Elements;
using Terraria_sMario.Classes.Management.Interaction_Elements;
using Terraria_sMario.Images;

namespace Terraria_sMario.Classes.Management.Screens
{
    class MenuScreen : ScreenParent
    {
        private Menu_Background menu_Background = new Menu_Background();

        public MenuScreen()
        {
            buttons = new List<Button_> {
                new Button_("New_game", Management_res.New_game, new Size(175, 40), new Point(135, 235)),
                new Button_("Continue", Management_res.Continue, new Size(175, 40), new Point(135, 325)),
                new Button_("Settings", Management_res.Settings, new Size(175, 40), new Point(135, 415)),
                new Button_("Exit", Management_res.Exit, new Size(100, 40), new Point(135, 505)),
            };
        }

        public override void Draw(Graphics g)
        {
            DesignElementsStatic.FillScreenWithColor(g, Brushes.LightBlue);
            menu_Background.Draw(g);
            buttons.ForEach(x => x.Draw(g));
        }

        

        public override void Update()
        {
            
        }

        public override void MouseMove(MouseEventArgs e) => buttons.ForEach(x => x.MouseOn(e.X, e.Y));

        public override void MouseClick(MouseEventArgs e)
        {
            buttons.ForEach(x => x.MouseClick(e.X, e.Y));

            if (isButtonClicked("New_game"))
            {
                ScreenControl.ChangeScreen(new GameScreen());
            }
            else if (isButtonClicked("Continue"))
            {
                ScreenControl.ChangeScreen(new GameScreen());
            }
            else if (isButtonClicked("Settings"))
            {
                ScreenControl.ChangeScreen(new OptionScreen());
            }
            else if (isButtonClicked("Exit"))
            {
                Application.Exit();
            }
        }


        // Useless in this class

        public override void checkField()
        {
        }

        public override void KeepMainPlayerInTheCenter()
        {
        }

        public override void KeyboardListenerKeyDown(KeyEventArgs e)
        {
        }

        public override void KeyboardListenerKeyUp(KeyEventArgs e)
        {
        }

    }
}
