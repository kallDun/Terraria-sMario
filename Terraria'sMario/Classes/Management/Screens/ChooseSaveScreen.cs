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
    class ChooseSaveScreen : ScreenParent
    {

        private List<SaveData_but> saveData_Buttons = new List<SaveData_but> { };
        private int saveData_Buttons_on;

        public ChooseSaveScreen()
        {
            var image_left = Management_res.But_arrow;
            var image_left_hovered = Management_res.But_arrow_hovered;
            image_left.RotateFlip(RotateFlipType.Rotate180FlipNone);
            image_left_hovered.RotateFlip(RotateFlipType.Rotate180FlipNone);

            var saveDataButton_location = new Point(525, 200);

            buttons = new List<ButtonParent> {
                new Button_("Exit", Management_res.Exit, new Size(100, 40), new Point(135, 505)),

                new Button_("Right_", Management_res.But_arrow, Management_res.But_arrow_hovered, Management_res.But_arrow, 
                new Size(40, 40), new Point(saveDataButton_location.X + 250, saveDataButton_location.Y + 110)),

                new Button_("Left_", image_left, image_left_hovered, image_left, 
                new Size(40, 40), new Point(saveDataButton_location.X - 40, saveDataButton_location.Y + 110)),
            };

            PullSaves(saveDataButton_location);
        }

        public void PullSaves(Point location)
        {
            saveData_Buttons.Add(new SaveData_but("SaveDataBut_New", location, null));

            foreach (var save in Saves.game_Saves)
            {
                saveData_Buttons.Add(new SaveData_but("SaveDataBut_Choose", location, save));
            }


            saveData_Buttons_on = saveData_Buttons.Count - 1;
            buttons.Add(saveData_Buttons[saveData_Buttons_on]);
        }

        public void LeafSaves(int direction)
        {
            int saves_next = saveData_Buttons_on + direction;

            if (saves_next >= saveData_Buttons.Count) saves_next = 0;
            if (saves_next < 0) saves_next = saveData_Buttons.Count - 1;

            buttons.Remove(saveData_Buttons[saveData_Buttons_on]);
            saveData_Buttons_on = saves_next;
            buttons.Add(saveData_Buttons[saves_next]);
        }



        public override void MouseClick(MouseEventArgs e)
        {
            buttons.ForEach(x => x.MouseClick(e.X, e.Y));

            if (isButtonClicked("Exit"))
                ScreenControl.ChangeScreen(new MenuScreen());
            else if (isButtonClicked("Right_"))
                LeafSaves(1);
            else if (isButtonClicked("Left_"))
                LeafSaves(-1);
            else if (isButtonClicked("SaveDataBut_Choose"))
            {
                Saves.activeSaveData = saveData_Buttons[saveData_Buttons_on].saveData;
                ScreenControl.ChangeScreen(new GameScreen());
            }
            else if (isButtonClicked("SaveDataBut_New"))
            {
                ScreenControl.ChangeScreen(new CreateNewSaveScreen());
            }
        }

        public override void MouseMove(MouseEventArgs e) => buttons.ForEach(x => x.MouseOn(e.X, e.Y));


        public override void Draw(Graphics g)
        {
            DesignElementsStatic.FillScreenWithColor(g, Brushes.LightBlue);
            buttons.ForEach(x => x.Draw(g));
        }

        // Useless voids

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
