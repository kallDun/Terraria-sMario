using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Terraria_sMario.Classes.Management.Interaction_Elements;

namespace Terraria_sMario.Classes.Management.Screens
{
    abstract class ScreenParent
    {
        public abstract void Update();

        public abstract void Draw(Graphics g);

        public abstract void KeepMainPlayerInTheCenter();

        public abstract void checkField();

        public abstract void KeyboardListenerKeyDown(KeyEventArgs e);

        public abstract void KeyboardListenerKeyUp(KeyEventArgs e);

        public abstract void MouseMove(MouseEventArgs e);

        public abstract void MouseClick(MouseEventArgs e);

        // Buttons

        protected List<Button_> buttons;

        protected bool isButtonClicked(string Name) => buttons.Where(x => x.Name == Name).First().isCLicked;
    }
}
