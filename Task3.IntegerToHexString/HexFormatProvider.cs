using System;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Task3.IntegerToHexString {
    public class HexFormatProvider : IFormatProvider, ICustomFormatter {

        private readonly IFormatProvider m_ParentProvider;

        #region Constructors
        public HexFormatProvider() : this(CultureInfo.CurrentCulture) { }
        public HexFormatProvider(IFormatProvider parent) {
            m_ParentProvider = parent;
        }
        #endregion

        #region Public Methods
        public object GetFormat(Type formatType) => formatType == typeof (ICustomFormatter) ? this : m_ParentProvider.GetFormat(formatType);
        
        public string Format(string format, object arg, IFormatProvider formatProvider) {
            if (arg == null || format != "H")
                return string.Format(m_ParentProvider, $"{{0:{format}}}", arg);

            if (!(arg is int) && !(arg is long)) 
                throw new ArgumentException("Wrong argument type. Argument name: " + nameof(arg));    

            long number = Convert.ToInt64(arg);
            return GetHexString(number);
            
        }
        #endregion

        #region Private methods

        private string GetHexString(long number) {
            string[] hexNumbers = "0 1 2 3 4 5 6 7 8 9 A B C D E F".Split();
            StringBuilder result = new StringBuilder();
            bool needMinus = false;
            if (number < 0) {
                number *= -1;
                needMinus = true;
            }

            do {
                int ch = (int)(number % 16);
                result.Append(hexNumbers[ch]);
                number /= 16;
            } while (number > 0);

            return (needMinus ? "-" : "") + "0x" + new string(result.ToString().Reverse().ToArray());
        }
        #endregion
    }
}
