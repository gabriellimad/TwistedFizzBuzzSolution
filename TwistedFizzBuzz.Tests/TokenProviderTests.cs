using Microsoft.Extensions.Configuration;
using TwistedFizzBuzz.Interfaces;
using TwistedFizzBuzz.Models;
using TwistedFizzBuzz.Tests.Helpers;

namespace TwistedFizzBuzz.Tests
{
    public class TokenProviderTests
    {
        [Trait("Category", "Integration")]
        [Fact]
        public async Task Should_Receive_Valid_TokenRule_From_Api()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var apiUrl = config["ApiSettings:TokenApiUrl"];
            ITokenProvider provider = new TokenProvider(new HttpClient());

            var tokenRule = new List<FizzBuzzRule>();

            if (apiUrl != null)
                tokenRule = await provider.GetRulesFromApiAsync(apiUrl);

            Assert.NotNull(tokenRule);
            Assert.NotEmpty(tokenRule);

            var rule = tokenRule.First();

            Assert.False(string.IsNullOrWhiteSpace(rule.Token));
            Assert.True(rule.Divisor > 0);
        }

        [Trait("Category", "Integration")]
        [Fact]
        public async Task Should_Apply_External_TokenRule_In_FizzBuzzEngine()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var apiUrl = config["ApiSettings:TokenApiUrl"];
            ITokenProvider provider = new TokenProvider(new HttpClient());

            var rules = new List<FizzBuzzRule>();
            if (apiUrl != null)
                rules = await provider.GetRulesFromApiAsync(apiUrl);

            var rule = rules.First();

            var numbers = NumberParser.FromRange(rule.Divisor, rule.Divisor);
            var engine = new FizzBuzzEngine();
            var output = engine.Execute(numbers, rules).ToList();

            Assert.Single(output);
            Assert.Equal(rule.Token, output[0]);
        }

        [Trait("Category", "Integration")]
        [Fact]
        public async Task Should_Handle_Api_Failure_Gracefully()
        {
            var fakeHandler = new HttpMessageHandlerStub((request, cancellationToken) =>
            {
                var response = new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
                return Task.FromResult(response);
            });

            var httpClient = new HttpClient(fakeHandler);
            ITokenProvider provider = new TokenProvider(httpClient);

            var rules = await provider.GetRulesFromApiAsync("https://fake-url.com");

            Assert.Empty(rules);
        }
    }
}
