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

        // This should not be possible
        public static void InvalidAge()
        {
            var user = new User
            {
                Id = -100, // can an id be negative?
                Age = 10000, // won't happen,
                Name = new string('a', count: int.MaxValue) // not a name
            };
        }

        //public static void ValidAgeElseException()
        //{
        //    var age = new Age(100);
        //    var user = new User
        //    {
        //        Age = age, // better, but not perfect
        //    };
        //}

        // best
        public static void ValidAgeElseDoesNotExist()
        {
            var ageOption = Age.Of(100);
            var user = ageOption.Match(
                // forced to make a decision, no nulls! But, what should you do in this case?
                None: () => new User(), 
                Some: (age) => new User { Age = age });
        }

        // map and bind (select and select many)
        public static void DoSomethingWithOption()
        {
            // fake repository
            Func<int, Option<User>> userById = (id) 
                => id % 2 == 0 // when is even
                    ? F.Some(new User())
                    : F.None;

            var userOption = userById(1);

            // map for option is select for IEnumerable<T>
            var shortNameOption = userOption
                .Map(u => u.Name)
                .Map(name => name.Substring(0,3));

            // bind for option is select many for IEnumerable<T>
            var addressesOption = userOption.Bind(u => u.Role);
        }
    }
}
