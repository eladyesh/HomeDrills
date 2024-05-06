using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpDrillsPart3
{
    /// <summary>
    /// Interface for language converters that convert numbers to their verbal representations in different languages.
    /// </summary>
    public interface ILanguageConverter
    {
        /// <summary>
        /// Convert a given number to its verbal representation.
        /// </summary>
        /// <param name="num">The number to convert.</param>
        /// <returns>The verbal representation of the number.</returns>
        string ToVerbal(long num);
    }

}
