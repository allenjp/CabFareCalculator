using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CabFareCalculator.Core
{
    // class to house general purpose methods
    public class Util
    {
        // method to round to nearest one-fifth; used for miBelow6mph param
        public static double RoundUpToNearest(Double passednumber, Double roundto)
        {
            //if no rounto then just pass original number back
            if (roundto == 0)
            {
                return passednumber;
            }
            else
            {
                return Math.Ceiling(passednumber / roundto) * roundto;
            }
        }

        // method to format string input as money:
        public static string FormatAsMoney(string input)
        {
            StringBuilder sb = new StringBuilder();
            string[] split = input.Split('.');

            if (split.Length == 1)
            {
                return input;
            }
            else
            {
                if (split[1].Length == 1)
                {
                    // indicates we need to add a trailing zero
                    sb.Append(input);
                    sb.Append("0");
                    return sb.ToString();
                }
                else
                {
                    return input;
                }
            }
        }
    }
}