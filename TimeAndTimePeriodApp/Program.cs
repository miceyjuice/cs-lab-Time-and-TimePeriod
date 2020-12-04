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
            var czas = new Time("13:20:20");
            var czas2 = new Time(14,10);
            
            var okresczasu = new TimePeriod("14:30:30");
            var okresczasu1 = new TimePeriod(czas, czas2);
            
            Console.WriteLine(czas2.ToString());
            Console.WriteLine(czas.ToString());
            
            
            Console.WriteLine(czas-okresczasu);
            Console.WriteLine(Minus(czas, okresczasu));
            
            Console.WriteLine(czas+okresczasu);
        }
    }
}