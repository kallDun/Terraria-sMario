using System;
using System.Collections.Generic;
using Terraria_sMario.Classes.Logic.Objects;
using Terraria_sMario.Classes.Logic.Objects.Creatures;
using Terraria_sMario.Classes.Logic.Objects.Creatures.Enemies;

namespace Terraria_sMario.Classes.Logic.Services
{
    static class CheckDistanceBetweenObjectsService
    {

        public static Entity findClosestObjectToListOfObjects(ParentObject ourObject, List<Entity> objects)
        {
            Entity closestObject = objects[0];
            double distanceToClosestObject = FindDistanceBetweenTwoObjects(ourObject, closestObject);

            foreach (var obj in objects)
            {
                if (FindDistanceBetweenTwoObjects(ourObject, obj) < distanceToClosestObject)
                {
                    distanceToClosestObject = FindDistanceBetweenTwoObjects(ourObject, obj);
                    closestObject = obj;
                }
            }
            return closestObject;
        }

        public static double FindDistanceBetweenTwoObjects(ParentObject ourObject, ParentObject otherObject)
        {
            var x_coords_gap = ourObject.coords.X < otherObject.coords.X ?
                otherObject.coords.X - (ourObject.coords.X + ourObject.size.Width) :
                (otherObject.coords.X + otherObject.size.Width) - ourObject.coords.X;

            return
                Math.Sqrt
                (
                    Math.Pow(x_coords_gap, 2) +
                    Math.Pow(otherObject.coords.Y - ourObject.coords.Y, 2)
                );
        }

    }
}
