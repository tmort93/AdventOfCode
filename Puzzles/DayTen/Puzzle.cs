using AdventOfCode.Common;

namespace AdventOfCode.DayTen;

public class Puzzle : PuzzleBase
{
    protected override string DayPart => "DayTen";

    /// <summary>
    /// Answer: 7063
    /// Failed attempts:
    /// 7062
    /// </summary>
    public override async Task<long> SolvePartOneAsync()
    {
        int sRowIndex = 0;
        int sColumnIndex = 0;
        var grid = new char[140][];
        var rows = File.ReadLines(GetPuzzleInputFilePath).ToArray();
        for (var r = 0; r < rows.Length; r++)
        {
            var row = rows[r];
            grid[r] = new char[140];
            for (var c = 0; c < row.Length; c++)
            {
                var column = row[c];
                grid[r][c] = column;
                if (column == 'S')
                {
                    sRowIndex = r;
                    sColumnIndex = c;
                }
            }
        }

        long stepCount = 1;
        var pCharRowI = sRowIndex;
        var pCharColI = sColumnIndex;
        var cCharRowI = sRowIndex - 1;
        var cCharColI = sColumnIndex;
        var prevChar = 'S';
        var currentChar = '7';
        do
        {
            var tCharRowI = cCharRowI;
            var tCharColI = cCharColI;
            if (currentChar == '|')
            {
                if (pCharRowI > cCharRowI)
                    cCharRowI--;
                else
                    cCharRowI++;
            }
            else if (currentChar == '-')
            {
                if (pCharColI < cCharColI)
                    cCharColI++;
                else
                    cCharColI--;
            }
            else if (currentChar == 'L')
            {
                if (pCharRowI < cCharRowI)
                {
                    cCharColI++;
                }
                else
                {
                    cCharRowI--;
                }
            }
            else if (currentChar == 'J')
            {
                if (pCharRowI < cCharRowI)
                {
                    cCharColI--;
                }
                else
                {
                    cCharRowI--;
                }
            }
            else if (currentChar == '7')
            {
                if (pCharRowI > cCharRowI)
                {
                    cCharColI--;
                }
                else
                {
                    cCharRowI++;
                }
            }
            else if (currentChar == 'F')
            {
                if (pCharRowI > cCharRowI)
                {
                    cCharColI++;
                }
                else
                {
                    cCharRowI++;
                }
            }
            else if (currentChar == '.')
            {
                throw new InvalidOperationException($"Unexpected ground at index [{cCharRowI}][{cCharColI}]");
            }
            pCharRowI = tCharRowI;
            pCharColI = tCharColI;
            prevChar = currentChar;
            stepCount++;
            currentChar = grid[cCharRowI][cCharColI];
        }
        while (currentChar != 'S');

        long midPoint = stepCount / 2;
        return midPoint;
    }

    /// <summary>
    /// Answer: 
    /// </summary>
    public override async Task<long> SolvePartTwoAsync()
    {
        long diffs = 0;

        var lines = File.ReadLinesAsync(GetPuzzleInputFilePath);
        await foreach (var line in lines)
        {
        }

        return diffs;
    }
}