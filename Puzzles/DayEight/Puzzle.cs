using AdventOfCode.Common;

namespace AdventOfCode.DayEight;

public class Puzzle : PuzzleBase
{
    protected override string DayPart => "DayEight";

    /// <summary>
    /// Answer: 18023
    /// </summary>
    public override async Task<long> SolvePartOneAsync()
    {
        var lines = File.ReadLines(GetPuzzleInputFilePath).ToArray();
        var instructions = lines[0].Replace('L', '1').Replace('R', '2').Select(i => int.Parse(i.ToString())).ToArray();
        var maps = new List<string[]>();
        for (var i = 2; i < lines.Length; i++)
        {
            var line = lines[i];
            var nodes = line.Replace(" =", ",").Replace("(", "").Replace(")", "").Replace(" ", "").Split(',');
            maps.Add(nodes);
        }

        var currentNode = "AAA";
        var stepCount = 0;
        for (int i = 0; i < instructions.Length; i++)
        {
            stepCount++;
            var instruction = instructions[i];
            var currentMapIndex = maps.FindIndex(i => i[0] == currentNode);
            currentNode = maps[currentMapIndex][instruction];

            if (currentNode == "ZZZ")
                break;

            if (i == instructions.Length - 1)
            {
                i = -1;
            }
        }

        return stepCount;
    }

    /// <summary>
    /// Answer: 14449445933179
    /// </summary>
    public override async Task<long> SolvePartTwoAsync()
    {
        var lines = File.ReadLines(GetPuzzleInputFilePath).ToArray();
        var instructions = lines[0].Replace('L', '1').Replace('R', '2').Select(i => int.Parse(i.ToString())).ToArray();
        var maps = new Dictionary<string, (string, string)>();
        for (var i = 2; i < lines.Length; i++)
        {
            var line = lines[i];
            var nodes = line.Replace(" =", ",").Replace("(", "").Replace(")", "").Replace(" ", "").Split(',');
            maps.Add(nodes[0], (nodes[1], nodes[2]));
        }

        var currentNodes = maps.Where(m => m.Key.EndsWith('Z')).Select(m => m.Key).ToArray();
        var nodeCount = currentNodes.Length;
        var stepCounts = new long[nodeCount];
        var instructionsLength = instructions.Length;
        var instructionsLengthMinusOne = instructions.Length - 1;
        for (var j = 0; j < nodeCount; j++)
        {
            for (int i = 0; i < instructionsLength; i++)
            {
                stepCounts[j]++;
                var instruction = instructions[i];

                var currentMap = maps[currentNodes[j]];
                currentNodes[j] = instruction == 1
                 ? currentMap.Item1
                    : currentMap.Item2;


                if (currentNodes[j].EndsWith('Z'))
                    break;

                if (i == instructionsLengthMinusOne)
                {
                    i = -1;
                }
            }
        }

        var lcm = Lcm(stepCounts);
        return lcm;
    }

    static long Lcm(long[] numbers)
    {
        return numbers.Aggregate(LcmAggregate);
    }
    static long LcmAggregate(long a, long b)
    {
        return Math.Abs(a * b) / Gcd(a, b);
    }
    static long Gcd(long a, long b)
    {
        return b == 0 ? a : Gcd(b, a % b);
    }
}