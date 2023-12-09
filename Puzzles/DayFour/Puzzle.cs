using AdventOfCode.Common;

namespace AdventOfCode.DayFour;

public class Puzzle : PuzzleBase
{
    protected override string DayPart => "DayFour";

    /// <summary>
    /// Answer: 21558
    /// </summary>
    public override async Task<long> SolvePartOneAsync()
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
    /// Answer: 10425665
    /// Failed attempts:
    /// 1027
    /// </summary>
    public override async Task<long> SolvePartTwoAsync()
    {
        var lines = File.ReadLines(GetPuzzleInputFilePath).ToArray();
        var scratchCardCounts = new int[lines.Length];
        for (var i = 0; i < lines.Length; i++)
        {
            var line = lines[i];
            var numberSplit = line.Split('|');
            var winningNumberTextNoCardTitle = numberSplit[0].Split(':');
            var normalizedWinningNumberText = winningNumberTextNoCardTitle[1].Trim().Replace("  ", " ");
            var winningNumbers = normalizedWinningNumberText.Split(' ').Select(int.Parse);
            var elfNumbers = numberSplit[1].Trim().Replace("  ", " ").Split(' ').Select(int.Parse);
            var matchingNumbersCount = winningNumbers.Intersect(elfNumbers).Count();

            scratchCardCounts[i] += 1;
            var kStop = scratchCardCounts[i];
            for (var k = 0; k < kStop; k++)
            {
                var jStop = i + 1 + matchingNumbersCount;
                if (jStop > lines.Length)
                    jStop = lines.Length;
                for (var j = i + 1; j < jStop; j++)
                {
                    scratchCardCounts[j] += 1;
                }
            }

        }
        var totalScratchCards = scratchCardCounts.Sum();
        return totalScratchCards;
    }
}
