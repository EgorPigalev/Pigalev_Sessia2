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
            TimeSpan[] listFreePeriods = new TimeSpan[0]; // Массив свободных периодов с указанием даты начала
            TimeSpan time = beginWorkingTime; // Время, которое изменяется
            TimeSpan gap = new TimeSpan(0, consultationTime, 0); // Минимальное необходимое время для работы менеджера
            while (time < endWorkingTime) // Пока время не достигло конца рабочего дня
            {
                int Index = getProverkaPeriod(time, time.Add(gap), startTimes, durations); // индекс элемента ближайшего времеми, которое занято если оно входит в предпологаемый занимаемый период
                if (Index != -1) // Если есть время, которое входит в занимаемый период
                {
                    TimeSpan timeSpanStart = startTimes[Index].Add(-gap); // Время начала, которое необходимо, чтобы не перекрыть занятое время
                    if (!getProverkaFreeTime(timeSpanStart, listFreePeriods, gap)) // Если время ещё не записано в список
                    {
                        if(getProverkaPeriod(timeSpanStart, timeSpanStart.Add(gap), startTimes, durations) == -1) // Если время не занято
                        {
                            Array.Resize(ref listFreePeriods, listFreePeriods.Length + 1);
                            listFreePeriods[listFreePeriods.Length - 1] = startTimes[Index].Add(-gap);
                        }
                    }
                    time = startTimes[Index].Add(new TimeSpan(0, durations[Index], 0));
                }
                else
                {
                    Array.Resize(ref listFreePeriods, listFreePeriods.Length + 1);
                    listFreePeriods[listFreePeriods.Length - 1] = time;
                    time = time.Add(gap);
                }
            }
            foreach(TimeSpan time1 in listFreePeriods) // Перемещение из типа TimeSpan[] в string[] с необходимым форматом
            {
                Array.Resize(ref periods, periods.Length + 1);
                periods[periods.Length - 1] = "" + time1.ToString(@"hh\:mm") + "-" + time1.Add(gap).ToString(@"hh\:mm");
            }
            return periods;
        }

        /// <summary>
        /// // Проверка, что время сеанса не занимает периоды занятых промежутков
        /// </summary>
        /// <param name="timeStart"></param>
        /// <param name="timeEnd"></param>
        /// <param name="startTimes"></param>
        /// <param name="durations"></param>
        /// <returns></returns>
        public static int getProverkaPeriod(TimeSpan timeStart, TimeSpan timeEnd, TimeSpan[] startTimes, int[] durations)
        {
            for(int i = 0; i < startTimes.Length; i++) // Цикл по всем занятым промежуткам
            {
                if(startTimes[i] >= timeStart) 
                {
                    if(startTimes[i] < timeEnd)
                    {
                        return i; // Если время начала занятого промежутка входит в время занимаемого
                    }
                }
                else 
                {
                    TimeSpan timeAdd = new TimeSpan(0, durations[i], 0);
                    if (startTimes[i].Add(timeAdd) > timeStart) // Если время начала занимаемого промежутка входит в время занятого
                    {
                        return i;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Проверка на то, что такое время уже занято в выходном списке
        /// </summary>
        /// <param name="time"></param>
        /// <param name="startTimes"></param>
        /// <param name="gap"></param>
        /// <returns></returns>
        public static bool getProverkaFreeTime(TimeSpan time, TimeSpan[] startTimes, TimeSpan gap)
        {
            for (int i = 0; i < startTimes.Length; i++) // Цикл по всем выходным промежуткам
            {
                if (startTimes[i] <= time)
                {
                    if (startTimes[i].Add(gap) > time)
                    {
                        return true; // Если время занято
                    }
                }
            }
            return false;
        }
    }
}
