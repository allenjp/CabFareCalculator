using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        /*
         * If we were using a DB
         * We could add an [HttpGet] route to get all saved fares from the DB
         * And display them to the user
         * 
         * We could also add a delete api route to remove fares from the DB if we wish
         * Similarly edit a saved fare
         */

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

                // Perform price calculations here:

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

                // continue parsing other input values:
                int fareHour = int.Parse(postData.hour.ToString());
                int fareMin = int.Parse(postData.min.ToString());
                string fareAmPm = postData.ampm.ToString();

                // create a new instance of the Fare class using these values:
                Fare fareFromPost = new Fare(fareMiBelow6mph, fareMinAbove6mph, fareDate, fareHour, fareMin, fareAmPm);

                // calculate the price:
                double fareCost = fareFromPost.CalculatePrice();
                string fareCost_string = fareCost.ToString();

                // format fareCost to money form:
                string fareCost_formatted = Util.FormatAsMoney(fareCost_string);

                // put fareCost back into the result and return it
                // we do this 
                result = postData;
                result.cost = fareCost_formatted;

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}