using TwistedFizzBuzz.Models;

namespace TwistedFizzBuzz.Interfaces
{
    public interface ITokenProvider
    {
        public Task<List<FizzBuzzRule>> GetRulesFromApiAsync(string apiUrl);
        public List<FizzBuzzRule> GetDefaultRules();
    }
}