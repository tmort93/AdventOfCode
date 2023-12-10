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
            6 => new DaySix.Puzzle(),
            7 => new DaySeven.Puzzle(),
            8 => new DayEight.Puzzle(),
            9 => new DayNine.Puzzle(),
            10 => new DayTen.Puzzle(),
            _ => throw new ArgumentOutOfRangeException(nameof(day))
        };
}
