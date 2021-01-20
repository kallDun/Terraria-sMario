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

        private List<SaveData_but> saveData_Buttons = new List<SaveData_but> { };

        public GameScreen()
        {
            var image_left = Management_res.But_arrow;
            var image_left_hovered = Management_res.But_arrow_hovered;
            image_left.RotateFlip(RotateFlipType.Rotate180FlipNone);
            image_left_hovered.RotateFlip(RotateFlipType.Rotate180FlipNone);

            buttons = new List<ButtonParent> {
                new Button_("Exit", Management_res.Exit, new Size(100, 40), new Point(135, 505)),

                new Button_("Right_", Management_res.But_arrow, Management_res.But_arrow_hovered, Management_res.But_arrow_hovered, 
                new Size(40, 40), new Point(500, 400)),

                new Button_("Left_", image_left, image_left_hovered, image_left_hovered, 
                new Size(40, 40), new Point(450, 400)),
            };

            PullSaves();
        }

        public void PullSaves()
        {
            saveData_Buttons.Add(new SaveData_but("SaveDataBut", new Point(60, 60), null));

            foreach (var save in Saves.game_Saves)
            {
                saveData_Buttons.Add(new SaveData_but("SaveDataBut", new Point(60, 60), save));
            }
        }


        public override void Draw(Graphics g)
        {
            DesignElementsStatic.FillScreenWithColor(g, Brushes.LightBlue);
            buttons.ForEach(x => x.Draw(g));
        }



        public override void MouseClick(MouseEventArgs e)
        {
            buttons.ForEach(x => x.MouseClick(e.X, e.Y));

            if (isButtonClicked("Exit"))
            {
                ScreenControl.ChangeScreen(new MenuScreen());
            }
        }

        public override void MouseMove(MouseEventArgs e) => buttons.ForEach(x => x.MouseOn(e.X, e.Y));



        public override void Update()
        {
        }

        public override void KeepMainPlayerInTheCenter()
        {
        }

        public override void checkField()
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
