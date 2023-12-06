namespace AdventOfCode.Common;

public abstract class PuzzleBase : IPuzzle
{
    protected abstract string DayPart { get; }
    protected string FileName => Program.IsTest
        ? "PuzzleInputExample.txt"
        : "PuzzleInput.txt";
    protected string GetPuzzleInputFilePath => $"./Puzzles/{DayPart}/{FileName}";

    public abstract Task<int> SolvePartOneAsync();
    public abstract Task<int> SolvePartTwoAsync();
}