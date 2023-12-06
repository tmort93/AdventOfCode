using AdventOfCode.Common;

namespace AdventOfCode.DayFive;

public class Puzzle : PuzzleBase
{
    protected override string DayPart => "DayFive";

    /// <summary>
    /// Answer: 226172555
    /// Failed attempts:
    /// 481772950
    /// </summary>
    public override async Task<int> SolvePartOneAsync()
    {
        // Tuple<Range, Range>[] map;
        // Tuple<Range, Range> mapping;
        var seeds = Array.Empty<long>();
        var maps = new List<List<Tuple<long, long, long>>>();
        var lines = File.ReadLinesAsync(GetPuzzleInputFilePath);
        await foreach (var line in lines)
        {
            if (line.StartsWith("seeds"))
            {
                var a = line.Split(':')[1];
                var b = a.Trim();
                var c = b.Split(' ');
                var d = c.Select(long.Parse);
                seeds = d.ToArray();
            }
            else if (string.IsNullOrWhiteSpace(line))
            {
                maps.Add(new List<Tuple<long, long, long>>());
            }
            else if (char.IsDigit(line[0]))
            {
                var mappingInput = line.Split(' ').Select(long.Parse).ToArray();
                maps.Last().Add(Tuple.Create(mappingInput[0], mappingInput[1], mappingInput[2]));
            }
        }

        var locationCodes = new List<long>();
        foreach (var seed in seeds)
        {
            var code = seed;
            foreach (var map in maps)
            {
                foreach (var mapping in map)
                {
                    var sourceRangeStart = mapping.Item2;
                    var rangeCount = mapping.Item3;
                    if (code >= sourceRangeStart && code <= sourceRangeStart + rangeCount - 1)
                    // if (Enumerable.Range(sourceRangeStart, rangeCount).Contains(code))
                    {
                        var diff = code - sourceRangeStart;
                        var destRangeStart = mapping.Item1;
                        code = destRangeStart + diff;
                        break;
                    }
                }
            }
            locationCodes.Add(code);
        }
        return (int)locationCodes.Min();
    }

    /// <summary>
    /// Answer: 
    /// </summary>
    public override async Task<int> SolvePartTwoAsync()
    {

        return -1;
    }
}
