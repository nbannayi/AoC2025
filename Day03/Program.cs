using System;
using System.Collections.Generic;
using System.IO;

// Day 3: Lobby.
namespace Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            var batteryBanks = File.ReadAllLines("Day03.txt");
            var totalBatteryJoltage1 = 0;
            var totalBatteryJoltage2 = 0L;

            foreach (var batteryBank in batteryBanks)
            {
                int maxBatteryJoltage1 = GetMaxBatteryJoltage(batteryBank);
                totalBatteryJoltage1 += maxBatteryJoltage1;
                long maxBatteryJoltage2 = GetMaxBatteryJoltage(batteryBank, 12);
                totalBatteryJoltage2 += maxBatteryJoltage2;
            }

            Console.WriteLine($"Part 1 answer: {totalBatteryJoltage1}");
            Console.WriteLine($"Part 2 answer: {totalBatteryJoltage2}");
        }

        static int GetMaxBatteryJoltage(string batteryBank)
        {
            char maxBatteryJoltage1 = '0';
            char maxBatteryJoltage2 = '0';
            var maxBatteryJoltageIndex = 0;

            for (int i = 0; i < batteryBank.Length - 1; i++)
            {
                if (batteryBank[i] > maxBatteryJoltage1)
                {
                    maxBatteryJoltage1 = batteryBank[i];
                    maxBatteryJoltageIndex = i;
                }
            }

            for (int i = maxBatteryJoltageIndex + 1; i < batteryBank.Length; i++)
            {
                if (batteryBank[i] > maxBatteryJoltage2)
                    maxBatteryJoltage2 = batteryBank[i];
            }

            return int.Parse(maxBatteryJoltage1.ToString() + maxBatteryJoltage2.ToString());
        }

        static long GetMaxBatteryJoltage(string batteryBank, int n)
        {
            char[] maxBatteryJolatages = new char[n];
            var startIndex = 0;            
            var newEndIndex = 0;
            var totalMaxBatteryJoltage = "";

            for (int j = 0; j < n; j++)
            {
                var currentEndIndex = batteryBank.Length - (n - j);
                for (int i = startIndex; i <= currentEndIndex; i++)
                {
                    if (batteryBank[i] > maxBatteryJolatages[j])
                    {
                        maxBatteryJolatages[j] = batteryBank[i];
                        newEndIndex = i;
                    }
                }
                startIndex = newEndIndex + 1;                
            }

            foreach (var maxBatteryJolatage in maxBatteryJolatages)
                totalMaxBatteryJoltage += maxBatteryJolatage.ToString();

            return long.Parse(totalMaxBatteryJoltage);
        }
    }
}