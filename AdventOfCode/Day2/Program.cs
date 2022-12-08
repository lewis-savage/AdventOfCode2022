int Score(Play a, Play b)
{
    if (a == b) return 3;
    switch (b)
    {
        case Play.Rock when a == Play.Scissors:
        case Play.Paper when a == Play.Rock:
        case Play.Scissors when a == Play.Paper:
            return 6;
        default:
            return 0;
    }
}

Result GetResult(Play a, Play b)
{
    if (a == b) return Result.Draw;
    switch (b)
    {
        case Play.Rock when a == Play.Scissors:
        case Play.Paper when a == Play.Rock:
        case Play.Scissors when a == Play.Paper:
            return Result.Win;
        default:
            return Result.Lose;
    }
}

var firstPlays = new Dictionary<char, Play>
{
    {'A', Play.Rock},
    {'B', Play.Paper},
    {'C', Play.Scissors}
};

var resultMap = new Dictionary<char, Result>()
{
    { 'X', Result.Lose },
    { 'Y', Result.Draw },
    { 'Z', Result.Win }
};

var score = 0;

foreach (var line in File.ReadLines("input.txt"))
{
    var a = firstPlays[line[0]];
    var neededResult = resultMap[line[2]];
    for (int i = 0; i < 3; i++)
    {
        var play = (Play)i;
        if (GetResult(a, (Play)i) == neededResult)
        {
            score += Score(a, play) + i + 1;
        }
    }
}

Console.WriteLine(score);

internal enum Play
{
    Rock,
    Paper,
    Scissors
}

internal enum Result
{
    Win,
    Lose,
    Draw
}