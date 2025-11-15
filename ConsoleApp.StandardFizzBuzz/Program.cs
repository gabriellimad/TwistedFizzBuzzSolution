using TwistedFizzBuzz;
using TwistedFizzBuzz.Interfaces;

var engine = new FizzBuzzEngine();

ITokenProvider provider = new TokenProvider(new HttpClient());

var rules = provider.GetDefaultRules();

var result = engine.Execute(NumberParser.FromRange(1, 100), rules);

foreach (var output in result)
{
    Console.WriteLine(output);
}