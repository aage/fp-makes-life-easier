using LaYumba.Functional;

namespace Calender.Domain.ValueObjects
{
    using static F;

    public class String100
    {
        private String100(string value)
        {
            this.Value = value;
        }

        public string Value { get; }

        public static implicit operator string(String100 s) => s.Value;

        public static Option<String100> Of(string value)
            => IsValid(value)
                ? Some(new String100(value))
                : None;

        static bool IsValid(string value)
            => !string.IsNullOrWhiteSpace(value)
                && value.Length < 101;
    }

    public class String1000
    {
        private String1000(string value)
        {
            this.Value = value;
        }

        public string Value { get; }

        public static implicit operator string(String1000 s) => s.Value;

        public static Option<String1000> Of(string value)
            => IsValid(value)
                ? Some(new String1000(value))
                : None;

        static bool IsValid(string value)
            => !string.IsNullOrWhiteSpace(value)
                && value.Length < 1001;
    }
}
