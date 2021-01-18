using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terraria_sMario.Classes.Management.Screens
{
    static class ScreenControl
    {
        public static ScreenParent Screen { get; private set; } = new MenuScreen();

        public static void ChangeScreen(ScreenParent Screen) => ScreenControl.Screen = Screen;
    }
}
