using System;

namespace TimeAndTimePeriod
{
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        private readonly byte _hours;
        private readonly byte _minutes;
        private readonly byte _seconds;

        public byte Hours => _hours;
        public byte Minutes => _minutes;
        public byte Seconds => _seconds;

        public Time(byte hours, byte minutes = 0, byte seconds = 0)
        {
            _hours = hours > 23 ? throw new ArgumentException("Wrong argument!") : hours;
            _minutes = minutes > 59 ? throw new ArgumentException("Wrong argument!") : minutes;
            _seconds = seconds > 59 ? throw new ArgumentException("Wrong argument!") : seconds;
        }

        public Time(string time)
        {
            var times = time.Split(':');

            _hours = Convert.ToByte(times[0]);
            _minutes = Convert.ToByte(times[1]);
            _seconds = Convert.ToByte(times[2]);
        }

        public override string ToString() => $"{_hours:00}:{_minutes:00}:{_seconds:00}";

        public bool Equals(Time other) => _hours == other._hours && _minutes == other._minutes && _seconds == other._seconds;

        public int CompareTo(Time other)
        {
            if (_hours - other._hours != 0) return _hours - other._hours;
            if (_minutes - other._minutes != 0) return _minutes - other._minutes;
            if (_seconds - other._seconds != 0) return _seconds - other._seconds;
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
            var full_seconds = _hours * 3600 + _minutes * 60 + _seconds + timeperiod.Seconds;

            var hours = (byte) (full_seconds / 3600 > 23 ? full_seconds / 3600 % 24 : full_seconds / 3600);
            var minutes = (byte) (full_seconds / 60 % 60);
            var seconds = (byte) (full_seconds % 60);
            
            return new Time(hours, minutes, seconds);
        }

        public static Time Plus(Time time, TimePeriod timeperiod)
        {
            var full_seconds = ConvertTimeToSeconds(time) + timeperiod.Seconds;
            
            var hours = (byte) (full_seconds / 3600 > 23 ? full_seconds / 3600 % 24 : full_seconds / 3600);
            var minutes = (byte) (full_seconds / 60 % 60);
            var seconds = (byte) (full_seconds % 60);
            
            return new Time(hours, minutes, seconds);
        }

        private static long ConvertTimeToSeconds(Time time) => time._hours * 3600 + time._minutes * 60 + time._seconds;
    }
}