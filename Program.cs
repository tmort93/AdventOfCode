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
        var dayText = args[0];
        var day = int.Parse(dayText.Split('.')[0]);
        var puzzle = GetPuzzle(day);

        if (!dayText.Contains(".2"))
        {
            var answer = await puzzle.SolvePartOneAsync();
            Console.WriteLine($"Part1: {answer}");
        }
        if (!dayText.Contains(".1"))
        {
            var answer2 = await puzzle.SolvePartTwoAsync();
            Console.WriteLine($"Part2: {answer2}");
        }
    }

    private static IPuzzle GetPuzzle(int day) =>
        day switch
        {
            1 => new DayOne.Puzzle(),
            2 => new DayTwo.Puzzle(),
            _ => throw new ArgumentOutOfRangeException(nameof(day))
        };
}
