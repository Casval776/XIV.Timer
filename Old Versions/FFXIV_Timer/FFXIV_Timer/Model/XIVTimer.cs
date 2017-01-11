using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Timers;
using FFXIV_Timer.Helpers;
using FFXIV_Timer.Interface;

namespace FFXIV_Timer.Model
{
    // ReSharper disable once InconsistentNaming
    public class XIVTimer : INotifyPropertyChanged
    {
        #region Properties

        public IEnumerable<IItemTime> Time
        {
            get { return _time; }
            set
            {
                _time = value;
                if (_time != null)
                    CalculateSpawn();
            }
        }

        public Timer NextSpawn { get; set; }
        public DateTime DisplayNextSpawn
        {
            get { return _displayNextSpawn; }
            set
            {
                _displayNextSpawn = value;
                NotifyPropertyChanged();
            }
        }

        public int SecondsToSpawn
        {
            get { return _secondsToSpawn; }
            set
            {
                _secondsToSpawn = value;
                if (value == 0)
                    RegisterAvailabilityTimer();
                NotifyPropertyChanged();
            }
        }

        public int AvailabilityHours
        {
            get { return _availabilityHours; }
            set
            {
                _availabilityHours = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Private Members
        private DateTime _displayNextSpawn;
        private int _secondsToSpawn;
        private IEnumerable<IItemTime> _time;
        private int _availabilityHours;
        private bool _isAvailable;
        private Timer _availabilityTimer;
        private const int _refreshIntervalInSeconds = 15;
        #endregion

        #region Public Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Public Methods
        #endregion

        #region Private Methods
        private void CalculateSpawn()
        {
            //Hold lowest value
            var lowestTime = Time.Select(CalculateOffset).Concat(new[] { double.MaxValue }).Min();
            //Set properties
            SecondsToSpawn = (int)lowestTime;
            DisplayNextSpawn = new DateTime(MainWindow.GameTime.Year, 
                                            MainWindow.GameTime.Month, 
                                            MainWindow.GameTime.Day, 
                                            0, 0, 0);
            DisplayNextSpawn = DisplayNextSpawn.AddSeconds((int) lowestTime);

            ////Register Timer
            //var tempTimer = new Timer()
            //{
            //    Interval = TimeSpan.FromSeconds(60).TotalMilliseconds
            //};
            //tempTimer.Elapsed += TimerEvent;
            //tempTimer.Enabled = true;
            //NextSpawn = tempTimer;
            NextSpawn = new Timer
            {
                Interval = TimeSpan.FromSeconds((double)MainWindow.TimerInterval).TotalMilliseconds,
                Enabled = true
            };
            NextSpawn.Elapsed += TimerEvent;
        }
        private double CalculateOffset(IItemTime time)
        {
            var tempTime = new DateTime(MainWindow.GameTime.Year, MainWindow.GameTime.Month, MainWindow.GameTime.Day, time.Time, 0, 0);
            var timeDiff = tempTime.Subtract(MainWindow.GameTime);
            //Calculate the difference in game time to real time
            //Difference in Minutes * IRL Seconds per Minute = IRL Seconds for interval / 60 = IRL Minutes for interval
            var offSet = timeDiff.TotalMinutes;//(((decimal) timeDiff.TotalMinutes*MainWindow.TimerInterval));
                                               //If offset is negative, time has already passed.
                                               //The returned time is negative to represent a date in the past, subtract this to get time until next
            if (offSet < 0)
            {
                var fullDay = TimeSpan.FromDays(1).TotalSeconds;//new TimeSpan(24, 0, 0).Minutes;
                offSet = fullDay - (int)Math.Abs(offSet);
            }
            //Convert the ingame minutes to real world minutes
            //Ingame Minutes * TimerInterval = Real World Seconds
            return (offSet * (double)MainWindow.TimerInterval);
        }

        private void RegisterAvailabilityTimer()
        {
            //Null check
            if (_availabilityTimer == null)
                _availabilityTimer = new Timer();
            //Use the Helper class to get the real world equivalent in milliseconds
            var availabilitySpan = EorzeanTimeHelper.GetEorzeanTimeInterval(AvailabilityHours);
            //Assign span to timer
            _availabilityTimer.Interval = availabilitySpan;
            _availabilityTimer.Elapsed += AvailabilityTimerEvent;
            _availabilityTimer.Enabled = true;
            //Assign flag
            _isAvailable = true;
        }

        #endregion

        #region Events
        private void NotifyPropertyChanged([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public void TimerEvent(object source, ElapsedEventArgs eea)
        {
            SecondsToSpawn = Math.Abs(SecondsToSpawn -= (int)MainWindow.TimerInterval);
            DisplayNextSpawn = new DateTime(MainWindow.GameTime.Year, 
                                            MainWindow.GameTime.Month, 
                                            MainWindow.GameTime.Day, 
                                            0, 0, 0).AddSeconds(SecondsToSpawn);
            //DisplayNextSpawn = DisplayNextSpawn.AddMinutes(SecondsToSpawn);
            //if (DisplayNextSpawn.Minute == 0)
            //{
            //    _isAvailable = true;
            //    RegisterAvailabilityTimer();
            //}
        }

        public void AvailabilityTimerEvent(object source, ElapsedEventArgs eea)
        {
            //When event fires, availability window ends
            _isAvailable = false;
            _availabilityTimer.Enabled = false;
        }
        #endregion

        #region Override Methods
        public override string ToString()
        {
            return _isAvailable ? 
                "Available" 
                : DisplayNextSpawn.Minute < 1 ?
                    "<" + "1 min"
                    : DisplayNextSpawn.ToString("mm") + " min";
        }
        #endregion
    }
}
