using System;
using System.Collections.Generic;
using System.Drawing;
using Terraria_sMario.Classes.Logic.Objects;
using Terraria_sMario.Classes.Logic.Objects.Creatures;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Items;

namespace Terraria_sMario.Classes.Logic.Services
{
    static class CheckNearObjectByPredicationService
    {

        public static ParentObject getNearObject(in List<ParentObject> objects, ParentObject ourObject, Predicate<ParentObject> predicate)
        {
            AbstractObject areaObject = new AbstractObject(ourObject.coords, ourObject.size);

            foreach (var obj in objects)
            {
                if (predicate(obj))
                {
                    if (areaObject.coords.X + areaObject.size.Width > obj.coords.X &&
                        areaObject.coords.X < obj.coords.X + obj.size.Width &&
                        areaObject.coords.Y + areaObject.size.Height > obj.coords.Y &&
                        areaObject.coords.Y < obj.coords.Y + obj.size.Height)
                    {
                        return obj;
                    }
                }
            }

            return null;
        }


    }
}
