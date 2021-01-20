using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Save_System;
using Terraria_sMario.Images;

namespace Terraria_sMario.Classes.Management.Interaction_Elements
{
    class SaveData_but : ButtonParent
    {
        // images & coords
        private Image back_image = Management_res.Border;
        private Image save_image = Management_res.Margin_in;
        private Image unknownMan_image = Management_res.UnknownMan;

        private Point save_image_coord = new Point(10, 10);


        // constructor
        private SaveData saveData;
        public SaveData_but(string Name, Point position, SaveData saveData) 
        {
            this.Name = Name;
            size = new Size(250, 250);
            this.position = position;
            this.saveData = saveData;
        }





        public override void Draw(Graphics g)
        {
            g.DrawImage(back_image, position);
           
            if (saveData != null)
            {
                g.DrawImage(save_image, position);
            }
            
        }


    }
}
