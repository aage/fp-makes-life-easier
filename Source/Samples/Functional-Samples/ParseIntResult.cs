namespace Functional_Samples
{
    public class ParseIntResult
    {
        public ParseIntResult(int result = 0, bool success = true)
        {
            this.Result = result;
            this.Success = true;
        }

        public int Result { get; }
        public bool Success { get; }
    }
}
