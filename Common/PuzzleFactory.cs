namespace AdventOfCode.Common;

public class PuzzleFactory
{
    public static IPuzzle GetPuzzle(int day) =>
        day switch
        {
            1 => new DayOne.Puzzle(),
            2 => new DayTwo.Puzzle(),
            3 => new DayThree.Puzzle(),
            4 => new DayFour.Puzzle(),
            5 => new DayFive.Puzzle(),
            _ => throw new ArgumentOutOfRangeException(nameof(day))
        };
}
