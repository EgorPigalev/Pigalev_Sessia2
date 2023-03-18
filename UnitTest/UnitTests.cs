using Microsoft.VisualStudio.TestTools.UnitTesting;
using SF2022User_NN_Lib;
using System;

namespace UnitTest
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod]
        public void AvailablePeriods_Correctly() // Проверка, что метод определяет корректное значениие
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

        [TestMethod]
        public void AvailablePeriods_NotCorrectly() // Проверка, что не корректное значение не является корректным
        {
            string[] expected = new string[5];
            expected[0] = "08:00-08:30";
            expected[1] = "08:30-09:00";
            expected[2] = "09:00-09:30";
            expected[3] = "09:30-10:00";
            expected[2] = "10:00-10:30";
            expected[3] = "10:30-11:00";
            expected[3] = "11:00-11:30";
            expected[4] = "11:30-12:00";
            TimeSpan[] startTimes = new TimeSpan[2];
            int[] durations = new int[2];
            startTimes[0] = new TimeSpan(9, 00, 0);
            durations[0] = 30;
            startTimes[1] = new TimeSpan(9, 30, 0);
            durations[1] = 30;
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(12, 0, 0);
            int consultationTime = 30;
            string[] actual = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            CollectionAssert.AreNotEqual(actual, expected);
        }

        [TestMethod]
        public void AvailablePeriods_NullStartTimesAndNotNullDurations() // Проверка, что если передаются неккоректные данные о занятых промежутков времени, то метод возвращает null
        {
            TimeSpan[] startTimes = new TimeSpan[0];
            int[] durations = new int[2];
            durations[0] = 30;
            durations[1] = 50;
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(12, 0, 0);
            int consultationTime = 30;
            string[] actual = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            Assert.IsNull(actual);
        }

        [TestMethod]
        public void AvailablePeriods_TwoDays() // Проверка, корректности работы если время работы дня сотрудника переходит с одного дня на другой
        {
            string[] expected = new string[4];
            expected[0] = "23:00-23:30";
            expected[1] = "23:30-00:00";
            expected[2] = "00:00-00:30";
            expected[3] = "00:30-01:00";
            TimeSpan[] startTimes = new TimeSpan[0];
            int[] durations = new int[0];
            TimeSpan beginWorkingTime = new TimeSpan(23, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(1, 0, 0);
            int consultationTime = 30;
            string[] actual = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            CollectionAssert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void AvailablePeriods_ConsultationTimeBig() // Проверка, при большом минимальном необходимом времени
        {
            string[] expected = new string[1];
            expected[0] = "11:30-14:00";
            TimeSpan[] startTimes = new TimeSpan[2];
            int[] durations = new int[2];
            startTimes[0] = new TimeSpan(10, 00, 0);
            durations[0] = 60;
            startTimes[1] = new TimeSpan(11, 0, 0);
            durations[1] = 30;
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(14, 0, 0);
            int consultationTime = 150;
            string[] actual = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            CollectionAssert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void AvailablePeriods_ConsultationTimeZero() // Проверка, при некоректном минимальном необходимом времени
        {
            TimeSpan[] startTimes = new TimeSpan[2];
            int[] durations = new int[2];
            startTimes[0] = new TimeSpan(10, 00, 0);
            durations[0] = 60;
            startTimes[1] = new TimeSpan(11, 0, 0);
            durations[1] = 30;
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(14, 0, 0);
            int consultationTime = 0;
            string[] actual = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            Assert.IsTrue(actual == null);
        }

        [TestMethod]
        public void AvailablePeriods_SmallPeriod() // Проверка, при небольшом интервале рабочего дня сотрудника, когда минимальное необходимое время меньше рабочего дня
        {
            string[] expected = new string[0];
            TimeSpan[] startTimes = new TimeSpan[0];
            int[] durations = new int[0];
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(8, 10, 0);
            int consultationTime = 20;
            string[] actual = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            CollectionAssert.AreEqual(actual, expected);
        }

        [TestMethod]
        public void AvailablePeriods_NotCorrectlyDurations() // Проверка, при некоректном времени в продолжительности занятых промежутков времени
        {
            TimeSpan[] startTimes = new TimeSpan[2];
            int[] durations = new int[2];
            startTimes[0] = new TimeSpan(10, 00, 0);
            durations[0] = -50;
            startTimes[1] = new TimeSpan(11, 0, 0);
            durations[1] = 30;
            TimeSpan beginWorkingTime = new TimeSpan(8, 0, 0);
            TimeSpan endWorkingTime = new TimeSpan(14, 0, 0);
            int consultationTime = 30;
            string[] actual = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            Assert.IsFalse(actual != null);
        }

        [TestMethod]
        public void AvailablePeriods_CorrectlyType() // Проверка, что метод возвращает данные с коректным типом
        {
            TimeSpan[] startTimes = new TimeSpan[5];
            int[] durations = new int[5];
            startTimes[0] = new TimeSpan(10, 00, 0);
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
            Assert.IsInstanceOfType(actual, typeof(string[]));
        }

        [TestMethod]
        public void AvailablePeriods_BusyBeginning() // Проверка, что метод возвращает правильный результат если список занятых промежутков времени в самом начале
        {
            string[] expected = new string[9];
            expected[0] = "13:30-14:00";
            expected[1] = "14:00-14:30";
            expected[2] = "14:30-15:00";
            expected[3] = "15:00-15:30";
            expected[4] = "15:30-16:00";
            expected[5] = "16:00-16:30";
            expected[6] = "16:30-17:00";
            expected[7] = "17:00-17:30";
            expected[8] = "17:30-18:00";
            TimeSpan[] startTimes = new TimeSpan[2];
            int[] durations = new int[2];
            startTimes[0] = new TimeSpan(12, 30, 0);
            durations[0] = 60;
            startTimes[1] = new TimeSpan(13, 0, 0);
            durations[1] = 30;
            TimeSpan beginWorkingTime = new TimeSpan(12, 30, 0);
            TimeSpan endWorkingTime = new TimeSpan(18, 0, 0);
            int consultationTime = 30;
            string[] actual = Calculations.AvailablePeriods(startTimes, durations, beginWorkingTime, endWorkingTime, consultationTime);
            CollectionAssert.AreEqual(actual, expected);
        }
    }
}
