using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace CabFareCalculator.Core.Tests
{
    [TestClass]
    public class FareControllerIntegrationTest
    {
        [TestMethod]
        public void TestPostFare()
        {
            DateTime testDate = DateTime.Parse("10/08/2010");

            Fare intTestFare = new Fare(2, 5, testDate, 5, 30, "pm");

            HttpConfiguration config = new HttpConfiguration();
            HttpServer _server = new HttpServer(config);

            var client = new HttpClient(_server);
            client.BaseAddress = new Uri("http://localhost:54422");
            client.PostAsJsonAsync("api/fare/createFare", intTestFare)
                .ContinueWith((postTask) => postTask.Result.EnsureSuccessStatusCode());
        }
    }
}