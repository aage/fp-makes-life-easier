using System;

namespace Functional_Samples
{
    // Example from: "Functional Programming in C#" book (Enrico Buonanno)
    public class Age
    {
        public Age(int value)
        {
            if (!IsValid(value))
            {
                throw new ArgumentException("An 'Age' needs to be in the 18-120 range.");
            }

            this.Value = value;
        }

        public int Value { get; }
        public static implicit operator int(Age age) => age.Value;

        static bool IsValid(int value) => value > 18 && value < 120; // the business should be able to tell you
    }
}
