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
        return await Task.FromResult(midPoint);
    }

    /// <summary>
    /// Answer: 
    /// </summary>
    public override async Task<long> SolvePartTwoAsync()
    {
        int sRowIndex = 0;
        int sColumnIndex = 0;
        var grid = new List<List<char>>();
        var lines = File.ReadLines(GetPuzzleInputFilePath).ToArray();
        foreach (var line in lines)
        {
            var row = new List<char>();
            grid.Add(row);
            foreach (var symbol in line)
            {
                row.Add(symbol);
                if (symbol == 'S')
                {
                    sRowIndex = grid.Count - 1;
                    sColumnIndex = row.Count - 1;
                }
            }
        }

        var gridPoints = new List<(int, int)>();
        long stepCount = 1;
        var pCharRowI = sRowIndex;
        var pCharColI = sColumnIndex;
        // var cCharRowI = sRowIndex - 1;
        var cRowIndex = sRowIndex + 1;
        var cColumnIndex = sColumnIndex;
        var prevChar = 'S';
        // var currentChar = '7';
        var currentChar = '|';
        // gridPoints.Add((sRowIndex, sColumnIndex));
        // gridPoints.Add((cRowIndex, cColumnIndex));
        var leftCount = 0;
        var rightCount = 0;
        do
        {
            var tCharRowI = cRowIndex;
            var tCharColI = cColumnIndex;
            if (currentChar == '|')
            {
                if (pCharRowI > cRowIndex)
                    cRowIndex--;
                else
                    cRowIndex++;
            }
            else if (currentChar == '-')
            {
                if (pCharColI < cColumnIndex)
                    cColumnIndex++;
                else
                    cColumnIndex--;
            }
            else if (currentChar == 'L')
            {
                if (pCharRowI < cRowIndex)
                {
                    cColumnIndex++;
                }
                else
                {
                    cRowIndex--;
                }
            }
            else if (currentChar == 'J')
            {
                if (pCharRowI < cRowIndex)
                {
                    cColumnIndex--;
                }
                else
                {
                    cRowIndex--;
                }
            }
            else if (currentChar == '7')
            {
                if (pCharRowI > cRowIndex)
                {
                    cColumnIndex--;
                }
                else
                {
                    cRowIndex++;
                }
            }
            else if (currentChar == 'F')
            {
                if (pCharRowI > cRowIndex)
                {
                    cColumnIndex++;
                }
                else
                {
                    cRowIndex++;
                }
            }
            else if (currentChar == '.')
            {
                throw new InvalidOperationException($"Unexpected ground at index [{cRowIndex}][{cColumnIndex}]");
            }
            pCharRowI = tCharRowI;
            pCharColI = tCharColI;
            prevChar = currentChar;
            stepCount++;
            currentChar = grid[cRowIndex][cColumnIndex];
            // gridPoints.Add((cRowIndex, cColumnIndex));
        }
        while (currentChar != 'S');

        // var inLoop = false;
        // var inLoopTileCount = 0;
        // for (var i = 0; i < grid.Count; i++)
        // {
        //     for (var j = 0; j < grid[i].Count; j++)
        //     {
        //         if (inLoop)
        //             inLoopTileCount++;
        //         if (gridPoints.Contains((i, j)))
        //         {
        //             if (grid[i][j] == '|')
        //                 inLoop = !inLoop;
        //         }
        //     }
        // }

        // var inLoop = false;
        var inLoopTileCount = 0;
        for (var r = 0; r < grid.Count; r++)
        {
            for (var c = 0; c < grid[r].Count; c++)
            {
                if (!gridPoints.Contains((r, c)))
                {
                    var inLoop = gridPoints
                        .Where(gp => gp.Item1 == r && gp.Item2 > c)
                        .Select(gp => gp.Item2)
                        .Count(ci => grid[r][ci] == '|') % 2 != 0;
                    if (inLoop)
                        inLoopTileCount++;
                }
            }
        }

        Console.WriteLine(leftCount);
        Console.WriteLine(rightCount);

        return await Task.FromResult(inLoopTileCount);
    }
}