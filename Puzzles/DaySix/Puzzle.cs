using AdventOfCode.Common;

namespace AdventOfCode.DaySix;

public class Puzzle : PuzzleBase
{
    protected override string DayPart => "DaySix";

    /// <summary>
    /// Answer: 588588
    /// </summary>
    public override async Task<long> SolvePartOneAsync()
    {
        var lines = File.ReadLines(GetPuzzleInputFilePath).ToArray();
        var times = lines[0].Split(':')[1].Split(' ').Where(i => i != string.Empty).Select(int.Parse).ToArray();
        var distances = lines[1].Split(':')[1].Split(' ').Where(i => i != string.Empty).Select(int.Parse).ToArray();

        var raceMargins = new List<int>();
        for (var j = 0; j < times.Length; j++)
        {
            var raceOptions = new List<int>();
            var time = times[j];
            var recordDistance = distances[j];
            for (var i = time - 1; i > 0; i--)
            {
                var speed = time - i;
                var distance = i * speed;
                if (distance > recordDistance)
                {
                    raceOptions.Add(i);
                }
            }
            raceMargins.Add(raceOptions.Count());
        }

        return await Task.FromResult(raceMargins.Aggregate(1, (a, b) => a * b));
    }

    /// <summary>
    /// Answer: 34655848
    /// </summary>
    public override async Task<long> SolvePartTwoAsync()
    {
        var lines = File.ReadLines(GetPuzzleInputFilePath).ToArray();
        var timeText = lines[0].Split(':')[1].Split(' ').Where(i => i != string.Empty).Aggregate((a, b) => a + b);
        var recordDistanceText = lines[1].Split(':')[1].Split(' ').Where(i => i != string.Empty).Aggregate((a, b) => a + b);
        var time = long.Parse(timeText);
        var recordDistance = long.Parse(recordDistanceText);

        var raceOptions = 0;
        for (var i = time - 1; i > 0; i--)
        {
            var speed = time - i;
            var distance = i * speed;
            if (distance > recordDistance)
            {
                raceOptions++;
            }
        }

        return await Task.FromResult(raceOptions);
    }
}
