

using System.Reflection;
using AoC2024;

var solutions = Assembly.GetAssembly(typeof(Solution))?.GetTypes().Where(t => t.BaseType == typeof(Solution)).OrderBy(x => x.Name);

foreach (var solutionType in solutions!)
{
    var solution = (Solution)Activator.CreateInstance(solutionType)!;
    Console.WriteLine(solution.Part1());
    Console.WriteLine(solution.Part2());
}