using System;

namespace DelegateEvents
{
    public class TimeEventArgs : EventArgs
    {
        public DateTime timer {get; set;}
        public TimeEventArgs(DateTime t)
        {
            timer = t;
        }
    }
}