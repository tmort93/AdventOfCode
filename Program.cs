namespace AdventOfCode;

public interface IPuzzle
{
    Task<int> SolvePartOneAsync();
    Task<int> SolvePartTwoAsync();
}

class Program
{
    static async Task Main(string[] args)
    {
        var day = int.Parse(args[0]);
        var puzzle = GetPuzzle(day);
        var answer = await puzzle.SolvePartOneAsync();
        var answer2 = await puzzle.SolvePartTwoAsync();
        Console.WriteLine($"Part1: {answer}");
        Console.WriteLine($"Part2: {answer2}");
    }

    private static IPuzzle GetPuzzle(int day) =>
        day switch
        {
            1 => new DayOne.Puzzle(),
            2 => new DayTwo.Puzzle(),
            _ => throw new ArgumentOutOfRangeException(nameof(day))
        };
}
