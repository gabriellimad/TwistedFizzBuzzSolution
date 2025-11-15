namespace TwistedFizzBuzz
{
    public static class NumberParser
    {
        public static IEnumerable<long> FromRange(long start, long end)
        {
            if (start <= end)
            {
                for (long i = start; i <= end; i++)
                    yield return i;
            }
            else
            {
                for (long i = start; i >= end; i--)
                    yield return i;
            }
        }

        public static IEnumerable<long> FromList(IEnumerable<long> numbers) => numbers;
    }
}
