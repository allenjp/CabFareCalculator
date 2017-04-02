using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace CabFareCalculator.Core
{
    public class Fare
    {
        // fields
        public double milesBelow6;
        public int minsAbove6;
        public DateTime date;
        public int hour;
        public int min;
        public string ampm;

        // constructor
        public Fare(double miB6, int minA6, DateTime fareDate, int fareH, int fareM, string fareAmPm)
        {
            milesBelow6 = miB6;
            minsAbove6 = minA6;
            date = fareDate;
            hour = fareH;
            min = fareM;
            ampm = fareAmPm;
        }

        // methods
        public double CalculatePrice()
        {
            /*
            Taken from the project outline:

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
            double miBelow6mph_rounded = Util.RoundUpToNearest(milesBelow6, 0.2);

            // calculate the charge to be added ($0.35 for each 1/5th of a mile below 6mph)
            double miBelow6mph_charge = miBelow6mph_rounded * 5 * 0.35;

            fareCost += miBelow6mph_charge;

            // calculate cost for minutes traveling 6mph or more:
            double minAbove6mph_charge = minsAbove6 * 0.35;

            fareCost += minAbove6mph_charge;

            // see if we need to charge the night surcharge (8:01 PM - 5:59 AM):
            bool night_charge = false;

            if (hour == 8 && min > 0 && ampm == "pm")
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
            // M-F = 1-5
            bool weekday_surcharge = false;

            if (day_of_week >= 1 && day_of_week <= 5)
            {
                if (hour == 4 && min > 0 && ampm.ToLower() == "pm")
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
    }
}