namespace Kata08ConflictingObjectives.Console.Models;

public record Triplet(string Word, string Part1, string Part2)
{
    public override string ToString()
    {
        return $"{Part1} + {Part2} => {Word}";
    }
}
