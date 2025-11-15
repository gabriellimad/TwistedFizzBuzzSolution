using System.Text.Json.Serialization;

namespace TwistedFizzBuzz.Models
{
    public class FizzBuzzRule
    {
        [JsonPropertyName("number")]
        public int Divisor { get; init; }
        [JsonPropertyName("word")]
        public string Token { get; init; }

        public FizzBuzzRule(int divisor, string token)
        {
            Divisor = divisor;
            Token = token;
        }
    }
}
