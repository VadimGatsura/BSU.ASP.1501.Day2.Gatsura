using System;
using System.Collections.Generic;
using System.Globalization;
using NUnit.Framework;
using Task2.CustomerFormatter;

namespace Task2.CustomerFormatterTest {
    [TestFixture]
    public class CustomerFormatProviderTest {

        private static readonly Customer m_Customer = new Customer();
        //Customer record: Jeffrey Richter, 1000000.00, +1 (425) 555-0100"
        public IEnumerable<TestCaseData> TestDatas {
            get {
                yield return new TestCaseData(m_Customer, "{0:A}").Returns(m_Customer.ToString("A", CultureInfo.InvariantCulture));

                yield return new TestCaseData(m_Customer, "{0:E}").Returns($"Customer record: {m_Customer.Name}\n\tRevenue: {m_Customer.Revenue}\n\tContact phone: {m_Customer.ContactPhone}");

                yield return new TestCaseData(m_Customer, "{0:W}").Throws(typeof(FormatException));

                yield return new TestCaseData(m_Customer, "{0:C}").Returns(m_Customer.ToString("C", CultureInfo.InvariantCulture));
            }
        } 
        
        [Test, TestCaseSource(nameof(TestDatas))]
        public string Format_Test(Customer customer, string format) {
            IFormatProvider fp = new CustomerFormatProvider();
            return string.Format(fp, format, customer);    
        }
    }
}
