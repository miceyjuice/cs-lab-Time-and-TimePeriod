using System;
using TimeAndTimePeriod;

namespace TimeAndTimePeriodApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var czas = new Time("13:22:10");
            var czas2 = new Time(13,22,10);
            
            Console.WriteLine(czas2.ToString());
            Console.WriteLine(czas.ToString());
            
            Console.WriteLine(czas.Equals(czas2));
        }
    }
}