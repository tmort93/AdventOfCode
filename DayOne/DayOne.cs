using System.ComponentModel;

namespace AdventOfCode.DayOne;

public class Puzzle
{
    public static async Task<int> SolvePartOneAsync()
    {
        var calibrationSum = 0;
        var lines = File.ReadLinesAsync("./DayOne/PuzzleInput.txt");
        await foreach (var line in lines)
        {
            var numerics = Array.FindAll(line.ToArray(), char.IsNumber);
            var calibrationValueText = $"{numerics.First()}{numerics.Last()}";
            calibrationSum += int.Parse(calibrationValueText);
        }
        return calibrationSum;
    }

    /// <summary>
    /// Failed attempts:
    /// 54766
    /// 54836
    /// 54846
    /// </summary>
    public static async Task<int> SolvePartTwoAsync()
    {
        var replacements = new[] { ("one", "o1e"), ("two", "t2o"), ("three", "t3e"), ("four", "f4r"), ("five", "f5e"), ("six", "s6x"), ("seven", "s7n"), ("eight", "e8t"), ("nine", "n9e") };
        var replacementWords = replacements.Select(r => r.Item1);
        var calibrationSum = 0;
        var lines = File.ReadLinesAsync("./DayOne/PuzzleInput.txt");
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