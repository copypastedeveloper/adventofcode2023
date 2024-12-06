using System.Collections.Concurrent;
using System.Text.RegularExpressions;

namespace AoC2024.Day4;

public class Day4Solution : Solution
{
    public record CharacterMap(Direction direction, int line, int index, char character);
    public record Chunk(int line, int startIndex, string characters);
    const string inputFilePath = "Day4/input.txt";
    
    public override string Part1()
    {   
        const string forward = "XMAS";
        const string backward = "SAMX";
        var answer = ParseInput(inputFilePath)
            
            .SelectMany((l, li) => l.SelectMany((c, i) => new List<CharacterMap>
            {
                new(Direction.Horizontal,i, li, c), 
                new(Direction.Vertical,li, i, c),
                new(Direction.DiagonalRight,li, i - li, c),
                new(Direction.DiagonalLeft,li, i + li, c)
            }))
            .GroupBy(c => c.direction)
            .Select(x => x.GroupBy(c => c.index).Select(row => new string(row.OrderBy(r => r.line).ThenBy(r => r.index).Select(r => r.character).ToArray()))
            .Select(y => y.Select((_, i) => y.Substring(i, y.Length > i + 3 ? 4 : y.Length - i)).Where(s => string.Equals(s,forward) || string.Equals(s,backward))))
            .SelectMany(y => y).SelectMany(y => y).Count();
        
        return answer.ToString();
    }

    public override string Part2()
    {
        const string forward = "MAS";
        const string backward = "SAM";

        var answer = ParseInput(inputFilePath).GroupBy(_ => 1).Select(input => input
            .SelectMany((_, li) =>
                (li < input.Count() - 2
                    ? input.Skip(li).Take(3).SelectMany((y, oli) =>
                            y.Select((_, i) => new Chunk(oli, i, y.Substring(i, y.Length > i + 2 ? 3 : y.Length - i)))).GroupBy(x => x.startIndex)
                    : null) ?? Array.Empty<IGrouping<int, Chunk>>()
            )
            .Select(x => x.Select(y => y.characters))
            .Select(smallBox => smallBox
                .SelectMany((l, li) => l.SelectMany((c, i) => new List<CharacterMap>
                {
                    new(Direction.DiagonalRight, li, i - li, c),
                    new(Direction.DiagonalLeft, li, i + li, c)
                }))
                .GroupBy(c => c.direction)
                .Select(x => x.GroupBy(c => c.index).Select(row =>
                        new string(row.OrderBy(r => r.line).ThenBy(r => r.index).Select(r => r.character).ToArray()))
                    .Count(s => string.Equals(s, forward) || string.Equals(s, backward))).Sum()).Count(x => x == 2)).First();
        
        return answer.ToString();
    }
}

public enum Direction
{
    Horizontal,Vertical,DiagonalLeft,DiagonalRight
}