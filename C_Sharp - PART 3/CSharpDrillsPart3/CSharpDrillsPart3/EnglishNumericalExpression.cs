using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDrillsPart3
{
    /// <summary>
    /// Class for converting numbers to their verbal representation in English.
    /// </summary>
    public class EnglishNumericalExpression : ILanguageConverter
    {
        /// <summary>
        /// Convert a given number to its verbal representation.
        /// </summary>
        /// <param name="expressionNumber">The number to convert.</param>
        /// <returns>The verbal representation of the number.</returns>
        public string ToVerbal(long expressionNumber)
        {
            // Arrays to hold string representations of units, teens, and tens.
            string[] units = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
            string[] teens = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            string[] tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            // If the number is zero, return "Zero".
            if (expressionNumber == 0)
            {
                return "Zero";
            }

            string result = "";

            // If the number is negative, add "Minus" to the result and make the number positive.
            if (expressionNumber < 0)
            {
                result += "Minus ";
                expressionNumber = -expressionNumber;
            }

            // Convert each group of digits (quadrillion, trillion, billion, etc.) to words.
            if ((expressionNumber / 1000000000000000) > 0)
            {
                result += ConvertToEnglishWords(expressionNumber / 1000000000000000) + " Quadrillion ";
                expressionNumber %= 1000000000000000;
            }

            if ((expressionNumber / 1000000000000) > 0)
            {
                result += ConvertToEnglishWords(expressionNumber / 1000000000000) + " Trillion ";
                expressionNumber %= 1000000000000;
            }

            if ((expressionNumber / 1000000000) > 0)
            {
                result += ConvertToEnglishWords(expressionNumber / 1000000000) + " Billion ";
                expressionNumber %= 1000000000;
            }

            if ((expressionNumber / 1000000) > 0)
            {
                result += ConvertToEnglishWords(expressionNumber / 1000000) + " Million ";
                expressionNumber %= 1000000;
            }

            if ((expressionNumber / 1000) > 0)
            {
                result += ConvertToEnglishWords(expressionNumber / 1000) + " Thousand ";
                expressionNumber %= 1000;
            }

            if ((expressionNumber / 100) > 0)
            {
                result += units[expressionNumber / 100] + " Hundred ";
                expressionNumber %= 100;
            }

            // Convert the remaining number less than 100 to words.
            if (expressionNumber > 0)
            {
                // If there is already some result, add "And ".
                if (result != "")
                {
                    result += "And ";
                }

                // Convert numbers less than 10 directly, and numbers between 10 and 99 using the tens and units arrays.
                if (expressionNumber < 10)
                {
                    result += units[expressionNumber];
                }
                else if (expressionNumber < 20)
                {
                    result += teens[expressionNumber - 10];
                }
                else
                {
                    result += tens[expressionNumber / 10];
                    if ((expressionNumber % 10) > 0)
                    {
                        result += "-" + units[expressionNumber % 10];
                    }
                }
            }

            return result.Trim();
        }

        /// <summary>
        /// Convert a given number to words. (In English)
        /// </summary>
        /// <param name="num">The number to convert.</param>
        /// <returns>The number converted to words.</returns>
        public string ConvertToEnglishWords(long num)
        {
            // Arrays to hold string representations of units, teens, and tens.
            string[] units = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
            string[] teens = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            string[] tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            string result = "";

            // Convert the hundreds place if present.
            if ((num / 100) > 0)
                result += units[num / 100] + " Hundred ";
            {
                num %= 100;
            }

            // Convert the remaining number less than 100 to words.
            if (num > 0)
            {
                // If there is already some result, add "And ".
                if (result != "")
                {
                    result += "And ";
                }

                // Convert numbers less than 10 directly, and numbers between 10 and 99 using the tens and units arrays.
                if (num < 10)
                {
                    result += units[num];
                }
                else if (num < 20)
                {
                    result += teens[num - 10];
                }
                else
                {
                    result += tens[num / 10];
                    if ((num % 10) > 0)
                    {
                        result += "-" + units[num % 10];
                    }
                }
            }

            return result;
        }

    }

    // This class represents spanish numeric conversion
    // Also allowing to implement conversion for number over 999 trillion
    public class SpanishNumericalExpression : ILanguageConverter
    {
        public string ToVerbal(long expressionNumber)
        {
            return "Spanish verbal representation implementation here";
        }
    }
}
