using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CabFareCalculator.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CabFareCalculator.Core.Tests
{
    [TestClass]
    public class FareUnitTests
    {
        [TestMethod]
        public void TestCalculatePrice()
        {
            DateTime testDate = DateTime.Parse("10/08/2010");
            Fare testFare = new Fare(2, 5, testDate, 5, 30, "pm");

            // act
            double actual = testFare.CalculatePrice();

            // assert
            double expected = 9.75;
            Assert.AreEqual(expected, actual, 0.001, "Fare not calculated correctly");
        }

        [TestMethod]
        public void TestRoundUpToNearest()
        {
            // arrange
            double initial = 105.5;
            double roundTo = 1;

            double rountTo2 = 0.3;

            // act
            double actual = Util.RoundUpToNearest(initial, roundTo);
            double actual2 = Util.RoundUpToNearest(initial, rountTo2);

            // assert
            double expected = 106;
            double expected2 = 105.6;

            Assert.AreEqual(expected, actual, 0.001, "Rounding not performed correctly");
            Assert.AreEqual(expected2, actual2, 0.001, "Rounding not performed correctly");
        }

        [TestMethod]
        public void TestFormatAsMoney()
        {
            // arrange
            string initial = "6.3";
            string initial2 = "0.2";
            string initial3 = "5.52";
            
            // act
            string actual = Util.FormatAsMoney(initial);
            string actual2 = Util.FormatAsMoney(initial2);
            string actual3 = Util.FormatAsMoney(initial3);

            // assert
            string expected = "6.30";
            string expected2 = "0.20";
            string expected3 = "5.52";

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expected2, actual2);
            Assert.AreEqual(expected3, actual3);
        }
    }
}