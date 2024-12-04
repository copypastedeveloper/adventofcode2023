namespace AoC2024.Day2;

public class Day2Solution : Solution
{
    record Report
    {
        public List<Level> Levels { get; set; }
        public Direction Direction => Levels[0] > Levels[1] ? Direction.Descending : Direction.Ascending;
        public bool Safe { get; set; }
        public int Id { get; set; }

    }
    enum Direction
    {
        Ascending,Descending
    }
    public class Level
    {
        public int Number { get; set; }
        public static implicit operator int(Level l) => l.Number;
        public static implicit operator Level(int i) => new() {Number = i};
    }
    
    public override string Part1()
    {
        var answer = ParseInput("Day2/input.txt")
            .Select( r => new Report {Levels = r.Split(" ").Select(x => (Level)int.Parse(x)).ToList()})
            .Select(r => r with {Safe = r.Levels.Select((x,i) => 
                i == 0 ? 1 : 
                    r.Direction == Direction.Descending ? 
                        (x < r.Levels[i-1] && x >= r.Levels[i-1] - 3 ? 1 : 0) : 
                        (x > r.Levels[i-1] && x <= r.Levels[i-1] + 3) ? 1 : 0).Sum() == r.Levels.Count })
            .Count(x => x.Safe);
        
        return answer.ToString();
    }

    public override string Part2()
    {
        var answer = ParseInput( "Day2/input.txt")
            .Select((r,i) => new Report { Levels = r.Split(" ").Select(x => (Level)int.Parse(x)).ToList(), Id = i })
            .Select(r =>r.Levels.Select((_,i) => new Report {Id = r.Id, Levels = r.Levels.Where((_,ix) => ix != i).ToList()}))
            .SelectMany(r => r)
            .Select(r => r with {Safe = r.Levels.Select((x,i) => 
                i == 0 ? 1 : 
                    r.Direction == Direction.Descending ? 
                        (x < r.Levels[i-1] && x >= r.Levels[i-1] - 3 ? 1 : 0) : 
                        (x > r.Levels[i-1] && x <= r.Levels[i-1] + 3) ? 1 : 0).Sum() >= r.Levels.Count })
            .GroupBy(r => r.Id)
            .Count(rg => rg.Any(x => x.Safe));
        
        return answer.ToString();
    }
}