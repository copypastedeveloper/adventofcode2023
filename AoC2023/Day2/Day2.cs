namespace AoC2023.Day2;

class Game
{
    public int Id { get; set; }
    public IEnumerable<ColorCount> Counts { get; set; }

    public override string ToString()
    {
        return $"{Id}: {string.Join(" ", Counts.Select(c => c.ToString()))}";
    }
}

enum Color { red,blue,green }

class ColorCount
{
    public string Color { get;set; }
    public int Count { get; set; }

    public static ColorCount Parse(string[] parts)
    {
        return new ColorCount
        {
            Color = parts[1],
            Count = int.Parse(parts[0])
        };
    }

    public override string ToString()
    {
        return $"{Color}: {Count}";
    }
}

public class Day2 : Solution
{
    public override string Part1()
    {
        var limits = new List<ColorCount>
        {
            new() {Color = "red", Count = 12},
            new() {Color = "green", Count = 13},
            new() {Color = "blue", Count = 14},
        };
        
        var sumIds = ParseGames()
            .Where(x=> x.Counts.All(cc => cc.Count <= limits.Find(l => l.Color == cc.Color)!.Count))
            .Sum(x => x.Id);

        return sumIds.ToString();
    }

    IEnumerable<Game> ParseGames()
    {
        return base.ParseInput("day2/input.txt").Select(line => new Game
        {
            Id = int.Parse(line.Split(':').First().Remove(0, 5)),
            Counts = line.Split(':').Last().Split(';')
                .SelectMany(round => round.Split(',',StringSplitOptions.TrimEntries)
                    .Select(x => ColorCount.Parse(x.Split(' ')))).GroupBy(cp => cp.Color.ToLower())
                .Select(roundByColor => new ColorCount
                    {Color = roundByColor.Key, Count = roundByColor.Max(x => x.Count)})
        });
    }

    public override string Part2()
    {
        var sumIds = ParseGames()
            .Select(game => game.Counts.Select(cc => cc.Count).Aggregate(1, (acc, count) => acc * count))
            .Sum();

        return sumIds.ToString();
    }
}