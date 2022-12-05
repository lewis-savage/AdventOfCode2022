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

var firstPlays = new Dictionary<char, Play>
{
    {'A', Play.Rock},
    {'B', Play.Paper},
    {'C', Play.Scissors}
};

var secondPlays = new Dictionary<char, Play>
{
    {'X', Play.Rock},
    {'Y', Play.Paper},
    {'Z', Play.Scissors}
};

var score = 0;

foreach (var line in File.ReadLines("input.txt"))
{
    var a = firstPlays[line[0]];
    var b = secondPlays[line[2]];
    score += Score(a, b) + (int)b + 1;
}

Console.WriteLine(score);

internal enum Play
{
    Rock,
    Paper,
    Scissors
}
