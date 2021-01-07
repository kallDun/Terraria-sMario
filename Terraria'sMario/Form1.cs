using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Terraria_sMario.Classes;

namespace Terraria_sMario
{
    public partial class Form1 : Form
    {
        public Form1()
        { 
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
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
            gameplay.Draw(g);
            pictureBox1.Refresh();
        }

        private void update_Tick(object sender, EventArgs e) => gameplay.Update();

        private void checkField_Tick(object sender, EventArgs e) => gameplay.checkField();

        private void checkCamera_Tick(object sender, EventArgs e) => gameplay.KeepMainPlayerInTheCenter();

        // KEYBOARD LISTENER

        void Form1_KeyDown(object sender, KeyEventArgs e) => gameplay.KeyboardListenerKeyDown(e);

        private void Form1_KeyUp(object sender, KeyEventArgs e) => gameplay.KeyboardListenerKeyUp(e);
    }
}
