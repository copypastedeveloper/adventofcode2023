// See https://aka.ms/new-console-template for more information

using AoC2023;
using AoC2023.Day1;
using AoC2023.Day2;

Console.WriteLine("Hello, World!");

Solution s = new Day1Solution();

Console.WriteLine(s.Part1());
Console.WriteLine(s.Part2());
s = new Day2();

Console.WriteLine(s.Part1());
Console.WriteLine(s.Part2());
