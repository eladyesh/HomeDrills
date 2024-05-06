using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDrillsPart3
{
    // ==================== EXERCISE 7 ====================
    // This interface used for all classes that want to inherit from it,
    // and implement ConvertToWords and ToString in their own language (e.g French) or implement numbers above 999 trillion
    public interface ILanguageConverter
    {
        /// <summary>
        /// Convert a given number to words.
        /// </summary>
        /// <param name="num">The number to convert.</param>
        /// <returns>The number converted to words.</returns>
        string ConvertToWords(long num);

        /// <summary>
        /// Returns a string representation of the language converter.
        /// </summary>
        /// <returns>A string representation of the language converter.</returns>
        string ToString();
    }
    public class EnglishNumericalExpression : ILanguageConverter
    {
        private long number;

        /// <summary>
        /// Initializes a new instance of the EnglishNumericalExpression class.
        /// </summary>
        /// <param name="number">The number to represent.</param>
        public EnglishNumericalExpression(long number)
        {
            this.number = number;
        }

        public override string ToString()
        {
            // Arrays to hold string representations of units, teens, and tens.
            string[] units = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
            string[] teens = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            string[] tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            // If the number is zero, return "Zero".
            if (number == 0)
            {
                return "Zero";
            }

            string result = "";

            // If the number is negative, add "Minus" to the result and make the number positive.
            if (number < 0)
            {
                result += "Minus ";
                number = -number;
            }

            // Convert each group of digits (quadrillion, trillion, billion, etc.) to words.
            if ((number / 1000000000000000) > 0)
            {
                result += ConvertToWords(number / 1000000000000000) + " Quadrillion ";
                number %= 1000000000000000;
            }

            if ((number / 1000000000000) > 0)
            {
                result += ConvertToWords(number / 1000000000000) + " Trillion ";
                number %= 1000000000000;
            }

            if ((number / 1000000000) > 0)
            {
                result += ConvertToWords(number / 1000000000) + " Billion ";
                number %= 1000000000;
            }

            if ((number / 1000000) > 0)
            {
                result += ConvertToWords(number / 1000000) + " Million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                result += ConvertToWords(number / 1000) + " Thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                result += units[number / 100] + " Hundred ";
                number %= 100;
            }

            // Convert the remaining number less than 100 to words.
            if (number > 0)
            {
                // If there is already some result, add "And ".
                if (result != "")
                {
                    result += "And ";
                }

                // Convert numbers less than 10 directly, and numbers between 10 and 99 using the tens and units arrays.
                if (number < 10)
                {
                    result += units[number];
                }
                else if (number < 20)
                {
                    result += teens[number - 10];
                }
                else
                {
                    result += tens[number / 10];
                    if ((number % 10) > 0)
                    {
                        result += "-" + units[number % 10];
                    }
                }
            }

            return result.Trim();
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
        /// <param name="expressionValueExtractor">A function that converts a number to words.</param>
        /// <param name="upperLimit">The upper limit of numbers to consider.</param>
        /// <returns>The sum of the lengths of the words for numbers from 0 to the given number.</returns>
        public static int SumLetters(Func<EnglishNumericalExpression, long> expressionValueExtractor, long upperLimit)
        {
            //  העקרון שממומש פה הוא אינקפסולציה. כימו
            // new EnglishNumericalExpression(i)).ToString() כאשר אנחנו משתמשים בפונקציה 
            // SumLetters אנחנו מסתירים את המימוש שלה מ 
            // EnglishNumericalExpression ובכך הפונקציה רק יודעת שהיא מקבלת את 
            // ToString() עם מספר, והיא יכולה לקרוא ל  
            // שלו ולקבל אותו וורבלי, ללא ידיעה על פרטים נוספים מהמחלקה

            int sum = 0;
            for (long i = 0; i <= upperLimit; i++)
            {
                sum += expressionValueExtractor(new EnglishNumericalExpression(i)).ToString().Replace(" ", "").Length;
            }
            return sum;
        }

        /// <summary>
        /// Convert a given number to words.
        /// </summary>
        /// <param name="num">The number to convert.</param>
        /// <returns>The number converted to words.</returns>
        public string ConvertToWords(long num)
        {
            // Arrays to hold string representations of units, teens, and tens.
            string[] units = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
            string[] teens = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            string[] tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            string result = "";

            // Convert the hundreds place if present.
            if ((num / 100) > 0)
            {
                result += units[num / 100] + " Hundred ";
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
}
