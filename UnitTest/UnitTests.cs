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
            string[] expected = new string[14];
            expected[0] = "08:00-08:30";
            expected[1] = "08:30-09:00";
            expected[2] = "09:00-09:30";
            expected[3] = "09:30-10:00";
            expected[4] = "11:30-12:00";
            expected[5] = "12:00-12:30";
            expected[6] = "12:30-13:00";
            expected[7] = "13:00-13:30";
            expected[8] = "13:30-14:00";
            expected[9] = "14:00-14:30";
            expected[10] = "14:30-15:00";
            expected[11] = "15:40-16:10";
            expected[12] = "16:10-16:40";
            expected[13] = "17:30-18:00";
            TimeSpan[] startTimes = new TimeSpan[5];
            int[] durations = new int[5];
            startTimes[0] = new TimeSpan(10,00,0);
            durations[0] = 60;
            startTimes[1] = new TimeSpan(11, 0, 0);
            durations[1] = 30;
            startTimes[2] = new TimeSpan(15, 0, 0);
            durations[2] = 10;
            startTimes[3] = new TimeSpan(15, 30, 0);
            durations[3] = 10;
            startTimes[4] = new TimeSpan(16, 50, 0);
            durations[4] = 40;
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            string[] actual = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            CollectionAssert.AreEqual(actual, expected);
        }
    }
}
