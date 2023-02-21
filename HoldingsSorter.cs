//Created by Alexander Fields https://github.com/roku674

using StarportObjects;
using System.Collections.Generic;

namespace StarportHelperClasses
{
    public static class HoldingsSorter
    {
        /// <summary>
        /// Sorting algorithm to sort by x,y distance
        /// </summary>
        /// <param name="holdingsList"></param>
        /// <param name="origin"></param>
        /// <returns>List of Holdings</returns>
        public static List<Holding> SortByDistance(List<Holding> holdingsList, Holding origin)
        {
            List<Holding> output = new List<Holding>();
            output.Add(holdingsList[NearestPoint(origin, holdingsList)]);
            holdingsList.Remove(output[0]);
            int x = 0;
            for (int i = 0; i < holdingsList.Count + x; i++)
            {
                output.Add(holdingsList[NearestPoint(output[output.Count - 1], holdingsList)]);
                holdingsList.Remove(output[output.Count - 1]);
                x++;
            }
            return output;
        }

        private static int NearestPoint(Holding origin, List<Holding> lookIn)
        {
            KeyValuePair<double, int> smallestDistance = new KeyValuePair<double, int>();
            for (int i = 0; i < lookIn.Count; i++)
            {
                double distance = System.Math.Sqrt(System.Math.Pow(origin.GalaxyX - lookIn[i].GalaxyX, 2) + System.Math.Pow(origin.GalaxyY - lookIn[i].GalaxyY, 2));
                if (i == 0)
                {
                    smallestDistance = new KeyValuePair<double, int>(distance, i);
                }
                else
                {
                    if (distance < smallestDistance.Key)
                    {
                        smallestDistance = new KeyValuePair<double, int>(distance, i);
                    }
                }
            }
            return smallestDistance.Value;
        }
    }
}