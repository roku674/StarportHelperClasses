//Created by Alexander Fields https://github.com/roku674

using System.Linq;

namespace StarportHelperClasses
{
    /// <summary>
    /// I'm Helping!
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Finds out if the planet type is zoundsable
        /// </summary>
        /// <param name="planetType"></param>
        /// <param name="research"></param>
        /// <returns>true/false</returns>
        public static bool IsZoundsable(string planetType, string research)
        {
            string[] arch2Up = new string[] { "Arch lvl 2", "Arch lvl 3", "Arch lvl 4", "Arch lvl 5" };
            string[] arch3Up = new string[] { "Arch lvl 3", "Arch lvl 4", "Arch lvl 5" };
            string[] arch4Up = new string[] { "Arch lvl 4", "Arch lvl 5" };

            if (planetType == "arctic" && arch2Up.Any(s => research.Contains(s)))
            {
                return true;
            }
            else if ((planetType == "rocky" || planetType == "greenhouse" || planetType == "Intergalactic paradise") && arch3Up.Any(s => research.Contains(s)))
            {
                return true;
            }
            else if ((planetType == "earthlike" || planetType == "volcanic" || planetType == "oceanic") && arch4Up.Any(s => research.Contains(s)))
            {
                return true;
            }
            else if ((planetType == "mountainous" || planetType == "desert") && research.Contains("Arch lvl 5"))
            {
                return true;
            }

            return false;
        }
    }
}