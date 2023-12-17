using AdventOfCode.Common;

namespace AdventOfCode.DayThirteen;

public class Puzzle : PuzzleBase
{
    protected override string DayPart => "DayThirteen";

    /// <summary>
    /// summary = # col left of VLOR + (100 * # row above HLOR)
    /// 
    /// Answer: 
    /// </summary>
    public override async Task<long> SolvePartOneAsync()
    {
        var patternsInput = new List<List<string>>();
        var patternInput = new List<string>();
        var lines = File.ReadLinesAsync(GetPuzzleInputFilePath);
        await foreach (var line in lines)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                patternsInput.Add(patternInput);
                patternInput = new List<string>();
            }
            else
            {
                patternInput.Add(line);
            }
        }
        patternsInput.Add(patternInput);

        var summary = 0;
        var originalPatternCount = patternsInput.Count;
        var patternCount = patternsInput.Count;
        for (var pi = 0; pi < patternCount; pi++)
        {
            var i = 0;
            var j = 0;
            var count = 0;
            var matchCount = 0;
            var isVertical = false;
            var pattern = patternsInput[pi];
            for (var ri = 1; ri < pattern.Count; ri++)
            {
                var upper = pattern[ri - 1];
                var lower = pattern[ri];
                if (upper == lower)
                {
                    i = ri - 2;
                    j = ri + 1;
                    matchCount = ri;
                    var midIndex = pattern.Count / 2;
                    if (ri > midIndex)
                    {
                        count = pattern.Count - 1 - ri;
                    }
                    else
                    {
                        count = ri - 1;

                    }
                }
            }

            if (matchCount == 0)
            {
                isVertical = true;
            }

            if (!isVertical)
            {
                for (var c = 0; c < count; c++)
                {
                    var upper = pattern[i];
                    var lower = pattern[j];
                    if (upper != lower)
                    {
                        isVertical = true;
                        break;
                    }
                    else
                    {
                        i--;
                        j++;
                    }
                }

                if (!isVertical)
                {
                    if (pi > (originalPatternCount - 1))
                    {
                        summary += matchCount;
                    }
                    else
                    {
                        summary += 100 * matchCount;
                    }
                    continue;
                }
            }

            var verticalPattern = new List<string>();
            foreach (var row in pattern)
            {
                for (var k = 0; k < row.Length; k++)
                {
                    if (verticalPattern.Count < k + 1)
                    {
                        verticalPattern.Add("");
                    }
                    verticalPattern[k] += row[k];
                }
            }
            patternCount += 1;
            patternsInput.Add(verticalPattern);
        }

        return await Task.FromResult(summary);
    }

    /// <summary>
    /// Answer: 
    /// </summary>
    public override async Task<long> SolvePartTwoAsync()
    {
        return await Task.FromResult(-1);
    }
}