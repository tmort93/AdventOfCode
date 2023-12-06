using System.Diagnostics;
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
    /// Answer: 47909639
    /// </summary>
    public override async Task<int> SolvePartTwoAsync()
    {

        var baseSeeds = new List<long>();
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
                baseSeeds = d.ToList();
            }
            else if (string.IsNullOrWhiteSpace(line))
            {
                maps.Add(new List<Tuple<long, long, long>>());
            }
            else if (char.IsDigit(line[0]))
            {
                var thing = maps.Last();
                var mappingInput = line.Split(' ').Select(long.Parse).ToArray();
                thing.Add(Tuple.Create(mappingInput[0], mappingInput[1], mappingInput[2]));
            }
        }
        var seeds = new Queue<Tuple<long, long>>();
        for (int i = 0; i < baseSeeds.Count(); i += 2)
        {
            var seedRangeStart = baseSeeds[i];
            var seedRangeEnd = seedRangeStart + baseSeeds[i + 1];
            seeds.Enqueue(Tuple.Create(seedRangeStart, seedRangeEnd));
        }

        foreach (var map in maps)
        {
            var news = new Queue<Tuple<long, long>>();
            while (seeds.Any())
            {
                var broke = false;
                var (s, e) = seeds.Dequeue();
                foreach (var (a, b, c) in map)
                {
                    var os = long.Max(s, b);
                    var oe = long.Min(e, b + c);
                    if (os < oe)
                    {
                        news.Enqueue(Tuple.Create(os - b + a, oe - b + a));
                        if (os > s)
                            seeds.Enqueue(Tuple.Create(s, os));
                        if (e > oe)
                            seeds.Enqueue(Tuple.Create(oe, e));

                        broke = true;
                        break;
                    }
                }
                if (!broke)
                    news.Enqueue(Tuple.Create(s, e));
            }
            seeds = news;
        }

        var min = seeds.Min();
        return (int)min!.Item1;
    }
}
