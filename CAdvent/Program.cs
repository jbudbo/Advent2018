using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;

namespace CAdvent
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = System.IO.File.ReadAllLines("D:\\AoC\\Advent2018\\day6.txt");

            
            
            //var d1p2 = Day1Part2(lines);
            //var x = Day4Part1(lines);
        }

        private static int Day4Part1(IEnumerable<string> lines)
        {
            SortedDictionary<DateTime, string> events = new SortedDictionary<DateTime, string>();

            foreach (string line in lines)
            {
                if(DateTime.TryParse(line.Substring(1, 16), out DateTime eventTime))
                {
                    events.Add(eventTime, new string(line.Skip(19).ToArray()));
                }
            }

            SortedDictionary<int, int[]> guardData = new SortedDictionary<int, int[]>();

            int sleepStart = 0;
            int guardId = 0;
            foreach (var @event in events)
            {
                string evt = @event.Value;

                if (evt.StartsWith("Guard"))
                {
                    guardId = int.Parse(Regex.Match(evt, ".+#(\\d{1,5})").Groups[1].Value);
                    if (!guardData.ContainsKey(guardId))
                    {
                        guardData.Add(guardId, new int[60]);
                    }
                }
                else if (evt == "falls asleep")
                {
                    sleepStart = @event.Key.Minute;
                }
                else
                {
                    for(int i = sleepStart, n = @event.Key.Minute; i < n; i++)
                    {
                        guardData[guardId][i]++;
                    }
                }
            }

            var sleepiestGuard = guardData.OrderByDescending(guard => guard.Value.Max()).Take(1).Single();

            for (int i = 0, m = sleepiestGuard.Value.Max(); i < 60; i++)
            {
                if(sleepiestGuard.Value[i] == m)
                {
                    return sleepiestGuard.Key * i;
                }
            }

            return 0;
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
