using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

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
            
            Regex reg = new Regex("[NRC\\s\\W]{1}");
            if (!reg.IsMatch(format))
                throw new ArgumentException("Unsupported format");
            
            StringBuilder result = new StringBuilder(format);
            int nIndex = format.IndexOf("N", StringComparison.Ordinal);
            int rIndex = format.IndexOf("R", StringComparison.Ordinal);
            int cIndex = format.IndexOf("C", StringComparison.Ordinal);

            int[] indexArrays = { nIndex, rIndex, cIndex};
            char[] letterArray = {'N', 'R', 'C'};

            Dictionary<char, string> dictionary = new Dictionary<char,string> {
                {'N', customer.Name },
                { 'R', customer.Revenue.ToString(CultureInfo.InvariantCulture) },
                { 'C', customer.ContactPhone }
            };
            
            for(int i = 3; i > 0; i--)
                for(int j = 0; j < i - 1; j++)
                    if (indexArrays[j] > indexArrays[j + 1]) {
                        int tempIndex = indexArrays[j];
                        char tempLetter = letterArray[j];
                        indexArrays[j] = indexArrays[j + 1];
                        letterArray[j] = letterArray[j + 1];
                        indexArrays[j + 1] = tempIndex;
                        letterArray[j + 1] = tempLetter;
                    }

            int insertIndex = 0;
            for (int i = 0; i < 3; i++) {
                if (indexArrays[i] >= 0) {
                    insertIndex += indexArrays[i];
                    if (i > 0 && indexArrays[i - 1] > 0)
                        insertIndex -= indexArrays[i - 1];
                    string insertString = dictionary[letterArray[i]];
                    result = result.Remove(insertIndex, 1).Insert(insertIndex, insertString);
                    insertIndex += insertString.Length - 1;
                }
            }
            return result.ToString();
        }

        #endregion

        #region Private Methods
        #endregion

    }
}
