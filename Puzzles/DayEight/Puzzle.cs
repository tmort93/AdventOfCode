using AdventOfCode.Common;

namespace AdventOfCode.DayEight;

public class Puzzle : PuzzleBase
{
    protected override string DayPart => "DayEight";

    /// <summary>
    /// Answer: 18023
    /// </summary>
    public override async Task<int> SolvePartOneAsync()
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
    /// Answer: 
    /// </summary>
    public override async Task<int> SolvePartTwoAsync()
    {
        return -1;
    }
}