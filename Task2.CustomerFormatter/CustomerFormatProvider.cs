using System;
using System.Globalization;

namespace Task2.CustomerFormatter {
    public class CustomerFormatProvider : IFormatProvider, ICustomFormatter {
        private readonly IFormatProvider m_ParentProvider;

        #region Constructors
        public CustomerFormatProvider() : this(CultureInfo.CurrentCulture) { }

        public CustomerFormatProvider(IFormatProvider parent) {
            m_ParentProvider = parent;
        }
        #endregion

        #region Public Methods

        public object GetFormat(Type formatType)
            => formatType == typeof (ICustomFormatter) ? this : m_ParentProvider.GetFormat(formatType);

        public string Format(string format, object arg, IFormatProvider formatProvider) {
            Customer customer = arg as Customer;
            if (customer == null)
                throw new ArgumentException("Wrong argument type. Argument name: " + nameof(arg));

            try {
                string result = customer.ToString(format, formatProvider);
                return result;
            } catch (FormatException) {
                
            }

            switch (format) {
                case "E":
                    return $"Customer record: {customer.Name}\n\tRevenue: {customer.Revenue}\n\tContact phone: {customer.ContactPhone}";
                default:
                    throw new FormatException($"The {format} format string not supported");
            }
        }

        #endregion

        #region Private Methods
        #endregion

    }
}
