using System;
using TimeAndTimePeriod;
using static TimeAndTimePeriod.Time;
using static TimeAndTimePeriod.TimePeriod;

namespace TimeAndTimePeriodApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var czas = new Time("13:20:00");
            var czas2 = new Time(14,10);
            
            var okresczasu = new TimePeriod(51254);
            var okresczasu1 = new TimePeriod(czas, czas2);
            
            Console.WriteLine(czas2.ToString());
            Console.WriteLine(czas.ToString());
            
            Console.WriteLine(czas.Equals(czas2));
            Console.WriteLine(okresczasu.ToString());
            Console.WriteLine(okresczasu1.ToString());
            
            Console.WriteLine(okresczasu.Plus(okresczasu1));
            
            Console.WriteLine(Plus(czas, okresczasu));
            Console.WriteLine(okresczasu == okresczasu1);
        }
    }
}