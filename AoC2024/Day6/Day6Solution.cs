using System.Collections;
using System.Security.AccessControl;

namespace AoC2024.Day6;

public class Day6Solution : Solution
{
    const int GridSize = 130;

    record CharacterMap(int Line, int Index, char Character);

    record Coordinate(int x, int y)
    {
        public Coordinate Rotate() => new(GridSize - 1 - y, x);

        public override string ToString() => $"({x}:{y})";
    };

    record Iteration(Grid Grid, Coordinate[] VisitedPositions)
    {
        public override string ToString()
        {
            return new Grid(Grid.Select(x => VisitedPositions.Last() == new Coordinate(x.Line, x.Index) ? x with{Character = '>'} :
                VisitedPositions.Contains(new Coordinate(x.Line, x.Index)) ? x with {Character = 'x'} : x)).ToString();
        }
    };

    class Grid(IEnumerable<CharacterMap> map) : List<CharacterMap>(map)
    {
        public override string ToString()
        {
             return string.Join(Environment.NewLine,
                this.GroupBy(x => x.Line).Select(x => new string(x.Select(y => y.Character).ToArray())));
        }
    }
    
    public override string Part1()
    {
        var answer = ParseInput("Day6/input.txt")
            .SelectMany((x, i) => x.Select((c, ci) => new CharacterMap(i, ci, c)))
            .GroupBy(_ => 1)
            .SelectMany(_ => Enumerable.Range(0, 4), (g, i) =>
                g.GroupBy(cm => i % 2 == 0 ? cm.Line : cm.Index)
                    .OrderBy(x => x.Key * (i < 2 ? -1 : 1))
                    .Select(x => new string(x.Select(y => y.Character).ToArray()))
                    .Select(x => i is 0 or 3 ? x.Reverse().ToArray() : x.ToArray())
                    .SelectMany((x, ix) => x.Select((c, ci) => new CharacterMap(ix, ci, c))))
            .Select((x,i) => new {GridIteration =i , Grid = new Grid(x)}).OrderBy(x => x.GridIteration)
            .GroupBy(_ => 1)
            .SelectMany(x => Enumerable.Repeat(new[] {3,2,1,0},200).SelectMany(y => y)
                .Select((i,n) => new Iteration(new Grid(x.First(y => y.GridIteration == i % 4).Grid),
                    n == 0 ? x.First(y => y.GridIteration == i % 4).Grid
                        .Where(y => y.Line == x.First(z => z.GridIteration == i % 4).Grid.First(z => z.Character == '^').Line)
                        .OrderBy(o => o.Index)
                        .Skip(x.First(z => z.GridIteration == i % 4).Grid.First(z => z.Character == '^').Index)
                        .TakeWhile(t => t.Character != '#')
                        .Select(y => new Coordinate(y.Line, y.Index)).ToArray() : [])))
            .Aggregate((acc, iteration) => 
                acc.VisitedPositions.Last().x != GridSize -1 && acc.VisitedPositions.Last().y != GridSize -1
                ? iteration with {VisitedPositions = acc.VisitedPositions.Select(x => x.Rotate()).ToArray().Concat(iteration.Grid
                    .Where(l => l.Line == acc.VisitedPositions?.Last().Rotate().x)
                    .OrderBy(x => x.Index)
                    .Skip(acc.VisitedPositions.Last().Rotate().y)
                    .TakeWhile(x => x.Character != '#')
                    .Select(x => new Coordinate(x.Line, x.Index))).ToArray()}
                : acc).VisitedPositions.Distinct().Count();

            return answer.ToString();
    }

    public override string Part2()
    {
        throw new NotImplementedException();
    }
}