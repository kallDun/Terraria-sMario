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

            foreach (var obj in objects)
            {
                if (predicate(obj))
                {
                    if (ourObject.coords.X + ourObject.size.Width > obj.coords.X &&
                        ourObject.coords.X < obj.coords.X + obj.size.Width &&
                        ourObject.coords.Y + ourObject.size.Height > obj.coords.Y &&
                        ourObject.coords.Y < obj.coords.Y + obj.size.Height)
                    {
                        return obj;
                    }
                }
            }

            return null;
        }

        public static List<ParentObject> getNearObjects(in List<ParentObject> objects, ParentObject ourObject, Predicate<ParentObject> predicate)
        {
            var list = new List<ParentObject> { };

            foreach (var obj in objects)
            {
                if (predicate(obj))
                {
                    if (ourObject.coords.X + ourObject.size.Width > obj.coords.X &&
                        ourObject.coords.X < obj.coords.X + obj.size.Width &&
                        ourObject.coords.Y + ourObject.size.Height > obj.coords.Y &&
                        ourObject.coords.Y < obj.coords.Y + obj.size.Height)
                    {
                        list.Add(obj);
                    }
                }
            }

            return list;
        }
    }
}
