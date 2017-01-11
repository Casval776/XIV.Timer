using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFXIV_Timer.Helpers
{
    public class EorzeanTimeHelper
    {
        public static double GetEorzeanTimeInterval(int availableHours)
        {
            //Calculate the amount of hours into real world time
            var hoursToMinutes = availableHours*60;
            //1 minute in game = 2 11/12 seconds real time
            var secondsToWait = MainWindow.TimerInterval* hoursToMinutes;
            return TimeSpan.FromSeconds((double)secondsToWait).TotalMilliseconds;
        }
    }
}
