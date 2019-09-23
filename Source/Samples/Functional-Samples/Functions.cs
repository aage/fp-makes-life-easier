using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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

        // could this be rewritten?
        public static Version GetFileVersion(Type type)
        {
            var location = type.Assembly.Location;
            var fileVersionInfo = FileVersionInfo.GetVersionInfo(location).FileVersion;
            var version = new Version(fileVersionInfo);
            return version;
        }

        // a simple and useful Higher Order Function
        static R Pipe<T, R>(this T t, Func<T, R> f) => f(t);

        // like this?
        public static Func<Type, Version> GetVersionFp =
            type => type.Assembly.Location
                .Pipe(location => FileVersionInfo.GetVersionInfo(location))
                .FileVersion
                .Pipe(fileVersion => new Version(fileVersion));

        public static object GetUserInfo()
        {
            var users = new[] { new User { Id = 1, Age = 20, Name = "Foo" } };
            var user = users.First();
            return new { Age = user.Age, FullName = user.Name };
        }

        public static object GetUserInfoPipe()
        {
            var users = new[] { new User { Id = 1, Age = 20, Name = "Foo" } };
            return users
                .First()
                .Pipe(u => new { Age = u.Age, FullName = u.Name });
        }
    }
}
