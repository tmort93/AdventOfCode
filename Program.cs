using AdventOfCode.DayOne;

namespace AdventOfCode;

class Program
{
    static async Task Main(string[] args)
    {
        var answer = await Puzzle.SolvePartOneAsync();
        var answer2 = await Puzzle.SolvePartTwoAsync();
        Console.WriteLine($"Part1: {answer}");
        Console.WriteLine($"Part2: {answer2}");
    }
}

