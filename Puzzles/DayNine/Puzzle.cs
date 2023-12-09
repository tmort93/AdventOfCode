using AdventOfCode.Common;

namespace AdventOfCode.DayNine;

public class Puzzle : PuzzleBase
{
    protected override string DayPart => "DayNine";

    /// <summary>
    /// Answer: 2008960228
    /// </summary>
    public override async Task<long> SolvePartOneAsync()
    {
        long diffs = 0;

        var lines = File.ReadLinesAsync(GetPuzzleInputFilePath);
        await foreach (var line in lines)
        {
            var codes = line.Split(' ').Select(int.Parse).ToList();
            var diff = GetDiff(codes);
            diffs += diff;
        }

        return diffs;
    }

    public long GetDiff(List<int> codes)
    {
        var newCodes = new List<int>();
        for (var i = 0; i < codes.Count - 1; i++)
        {
            var diff = codes[i + 1] - codes[i];
            newCodes.Add(diff);
        }
        if (newCodes.All(nc => nc == 0))
            return codes.Last();

        var thisNext = codes.Last() + GetDiff(newCodes);
        return thisNext;
    }

    /// <summary>
    /// Answer: 
    /// </summary>
    public override async Task<long> SolvePartTwoAsync()
    {
        return -1;
    }
}