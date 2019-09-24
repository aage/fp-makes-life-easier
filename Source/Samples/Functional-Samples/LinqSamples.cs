using System.Linq;

namespace Functional_Samples
{
    public static class LinqSamples
    {
        // to compare with map and bind
        public static void DoSomethingWithEnumerable()
        {
            // we can use IEnumerable<T>, we don't know whether it is empty
            var users = Enumerable.Range(1, 10).Select(_ => new User());

            var names = users.Select(u => u.Name); // similar to map
            var emailAddresses = users.SelectMany(u => u.EmailAddresses); // similar to bind
        }
    }
}
