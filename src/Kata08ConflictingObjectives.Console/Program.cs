using System.Text;
using Kata08ConflictingObjectives.Console.Models;

public class Program
{
    static async Task Main()
    {
        var startDate = DateTime.Now;
        var words = GetAllWords();
        var results = GetResults(words);
        var endDate = DateTime.Now;

        DisplayResult(results, startDate, endDate);
    }

    private static Dictionary<string, int> GetAllWords()
    {
        string filePath = @"D:\Dev\Tests\Kata08ConflictingObjectives\src\Kata08ConflictingObjectives.Console\Data\words.txt";
        var words = new Dictionary<string, int>();
        
        try
        {
            string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
    
            foreach (string line in lines)
            {
                var word = line.Trim().ToLower();
                words.TryAdd(word, 1);
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error reading the file: {ex.Message}");
        }
        
        return words;
    }

    private static List<WordResult> GetResults(Dictionary<string, int> words, int wordLength = 6)
    {
        var results = new List<WordResult>();
        foreach (var word in words.Where(x => x.Key.Length == wordLength))
        {
            for (int i = 1; i < word.Key.Length; i++)
            {
                var part1 = word.Key.Substring(0, i);
                var part2 = word.Key.Substring(i);
        
                if (words.ContainsKey(part1) && words.ContainsKey(part2))
                {
                    results.Add(new WordResult(word.Key, part1, part2));
                }
            }
        }

        return results;
    }

    private static void DisplayResult(List<WordResult> results, DateTime startDate, DateTime endDate)
    {
        Console.WriteLine($"Start Time: {startDate.ToLongTimeString()}");
        Console.WriteLine();
        
        foreach (var result in results)
        {
            Console.WriteLine($"{result.Part1} + {result.Part2} => {result.Word}"); 
        }
        
        Console.WriteLine();
        Console.WriteLine($"Triplet Count: {results.Count}");
        Console.WriteLine($"End Time: {endDate.ToLongTimeString()}");
        Console.WriteLine($"Process Time: {(endDate - startDate).TotalSeconds} sec");
    }
}







