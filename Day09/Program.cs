using System;
using System.Collections.Generic;
using System.IO;

// Day 9: Movie Theater.
namespace Day09
{
    class Program
    {
        static long GetArea((int,int) corner1, (int,int) corner2)
        {
            long length = Math.Abs(corner2.Item1 - corner1.Item1) + 1;
            long width = Math.Abs(corner2.Item2 - corner1.Item2) + 1;
            return length * width;
        }

        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("Day09.txt");
            var corners = new List<(int, int)>();
            foreach (var line in lines)
            {
                var lineSplit = line.Split(",");
                corners.Add((int.Parse(lineSplit[0]), int.Parse(lineSplit[1])));
            }
            var cornerPairs = new List<(long,(int, int), (int, int))>();
            var maxArea = 0L;
            for (var i = 0; i < corners.Count; i++)
            {
                for (int j = i + 1; j < corners.Count; j++)
                {
                    var corner1 = corners[i];
                    var corner2 = corners[j];
                    var area = GetArea(corner1, corner2);
                    cornerPairs.Add((area, corner1, corner2));
                    if (area > maxArea) maxArea = area;
                }
            }
            Console.WriteLine($"Part 1 answer: {maxArea}");
        }
    }
}