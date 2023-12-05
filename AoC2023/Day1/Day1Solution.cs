using System.Text.RegularExpressions;

namespace AoC2023.Day1;

public class Day1Solution : Solution
{
    public override string Part1()
    {
        var solved = ParseInput("Day1/input.txt")
            .Select(x => x.Where(c => int.TryParse(c.ToString(), out _)))
            .Select(x => $"{x.First()}{x.Last()}")
            .Select(int.Parse)
            .Sum();

        return solved.ToString();
    }

    public override string Part2()
    {
        var substitutions = new List<(string, string)>
        {
            ("one", "1"),
            ("two", "2"),
            ("three", "3"),
            ("four", "4"),
            ("five", "5"),
            ("six", "6"),
            ("seven", "7"),
            ("eight", "8"),
            ("nine", "9")
        };
        
        var solved = ParseInput("Day1/input.txt")
            .Select(x => string.Join("", x.Select((c, ix) => new CharacterIndex(c.ToString(), ix))
                .Where(s => s.IsInt())
                .Concat(substitutions
                    .SelectMany(s => Regex.Matches(x, s.Item1).Select(m => new CharacterIndex(s.Item2, m.Index)))
                    .Where(s => s.Index >= 0))
                .OrderBy(s => s.Index)
                .Select(s => s.Number)))
            .Select(x =>int.Parse($"{x.First()}{x.Last()}"))
            .Sum();

        return solved.ToString();
    }
}

internal record CharacterIndex(string Number, int Index)
{
    public bool IsInt() => int.TryParse(Number, out _);
};