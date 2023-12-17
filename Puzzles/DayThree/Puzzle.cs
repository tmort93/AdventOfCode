using AdventOfCode.Common;

namespace AdventOfCode.DayThree;

public class Puzzle : PuzzleBase
{
    protected override string DayPart => "DayThree";

    /// <summary>
    /// Answer: 560670
    /// 
    /// Assumption: Symbol is any character not a number and not a '.'
    /// 1. Parse a line for all its part numbers and the index of the first character
    ///     of each part #
    /// 2. Get the length of the part number
    /// 3. Check all surrounding characters for a symbol
    ///     a. Use part # first char index and length of part # to calculate range
    ///         of characters to inspect on previous, current, and next line
    /// 4. If symbol is found, add part number to sum
    /// </summary>
    public override async Task<long> SolvePartOneAsync()
    {
        var partNumberSum = 0;
        var lines = File.ReadLines(GetPuzzleInputFilePath).ToArray();
        string? previousLine = null;
        var lineCount = lines.Length;
        for (var i = 0; i < lineCount; i++)
        {
            var currentLine = lines[i];
            var possiblePartNumbers = new List<(string, List<int>)>();
            var partNumberText = string.Empty;
            var partNumberDigitIndexes = new List<int>();
            for (var j = 0; j < currentLine.Length; j++)
            {
                var currentChar = currentLine[j];
                if (char.IsNumber(currentChar))
                {
                    partNumberText += currentChar;
                    partNumberDigitIndexes.Add(j);
                }
                else if (partNumberText != string.Empty)
                {
                    var lowerDigit = partNumberDigitIndexes.Min() - 1;
                    if (lowerDigit >= 0)
                    {
                        partNumberDigitIndexes.Add(lowerDigit);
                    }
                    var higherDigit = partNumberDigitIndexes.Max() + 1;
                    if (higherDigit < currentLine.Length)
                    {
                        partNumberDigitIndexes.Add(higherDigit);
                    }
                    possiblePartNumbers.Add((partNumberText, partNumberDigitIndexes));
                    partNumberText = string.Empty;
                    partNumberDigitIndexes = new List<int>();
                }
            }
            // Handles number at end of line
            if (partNumberText != string.Empty)
            {
                var lowerDigit = partNumberDigitIndexes.Min() - 1;
                if (lowerDigit >= 0)
                {
                    partNumberDigitIndexes.Add(lowerDigit);
                }
                var higherDigit = partNumberDigitIndexes.Max() + 1;
                if (higherDigit < currentLine.Length)
                {
                    partNumberDigitIndexes.Add(higherDigit);
                }
                possiblePartNumbers.Add((partNumberText, partNumberDigitIndexes));
            }

            var nextLine = ((i + 1) < lineCount) ? lines[i + 1] : null;
            foreach (var (partNumText, partNumDigitIndexes) in possiblePartNumbers)
            {
                var isPartNumber = false;

                foreach (var partNumDigitIndex in partNumDigitIndexes)
                {
                    if (previousLine != null)
                    {
                        var digitIndexChar = previousLine[partNumDigitIndex];
                        if (!char.IsNumber(digitIndexChar) && digitIndexChar != '.')
                        {
                            isPartNumber = true;
                            break;
                        }
                    }

                    var currentLineDigitIndexChar = currentLine[partNumDigitIndex];
                    if (!char.IsNumber(currentLineDigitIndexChar) && currentLineDigitIndexChar != '.')
                    {
                        isPartNumber = true;
                        break;
                    }

                    if (nextLine != null)
                    {
                        var digitIndexChar = nextLine[partNumDigitIndex];
                        if (!char.IsNumber(digitIndexChar) && digitIndexChar != '.')
                        {
                            isPartNumber = true;
                            break;
                        }
                    }
                }
                if (isPartNumber)
                {
                    partNumberSum += int.Parse(partNumText);
                }
            }
            previousLine = currentLine;
        }
        return await Task.FromResult(partNumberSum);
    }

    /// <summary>
    /// Answer: 91622824
    /// </summary>
    public override async Task<long> SolvePartTwoAsync()
    {
        var partNumberSum = 0;
        var lines = File.ReadLines(GetPuzzleInputFilePath).ToArray();
        List<(string, List<int>)>? previousLinePossiblePartNumbers = null;
        List<(string, List<int>)>? currentLinePossiblePartNumbers = null;
        var lineCount = lines.Length;
        for (var i = 0; i < lineCount; i++)
        {
            var currentLine = lines[i];
            var possibleGearIndexes = new List<int>();
            for (var j = 0; j < currentLine.Length; j++)
            {
                if (currentLine[j] == '*')
                {
                    possibleGearIndexes.Add(j);
                }
            }

            var nextLine = ((i + 1) < lineCount) ? lines[i + 1] : null;
            currentLinePossiblePartNumbers ??= GetPossiblePartNumbers(currentLine);
            var nextLinePossiblePartNumbers = GetPossiblePartNumbers(nextLine);
            foreach (var possibleGearIndex in possibleGearIndexes)
            {
                var possibleGearPartNumbers = new List<int>();
                var possibleGearIndexGroup = new List<int> { possibleGearIndex };
                var lowerIndex = possibleGearIndex - 1;
                if (lowerIndex > 0)
                {
                    possibleGearIndexGroup.Add(lowerIndex);
                }
                var higherIndex = possibleGearIndex + 1;
                if (higherIndex < currentLine.Length)
                {
                    possibleGearIndexGroup.Add(higherIndex);
                }

                foreach (var partNumDigitIndex in possibleGearIndexGroup)
                {
                    if (previousLinePossiblePartNumbers != null)
                    {
                        var previousLinePartNumbers = previousLinePossiblePartNumbers.Where((ppn) => ppn.Item2.Contains(partNumDigitIndex)).Select(t => int.Parse(t.Item1));
                        possibleGearPartNumbers.AddRange(previousLinePartNumbers);
                    }

                    var currentLinePartNumbers = currentLinePossiblePartNumbers.Where((ppn) => ppn.Item2.Contains(partNumDigitIndex)).Select(t => int.Parse(t.Item1));
                    possibleGearPartNumbers.AddRange(currentLinePartNumbers);

                    var nextLinePartNumbers = nextLinePossiblePartNumbers.Where((ppn) => ppn.Item2.Contains(partNumDigitIndex)).Select(t => int.Parse(t.Item1));
                    possibleGearPartNumbers.AddRange(nextLinePartNumbers);
                }
                possibleGearPartNumbers = possibleGearPartNumbers.Distinct().ToList();
                if (possibleGearPartNumbers.Count() == 2)
                {
                    var gearRatio = possibleGearPartNumbers.Aggregate(1, (a, b) => a * b);
                    partNumberSum += gearRatio;
                }
            }
            previousLinePossiblePartNumbers = currentLinePossiblePartNumbers;
            currentLinePossiblePartNumbers = nextLinePossiblePartNumbers;
        }
        return await Task.FromResult(partNumberSum);
    }

    #region Helpers

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

    #endregion
}