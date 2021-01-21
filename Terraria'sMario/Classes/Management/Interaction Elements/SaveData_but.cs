using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria_sMario.Classes.Logic.DrawingElements;
using Terraria_sMario.Classes.Save_System;
using Terraria_sMario.Images;

namespace Terraria_sMario.Classes.Management.Interaction_Elements
{
    class SaveData_but : ButtonParent
    {
        // images & coords
        private Image back_image = Management_res.Border;
        private Image back_image_hovered = Management_res.Border_Hovered;
        private Image save_image = Management_res.Margin_in;
        private Image unknownMan_image = Management_res.UnknownMan;

        private List<Point> unknownMans__coord = new List<Point> { new Point(60, 75), new Point(140, 75) };
        private Point save_name__coord = new Point(10, 25);


        // constructor
        public SaveData saveData { get; private set; }
        public SaveData_but(string Name, Point position, SaveData saveData) 
        {
            this.Name = Name;
            size = new Size(250, 250);
            this.position = position;
            this.saveData = saveData;
        }



        public override void Draw(Graphics g)
        {
            g.DrawImage(isHovered? back_image_hovered : back_image, position);

            g.DrawImage(save_image, position);            
            
            UI_Drawing_Static.DrawString(g, 
                saveData == null ? "Create New Game" : saveData.DataName,
                saveData == null ? Brushes.SlateGray : Brushes.Black, 
                17,
                new RectangleF(new Point(position.X + save_name__coord.X, position.Y + save_name__coord.Y),
                new Size(230, 35)));


            if (saveData == null)
            {
                g.DrawImage(unknownMan_image, new Point(position.X + unknownMans__coord[0].X, position.Y + unknownMans__coord[0].Y));
                g.DrawImage(unknownMan_image, new Point(position.X + unknownMans__coord[1].X, position.Y + unknownMans__coord[1].Y));
            }
            else
            {
                var img_list = saveData.getPlayerImages();
                var name_list = saveData.getPlayerNames();
                var lvl_list = saveData.getPlayerLevels();

                if (img_list != null) 
                {
                    for (int i = 0; i < img_list.Count; i++)
                    {
                        var coord = new Point(position.X + unknownMans__coord[i].X, position.Y + unknownMans__coord[i].Y);
                        g.DrawImage(img_list[i], coord);
                        coord.Offset(0, img_list[i].Height);
                        UI_Drawing_Static.DrawString(g, coord, $"{name_list[i]}", Brushes.DarkBlue, 13);
                        coord.Offset(0, 20);
                        UI_Drawing_Static.DrawString(g, coord, $"lvl: {lvl_list[i]}", Brushes.DarkOrange, 12);
                    }
                }


            }
            
        }
        

    }
}
