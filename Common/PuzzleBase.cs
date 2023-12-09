namespace AdventOfCode.Common;

public abstract class PuzzleBase : IPuzzle
{
    protected abstract string DayPart { get; }
    protected string FileName => Program.IsTest
        ? "PuzzleInputExample.txt"
        : "PuzzleInput.txt";
    protected string GetPuzzleInputFilePath => $"./Puzzles/{DayPart}/{FileName}";

    public abstract Task<long> SolvePartOneAsync();
    public abstract Task<long> SolvePartTwoAsync();
}