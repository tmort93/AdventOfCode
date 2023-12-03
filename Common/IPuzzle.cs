namespace AdventOfCode.Common;

public interface IPuzzle
{
    Task<int> SolvePartOneAsync();
    Task<int> SolvePartTwoAsync();
}
