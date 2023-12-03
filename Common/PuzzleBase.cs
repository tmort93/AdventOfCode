namespace AdventOfCode.Common;

public abstract class PuzzleBase : IPuzzle
{
    protected abstract string DayPart { get; }
    protected string GetPuzzleInputFilePath => $"./Puzzles/{DayPart}/PuzzleInput.txt";

    public abstract Task<int> SolvePartOneAsync();
    public abstract Task<int> SolvePartTwoAsync();
}