using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeAndTimePeriod;

namespace TimeAndPeriodUnitTests
{
    [TestClass]
    public class UnitTests
    {
        private const byte defaultValue = 0;
        private void AssertTime(Time t, byte expectedH, byte expectedM, byte expectedS)
        {
            Assert.AreEqual(expectedH, t.Hours);
            Assert.AreEqual(expectedM, t.Minutes);
            Assert.AreEqual(expectedS, t.Seconds);
        }

        private void AssertTimePeriod(TimePeriod tp, ulong expectedH, byte expectedM, byte expectedS)
        {
            Assert.AreEqual(expectedH, (ulong) (tp.Seconds / 3600));
            Assert.AreEqual(expectedM,(byte) (tp.Seconds / 60 % 60));
            Assert.AreEqual(expectedS, (byte) (tp.Seconds % 60));
        }

        #region <---| Constructors |--->

        [TestMethod, TestCategory("Constructors")]
        public void TimeConstructorDefault()
        {
            var time = new Time();
            
            AssertTime(time, defaultValue, defaultValue, defaultValue);
        }
        
        [TestMethod, TestCategory("Constructors")]
        [DataRow((byte)13, (byte)32, (byte)59, (byte)13, (byte)32, (byte)59)]
        [DataRow((byte)7, (byte)11, (byte)44, (byte)7, (byte)11, (byte)44)]
        [DataRow((byte)22,(byte)54,(byte)29,(byte)22,(byte)54, (byte)29)]
        public void TimeConstructorThreeParameters(byte h, byte m, byte s, byte expectedH, byte expectedM, byte exptectedS)
        {
            var time = new Time(h,m, s);

            AssertTime(time,expectedH,expectedM,exptectedS);
        }
        
        [TestMethod, TestCategory("Constructors")]
        [DataRow((byte)13, (byte)32, (byte)13, (byte)32)]
        [DataRow((byte)7, (byte)11, (byte)7, (byte)11)]
        [DataRow((byte)22,(byte)54,(byte)22,(byte)54)]
        public void TimeConstructorTwoParameters(byte h, byte m, byte expectedH, byte expectedM)
        {
            var time = new Time(h,m);

            AssertTime(time,expectedH,expectedM,0);
        }
        
        [TestMethod, TestCategory("Constructors")]
        [DataRow((byte)13, (byte)13)]
        [DataRow((byte)11,(byte)11)]
        [DataRow((byte)21,(byte)21)]
        public void TimeConstructorOneParameter(byte h, byte expectedH)
        {
            var time = new Time(h);

            AssertTime(time,expectedH,0,0);
        }
        
        [TestMethod, TestCategory("Constructors")]
        [DataRow("15:23:10", (byte)15,(byte)23,(byte)10)]
        [DataRow("11:11:13",(byte)11,(byte)11, (byte)13)]
        [DataRow("23:51:21",(byte)23,(byte)51,(byte)21)]
        public void TimeConstructorStringParameter(string t, byte h, byte m, byte s)
        {
            var stringTime = new Time(t);
            
            AssertTime(stringTime,h,m,s);
        }

        [TestMethod, TestCategory("Constructors")]
        public void TimePeriodConstructorDefault()
        {
            var timePeriod = new TimePeriod();
            
            Assert.AreEqual(defaultValue, timePeriod.Seconds);
        }

        [TestMethod, TestCategory("Constructors")]
        [DataRow((ulong)13,(byte)51,(byte)2,(ulong)13,(byte)51,(byte)2)]
        public void TimePeriodConstructorThreeParameters(ulong h, byte m, byte s, ulong expectedH, byte expectedM, byte exptectedS)
        {
            var timePeriod = new TimePeriod(h,m,s);
            
            AssertTimePeriod(timePeriod, expectedH, expectedM, exptectedS);
        }
        
