using System.Text.RegularExpressions;

namespace AoC2024.Day3;

public class Day3Solution : Solution
{
    public override string Part1()
    {
        var answer = ParseInput("Day3/input.txt").Select(x => x.Split(")")).SelectMany(x => x)
            .Select(x => x.LastIndexOf("mul(") > -1 ? new string(x.TakeLast(x.Length-(x.LastIndexOf("mul(") + 4)).ToArray()) : null)
            .Where(x => x is not null && x.Contains(',') && int.TryParse(x.Replace(",",""), out _) && !x.Contains(' '))
            .Select(x => x.Split(',').Select(int.Parse).Aggregate((acc,n) => acc * n))
            .Sum();
        return answer.ToString();
    }

    public override string Part2()
    {
        var answer = ParseInput("Day3/input.txt").Aggregate((acc,s) => acc + s)
            .Replace("do()", "|do|do()").Replace("don't()","|dont|don't()")
            .Split(["|do|","|dont|"],StringSplitOptions.RemoveEmptyEntries)
            .Where(l => !l.StartsWith("don't()"))
            .Select(x => x.Split(")")).SelectMany(x => x)
            .Select(x => x.LastIndexOf("mul(") > -1 ? new string(x.TakeLast(x.Length-(x.LastIndexOf("mul(") + 4)).ToArray()) : null)
            .Where(x => x is not null && x.Contains(',') && int.TryParse(x.Replace(",",""), out _) && !x.Contains(' '))
            .Select(x => x.Split(',').Select(int.Parse).Aggregate((acc,n) => acc * n))
            .Sum();

        return answer.ToString();
    }
}