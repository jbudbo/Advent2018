using System.Collections.Generic;
using System.Linq;

namespace CAdvent
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines("D:\\AoC\\Advent2018\\day1.txt");

            var d1p2 = Day1Part2(lines);
        }

        private static int Day1Part2(IEnumerable<string> lines)
        {
            int Iterate(IEnumerable<int> ints)
            {
                HashSet<int> frequencies = new HashSet<int>();
                int sum = 0;
                while (true)
                {
                    foreach (var i in ints)
                    {
                        sum += i;

                        if (frequencies.Contains(sum))
                            return sum;
                        else
                            frequencies.Add(sum);
                    }
                }
            }

            return Iterate(lines.Select(int.Parse));
        }
        
    }
}
