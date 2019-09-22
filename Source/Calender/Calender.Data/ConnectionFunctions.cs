using System;
using System.Data;
using System.Data.SqlClient;
using LaYumba.Functional;

namespace Calender.Data
{
    using static F;

    public static class ConnectionFunctions
    {
        // based on an example in: Functional Programming in C# (Enrinco Buonanno).
        public static T Connect<T>
            (ConnectionString connStr,
            Func<IDbConnection, T> f)
        {
            return Using(
                () => new SqlConnection(connStr), f);
        }

        public class ConnectionString
        {
            readonly string value;

            private ConnectionString(string value)
            {
                this.value = value;
            }

            public static implicit operator ConnectionString(string s)
                => new ConnectionString(s);

            public static implicit operator string(ConnectionString c)
                => c.value;
        }
    }
}
