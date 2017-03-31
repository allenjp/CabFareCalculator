using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CabFareCalculator.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CabFareCalculator.Core.Tests
{
    [TestClass]
    public class FareTests
    {
        [TestMethod]
        public void TestCalculatePrice()
        {
            // arrange based on unit test provided in project outline
            double testMiBelowSix = 2;
            int testMinAboveSix = 5;
            DateTime testDate = DateTime.Parse("10/08/2010");
            int testHour = 5;
            int testMin = 30;
            string testAmPm = "pm";

            // act
            double actual = FareController.CalculatePrice(testMiBelowSix, testMinAboveSix, testDate, testHour, testMin, testAmPm);

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
            double actual = FareController.RoundUpToNearest(initial, roundTo);
            double actual2 = FareController.RoundUpToNearest(initial, rountTo2);

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
            string actual = FareController.FormatAsMoney(initial);
            string actual2 = FareController.FormatAsMoney(initial2);
            string actual3 = FareController.FormatAsMoney(initial3);

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