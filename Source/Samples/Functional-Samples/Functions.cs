using System;
using System.Collections.Generic;

namespace Functional_Samples
{
    public static class Functions
    {
        static Dictionary<int, int> map =
            new Dictionary<int, int>
            {
                { 1, 2 },
                { 2, 3 },
                { 3, 4 },
                { 4, 5 }
                // ...
            };

        // value x to value y
        public static int PlusOne(int x) => map[x];

        // type A to type B
        public static Func<int, string> AsNumber = (num) => num.ToString();

        // This is a dishonest function; it says it will return an int when you give it a string
        // it not only won't do that but it'll also throw exceptions.
        public static Func<string, int> ParseDishonest = int.Parse;

        // This is more honest (but uses a specific type)
        public static ParseIntResult ParseHonest(this string s)
        {
            int i;
            return int.TryParse(s, out i)
                ? new ParseIntResult(result: i)
                : new ParseIntResult(success: false);
        }
    }
}