        [TestMethod, TestCategory("Constructors")]
        [DataRow((ulong)13,(byte)51,(ulong)13,(byte)51)]
        public void TimePeriodConstructorTwoParameters(ulong h, byte m,  ulong expectedH, byte expectedM)
        {
            var timePeriod = new TimePeriod(h,m);
            
            AssertTimePeriod(timePeriod, expectedH, expectedM,defaultValue);
        }
        
        [TestMethod, TestCategory("Constructors")]
        [DataRow((byte)55,(byte)55)]
        public void TimePeriodConstructorOneParameter(byte s,  byte expectedS)
        {
            var timePeriod = new TimePeriod(s);
            
            AssertTimePeriod(timePeriod, defaultValue, defaultValue,expectedS);
        }
        
        [TestMethod, TestCategory("Constructors")]
        [DataRow("15:23:10", (ulong)15,(byte)23,(byte)10)]
        [DataRow("11:11:13",(ulong)11,(byte)11, (byte)13)]
        [DataRow("23:51:21",(ulong)23,(byte)51,(byte)21)]
        public void TimePeriodConstructorStringParameter(string t, ulong h, byte m, byte s)
        {
            var stringTime = new TimePeriod(t);
            
            AssertTimePeriod(stringTime,h,m,s);
        }

        #endregion

        #region <---| ToString |--->

        [TestMethod, TestCategory("String representation")]
        public void TimeToStringDefault()
        {
            var time = new Time(14,25);
            string expectedTime = "14:25:00";
            
            Assert.AreEqual(expectedTime, time.ToString());
        }

        [TestMethod, TestCategory("String representation")]
        public void TimePeriodToStringDefault()
        {
            var timePeriod = new TimePeriod(135,44,13);
            var expectedTimePeriod = "135:44:13";
            
            Assert.AreEqual(expectedTimePeriod, timePeriod.ToString());
        }

        #endregion

        #region <---| Equals |--->

        [TestMethod, TestCategory("Equals")]
        [DataRow((byte)13,(byte)21,(byte)52,(byte)13,(byte)21,(byte)52)]
        [DataRow((byte)3,(byte)10,(byte)6,(byte)3,(byte)10,(byte)6)]
        public void CheckIfTimesAreEqual(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            var timeOne = new Time(h1,m1,s1);
            var timeTwo = new Time(h2,m2,s2);
            
            Assert.AreEqual(true, timeOne == timeTwo);
        }
        
        [TestMethod, TestCategory("Equals")]
        [DataRow((byte)13,(byte)21,(byte)52,(byte)12,(byte)21,(byte)52)]
        [DataRow((byte)3,(byte)10,(byte)6,(byte)8,(byte)10,(byte)6)]
        public void CheckIfTimesAreNotEqual(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            var timeOne = new Time(h1,m1,s1);
            var timeTwo = new Time(h2,m2,s2);
            
            Assert.AreEqual(true, timeOne != timeTwo);
        }

        [TestMethod, TestCategory("Equals")]
        [DataRow(2143,2143)]
        [DataRow(91,91)]
        [DataRow(123,123)]
        public void CheckIfTimePeriodsAreEqual(long t1, long t2)
        {
            var timePeriodOne = new TimePeriod(t1);
            var timePeriodTwo = new TimePeriod(t2);
            
            Assert.AreEqual(true, timePeriodOne == timePeriodTwo);
        }
        
        [TestMethod, TestCategory("Equals")]
        [DataRow(2143,2143)]
        [DataRow(91,91)]
        [DataRow(123,123)]
        public void CheckIfTimePeriodsAreNotEqual(long t1, long t2)
        {
            var timePeriodOne = new TimePeriod(t1);
            var timePeriodTwo = new TimePeriod(t2);
            
            Assert.AreEqual(true, timePeriodOne == timePeriodTwo);
        }

        #endregion

        #region <---| Operators |--->

