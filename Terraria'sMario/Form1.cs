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
        public Form1() => InitializeComponent();

        private Gameplay gameplay;
        private Graphics g;

        private void Form1_Load(object sender, EventArgs e)
        {
            gameplay = new Gameplay();
            update.Start();
            draw.Start();
            collision.Start();
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

        private void collision_Tick(object sender, EventArgs e) => gameplay.checkCollision();

        private void checkField_Tick(object sender, EventArgs e) => gameplay.checkField();
    }
}
