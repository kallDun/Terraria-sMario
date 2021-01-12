using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects;
using Terraria_sMario.Classes.Logic.Objects.Creatures;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Items;

namespace Terraria_sMario.Classes.Logic.Services
{
    static class CheckItemService
    {
        private const int search_X_Area = 10;

        public static ParentItem getNearItem(in List<ParentObject> objects, Entity ourObject)
        {
            var coords = ourObject.coords;
            var size = new Size(ourObject.size.Width + search_X_Area, ourObject.size.Height);
            if (!ourObject.isTurnToRight) coords.Offset(0, -search_X_Area);

            AbstractObject areaObject = new AbstractObject(coords, size);

            foreach (var obj in objects)
            {
                if (obj is ParentItem)
                {
                    if (areaObject.coords.X + areaObject.size.Width > obj.coords.X &&
                        areaObject.coords.X < obj.coords.X + obj.size.Width &&
                        areaObject.coords.Y + areaObject.size.Height > obj.coords.Y &&
                        areaObject.coords.Y < obj.coords.Y + obj.size.Height)
                    {
                        return obj as ParentItem;
                    }
                }
            }

            return null;
        }


    }
}
