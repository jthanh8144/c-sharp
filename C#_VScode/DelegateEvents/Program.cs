using System;

namespace DelegateEvents
{
    class Program
    {
        static void Main(string[] args)
        {
            Clock c = new Clock();
            AnalogClock ac = new AnalogClock();
            DigitalClock dc = new DigitalClock();

            c.OnSecondChange += new Clock.SecondHandler(ac.ShowAC);
            c.OnSecondChange += new Clock.SecondHandler(dc.ShowDC);
            ac.DK(c);
            dc.DK(c);
            c.Run();
        }
    }
}