        [TestMethod, TestCategory("Operators")]
        [DataRow((byte)13,(byte)21,(byte)52,(byte)13,(byte)21,(byte)50)]
        [DataRow((byte)3,(byte)10,(byte)6,(byte)1,(byte)15,(byte)26)]
        public void CheckIfOneTimeIsGreaterThanAnother(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            var timeOne = new Time(h1,m1,s1);
            var timeTwo = new Time(h2,m2,s2);
            
            Assert.AreEqual(true, timeOne > timeTwo);
        }
        
        [TestMethod, TestCategory("Operators")]
        [DataRow((byte)13,(byte)21,(byte)52,(byte)13,(byte)21,(byte)50)]
        [DataRow((byte)3,(byte)10,(byte)6,(byte)1,(byte)15,(byte)26)]
        [DataRow((byte)13,(byte)21,(byte)52,(byte)13,(byte)21,(byte)52)]
        [DataRow((byte)23,(byte)10,(byte)6,(byte)1,(byte)15,(byte)26)]
        public void CheckIfOneTimeIsGreaterOrEqualToAnother(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            var timeOne = new Time(h1,m1,s1);
            var timeTwo = new Time(h2,m2,s2);
            
            Assert.AreEqual(true, timeOne >= timeTwo);
        }
        
        [TestMethod, TestCategory("Operators")]
        [DataRow((byte)11,(byte)21,(byte)52,(byte)13,(byte)21,(byte)50)]
        [DataRow((byte)23,(byte)10,(byte)6,(byte)23,(byte)55,(byte)26)]
        public void CheckIfOneTimeIsLesserThanAnother(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            var timeOne = new Time(h1,m1,s1);
            var timeTwo = new Time(h2,m2,s2);
            
            Assert.AreEqual(true, timeOne < timeTwo);
        }
        
        [TestMethod, TestCategory("Operators")]
        [DataRow((byte)11,(byte)21,(byte)52,(byte)13,(byte)21,(byte)50)]
        [DataRow((byte)3,(byte)10,(byte)6,(byte)11,(byte)15,(byte)26)]
        [DataRow((byte)13,(byte)21,(byte)52,(byte)13,(byte)21,(byte)52)]
        [DataRow((byte)23,(byte)10,(byte)6,(byte)23,(byte)10,(byte)6)]
        public void CheckIfOneTimeIsLesserOrEqualToAnother(byte h1, byte m1, byte s1, byte h2, byte m2, byte s2)
        {
            var timeOne = new Time(h1,m1,s1);
            var timeTwo = new Time(h2,m2,s2);
            
            Assert.AreEqual(true, timeOne <= timeTwo);
        }
        
        [TestMethod, TestCategory("Operators")]
        [DataRow((byte)11,(byte)21,(byte)52,(ulong)10,(byte)21,(byte)50, (byte)21, (byte)43, (byte)42)]
        [DataRow((byte)5,(byte)21,(byte)3,(ulong)10,(byte)21,(byte)50, (byte)15, (byte)42, (byte)53)]
        [DataRow((byte)20,(byte)21,(byte)52,(ulong)6,(byte)21,(byte)50, (byte)2, (byte)43, (byte)42)]
        public void AddingTimePeriodToTime(byte h1, byte m1, byte s1, ulong h2, byte m2, byte s2, byte expectedH, byte expectedM, byte expectedS)
        {
            var time = new Time(h1,m1,s1);
            var timePeriod = new TimePeriod(h2,m2,s2);
            var expectedTime = new Time(expectedH, expectedM, expectedS);
            
            Assert.AreEqual(expectedTime, time + timePeriod);
        }
        
