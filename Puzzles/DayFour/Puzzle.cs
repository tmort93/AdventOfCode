using AdventOfCode.Common;

namespace AdventOfCode.DayFour;

public class Puzzle : PuzzleBase
{
    protected override string DayPart => "DayFour";

    /// <summary>
    /// Answer: 21558
    /// </summary>
    public override async Task<int> SolvePartOneAsync()
    {
        var totalPoints = 0;
        var lines = File.ReadLinesAsync(GetPuzzleInputFilePath);
        await foreach (var line in lines)
        {
            var numberSplit = line.Split('|');
            var winningNumberTextNoCardTitle = numberSplit[0].Split(':');
            var normalizedWinningNumberText = winningNumberTextNoCardTitle[1].Trim().Replace("  ", " ");
            var winningNumbers = normalizedWinningNumberText.Split(' ').Select(int.Parse);
            var elfNumbers = numberSplit[1].Trim().Replace("  ", " ").Split(' ').Select(int.Parse);
            var matchingNumbersCount = winningNumbers.Intersect(elfNumbers).Count();
            var points = matchingNumbersCount > 0 ? 1 : 0;
            for (var i = 1; i < matchingNumbersCount; i++)
            {
                points *= 2;
            }
            totalPoints += points;
        }
        return totalPoints;
    }

    /// <summary>
    /// Answer: 
    /// </summary>
    public override async Task<int> SolvePartTwoAsync()
    {
        return -1;
    }

    private List<(string, List<int>)> GetPossiblePartNumbers(string? line)
    {
        var possiblePartNumbers = new List<(string, List<int>)>();

        if (line == null)
            return possiblePartNumbers;

        var partNumberText = string.Empty;
        var partNumberDigitIndexes = new List<int>();
        for (var j = 0; j < line.Length; j++)
        {
            var currentChar = line[j];
            if (char.IsNumber(currentChar))
            {
                partNumberText += currentChar;
                partNumberDigitIndexes.Add(j);
            }
            else if (partNumberText != string.Empty)
            {
                possiblePartNumbers.Add((partNumberText, partNumberDigitIndexes));
                partNumberText = string.Empty;
                partNumberDigitIndexes = new List<int>();
            }
        }
        // Handles number at end of line
        if (partNumberText != string.Empty)
        {
            possiblePartNumbers.Add((partNumberText, partNumberDigitIndexes));
        }

        return possiblePartNumbers;
    }
}
