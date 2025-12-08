using System;
using System.Collections.Generic;
using System.IO;

// Day 7: Laboratories.
namespace Day07
{
    class Program
    {
        static char[,] ParseDiagram(string fileName)
        {
            var inputLines = File.ReadAllLines(fileName);
            var height = inputLines.Length;
            var width = inputLines[0].Length;
            var diagram = new char[height, width];
            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    diagram[i, j] = inputLines[i][j];
                }
            }
            return diagram;
        }

        static void DisplayDiagram(char[,] diagram)
        {
            for (var i = 0; i < diagram.GetLength(0); i++)
            {
                for (var j = 0; j < diagram.GetLength(1); j++)
                {
                    Console.Write(diagram[i, j]);
                }
                Console.WriteLine();
            }
        }

        static void SplitBeams(char[,] diagram)
        {
            var noRows = diagram.GetLength(0);
            var noCols = diagram.GetLength(1);
            for (var r = 1; r < noRows; r++)
            {
                for (var c = 0; c < noCols; c++)
                {
                    var above = diagram[r - 1, c];
                    var current = diagram[r, c];
                    if ((above == 'S' || above == '|') && current != '^')
                        diagram[r, c] = '|';
                    if (current == '^')
                    {
                        diagram[r, c - 1] = '|';
                        diagram[r, c + 1] = '|';
                    }
                }
            }            
        }

        static int CountSplits(char[,] diagram)
        {
            var noRows = diagram.GetLength(0);
            var noCols = diagram.GetLength(1);
            var noSplits = 0;
            for (var r = 1; r < noRows; r++)
            {
                for (var c = 0; c < noCols; c++)
                {
                    var above = diagram[r - 1, c];
                    var current = diagram[r, c];
                    if (current == '^' && above == '|')
                        noSplits++;
                }
            }
            return noSplits;
        }

        static long CountPaths((int, int) start, char[,] diagram)
        {
            var noRows = diagram.GetLength(0);
            var noCols = diagram.GetLength(1);
            long[,] counts = new long[noRows, noCols];
            counts[start.Item1, start.Item2] = 1;
            for (var r = start.Item1; r < noRows-1; r++)
            {
                for (var c = 0; c < noCols; c++)
                {
                    long noPaths = counts[r, c];
                    if (noPaths == 0) continue;
                    char current = diagram[r, c];
                    if (current == '|' || current == 'S')
                        counts[r + 1, c] += noPaths;
                    else if (current == '^')
                    {
                        counts[r + 1, c-1] += noPaths;
                        counts[r + 1, c+1] += noPaths;
                    }
                }
            }
            long totalPaths = 0;
            for (var c = 0; c < noCols; c++)
                totalPaths += counts[noRows - 1, c];
            return totalPaths;
        }

        static void Main(string[] args)
        {
            // NB: first subscript rows, second subscript cols.
            var diagram = ParseDiagram("Day07.txt");

            SplitBeams(diagram);
            var noSplits = CountSplits(diagram);
            Console.WriteLine($"Part 1 answer: {noSplits}");

            var root = 0;
            for (var i = 0; i < diagram.GetLength(1); i++)
            {
                if (diagram[0, i] == 'S')
                {
                    root = i;
                    break;
                }
            }
            var noPaths = CountPaths((0, root), diagram);
            Console.WriteLine($"Part 2 answer: {noPaths}");
            // Looks like a Christmas tree vv
            DisplayDiagram(diagram);
        }
    }
}