using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Terraria_sMario.Classes.Logic.Objects;
using Terraria_sMario.Classes.Logic.Objects.Creatures;

namespace Terraria_sMario.Classes.Control
{
    class ControlKeyboard
    {
        public bool isWentRight_player1 { get; private set; } = false;
        public bool isPressedShift_player1 { get; private set; } = false;
        public bool isWentLeft_player1 { get; private set; } = false;
        public bool isJumped_player1 { get; private set; } = false;
        public bool isHitting_player1 { get; private set; } = false;
        


        public void KeyPress(KeyEventArgs e)
        {
            if (checkOnPressedLeft(e)) isWentLeft_player1 = true;
            if (checkOnPressedRight(e)) isWentRight_player1 = true;
            if (checkOnPressedSpace(e)) isJumped_player1 = true;
            if (checkOnPressedE(e)) isHitting_player1 = true;
            isPressedShift_player1 = checkOnPressedShift(e);
        }

        public void KeyUp(KeyEventArgs e)
        {
            if (checkOnPressedLeft(e)) isWentLeft_player1 = false;
            if (checkOnPressedRight(e)) isWentRight_player1 = false;
            if (checkOnPressedSpace(e)) isJumped_player1 = false;
            if (checkOnPressedE(e)) isHitting_player1 = false;
            isPressedShift_player1 = checkOnPressedShift(e);
        }

        public void updateMove(List<Player> players, in List<ParentObject> objects)
        {
            if (isWentRight_player1) 
                players[0].moveRightOrLeft(objects, 1, isPressedShift_player1);
            if (isWentLeft_player1)
                players[0].moveRightOrLeft(objects, -1, isPressedShift_player1);
            if (isJumped_player1)
                players[0].Jump();
            if (isHitting_player1)
                players[0].Hit(objects);

        }

        private bool checkOnPressedSpace(KeyEventArgs e)
        {
            return e.KeyValue == (char) Keys.Space;
        }
        private bool checkOnPressedRight(KeyEventArgs e)
        {
            return (e.KeyCode == Keys.Right) || (e.KeyCode == Keys.D);
        }
        private bool checkOnPressedLeft(KeyEventArgs e)
        {
            return (e.KeyCode == Keys.Left) || (e.KeyCode == Keys.A);
        }
        private bool checkOnPressedE(KeyEventArgs e)
        {
            return (e.KeyCode == Keys.E);
        }

        private bool checkOnPressedShift(KeyEventArgs e)
        {
            return (e.Shift);
        }
    }
}
