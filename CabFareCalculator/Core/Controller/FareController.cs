using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace CabFareCalculator.Core
{
    public class FareController : ApiController
    {
        // POST api route
        [HttpPost]
        public dynamic createFare(dynamic postData)
        {
            dynamic result = new System.Dynamic.ExpandoObject();

            try
            {
                /*
                 * Can send the data to a db here if we wish
                 */ 

                /*
                 * Perform price calculations here
                 */

                // parse all our info from postData into the correct formats:
                double fareMiBelow6mph = double.Parse(postData.milesBelow6.ToString());
                int fareMinAbove6mph = int.Parse(postData.minsAbove6.ToString());

                // validating date here in backend:
                DateTime fareDate;
                string dateString = postData.date.ToString();

                if (DateTime.TryParse(dateString, out fareDate))
                {
                    Console.Write("Date is OK");
                }
                else
                {
                    result.status = 0;
                    result.message = "Date is not in correct format (MM/DD/YYYY)";
                    return result;
                }

                // DateTime fareDate = DateTime.Parse(postData.date.ToString());
                int fareHour = int.Parse(postData.hour.ToString());
                int fareMin = int.Parse(postData.min.ToString());
                string fareAmPm = postData.ampm.ToString();

                double fareCost = CalculatePrice(fareMiBelow6mph, fareMinAbove6mph, fareDate, fareHour, fareMin, fareAmPm);
                string fareCost_string = fareCost.ToString();

                // format fareCost to money form:
                string fareCost_formatted = FormatAsMoney(fareCost_string);

                // put fareCost back into the result and return it
                result = postData;
                result.cost = fareCost_formatted;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public double CalculatePrice(double miBelow6mph, int minAbove6mph, DateTime date, int hour, int minute, string ampm)
        {
            /*
            The meter is required to be engaged or "hired" when a taxicab is occupied by anyone in addition to the driver
            $3.00 upon entry
            $0.35 for each additional unit
          
            The unit fare is:
            •	one-fifth of a mile, when the taxicab is traveling at less than 6 miles an hour
            •	60 seconds when not in motion or traveling at 6 miles per hour or more.
            •	Night surcharge of $.50 after 8:00 PM & before 6:00 AM
            •	Peak hour Weekday Surcharge of $1.00 Monday - Friday after 4:00 PM & before 8:00 PM
            •	New York State Tax Surcharge of $.50 per ride.
            •	All examples/problems will have distance in units of 1/5 of a mile and in minutes - there will be no fractional units)
            */

            // initialize price at $3.50 since that is the entry charge + NY tax
            double fareCost = 3.50;

            // first make sure our miBelow6mph param is rounded to nearest one-fifth:
            double miBelow6mph_rounded = RoundUpToNearest(miBelow6mph, 0.2);

            // calculate the charge ($0.35 for each 1/5th of a mile)
            double miBelow6mph_charge = miBelow6mph_rounded * 5 * 0.35;

            fareCost += miBelow6mph_charge;

            // calculate cost for minutes traveling 6mph or more:
            double minAbove6mph_charge = minAbove6mph * 0.35;

            fareCost += minAbove6mph_charge;

            // see if we need to charge the night surcharge (8:01 PM - 5:59 AM):
            bool night_charge = false;

            if (hour == 8 && minute > 0 && ampm == "pm") 
            {
                night_charge = true;
            }
            else if (hour >= 9 && hour <= 11 && ampm == "pm") 
            {
                night_charge = true;
            }
            else if (hour >= 12 && hour <= 5 && ampm == "am") 
            {
                night_charge = true;
            }

            if (night_charge)
            {
                fareCost += 0.5;
            }

            // next figure out what day of week ride took place:
            int day_of_week = (int) date.DayOfWeek;

            // we have the day of the week in number form
            // surcharge is M-F 4:01-7:59 PM
            bool weekday_surcharge = false;

            if (day_of_week >= 1 && day_of_week <= 5) 
            {
                if (hour == 4 && minute > 0 && ampm.ToLower() == "pm") 
                {
                    weekday_surcharge = true;
                }
                else if (hour >= 5 && hour <= 7 && ampm.ToLower() == "pm")
                {
                    weekday_surcharge = true;
                }
            }

            if (weekday_surcharge) 
            {
                fareCost += 1;
            }

            return fareCost;

        }

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