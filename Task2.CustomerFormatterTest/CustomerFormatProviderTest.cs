using System;
using System.Collections.Generic;
using NUnit.Framework;
using Task2.CustomerFormatter;

namespace Task2.CustomerFormatterTest {
    [TestFixture]
    public class CustomerFormatProviderTest {

        private static readonly Customer m_Customer = new Customer();

        public IEnumerable<TestCaseData> TestDatas {
            get {
                yield return new TestCaseData(m_Customer, "Customer record: {0:N, R, C}").Returns($"Customer record: {m_Customer.Name}, {m_Customer.Revenue}, {m_Customer.ContactPhone}");
                yield return new TestCaseData(m_Customer, "Customer record: {0:C}").Returns($"Customer record: {m_Customer.ContactPhone}");
                yield return new TestCaseData(m_Customer, "Customer record: {0:N, R}").Returns($"Customer record: {m_Customer.Name}, {m_Customer.Revenue}");
                yield return new TestCaseData(m_Customer, "Customer record: {0:N}").Returns($"Customer record: {m_Customer.Name}");
                yield return new TestCaseData(m_Customer, "Customer record: {0:R}").Returns($"Customer record: {m_Customer.Revenue}");
                yield return new TestCaseData(m_Customer, "{0:N, C}").Returns($"{m_Customer.Name}, {m_Customer.ContactPhone}");
                yield return new TestCaseData(m_Customer, "Customer record:\nName: {0:N}\nRevenue: {0:R}\nContactPhone: {0:C}").Returns($"Customer record:\nName: {m_Customer.Name}\nRevenue: {m_Customer.Revenue}\nContactPhone: {m_Customer.ContactPhone}");

                yield return new TestCaseData(null, "Customer record: {0:N, R, C}").Throws(typeof(ArgumentException));
                yield return new TestCaseData(m_Customer, "Customer record: {0:B}").Throws(typeof(ArgumentException));
            }
        } 
        
        [Test, TestCaseSource(nameof(TestDatas))]
        public string Format_Test(Customer customer, string format) {
            IFormatProvider fp = new CustomerFormatProvider();
            return string.Format(fp, format, customer);    
        }
    }
}
