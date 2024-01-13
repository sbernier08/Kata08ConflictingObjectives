namespace Kata08ConflictingObjectives.Application.Features.Word.GetList.DTOs;

public class WordDTO
{
    public string Value { get; set; }
    public List<ComposedWord> ComposedWords { get; set; } = new();

    public WordDTO(string value)
    {
        Value = value;
    }
}