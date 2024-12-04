namespace AoC2024;

public abstract class Solution
{
    public abstract string Part1();
    public abstract string Part2();

    protected virtual IEnumerable<string> ParseInput(string inputFilePath)
    {
        return File.ReadLines(inputFilePath);
    }
}