        [TestMethod, TestCategory("Operators")]
        [DataRow((byte)11,(byte)21,(byte)52,(ulong)10,(byte)21,(byte)50, (byte)1, (byte)0, (byte)2)]
        [DataRow((byte)5,(byte)11,(byte)3,(ulong)10,(byte)21,(byte)50, (byte)18, (byte)49, (byte)13)]
        [DataRow((byte)20,(byte)21,(byte)52,(ulong)6,(byte)21,(byte)50, (byte)14, (byte)0, (byte)2)]
        public void SubtractingTimePeriodFromTime(byte h1, byte m1, byte s1, ulong h2, byte m2, byte s2, byte expectedH, byte expectedM, byte expectedS)
        {
            var time = new Time(h1,m1,s1);
            var timePeriod = new TimePeriod(h2,m2,s2);
            var expectedTime = new Time(expectedH, expectedM, expectedS);
            
            Assert.AreEqual(expectedTime, time - timePeriod);
        }

        [TestMethod, TestCategory(("Operators"))]
        [DataRow(1555,931)]
        [DataRow(551522,51232)]
        [DataRow(234324,1223)]
        public void CheckIfOneTimePeriodIsGreaterThanAnother(long s1, long s2)
        {
            var timPeriodOne = new TimePeriod(s1);
            var timPeriodTwo = new TimePeriod(s2);
            
            Assert.AreEqual(true, timPeriodOne > timPeriodTwo);
        }
        
        [TestMethod, TestCategory(("Operators"))]
        [DataRow(1555,931)]
        [DataRow(551522,51232)]
        [DataRow(234324,234324)]
        public void CheckIfOneTimePeriodIsGreaterOrEqualToAnother(long s1, long s2)
        {
            var timPeriodOne = new TimePeriod(s1);
            var timPeriodTwo = new TimePeriod(s2);
            
            Assert.AreEqual(true, timPeriodOne >= timPeriodTwo);
        }
        
        [TestMethod, TestCategory(("Operators"))]
        [DataRow(1555,9331)]
        [DataRow(551522,565232)]
        [DataRow(234324,1232663)]
        public void CheckIfOneTimePeriodIsLesserThanAnother(long s1, long s2)
        {
            var timPeriodOne = new TimePeriod(s1);
            var timPeriodTwo = new TimePeriod(s2);
            
            Assert.AreEqual(true, timPeriodOne < timPeriodTwo);
        }
        
        [TestMethod, TestCategory(("Operators"))]
        [DataRow(1555,9331)]
        [DataRow(551522,551522)]
        [DataRow(234324,1232663)]
        public void CheckIfOneTimePeriodIsLesserOrEqualToAnother(long s1, long s2)
        {
            var timPeriodOne = new TimePeriod(s1);
            var timPeriodTwo = new TimePeriod(s2);
            
            Assert.AreEqual(true, timPeriodOne <= timPeriodTwo);
        }
        
        [TestMethod, TestCategory("Operators")]
        [DataRow("12:50:00", "3:04:00","15:54:00")]
        [DataRow("23:30:00", "1:40:00","25:10:00")]
        [DataRow("133:13:22","20:22:10","153:35:32")]
        public void AddingTimePeriodToTimePeriod(string tp1, string tp2, string expectedTp)
        {
            var timePeriodOne = new TimePeriod(tp1);
            var timePeriodTwo = new TimePeriod(tp2);
            var expectedTimePeriod = new TimePeriod(expectedTp);

            Assert.AreEqual(expectedTimePeriod, timePeriodOne + timePeriodTwo);
        }
        
        [TestMethod, TestCategory("Operators")]
        [DataRow("12:50:00", "3:04:00","9:46:00")]
        [DataRow("23:30:00", "1:40:00","21:50:00")]
        [DataRow("133:13:22","20:22:10","112:51:12")]
        public void SubtractingTimePeriodFromTimePeriod(string tp1, string tp2, string expectedTp)
        {
            var timePeriodOne = new TimePeriod(tp1);
            var timePeriodTwo = new TimePeriod(tp2);
            var expectedTimePeriod = new TimePeriod(expectedTp);

            Assert.AreEqual(expectedTimePeriod, timePeriodOne - timePeriodTwo);
        }

        #endregion

    }
}