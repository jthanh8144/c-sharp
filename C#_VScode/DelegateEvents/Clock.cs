using System;
using System.Threading;

namespace DelegateEvents
{
    public class Clock
    {
        public delegate void SecondHandler(object o, TimeEventArgs e);
        public event SecondHandler OnSecondChange;
        public void Run()
        {
            while (true)
            {
                Thread.Sleep(1000);
                if (OnSecondChange != null)
                {
                    DateTime d = DateTime.Now;
                    OnSecondChange(this, new TimeEventArgs(DateTime.Now));
                }
            }
        }
    }
}