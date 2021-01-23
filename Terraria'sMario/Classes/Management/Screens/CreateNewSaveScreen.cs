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
    class CreateNewSaveScreen : ScreenParent
    {

        // constructor
        public CreateNewSaveScreen()
        {
            buttons = new List<ButtonParent> {
                new Button_("Exit", Management_res.Exit, new Size(100, 40), new Point(135, 505)),
            };

        }

        public override void Draw(Graphics g)
        {
            DesignElementsStatic.FillScreenWithColor(g, Brushes.LightBlue);
            buttons.ForEach(x => x.Draw(g));
        }

        public override void Update()
        {

        }

        // mouse
        public override void MouseClick(MouseEventArgs e)
        {
            buttons.ForEach(x => x.MouseClick(e.X, e.Y));

            if (isButtonClicked("Exit"))
            {
                ScreenControl.ChangeScreen(new MenuScreen());
            }
        }

        public override void MouseMove(MouseEventArgs e) => buttons.ForEach(x => x.MouseOn(e.X, e.Y));


        // useless voids
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
