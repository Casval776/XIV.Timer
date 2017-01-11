using FFXIV_Timer.Controller;
using FFXIV_Timer.Interface;
using FFXIV_Timer.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace FFXIV_Timer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    // ReSharper disable once RedundantExtendsListEntry
    public partial class MainWindow : Window
    {
        #region Members
        public static DateTime GameTime;
        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
        private MainController _controller;
        private static ObservableCollection<IUnspoiledItem> _varCollection;
        private static ObservableCollection<IUnspoiledItem> _varCollectionTmp; 

        public static readonly decimal TimerInterval = (2 + decimal.Divide(11, 12));
        #endregion

        public MainWindow()
        {
            //OOB Code
            InitializeComponent();
            //Initialize our Main Controller for the application
            _controller = MainController.Instance;
            //Register the Timer used to track Eorzean Time
            RegisterTimer();
            IEnumerable<IUnspoiledItem> tempCollection = _controller.dbConn.Get(new UnspoiledItem());
            //this._controller.CalculateNextSpawns(ref tempCollection);
            _varCollection = new ObservableCollection<IUnspoiledItem>(tempCollection);
            _varCollectionTmp = new ObservableCollection<IUnspoiledItem>();
            //dataGrid1.DataContext = this;
            dataGrid1.ItemsSource = _varCollection;
            //subscribe to event
            foreach (var item in _varCollection)
            {
                //item.NextSpawn.PropertyChanged += RenderDataSource;
            }
            //Cleanup grid
            dataGrid1.HeadersVisibility = DataGridHeadersVisibility.Column;
            dataGrid1.RowStyle = new Style();
        }

        private void RenderDataSource(object sender, PropertyChangedEventArgs pcea)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action<IEnumerable<IUnspoiledItem>>(UpdateDataGridSource), _varCollection);
        }

        /// <summary>
        /// Registers the Timer used to track Eorzean Time
        /// </summary>
        private void RegisterTimer()
        {
            //Register Timer and properties.
            var timer = new Timer();
            timer.Elapsed += TimerEvent;
            timer.Interval = TimeSpan.FromSeconds((double)TimerInterval).TotalMilliseconds;
            timer.Enabled = true;

            //Debug timer
            var timer2 = new Timer();
            timer2.Elapsed += Timer2_Elapsed;
            timer2.Interval = TimeSpan.FromMinutes(1).TotalMilliseconds;
            timer2.Enabled = true;

            //Assign Game Time depending on passage of Real Time
            EvaluateTime();
        }

        private void Timer2_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action<IEnumerable<IUnspoiledItem>>(UpdateDataGridSource), _varCollection);
        }

        private void EvaluateTime()
        {
            #region Delete Me
            //Find how much time has passed since start of day.
            //_gameTime is always initialized to beginning of day.
            //var timePassed = DateTime.Now.Subtract(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0));
            ////1m gametime = 2.9s real time
            ////Calculate seconds passed and multiply to minutes
            //var inGameMinutesPassed = timePassed.TotalSeconds*(double)TimerInterval;
            ////Assign these values to the current gameTime
            ////_gameTime = _gameTime.AddHours(timePassed.Hours);
            //_gameTime = _gameTime.AddMinutes(inGameMinutesPassed);
            //_gameTime = new DateTime(1, 1, 1, (int)timePassed.TotalHours, (int)timePassed.Minutes, 0);
            #endregion
            const double eorzeaMultiplier = 3600D / 175D; //175 Earth seconds for every 3600 Eorzea seconds

            // Calculate how many ticks have elapsed since 1/1/1970
            long epochTicks = DateTime.UtcNow.Ticks - (new DateTime(1970, 1, 1).Ticks);

            // Multiply that value by 20.5714285714...
            long eorzeaTicks = (long)Math.Round(epochTicks * eorzeaMultiplier);

            GameTime = new DateTime(eorzeaTicks);
        }

        /// <summary>
        /// Event fired when Interval elapses. 
        /// Used to track Eorzean Time.
        /// </summary>
        /// <param name="source">The Caller</param>
        /// <param name="eea">Event Args</param>
        public void TimerEvent(object source, ElapsedEventArgs eea)
        {
            GameTime = GameTime.AddMinutes(1);
            //This thread cannot access UI properties. Invoke to set Label
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action<string>(SetLabel), GameTime.ToString("hh:mmtt"));
        }

        /// <summary>
        /// Sets the Label for the UI Time
        /// </summary>
        /// <param name="val">String value to assign to the Label</param>
        private void SetLabel(string val)
        {
            label1.Content = val;
        }

        private void UpdateDataGridSource(IEnumerable<IUnspoiledItem> source)
        {
            dataGrid1.CommitEdit(DataGridEditingUnit.Row, true);
            //dataGrid1.ItemsSource = null;
            //dataGrid1.ItemsSource = source;
            dataGrid1.Items.Refresh();
            //dataGrid1.so
            //dataGrid1.render
        }

        private void ToggleSelected_OnClick(object sender, RoutedEventArgs e)
        {
            dataGrid1.CommitEdit(DataGridEditingUnit.Row, true);
            if (_varCollectionTmp.Count == 0)
            {
                _varCollectionTmp = _varCollection;
                _varCollection = new ObservableCollection<IUnspoiledItem>(_varCollection.Where(p => p.Selected));
            }
            else
            {
                _varCollection = _varCollectionTmp;
                _varCollectionTmp = new ObservableCollection<IUnspoiledItem>();
            }
            dataGrid1.ItemsSource = _varCollection;
        }
    }
}
