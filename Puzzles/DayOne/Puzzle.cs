using AdventOfCode.Common;

namespace AdventOfCode.DayOne;

public class Puzzle : PuzzleBase
{
    protected override string DayPart => "DayOne";

    /// <summary>
    /// Answer: 56042
    /// </summary>
    public override async Task<int> SolvePartOneAsync()
    {
        var calibrationSum = 0;
        var lines = File.ReadLinesAsync(GetPuzzleInputFilePath);
        await foreach (var line in lines)
        {
            var numerics = Array.FindAll(line.ToArray(), char.IsNumber);
            var calibrationValueText = $"{numerics.First()}{numerics.Last()}";
            calibrationSum += int.Parse(calibrationValueText);
        }
        return calibrationSum;
    }

    /// <summary>
    /// Answer: 55358
    /// Failed attempts:
    /// 54766
    /// 54836
    /// 54846 
    /// </summary>
    public override async Task<int> SolvePartTwoAsync()
    {
        var replacements = new[] { ("one", "o1e"), ("two", "t2o"), ("three", "t3e"), ("four", "f4r"), ("five", "f5e"), ("six", "s6x"), ("seven", "s7n"), ("eight", "e8t"), ("nine", "n9e") };
        var replacementWords = replacements.Select(r => r.Item1);
        var calibrationSum = 0;
        var lines = File.ReadLinesAsync(GetPuzzleInputFilePath);
        await foreach (var line in lines)
        {
            var updatedLine = line;
            foreach (var (word, numberText) in replacements)
            {
                updatedLine = updatedLine.Replace(word, numberText);
            }
            var numerics = Array.FindAll(updatedLine.ToArray(), char.IsNumber);
            var calibrationValueText = $"{numerics.First()}{numerics.Last()}";
            calibrationSum += int.Parse(calibrationValueText);
        }
        return calibrationSum;
    }
}