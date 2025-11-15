using Microsoft.Extensions.Configuration;
using TwistedFizzBuzz.Interfaces;
using TwistedFizzBuzz.Models;

namespace TwistedFizzBuzz.Tests
{
    public class FizzBuzzEngineTests
    {
        [Trait("Category", "Unit")]
        [Fact]
        public void Should_Return_Fizz_When_Divisible_By_3()
        {
            var engine = new FizzBuzzEngine();
            var numbers = new List<long> { 3 };
            var rules = new List<FizzBuzzRule> { new(3, "Fizz") };

            var result = engine.Execute(numbers, rules);

            Assert.Equal("Fizz", result.First());
        }

        [Trait("Category", "Unit")]
        [Fact]
        public void Should_Return_Buzz_When_Divisible_By_5()
        {
            var engine = new FizzBuzzEngine();
            var numbers = new List<long> { 5 };
            var rules = new List<FizzBuzzRule> { new(5, "Buzz") };

            var result = engine.Execute(numbers, rules);

            Assert.Equal("Buzz", result.First());
        }

        [Trait("Category", "Unit")]
        [Fact]
        public void Should_Return_FizzBuzz_When_Divisible_By_3_And_5()
        {
            var engine = new FizzBuzzEngine();
            var numbers = new List<long> { 15 };
            var rules = new TokenProvider(new HttpClient()).GetDefaultRules();

            var result = engine.Execute(numbers, rules);

            Assert.Equal("FizzBuzz", result.First());
        }

        [Trait("Category", "Unit")]
        [Fact]
        public void Should_Print_Numbers_When_No_Rules_Provided()
        {
            var engine = new FizzBuzzEngine();
            var numbers = new List<long> { 1, 2, 3 };
            IEnumerable<FizzBuzzRule>? rules = null;

            var result = engine.Execute(numbers, rules).ToList();

            Assert.Equal(new List<string> { "1", "2", "3" }, result);
        }

        [Trait("Category", "Unit")]
        [Fact]
        public void Should_Skip_Invalid_Rules_With_Zero_Divisor()
        {
            var engine = new FizzBuzzEngine();
            var numbers = new List<long> { 5 };
            var rules = new List<FizzBuzzRule> { new(0, "Skip") };

            var result = engine.Execute(numbers, rules).ToList();

            Assert.Equal("5", result[0]);
        }

        [Trait("Category", "Unit")]
        [Fact]
        public void Should_Skip_Rules_With_Null_Or_Empty_Token()
        {
            var engine = new FizzBuzzEngine();
            var numbers = new List<long> { 10 };
            var rules = new List<FizzBuzzRule>
            {
                new(5, ""),
                new(10, null!)
            };

            var result = engine.Execute(numbers, rules).ToList();

            Assert.Equal("10", result[0]);
        }

        [Trait("Category", "Unit")]
        [Fact]
        public void Should_Process_Multiple_Numbers_With_Multiple_Rules()
        {
            var engine = new FizzBuzzEngine();
            var numbers = NumberParser.FromRange(1, 5);
            var rules = new List<FizzBuzzRule> { new(2, "A"), new(3, "B") };

            var result = engine.Execute(numbers, rules).ToList();

            Assert.Equal(new List<string> { "1", "A", "B", "A", "5" }, result);
        }

        [Trait("Category", "Integration")]
        [Fact]
        public async Task Should_Print_Custom_Token_When_ApiRule_Applied()
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

            long max = tokenRule.First().Divisor + 10;

            var engine = new FizzBuzzEngine();
            var output = engine.Execute(NumberParser.FromRange(1, max), tokenRule).ToList();

            Assert.Contains(tokenRule[0].Token, output);
            Assert.DoesNotContain(tokenRule[0].Divisor.ToString(), output);
        }

        [Trait("Category", "Unit")]
        [Fact]
        public void Should_Handle_Negative_Numbers()
        {
            var engine = new FizzBuzzEngine();
            var numbers = new List<long> { -3 };
            var rules = new List<FizzBuzzRule> { new(3, "Fizz") };

            var result = engine.Execute(numbers, rules).ToList();

            Assert.Equal("Fizz", result[0]);
        }

        [Trait("Category", "Unit")]
        [Fact]
        public void Should_Process_NonSequential_Numbers_With_Standard_Rules()
        {
            var numbers = NumberParser.FromList(new List<long> { -5, 6, 300, 12, 15 });
            var rules = new TokenProvider(new HttpClient()).GetDefaultRules();

            var engine = new FizzBuzzEngine();
            var result = engine.Execute(numbers, rules).ToList();

            var expected = new List<string>
            {
                "Buzz",
                "Fizz",
                "FizzBuzz",
                "Fizz",
                "FizzBuzz"
            };

            Assert.Equal(expected, result);
        }

        [Trait("Category", "Unit")]
        [Fact]
        public void Should_Handle_Descending_Range_Correctly()
        {
            var engine = new FizzBuzzEngine();

            ITokenProvider provider = new TokenProvider(new HttpClient());

            var rules = provider.GetDefaultRules();

            var input = NumberParser.FromRange(-2, -37).ToList();

            var result = engine.Execute(input, rules).ToList();

            Assert.Equal(input.Count, result.Count);

            Assert.Contains("Fizz", result);
            Assert.Contains("Buzz", result);
            Assert.Contains("FizzBuzz", result);
        }

        [Trait("Category", "Unit")]
        [Fact]
        public void Should_Handle_Negative_Divisors()
        {
            var engine = new FizzBuzzEngine();
            var numbers = new List<long> { 6 };
            var rules = new List<FizzBuzzRule> { new(-3, "NegFizz") };

            var result = engine.Execute(numbers, rules).ToList();

            Assert.Equal("NegFizz", result[0]);
        }

        [Trait("Category", "Unit")]
        [Fact]
        public void Should_Not_Throw_When_Numbers_Is_Null()
        {
            var engine = new FizzBuzzEngine();
            var rules = new List<FizzBuzzRule> { new(3, "Fizz") };

            var result = engine.Execute(null, rules);

            Assert.Empty(result);
        }

    }
}