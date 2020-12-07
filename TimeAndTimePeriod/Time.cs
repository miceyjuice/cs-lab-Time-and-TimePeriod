using System;

namespace TimeAndTimePeriod
{
    public struct Time : IEquatable<Time>, IComparable<Time>
    {
        private readonly byte _hours;
        private readonly byte _minutes;
        private readonly byte _seconds;

        /// <summary>
        /// Czas wyrażany w godzinach
        /// </summary>
        public byte Hours => _hours;
        /// <summary>
        /// Czas wyrażany w minutach
        /// </summary>
        public byte Minutes => _minutes;
        /// <summary>
        /// Czas wyrażany w sekundach
        /// </summary>
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
            
            if(times.Length != 3) throw new Exception("Podano zły format czasu! Poprawny format to: { hh:mm:ss }");

            _hours = Convert.ToByte(times[0]) < 24 ? Convert.ToByte(times[0]) : throw new ArgumentOutOfRangeException("Godziny muszą mieścić się w przedziale: 0 - 23");
            _minutes = Convert.ToByte(times[1]) < 60 ? Convert.ToByte(times[0]) : throw new ArgumentOutOfRangeException("Minuty muszą mieścić się w przedziale: 0 - 59");
            _seconds = Convert.ToByte(times[2]) < 60 ? Convert.ToByte(times[0]) : throw new ArgumentOutOfRangeException("Sekundy muszą mieścić się w przedziale: 0 - 59");
        }

        public override string ToString() => $"{_hours:00}:{_minutes:00}:{_seconds:00}";

        public bool Equals(Time other) => _hours == other._hours && _minutes == other._minutes && _seconds == other._seconds;
        
        public override bool Equals(object obj) => obj is Time other && Equals(other);

        public override int GetHashCode() => HashCode.Combine(_hours, _minutes, _seconds);

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
        public static Time operator -(Time a, TimePeriod b) => a.Minus(b);

        private Time Minus(TimePeriod timeperiod)
        {
            var hour = (byte) (Hours - timeperiod.Hours >= 0
                ? Hours - timeperiod.Hours
                : 24 + Hours - timeperiod.Hours);
            byte minutes, seconds;
            
            if (Minutes - timeperiod.Minutes >= 0) minutes = (byte) (Minutes - timeperiod.Minutes);
            else
            {
                minutes = (byte) (60 + Minutes - timeperiod.Minutes);
                hour--;
            }

            if (Seconds - timeperiod.Seconds >= 0) seconds = (byte) (Seconds - timeperiod.Seconds);
            else
            {
                seconds = (byte) (60 + Seconds - timeperiod.Seconds);
                minutes--;
            }
            
            return new Time(hour, minutes, seconds);
        }

        public static Time Minus(Time t, TimePeriod timeperiod)
        {
            var hour = (byte) (t.Hours - timeperiod.Hours >= 0
                ? t.Hours - timeperiod.Hours
                : 24 + t.Hours - timeperiod.Hours);
            byte minutes, seconds;
            
            if (t.Minutes - timeperiod.Minutes >= 0) minutes = (byte) (t.Minutes - timeperiod.Minutes);
            else
            {
                minutes = (byte) (60 + t.Minutes - timeperiod.Minutes);
                hour--;
            }

            if (t.Seconds - timeperiod.Seconds >= 0) seconds = (byte) (t.Seconds - timeperiod.Seconds);
            else
            {
                seconds = (byte) (60 + t.Seconds - timeperiod.Seconds);
                minutes--;
            }
            
            return new Time(hour, minutes, seconds);
        }


        public Time Plus(TimePeriod timeperiod)
        {
            var fullSeconds = ConvertTimeToSeconds(this) + timeperiod.FullSeconds;

            var hours = (byte) (fullSeconds / 3600 > 23 ? fullSeconds / 3600 % 24 : fullSeconds / 3600);
            var minutes = (byte) (fullSeconds / 60 % 60);
            var seconds = (byte) (fullSeconds % 60);
            
            return new Time(hours, minutes, seconds);
        }

        public static Time Plus(Time time, TimePeriod timeperiod)
        {
            var fullSeconds = ConvertTimeToSeconds(time) + timeperiod.Hours + timeperiod.Minutes + timeperiod.Seconds;
            
            var hours = (byte) (fullSeconds / 3600 > 23 ? fullSeconds / 3600 % 24 : fullSeconds / 3600);
            var minutes = (byte) (fullSeconds / 60 % 60);
            var seconds = (byte) (fullSeconds % 60);
            
            return new Time(hours, minutes, seconds);
        }

        private static long ConvertTimeToSeconds(Time time) => time._hours * 3600 + time._minutes * 60 + time._seconds;
    }
}