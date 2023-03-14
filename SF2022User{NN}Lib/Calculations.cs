using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SF2022User_NN_Lib
{
    public class Calculations
    {
        /// <summary>
        /// Метод, который возвращает список свободных временных интервалов в графике сотрудника
        /// </summary>
        /// <param name="startTimes"></param>
        /// <param name="durations"></param>
        /// <param name="beginWorkingTime"></param>
        /// <param name="endWorkingTime"></param>
        /// <param name="consultationTime"></param>
        /// <returns></returns>
        public static string[] AvailablePeriods(TimeSpan[] startTimes, int[] durations, TimeSpan beginWorkingTime, TimeSpan endWorkingTime, int consultationTime)
        {
            string[] periods = new string[0];
            TimeSpan[] listFreePeriods = new TimeSpan[0];
            TimeSpan time = beginWorkingTime;
            TimeSpan gap = new TimeSpan(0, consultationTime, 0);
            while(time < endWorkingTime)
            {
                TimeSpan timeSpan = getProverkaPeriod(time, time.Add(gap), startTimes, durations);
                if (timeSpan != new TimeSpan())
                {
                    TimeSpan timeSpanEnd = timeSpan.Add(-gap);
                    if (timeSpanEnd > beginWorkingTime)
                    {
                        Array.Resize(ref listFreePeriods, listFreePeriods.Length + 1);
                        listFreePeriods[listFreePeriods.Length - 1] = timeSpan.Add(-gap);
                    }
                }
                else
                {
                    Array.Resize(ref listFreePeriods, listFreePeriods.Length + 1);
                    listFreePeriods[listFreePeriods.Length - 1] =time;
                }
                time = time.Add(gap);
            }
            foreach(TimeSpan time1 in listFreePeriods)
            {
                Array.Resize(ref periods, periods.Length + 1);
                periods[periods.Length - 1] = "" + time1 + " - " + time1.Add(gap);
            }
            return periods;
        }

        public static TimeSpan getProverkaPeriod(TimeSpan timeStart, TimeSpan timeEnd, TimeSpan[] startTimes, int[] durations)
        {
            for(int i = 0; i < startTimes.Length; i++)
            {
                TimeSpan startSession = startTimes[i];
                TimeSpan timeAdd = new TimeSpan(0, durations[i], 0);
                TimeSpan endSession = startTimes[i].Add(timeAdd);
                if(timeStart < startSession.Add(new TimeSpan(0, 0, 1)))
                {
                    if(timeEnd >= endSession.Add(new TimeSpan(0, 0, 1)))
                    {
                        return startSession;
                    }
                }
            }
            return new TimeSpan();
        }
    }
}
