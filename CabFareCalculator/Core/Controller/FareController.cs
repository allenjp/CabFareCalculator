using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

            /*
             * Can send the data to a db here if we wish
             */ 

            /*
             * Perform price calculations here
             */

            result = postData;

            return result;
        }
    }
}