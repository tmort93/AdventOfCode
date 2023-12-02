namespace AdventOfCode.DayTwo;

public class Puzzle : IPuzzle
{
    /// <summary>
    /// Answer: 2256
    /// </summary>
    public async Task<int> SolvePartOneAsync()
    {
        var possibleGamesSum = 0;
        var lines = File.ReadLinesAsync("./DayTwo/PuzzleInput.txt");
        await foreach (var line in lines)
        {
            var isValidGame = true;
            var gameSplit = line.Split(':');
            var game = gameSplit[0];
            var setSplit = gameSplit[1].Split(';');
            foreach (var set in setSplit)
            {
                var cubes = set.Split(',');
                foreach (var cube in cubes)
                {
                    var cubeColorCountSplit = cube.TrimStart().Split(' ');
                    var cubeColor = cubeColorCountSplit[1];
                    var cubeCount = int.Parse(cubeColorCountSplit[0]);

                    isValidGame = cubeColor switch
                    {
                        "red" when cubeCount > 12 => false,
                        "green" when cubeCount > 13 => false,
                        "blue" when cubeCount > 14 => false,
                        _ => true
                    };

                    if (!isValidGame)
                        break;
                }

                if (!isValidGame)
                    break;
            }

            if (isValidGame)
            {
                var gameNumber = int.Parse(game.Split(' ')[1]);
                possibleGamesSum += gameNumber;
            }
        }
        return possibleGamesSum;
    }

    public async Task<int> SolvePartTwoAsync()
    {
        return -1;
    }
}