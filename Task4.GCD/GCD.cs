using System.Diagnostics;
using System.Linq;

namespace Task4.GCD {
    public static class GCD {

        #region Public Methods

        #region Euclidean
        /// <summary>
        /// Calculate the greatest common divisor by Euclidean algorithm
        /// </summary>
        /// <param name="a">The first number</param>
        /// <param name="b">The second number</param>
        /// <returns>The greatest common divisor</returns>
        public static long Euclidean(long a, long b) {
            if (a < 0)
                a *= -1;
            if (b < 0)
                b *= -1;
            while (b != 0) {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        /// <summary>
        /// Calculate the greatest common divisor by Euclidean algorithm
        /// </summary>
        /// <param name="a">The first number</param>
        /// <param name="b">The second number</param>
        /// <param name="ticks">The number of timer ticks that have been spent on the calculation</param>
        /// <returns>The greatest common divisor</returns>
        public static long Euclidean(long a, long b, out long ticks) {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            long result = Euclidean(a, b);
            timer.Stop();
            ticks = timer.ElapsedTicks;
            return result;
        }

        /// <summary>
        /// Calculate the greatest common divisor by Euclidean algorithm
        /// </summary>
        /// <param name="array">Array of parameters for calculating the greatest common divisor</param>
        /// <returns>The greatest common divisor</returns>
        public static long Euclidean(params long[] array) {
            long result = 0;
            for (int i = 0; i < array.Length; i++) {
                result = Euclidean(result, array[i]);
            }
            return result;
        }

        /// <summary>
        /// Calculate the greatest common divisor by Euclidean algorithm
        /// </summary>
        /// <param name="ticks">The number of timer ticks that have been spent on the calculation</param>
        /// <param name="array">Array of parameters for calculating the greatest common divisor</param>
        /// <returns>The greatest common divisor</returns>
        public static long Euclidean(out long ticks, params long[] array) {
            long result = 0;
            ticks = 0;
            long t;
            for (int i = 0; i < array.Length; i++) {
                result = Euclidean(result, array[i], out t);
                ticks += t;
            }
            return result;
        }
        #endregion

        #region Stein
        /// <summary>
        /// Calculate the greatest common divisor by Stein algorithm
        /// </summary>
        /// <param name="a">The first number</param>
        /// <param name="b">The second number</param>
        /// <returns>The greatest common divisor</returns>
        public static long Stein(long a, long b) {
            if (a < 0)
                a *= -1;
            if (b < 0)
                b *= -1; 
            if (a == b)
                return b;

            if (a == 0)
                return b;

            if (b == 0)
                return a;

            if (a %2 == 0) { 
                if ( b % 2 != 0)
                    return Stein(a >> 1, b);
                return Stein(a >> 1, b >> 1) << 1;
            }
            if (b % 2 == 0) 
                return Stein(a, b >> 1);

            if (a > b)
                return Stein((a - b) >> 1, b);

            return Stein((b - a) >> 1, a);
        }

        /// <summary>
        /// Calculate the greatest common divisor by Stein algorithm
        /// </summary>
        /// <param name="a">The first number</param>
        /// <param name="b">The second number</param>
        /// <param name="ticks">The number of timer ticks that have been spent on the calculation</param>
        /// <returns>The greatest common divisor</returns>
        public static long Stein(long a, long b, out long ticks) {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            long result = Stein(a, b);
            timer.Stop();
            ticks = timer.ElapsedTicks;
            return result;
        }

        /// <summary>
        /// Calculate the greatest common divisor by Stein algorithm
        /// </summary>
        /// <param name="array">Array of parameters for calculating the greatest common divisor</param>
        /// <returns>The greatest common divisor</returns>
        public static long Stein(params long[] array) {
            return array.Aggregate<long, long>(0, Stein);
        }

        /// <summary>
        /// Calculate the greatest common divisor by Stein algorithm
        /// </summary>
        /// <param name="ticks">The number of timer ticks that have been spent on the calculation</param>
        /// <param name="array">Array of parameters for calculating the greatest common divisor</param>
        /// <returns>The greatest common divisor</returns>
        public static long Stein(out long ticks, params long[] array) {
            long result = 0;
            ticks = 0;
            long t;
            foreach (long element in array) {
                result = Stein(result, element, out t);
                ticks += t;
            }
            return result;
        }
        #endregion

        #endregion
    }
}
