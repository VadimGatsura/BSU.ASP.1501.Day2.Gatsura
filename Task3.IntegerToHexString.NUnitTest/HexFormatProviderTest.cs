using System;
using NUnit.Framework;

namespace Task3.IntegerToHexString.NUnitTest {

    [TestFixture]
    public class HexFormatProviderTest {

        [TestCase(145, Result = "0x91")]
        [TestCase(-145, Result = "-0x91")]
        [TestCase(0, Result = "0x0")]
        [TestCase(41837, Result = "0xA36D")]
        [TestCase(47, Result = "0x2F")]
        public string Format_Test(int number) {
            IFormatProvider fp = new HexFormatProvider();
            return string.Format(fp, "{0:H}", number);
        }

        [TestCase(47, "{0:X}", Result = "2F")]
        public string ParentFormat_Test(int number, string format) {
            IFormatProvider fp = new HexFormatProvider();
            return string.Format(fp, format, number);
        }
    }
}
