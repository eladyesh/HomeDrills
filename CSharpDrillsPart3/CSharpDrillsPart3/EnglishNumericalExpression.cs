using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDrillsPart3
{
    public interface ILanguageConverter
    {
        string ConvertToWords(long num);
    }
    public class EnglishNumericalExpression : ILanguageConverter
    {
        private long number;

        public EnglishNumericalExpression(long number)
        {
            this.number = number;
        }

        public override string ToString()
        {
            /*
             * Convert the number to its verbal expression.
             *
             * Returns:
             *     string: The verbal expression of the number.
             */
            string[] units = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
            string[] teens = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            string[] tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            if (number == 0)
            {
                return "Zero";
            }

            string result = "";

            if (number < 0)
            {
                result += "Minus ";
                number = -number;
            }

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

            if (number > 0)
            {
                if (result != "")
                {
                    result += "And ";
                }

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

        public long GetValue()
        {
            /*
             * Get the number value.
             *
             * Returns:
             *     long: The value of the number.
             */
            return number;
        }

        public static int SumLetters(Func<EnglishNumericalExpression, long> numberFunction, long num)
        {
            /*
             * Calculate the sum of letters needed to write all numerical expressions from 0 to the given number.
             *
             * Args:
             *     numberFunction (Func<NumericalExpression, long>): Function to create NumericalExpression instances.
             *     num (long): The number up to which the sum is calculated.
             *
             * Returns:
             *     int: The sum of letters.
             */

            // The principle 
            int sum = 0;
            for (long i = 0; i <= num; i++)
            {
                sum += numberFunction(new EnglishNumericalExpression(i)).ToString().Replace(" ", "").Length;
            }
            return sum;
        }

        public string ConvertToWords(long num)
        {
            string[] units = { "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
            string[] teens = { "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
            string[] tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

            string result = "";

            if ((num / 100) > 0)
            {
                result += units[num / 100] + " Hundred ";
                num %= 100;
            }

            if (num > 0)
            {
                if (result != "")
                {
                    result += "And ";
                }

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
