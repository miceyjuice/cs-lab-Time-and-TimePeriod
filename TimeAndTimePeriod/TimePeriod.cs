using System;
using System.Linq;

namespace TimeAndTimePeriod
{
    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        
        private readonly long seconds;
        public long Seconds => seconds;

        public TimePeriod(ulong hours, byte minutes, byte seconds = 0)
        {
            if(minutes < 60 && seconds < 60) this.seconds = (long) (hours * 3600 + (ulong) (minutes * 60) + seconds);
            else throw new ArgumentException();
        }

        public TimePeriod(long seconds)
        {
            this.seconds = seconds;
        }

        public TimePeriod(string timeperiod)
        {
            var time = timeperiod.Split(':');
            var hour = Convert.ToInt64(time[0]) >= 0 ? Convert.ToInt64(time[0]) : throw new ArgumentException();
            var minute = Convert.ToInt64(time[1]) < 60 ? Convert.ToInt64(time[1]) : throw new ArgumentException();
            var second = Convert.ToInt64(time[2]) < 60 ? Convert.ToInt64(time[2]) : throw new ArgumentException();
            seconds = hour * 3600 + minute * 60 + second;
        }

        public TimePeriod(Time t1, Time t2)
        {
            var firstTime = ConvertTimeToSeconds(t1);
            var secondTime = ConvertTimeToSeconds(t2);
            
            seconds = secondTime - firstTime < 0 ? (secondTime - firstTime) + 24 * 3600 : secondTime - firstTime;
        }

        public override string ToString() => $"{Seconds / 3600}:{(Seconds / 60) % 60:00}:{Seconds % 60:00}";

        public static bool operator ==(TimePeriod a, TimePeriod b) => a.Equals(b);
        public static bool operator !=(TimePeriod a, TimePeriod b) => !(a == b);
        public static bool operator <(TimePeriod a, TimePeriod b) => a.CompareTo(b) < 0;
        public static bool operator <=(TimePeriod a, TimePeriod b) => a.CompareTo(b) <= 0;
        public static bool operator >(TimePeriod a, TimePeriod b) => a.CompareTo(b) > 0;
        public static bool operator >=(TimePeriod a, TimePeriod b) => a.CompareTo(b) >= 0;
        public static TimePeriod operator +(TimePeriod a, TimePeriod b) => a.Plus(b);

        public override bool Equals(object obj) => obj is TimePeriod other && Equals(other);

        public override int GetHashCode() => seconds.GetHashCode();
        public bool Equals(TimePeriod other) => seconds == other.seconds;
        public int CompareTo(TimePeriod other) => (int) (seconds - other.seconds);

        public TimePeriod Plus(TimePeriod timeperiod) => new TimePeriod(seconds + timeperiod.seconds);
        public static TimePeriod Plus(TimePeriod timeperiod1, TimePeriod timeperiod2) => new TimePeriod(timeperiod1.seconds + timeperiod2.seconds);
        private static long ConvertTimeToSeconds(Time time) => time.Hours * 3600 + time.Minutes * 60 + time.Seconds;
    }
}