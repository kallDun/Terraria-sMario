using System;
using System.Collections.Generic;
using System.Windows;
using Terraria_sMario.Classes.Logic.Objects;
using Terraria_sMario.Classes.Logic.Objects.Creatures;

namespace Terraria_sMario.Classes.Logic.Services
{
    static class CheckDistanceBetweenObjectsService
    {

        public static Entity findClosestObjectToListOfObjects(ParentObject ourObject, List<Entity> objects)
        {
            if (objects.Count == 0 || objects[0] == null) return null;

            Entity closestObject = objects[0];
            double? distanceToClosestObject = FindDistanceBetweenTwoObjects(ourObject, closestObject);

            foreach (var obj in objects)
            {
                if (obj == null) continue;

                if (FindDistanceBetweenTwoObjects(ourObject, obj) < distanceToClosestObject)
                {
                    distanceToClosestObject = FindDistanceBetweenTwoObjects(ourObject, obj);
                    closestObject = obj;
                }
            }
            return closestObject;
        }

        public static double? FindDistanceBetweenTwoObjects(ParentObject ourObject, ParentObject otherObject)
        {
            if (ourObject == null || otherObject == null) return null;
            return
            Math.Sqrt
            (
                Math.Pow((double) FindDistance_X_BetweenTwoObjects(ourObject, otherObject), 2) +
                Math.Pow((double) FindDistance_Y_BetweenTwoObjects(ourObject, otherObject), 2)
            );
        }

        public static double? FindDistance_Y_BetweenTwoObjects(ParentObject ourObject, ParentObject otherObject)
        {
            if (ourObject == null || otherObject == null) return null;

            var y_coords_gap = ourObject.coords.Y - otherObject.coords.Y;

            return Math.Abs(y_coords_gap);
        }

        public static double? FindDistance_X_BetweenTwoObjects(ParentObject ourObject, ParentObject otherObject)
        {
            if (ourObject == null || otherObject == null) return null;

            var x_coords_gap = ourObject.coords.X < otherObject.coords.X ?
                otherObject.coords.X - (ourObject.coords.X + ourObject.size.Width) :
                (otherObject.coords.X + otherObject.size.Width) - ourObject.coords.X;

            return Math.Abs(x_coords_gap);
        }

        public static float FindAngleBetweenTwoObjects(ParentObject ourObject, ParentObject otherObject)
        {
            var vect1 = new Vector(10, 0);
            var vect2 = new Vector(otherObject.coords.X - ourObject.coords.X, otherObject.coords.Y - ourObject.coords.Y - 25);

            double cos = (vect1.X * vect2.X + vect1.Y * vect2.Y) / 
                (Math.Sqrt(Math.Pow(vect1.X, 2) + Math.Pow(vect1.Y, 2)) 
                * Math.Sqrt(Math.Pow(vect2.X, 2) + Math.Pow(vect2.Y, 2)));

            var angle = (float)((float)Math.Acos(cos) * 180 / Math.PI);

            if (ourObject.coords.Y > otherObject.coords.Y)
                return 360 - angle;
            else return angle;
        }

    }
}
