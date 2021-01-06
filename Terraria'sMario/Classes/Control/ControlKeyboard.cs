using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Terraria_sMario.Classes.Control
{
    static class ControlKeyboard
    {
        public static bool checkOnPressedSpace(KeyEventArgs e)
        {
            return e.KeyCode == Keys.Space;
        }

        public static bool checkOnPressedRight(KeyEventArgs e)
        {
            return (e.KeyCode == Keys.Right) || (e.KeyCode == Keys.D);
        }
    }
}
