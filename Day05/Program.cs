using System;
using System.Collections.Generic;
using System.IO;

// Day 5: Cafeteria.
namespace Day05
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputText = File.ReadAllText("Day05.txt");
            var inputTextSplit = inputText.Split("\n\n");
            var ranges = new List<(long, long)>();
            var ids = new List<long>();
            foreach (var rangeLine in inputTextSplit[0].Split('\n'))
            {
                var rangesSplit = rangeLine.Split("-");
                ranges.Add((long.Parse(rangesSplit[0]), long.Parse(rangesSplit[1])));
            }                
            foreach (var id in inputTextSplit[1].Split("\n"))
                ids.Add(long.Parse(id));

            var noFreshIds = 0;
            foreach (var id in ids)
                if (IsIdInRanges(id, ranges)) noFreshIds++;

            Console.WriteLine($"Part 1 answer: {noFreshIds}");
            Console.WriteLine($"Part 2 answer: {GetTotalNoFreshIds(ranges)}");            
        }

        static bool IsIdInRanges(long id, List<(long, long)> ranges)
        {
            foreach (var range in ranges)            
                if (id >= range.Item1 && id <= range.Item2) return true;            
            return false;
        }

        static long GetTotalNoFreshIds(List<(long, long)> ranges)
        {                        
            ranges.Sort();
            var newRanges = new List<(long, long)>();
            newRanges.Add(ranges[0]);
            while (ranges.Count > 0)
            {
                var endRange = newRanges[newRanges.Count - 1].Item2;
                ranges.RemoveAt(0);
                if (ranges.Count > 0)
                {
                    if (ranges[0].Item1 <= endRange)
                    {
                        if (endRange + 1 <= ranges[0].Item2)
                            newRanges.Add((endRange + 1, ranges[0].Item2));
                    }
                    else                    
                        newRanges.Add(ranges[0]);                    
                }
            }
            long totalNoFreshIds = 0;
            foreach (var range in newRanges)
                totalNoFreshIds += (range.Item2 - range.Item1 + 1);            
            return totalNoFreshIds;
        }
    }
}