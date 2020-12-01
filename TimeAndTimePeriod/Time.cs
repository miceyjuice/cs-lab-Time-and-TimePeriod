using System;

namespace TimeAndTimePeriod
{
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        public byte Hours { get; }
        public byte Minutes { get; }
        public byte Seconds { get; }

        public Time(byte hours, byte minutes = 0, byte seconds = 0)
        {
            Hours = hours > 23 ? throw new ArgumentException("Wrong argument!") : hours;
            Minutes = minutes > 59 ? throw new ArgumentException("Wrong argument!") : minutes;
            Seconds = seconds > 59 ? throw new ArgumentException("Wrong argument!") : seconds;
        }

        public Time(string time)
        {
            var times = time.Split(':');

            Hours = Convert.ToByte(times[0]);
            Minutes = Convert.ToByte(times[1]);
            Seconds = Convert.ToByte(times[2]);
        }

        public override string ToString() => $"{Hours:00}:{Minutes:00}:{Seconds:00}";

        public bool Equals(Time other) => Hours == other.Hours && Minutes == other.Minutes && Seconds == other.Seconds;

        public int CompareTo(Time other)
        {
            if (Hours - other.Hours != 0) return Hours - other.Hours;
            if (Minutes - other.Minutes != 0) return Minutes - other.Minutes;
            if (Seconds - other.Seconds != 0) return Seconds - other.Seconds;
            return 0;
        }

        public static bool operator ==(Time a, Time b) => a.Equals(b);
        public static bool operator !=(Time a, Time b) => !(a == b);
        public static bool operator <(Time a, Time b) => a.CompareTo(b) < 0;
        public static bool operator <=(Time a, Time b) => a.CompareTo(b) <= 0;
        public static bool operator >(Time a, Time b) => a.CompareTo(b) > 0;
        public static bool operator >=(Time a, Time b) => a.CompareTo(b) >= 0;
        public static Time operator +(Time a, TimePeriod b) => a.Plus(b);


        public Time Plus(TimePeriod timeperiod)
        {
            var fullSeconds = Hours * 3600 + Minutes * 60 + Seconds + timeperiod.Seconds;

            var hours = (byte) (fullSeconds / 3600 > 23 ? fullSeconds / 3600 % 24 : fullSeconds / 3600);
            var minutes = (byte) (fullSeconds / 60 % 60);
            var seconds = (byte) (fullSeconds % 60);
            
            return new Time(hours, minutes, seconds);
        }

        public static Time Plus(Time time, TimePeriod timeperiod)
        {
            var fullSeconds = ConvertTimeToSeconds(time) + timeperiod.Seconds;
            
            var hours = (byte) (fullSeconds / 3600 > 23 ? fullSeconds / 3600 % 24 : fullSeconds / 3600);
            var minutes = (byte) (fullSeconds / 60 % 60);
            var seconds = (byte) (fullSeconds % 60);
            
            return new Time(hours, minutes, seconds);
        }

        private static long ConvertTimeToSeconds(Time time) => time.Hours * 3600 + time.Minutes * 60 + time.Seconds;
    }
}