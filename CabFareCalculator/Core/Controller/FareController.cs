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
        // GET api/<controller>
        [HttpGet]
        public int Get()
        {
            return 1;
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post(dynamic data)
        {
            Console.Write(data);
        }
    }
}