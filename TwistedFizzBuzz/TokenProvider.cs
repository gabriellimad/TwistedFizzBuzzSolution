using System.Text.Json;
using TwistedFizzBuzz.Interfaces;
using TwistedFizzBuzz.Models;

namespace TwistedFizzBuzz
{
    public class TokenProvider : ITokenProvider
    {
        private readonly HttpClient _httpClient;
        public TokenProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0");
        }

        public async Task<List<FizzBuzzRule>> GetRulesFromApiAsync(string apiUrl)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);

                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var rule = JsonSerializer.Deserialize<FizzBuzzRule>(content);

                if (rule == null)
                    return new List<FizzBuzzRule>();

                return new List<FizzBuzzRule> { rule };
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP error fetching rules: {ex.Message}");
                return new List<FizzBuzzRule>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON deserialization error: {ex.Message}");
                return new List<FizzBuzzRule>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                return new List<FizzBuzzRule>();
            }
        }

        public List<FizzBuzzRule> GetDefaultRules() => new()
        { 
            new FizzBuzzRule(3, "Fizz"),
            new FizzBuzzRule(5, "Buzz"),
        };
    }
}
