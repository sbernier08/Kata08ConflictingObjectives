namespace Kata08ConflictingObjectives.Domain.Entities;

public class Word
{
    public int Id { get; init; }
    public string Value { get; init; }

    public Word(string value)
    {
        Value = value;
    }
}