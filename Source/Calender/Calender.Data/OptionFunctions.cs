using System.IO;
using LaYumba.Functional;

namespace Calender.Data
{
    internal static class OptionFunctions
    {
        // When certain the data is correct we can force option creation
        // to avoid redundant creation ceremony.

        internal static T ValueUnsafe<T>(this Option<T> option)
            => option.Match(
                () => throw new InvalidDataException("Given option was None"),
                (some) => some);
    }
}
