using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day4
{
    internal static class Part2
    {
        public static void Run()
        {
            int overlap = 0;
            foreach (var line in File.ReadLines("input.txt"))
            {
                var elves = line.Split(',');
                var section1Split = elves[0].Split('-');
                var section2Split = elves[1].Split('-');

                var section1 = (start: int.Parse(section1Split[0]), end: int.Parse(section1Split[1]));
                var section2 = (start: int.Parse(section2Split[0]), end: int.Parse(section2Split[1]));
                var sec1HashSet = new HashSet<int>();
                for (int i = section1.start; i <= section1.end; i++)
                {
                    sec1HashSet.Add(i);
                }

                for (int i = section2.start; i <= section2.end; i++)
                {
                    if (!sec1HashSet.Contains(i)) continue;

                    overlap++;
                    break;
                }
            }
            Console.WriteLine(overlap);
        }
    }
}
