using TwistedFizzBuzz;
using TwistedFizzBuzz.Interfaces;
using TwistedFizzBuzz.Models;

//ITokenProvider provider = new TokenProvider(new HttpClient());
//var rules = provider.GetRulesFromApiAsync("https://pie-healthy-swift.glitch.me/word");

var rules = new List<FizzBuzzRule>
{
    new FizzBuzzRule(5,"Fizz"),
    new FizzBuzzRule(9,"Buzz"),
    new FizzBuzzRule(27,"Bar")
};

var engine = new FizzBuzzEngine();

var result = engine.Execute(NumberParser.FromRange(-20, 127), rules);

foreach (var output in result)
{
    Console.WriteLine(output);
}