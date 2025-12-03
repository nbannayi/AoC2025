using System;
using System.IO;

// Day 1: Secret Entrance.
namespace Day01
{
    class Program
    {
        static void Main(string[] args)
        {
            var rotations = File.ReadAllLines("Day01.txt");
            var dialPointer1 = 50;
            var dialPointer2 = 50;
            var numZeros1 = 0;
            var numZeros2 = 0;

            foreach (var rotation in rotations)
            {
                var direction = rotation[0];
                var distance = int.Parse(rotation.Substring(1));
                var retZeros = 0;
                (dialPointer1, retZeros) = RotateFully(dialPointer1, direction, distance);
                numZeros1 += retZeros;
                (dialPointer2, retZeros) = RotateByClicks(dialPointer2, direction, distance);
                numZeros2 += retZeros;
            }

            Console.WriteLine($"Part 1 answer: {numZeros1}");
            Console.WriteLine($"Part 2 answer: {numZeros2}");
        }

        static (int dialPointer, int numZeros) RotateFully(int dialPointer, char direction, int distance)
        {
            int numZeros = 0;
            if (direction == 'L')
                dialPointer -= distance;
            else
                dialPointer += distance;

            dialPointer %= 100;
            if (dialPointer < 0) dialPointer += 100;
            if (dialPointer == 0) numZeros++;

            return (dialPointer, numZeros);
        }

        static (int dialPointer, int numZeros) RotateByClicks(int dialPointer, char direction, int distance)
        {
            int numZeros = 0;
            int offset = (direction == 'L') ? -1 : 1;

            for (int i = 0; i < distance; i++)
            {
                dialPointer += offset;
                if (dialPointer < 0) dialPointer += 100;
                if (dialPointer > 99) dialPointer -= 100;
                if (dialPointer == 0) numZeros++;
            }

            return (dialPointer, numZeros);
        }
    }
}