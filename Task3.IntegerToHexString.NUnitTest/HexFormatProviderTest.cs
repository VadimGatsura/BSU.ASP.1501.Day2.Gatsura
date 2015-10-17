using System;
using NUnit.Framework;

namespace Task3.IntegerToHexString.NUnitTest {

    [TestFixture]
    public class HexFormatProviderTest {

        [TestCase(145, Result = "0x91")]
        [TestCase(-145, Result = "-0x91")]
        [TestCase(0, Result = "0x0")]
        [TestCase(41837, Result = "0xA36D")]
        public string FormatTest(int number) {
            IFormatProvider fp = new HexFormatProvider();
            return string.Format(fp, "{0:H}", number);
        }
    }
}
