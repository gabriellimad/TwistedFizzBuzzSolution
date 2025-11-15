# TwistedFizzBuzz 

An extensible and modern solution to the classic FizzBuzz problem — supporting multiple rules, custom input, external API integration, and robust testing.

---

## Overview

This project solves the FizzBuzz challenge with additional complexity. It supports:

- Custom numeric ranges (including negative and descending)
- Non-sequential number lists
- User-defined alternative rules (tokens and divisors)
- Integration with an external API for dynamic rule generation

---

## Project Structure

- **TwistedFizzBuzz** → Core library containing the engine, rules, and number parsing utilities
- **ConsoleApp.StandardFizzBuzz** → Console app that runs the classic FizzBuzz (1 to 100)
- **ConsoleApp.TwistedFizzBuzz** → Console app with custom rules and ranges
- **TwistedFizzBuzz.Tests** → Unit and integration tests using xUnit

---

## How It Works

### Engine

`FizzBuzzEngine` applies the defined rules to a sequence of numbers:

var engine = new FizzBuzzEngine();
var result = engine.Execute(numbers, rules);

Number Parser

Generates sequences from ranges or explicit lists:

NumberParser.FromRange(1, 100);
NumberParser.FromList(new[] { -5, 6, 300, 12, 15 });

Rules
Rules consist of a divisor and a token (string to output):

Example JSON rule:

{ "number": 3, "word": "Fizz" }

External API Integration
The project fetches rules dynamically from:

GET https://pie-healthy-swift.glitch.me/word

Response example:

{ "word": "different", "number": 60 }
The returned rule is applied during execution.

The API URL is configurable via appsettings.json in the test project.

Running the Project

Requirements:
.NET 8 SDK

Visual Studio or .NET CLI

Steps:
Clone the repository

Build the solution:

dotnet build

Run the console applications:

dotnet run --project ConsoleApp.StandardFizzBuzz
dotnet run --project ConsoleApp.TwistedFizzBuzz

Run all tests:

dotnet test

Testing

- Test coverage includes:

- Standard cases (divisible by 3, 5, and both)

- Empty or null tokens

- Invalid divisors (zero)

- Negative numbers

- Descending ranges

- Non-sequential lists

- External API-based rules

Run tests selectively:

dotnet test --filter Category=Unit
dotnet test --filter Category=Integration

Sample Output (StandardConsole)

1
2
Fizz
4
Buzz
Fizz
...
14
FizzBuzz

appsettings.json (Test Project)

{
  "ApiSettings": {
    "TokenApiUrl": "https://pie-healthy-swift.glitch.me/word"
  }
}

This configuration is used only for integration tests.

