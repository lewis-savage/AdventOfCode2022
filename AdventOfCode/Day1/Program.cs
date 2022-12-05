var min = 0;
var sum = 0;

var topThree = new int[3];

void Insert(int number)
{
    if (number <= min) return;
    
    if (number > topThree[0])
    {
        topThree[2] = topThree[1];
        topThree[1] = topThree[0];
        topThree[0] = number;
    }
    else if (number > topThree[1])
    {
        topThree[2] = topThree[1];
        topThree[1] = number;
    }
    else if (number > topThree[2])
    {
        topThree[2] = number;
    }

    min = topThree[2];
}

foreach (var line in File.ReadLines("input.txt"))
{
    if (line == "")
    {
        Insert(sum);
        sum = 0;
    }
    else
    {
        sum += int.Parse(line);
    }
}

Console.WriteLine(topThree.Sum());