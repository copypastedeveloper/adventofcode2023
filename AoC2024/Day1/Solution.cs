namespace AoC2024.Day1;

public class Day1Solution : Solution
{
    public class location
    {
        public string side { get; set; }
        public string id { get; set; }
        public int answer { get; set; }
    }

    public override string Part1()
    {
        var answer = ParseInput("Day1/input.txt").Select(l => l.Split(" ", StringSplitOptions.RemoveEmptyEntries))
            .Select(x => new List<location> {new() {side = "Left", id = x[0]}, new() {side = "Right", id = x[1]}})
            .SelectMany(x => x)
            .OrderBy(x => x.id)
            .GroupBy(x => x.side)
            .Select(g => g.Aggregate((x, y) => new() {side = y.side, id = $"{x.id}|{y.id}"})) //should be 2 
            .Aggregate((l,r) => new location {answer = 
                l.id.Split("|",StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Order()
                    .Select((x,i) => Math.Abs(x - r.id.Split("|",StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList()[i])).Sum()})
            .answer;

        return answer.ToString();
    }

    public override string Part2()
    {
        var answer = ParseInput("Day1/input.txt").Select(l => l.Split(" ", StringSplitOptions.RemoveEmptyEntries))
            .Select(x => new List<location> {new() {side = "Left", id = x[0]}, new() {side = "Right", id = x[1]}})
            .SelectMany(x => x)
            .OrderBy(x => x.id)
            .GroupBy(x => x.side)
            .Select(g => g.Aggregate((x, y) => new() {side = y.side, id = $"{x.id}|{y.id}"})) //should be 2 
            .Aggregate((l,r) => new location {answer = 
                l.id.Split("|",StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Order()
                    .Select((x) => x * r.id.Split("|",StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).Count(y => y == x)).Sum()})
            .answer;

        return answer.ToString();
    }
}