namespace AoC2024.Day5;

public class Day5Solution : Solution
{
    record PageRequirement(int FirstPage, int SecondPage)
    {
        public PageRequirement(int[] args) : this(args[0],args[1])
        {}

        public List<int> ToList() => [FirstPage, SecondPage];
    }
    record ManualInstructions(IEnumerable<PageRequirement> PageRequirements, IEnumerable<IEnumerable<int>> PageSequences);

    record InstructionSequenceCombination(PageRequirement Requirement, IEnumerable<int> Sequence);
    
    public override string Part1()
    {
        var answer = ParseInput("Day5/input.txt").GroupBy(_ => 1)
            .Select(x => new ManualInstructions(x.TakeWhile(l => !string.IsNullOrWhiteSpace(l)).Select(y => new PageRequirement(y.Split("|").Select(int.Parse).ToArray())),
                x.SkipWhile(l => !string.IsNullOrWhiteSpace(l)).Skip(1).Select(l => l.Split(",").Select(int.Parse))))
            .SelectMany(x => x.PageRequirements.SelectMany(_ => x.PageSequences,(req,seq) => new InstructionSequenceCombination(req,seq.ToList())))
            .GroupBy(x => string.Join('|',x.Sequence))
            .Where(y => y.All(x => !x.Requirement.ToList().All(y => x.Sequence.Contains(y)) || (x.Requirement.ToList().All(y => x.Sequence.Contains(y)) 
                && x.Sequence.ToList().IndexOf(x.Requirement.FirstPage) < x.Sequence.ToList().IndexOf(x.Requirement.SecondPage))))
            .Select(x => x.First().Sequence)
            .Sum(x => x.ToList()[x.Count()/2]);
        return answer.ToString();
    }

    public override string Part2()
    {   
        var answer = ParseInput("Day5/input.txt").GroupBy(_ => 1)
            .Select(x => new ManualInstructions(x.TakeWhile(l => !string.IsNullOrWhiteSpace(l)).Select(y => new PageRequirement(y.Split("|").Select(int.Parse).ToArray())),
                x.SkipWhile(l => !string.IsNullOrWhiteSpace(l)).Skip(1).Select(l => l.Split(",").Select(int.Parse))))
            .SelectMany(x => x.PageRequirements.SelectMany(_ => x.PageSequences,(req,seq) => new InstructionSequenceCombination(req,seq)))
            .Where(x => x.Requirement.ToList().All(y => x.Sequence.Contains(y)) 
                        && x.Sequence.ToList().IndexOf(x.Requirement.FirstPage) > x.Sequence.ToList().IndexOf(x.Requirement.SecondPage))
            .GroupBy(x => string.Join('|',x.Sequence))
            .Select(x => x.Key.Split("|").Select(int.Parse)
                .OrderBy(y=> y,Comparer<int>.Create((i, ii) => 
                    x.Select(y => y.Requirement).Any(y => y.FirstPage == ii && y.SecondPage == i) 
                        ? 1 
                        : x.Select(y => y.Requirement).Any(y => y.FirstPage == i && y.SecondPage == ii) 
                            ? -1 
                            : 0 )
                ).ToList())
            .Sum(x => x.ToList()[x.Count/2]);
        
        return answer.ToString();
    }
}