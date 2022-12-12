int GetPriority(char character)
{
    if (character >= 'a')
    {
        return character - 'a' + 1;
    }

    return character - 'A' + 27;
}

char[][] SplitLine(string line)
{
    var half = line.Length / 2;
    return new[] { line[..half].Distinct().ToArray(), line[half..].Distinct().ToArray() };
}

var sum = 0;

foreach (var line in File.ReadLines("input.txt"))
{
    var compartments = SplitLine(line);
    var hashSet = new HashSet<char>();
    foreach (var compartment in compartments)
    {
        foreach (var c in compartment)
        {
            if (!hashSet.Add(c))
            {
                sum += GetPriority(c);
                break;
            }
        }
    }
}

Console.WriteLine(sum);