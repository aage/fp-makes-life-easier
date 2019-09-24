using System;

namespace Functional_Samples
{
    // Taken from: "Functional Programming in C#" book (Enrico Buonanno)
    public static partial class F
    {
        public static Option<T> Some<T>(T value) => new Option.Some<T>(value); // wrap the given value into a Some
        public static Option.None None => Option.None.Default;  // the None value
    }

    namespace Option
    {
        public struct None { internal static readonly None Default = new None(); }

        public struct Some<T>
        {
            internal T Value { get; }
            internal Some(T value)
            {
                if (value == null)
                    throw new ArgumentNullException();
                Value = value;
            }
        }
    }

    public struct Option<T>
    {
        readonly bool isSome;
        readonly T value;

        private Option(T value)
        {
            this.isSome = true;
            this.value = value;
        }

        public static implicit operator Option<T>(Option.None _) => new Option<T>();

        public static implicit operator Option<T>(Option.Some<T> some) => new Option<T>(some.Value);

        public R Match<R>(Func<R> None, Func<T, R> Some) => isSome ? Some(value) : None();
    }
}
