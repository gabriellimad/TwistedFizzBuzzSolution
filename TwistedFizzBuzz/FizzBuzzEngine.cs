using TwistedFizzBuzz.Models;

namespace TwistedFizzBuzz
{
    public class FizzBuzzEngine
    {
        public IEnumerable<string> Execute(IEnumerable<long> numbers, IEnumerable<FizzBuzzRule> rules)
        {
            var safeNumbers = numbers ?? Enumerable.Empty<long>();
            var safeRules = rules ?? Enumerable.Empty<FizzBuzzRule>();

            foreach (long number in safeNumbers)
            {
                var tokens = safeRules
                    .Where(r => r != null && r.Divisor != 0 && number % r.Divisor == 0)
                    .Select(r => r.Token)
                    .Where(token => !string.IsNullOrEmpty(token))
                    .ToList();

                yield return tokens.Any() ? string.Join("", tokens) : number.ToString();
            }
        }

    }
}