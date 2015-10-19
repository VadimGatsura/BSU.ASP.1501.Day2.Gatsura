using System;
using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;
using Task2.CustomerFormatter;

namespace Task2.CustomerFormatterTest {
    [TestFixture]
    class CustomerTest {

        private static readonly Customer m_Customer = new Customer();

        private static readonly string m_Revenue = m_Customer.Revenue.ToString("F2", CultureInfo.InvariantCulture);
        public IEnumerable<TestCaseData> TestDatas {
            get {
                yield return new TestCaseData("A", CultureInfo.InvariantCulture).Returns($"Customer record: {m_Customer.Name}, {m_Revenue}, {m_Customer.ContactPhone}");
                yield return new TestCaseData("C", CultureInfo.InvariantCulture).Returns($"Customer record: {m_Customer.ContactPhone}");
                yield return new TestCaseData("B", CultureInfo.InvariantCulture).Returns($"Customer record: {m_Customer.Name}, {m_Revenue}");
                yield return new TestCaseData("N", CultureInfo.InvariantCulture).Returns($"Customer record: {m_Customer.Name}");
                yield return new TestCaseData("R", CultureInfo.InvariantCulture).Returns($"Customer record: {m_Revenue}");
                yield return new TestCaseData("D", CultureInfo.InvariantCulture).Returns($"Customer record: {m_Customer.Name}, {m_Customer.ContactPhone}");
                yield return new TestCaseData("", CultureInfo.InvariantCulture).Returns($"Customer record: {m_Customer.Name}, {m_Revenue}, {m_Customer.ContactPhone}");
            }
        }

        [Test, TestCaseSource(nameof(TestDatas))]
        public string ToString_Test(string format, IFormatProvider formatProvider) {
            return m_Customer.ToString(format, formatProvider);
        }
    }
}
