using System;

namespace Functional_Samples
{
    using static F;

    // Example from: "Functional Programming in C#" book (Enrico Buonanno)
    public class Age
    {
        private Age(int value)
        {
            this.Value = value;
        }

        public int Value { get; }
        public static implicit operator int(Age age) => age.Value;

        public static Option<Age> Of(int value)
            => IsValid(value)
                ? Some(new Age(value))
                : None;

        static bool IsValid(int value) => value > 18 && value < 120; // the business should be able to tell you
    }
}
