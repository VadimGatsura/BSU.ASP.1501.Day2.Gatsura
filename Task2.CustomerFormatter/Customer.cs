using System;
using System.Globalization;

namespace Task2.CustomerFormatter {
    public class Customer: IFormattable {
        public string Name { get; private set; } = "Jeffrey Richter";
        public string ContactPhone { get; private set; } = "+1 (425) 555-0100";
        public decimal Revenue { get; private set; } = 1000000;

        public override string ToString() => this.ToString("G", CultureInfo.CurrentCulture);

        public string ToString(string format) => this.ToString(format, CultureInfo.CurrentCulture);

        public string ToString(string format, IFormatProvider formatProvider) {
            if (string.IsNullOrEmpty(format))
                format = "G";
            if (formatProvider == null)
                formatProvider = CultureInfo.CurrentCulture;

            switch (format.ToUpperInvariant()) {
                case "G":
                case "A":
                    return $"Customer record: {Name}, {Revenue.ToString("F2", formatProvider)}, {ContactPhone}";
                case "B":
                    return $"Customer record: {Name}, {Revenue.ToString("F2", formatProvider)}";
                case "D":
                    return $"Customer record: {Name}, {ContactPhone}";
                case "N":
                    return $"Customer record: {Name}";
                case "R":
                    return $"Customer record: {Revenue.ToString("F2", formatProvider)}";
                case "C":
                    return $"Customer record: {ContactPhone}";
                default:
                    throw new FormatException($"The {format} format string not supported");
            }
        }
    }
}
