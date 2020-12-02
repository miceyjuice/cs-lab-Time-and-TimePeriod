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

        #region <---| TimeStructConstructors |--->

        [TestMethod, TestCategory("Time Constructors")]
        public void TimeConstructorDefault()
        {
            var time = new Time();
            
            Assert.AreEqual(defaultValue, time.Hours);
            Assert.AreEqual(defaultValue, time.Minutes);
            Assert.AreEqual(defaultValue, time.Seconds); 
        }
        
        [TestMethod, TestCategory("Time Constructors")]
        [DataRow((byte)13, (byte)32, (byte)59, (byte)13, (byte)32, (byte)59)]
        [DataRow((byte)7, (byte)11, (byte)44, (byte)7, (byte)11, (byte)44)]
        [DataRow((byte)22,(byte)54,(byte)29,(byte)22,(byte)54, (byte)29)]
        public void TimeConstructorThreeParameters(byte h, byte m, byte s, byte expectedH, byte expectedM, byte exptectedS)
        {
            var time = new Time(h,m, s);

            AssertTime(time,expectedH,expectedM,exptectedS);
        }
        
        [TestMethod, TestCategory("Time Constructors")]
        [DataRow((byte)13, (byte)32, (byte)13, (byte)32)]
        [DataRow((byte)7, (byte)11, (byte)7, (byte)11)]
        [DataRow((byte)22,(byte)54,(byte)22,(byte)54)]
        public void TimeConstructorTwoParameters(byte h, byte m, byte expectedH, byte expectedM)
        {
            var time = new Time(h,m);

            AssertTime(time,expectedH,expectedM,0);
        }
        
        [TestMethod, TestCategory("Time Constructors")]
        [DataRow((byte)13, (byte)13)]
        [DataRow((byte)11,(byte)11)]
        [DataRow((byte)21,(byte)21)]
        public void TimeConstructorOneParameter(byte h, byte expectedH)
        {
            var time = new Time(h);

            AssertTime(time,expectedH,0,0);
        }
        
        [TestMethod, TestCategory("Time Constructors")]
        [DataRow("15:23:10", (byte)15,(byte)23,(byte)10)]
        [DataRow("11:11:13",(byte)11,(byte)11, (byte)13)]
        [DataRow("23:51:21",(byte)23,(byte)51,(byte)21)]
        public void TimeConstructorStringParameter(string t, byte h, byte m, byte s)
        {
            var stringTime = new Time(t);
            
            AssertTime(stringTime,h,m,s);
        }

        #endregion

        #region <---| TimeStructToString |--->

        [TestMethod, TestCategory("String representation")]
        public void TimeToStringDefault()
        {
            var time = new Time(14,25);
            string expectedTime = "14:25:00";
            
            Assert.AreEqual(expectedTime, time.ToString());
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

        #endregion
        
        

    }
}