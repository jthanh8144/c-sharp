using System;

namespace DelegateEvents
{
    public class DigitalClock
    {
        public void DK(Clock c)
        {
            c.OnSecondChange += new Clock.SecondHandler(ShowDC);
        }
        public void ShowDC(object o, EventArgs e)
        {
            DateTime d = DateTime.Now;
            Console.WriteLine("DC: " + d.Hour + ":" + d.Minute + ":" + d.Second + ":" + d. Millisecond);
        }
    }
}