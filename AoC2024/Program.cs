

using System.Diagnostics;
using System.Reflection;
using AoC2024;


var solutions = Assembly.GetAssembly(typeof(Solution))?.GetTypes().Where(t => t.BaseType == typeof(Solution)).OrderBy(x => x.Name);
var timer = new Stopwatch();
foreach (var solutionType in solutions!)
{
    var solution = (Solution)Activator.CreateInstance(solutionType)!;
    timer.Start();
    Console.WriteLine(solution.Part1());
    timer.Stop();
    Console.WriteLine($"{solutionType.Name} Part 1 took {timer.ElapsedMilliseconds}ms");
    timer.Reset();
    timer.Start();
    Console.WriteLine(solution.Part2());
    timer.Stop();
    Console.WriteLine($"{solutionType.Name} Part 2 took {timer.ElapsedMilliseconds}ms");

}