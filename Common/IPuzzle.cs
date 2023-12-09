namespace AdventOfCode.Common;

public interface IPuzzle
{
    Task<long> SolvePartOneAsync();
    Task<long> SolvePartTwoAsync();
}
