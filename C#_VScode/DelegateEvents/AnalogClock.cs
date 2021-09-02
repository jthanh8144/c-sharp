using System;

namespace DelegateEvents
{
    public class AnalogClock
    {
        public void DK(Clock c)
        {
            c.OnSecondChange += new Clock.SecondHandler(ShowAC);
        }
        public void ShowAC(object o, TimeEventArgs e)
        {
            Console.WriteLine("AC: " + e.timer.Hour + ":" + e.timer.Minute + ":" + e.timer.Second + ":" + e.timer. Millisecond);
        }
        
    }
}