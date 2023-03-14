using Microsoft.VisualStudio.TestTools.UnitTesting;
using SF2022User_NN_Lib;
using System;

namespace UnitTest
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void AvailablePeriods_Correctly()
        {
            string[] expected = new string[0];
            TimeSpan[] startTimes = new TimeSpan[2];
            int[] durations = new int[2];
            startTimes[0] = new TimeSpan(2,15,0);
            durations[0] = 15;
            startTimes[1] = new TimeSpan(3, 0, 0);
            durations[1] = 30;
            TimeSpan beginWorkingTime = new TimeSpan(1, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(4, 0, 0);
            int consultationTime = 30;
            string[] actual = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            Assert.AreEqual(actual, expected);
        }
    }
}
