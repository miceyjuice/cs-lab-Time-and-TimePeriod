using System;

namespace TimeAndTimePeriod
{
    public struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        private long seconds;

        public long Seconds => seconds;

        public TimePeriod(ulong hours, byte minutes, byte seconds = 0)
        {
            if(minutes < 60 && seconds < 60) this.seconds = (long) (hours * 3600 + (ulong) (minutes * 60) + seconds);
            else throw new ArgumentException();
        }
        
        public bool Equals(TimePeriod other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(TimePeriod other)
        {
            throw new NotImplementedException();
        }
    }
}