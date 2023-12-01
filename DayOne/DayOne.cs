namespace AdventOfCode.DayOne;

public class Puzzle
{
    public static async Task<int> SolveAsync()
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
}