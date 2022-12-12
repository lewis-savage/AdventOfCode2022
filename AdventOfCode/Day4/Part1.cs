namespace Day4;

internal static class Part1
{
    private static int SectionLength((int start, int end) section)
    {
        return section.end - section.start;
    }

    public static void Run()
    {
        int inside = 0;
        foreach (var line in File.ReadLines("input.txt"))
        {
            var elves = line.Split(',');
            var section1Split = elves[0].Split('-');
            var section2Split = elves[1].Split('-');

            var section1 = (start: int.Parse(section1Split[0]), end: int.Parse(section1Split[1]));
            var section2 = (start: int.Parse(section2Split[0]), end: int.Parse(section2Split[1]));

            (int start, int end) bigger;
            (int start, int end) smaller;

            if (SectionLength(section1) < SectionLength(section2))
            {
                bigger = section2;
                smaller = section1;
            }
            else
            {
                bigger = section1;
                smaller = section2;
            }
            if (smaller.start >= bigger.start && smaller.end <= bigger.end)
            {
                inside++;
            }
        }
        Console.WriteLine(inside);
    }
}