using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;

namespace DrumRoll
{
    class Stopwatch
    {
        private DispatcherTimer _timer;
        private DateTime _startTime;
        private TimeSpan _startingTimeSpan;
        private TimeSpan _remainingTimeSpan;

        public event EventHandler Tick;
        public event EventHandler Elapsed;

        public Stopwatch()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Interval = TimeSpan.FromMilliseconds(100);
        }

        public TimeSpan RemainingTime
        {
            get
            {
                return (_remainingTimeSpan);
            }
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            UpdateTime();
        }

        private void UpdateTime()
        {
            TimeSpan elapsedTimeSpan = DateTime.Now.Subtract(_startTime);
            if (elapsedTimeSpan < _startingTimeSpan)
            {
                _remainingTimeSpan = _startingTimeSpan.Subtract(elapsedTimeSpan);
                if (Tick != null)
                {
                    Tick(this, EventArgs.Empty);
                }
            }
            else
            {
                _timer.Stop();
                _remainingTimeSpan = TimeSpan.Zero;
                if (Elapsed != null)
                {
                    Elapsed(this, EventArgs.Empty);
                }
            }
        }

        public void Reset(int seconds)
        {
            _timer.Stop();
            _startingTimeSpan = TimeSpan.FromSeconds(seconds);
            _remainingTimeSpan = _startingTimeSpan;
            if (Tick != null)
            {
                Tick(this, EventArgs.Empty);
            }
        }

        public bool IsRunning
        {
            get 
            {
                return(_timer.IsEnabled);
            }
        }

        public void Start()
        {
            _startTime = DateTime.Now;
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
            _startingTimeSpan = _remainingTimeSpan;
            UpdateTime();
        }

    }

}
