using System;
using System.Drawing;
using System.Windows.Forms;
using Terraria_sMario.Classes;
using Terraria_sMario.Classes.Management.Screens;

namespace Terraria_sMario
{
    public partial class Form1 : Form
    {
        public Form1()
        { 
            InitializeComponent();
            KeyDown += (s, e) => gameplay.KeyboardListenerKeyDown(e);
            KeyDown += (s, e) => ScreenControl.Screen.KeyboardListenerKeyDown(e);
        }

        private Gameplay gameplay;
        private Graphics g;
        
        private void Form1_Load(object sender, EventArgs e)
        {
            gameplay = new Gameplay();
            update.Start();
            draw.Start();
            checkCamera.Start();
            checkField.Start();
        }

        private void draw_Tick(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(Size.Width, Size.Height);
            g = Graphics.FromImage(pictureBox1.Image);
            ScreenControl.Screen.Draw(g);
            gameplay.Draw(g);
            pictureBox1.Refresh();
        }

        private void update_Tick(object sender, EventArgs e) 
        { 
            gameplay.Update();
            ScreenControl.Screen.Update();
        }

        private void checkField_Tick(object sender, EventArgs e) 
        { 
            gameplay.checkField();
            ScreenControl.Screen.checkField();
        }

        private void checkCamera_Tick(object sender, EventArgs e) 
        { 
            gameplay.KeepMainPlayerInTheCenter();
            ScreenControl.Screen.KeepMainPlayerInTheCenter();
        }

        // KEYBOARD LISTENER

        private void Form1_KeyUp(object sender, KeyEventArgs e) 
        { 
            gameplay.KeyboardListenerKeyUp(e);
            ScreenControl.Screen.KeyboardListenerKeyUp(e);
        }
    }
}
