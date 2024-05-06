using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDrillsPart3
{
    // ====================================================
    // ==================== EXERCISE 7 ====================
    // ====================================================
    /// <summary>
    /// Represents a numerical expression that converts numbers to their verbal representations.
    /// </summary>
    public class NumericalExpression
    {
        private long number;
        private Func<long, string> convertToWordsFunc;

        /// <summary>
        /// Initializes a new instance of the NumericalExpression class.
        /// </summary>
        /// <param name="number">The number to represent.</param>
        /// <param name="convertToWordsFunc">The function used to convert numbers to words.</param>
        public NumericalExpression(long number, Func<long, string> convertToWordsFunc)
        {
            this.number = number;
            this.convertToWordsFunc = convertToWordsFunc;
        }

        /// <summary>
        /// Converts the current number to its verbal representation.
        /// </summary>
        /// <returns>The verbal representation of the number.</returns>
        public override string ToString()
        {
            return this.convertToWordsFunc(this.number);
        }

        /// <summary>
        /// Get the numeric value represented by this instance.
        /// </summary>
        /// <returns>The numeric value represented by this instance.</returns>
        public long GetValue()
        {
            return number;
        }

        /// <summary>
        /// Sum the lengths of the words for numbers from 0 to the given number.
        /// </summary>
        /// <param name="upperLimit"></param>
        /// <returns>The sum of the lengths of the words for numbers from 0 to the given number.</returns>
        public static int SumLetters(long upperLimit, Func<long, string> convertToWordsFunc)
        {
            int sum = 0;
            for (long i = 0; i <= upperLimit; i++)
            {
                sum += new NumericalExpression(i, convertToWordsFunc).ToString().Replace(" ", "").Length;
            }
            return sum;
        }

        /// <summary>
        /// Sum the lengths of the words for numbers from 0 to the given number represented by the EnglishNumericalExpression instance.
        /// </summary
        /// <param name="numericalExpression">The NumericalExpression instance representing the upper limit of the range.</param>
        /// <param name="convertToWordsFunc">The 'convert number to verbal' function the NumericalExpression class would use 
        /// for the builder and then 'ToString() method.</param>
        /// <returns>The sum of the lengths of the words for numbers from 0 to the given number.</returns>
        /// 
        /// 
        /// <remarks>
        /// The second function showcases overloading, an OOP principle. It allows for multiple versions of the same function with different parameter types, enhancing flexibility and code clarity.
        /// By providing different versions of SumLetters in NumericalExpression — one accepting a long and the other an NumericalExpression.
        /// </remarks>
        /// 
        ///
        public static int SumLetters(NumericalExpression numericalExpression, Func<long, string> convertToWordsFunc)
        {
            long upperLimit = numericalExpression.GetValue();
            int sum = 0;
            for (long i = 0; i <= upperLimit; i++)
            {
                sum += new NumericalExpression(i, convertToWordsFunc).ToString().Replace(" ", "").Length;
            }
            return sum;
        }

    }
}
