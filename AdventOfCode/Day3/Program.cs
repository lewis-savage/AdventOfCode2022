int GetPriority(char character)
{
    if (character >= 'a')
    {
        return character - 'a' + 1;
    }

    return character - 'A' + 27;
}

var sum = 0;

var setIndex = 0;
HashSet<char>[] groups = new HashSet<char>[3];
foreach (var line in File.ReadLines("input.txt"))
{
    groups[setIndex] = line.Distinct().ToHashSet();
    setIndex++;

    if (setIndex == 3)
    {
        char found = '0';
        foreach (var c in groups[0])
        {
            if (groups[1].Contains(c) && groups[2].Contains(c))
            {
                found = c;
                break;
            }
        }

        sum += GetPriority(found);
        setIndex = 0;
    }
}

Console.WriteLine(sum);