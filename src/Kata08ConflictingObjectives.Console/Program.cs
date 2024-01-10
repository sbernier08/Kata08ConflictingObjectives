using System.Collections.Concurrent;
using System.Text;
using Kata08ConflictingObjectives.Console.Models;

public class Program
{
    public static async Task Main()
    {
        var startDate = DateTime.Now;
        var words = await GetAllWords();
        var triplets = await GetAllTriplets(words);
        var endDate = DateTime.Now;

        DisplayTriplets(triplets, startDate, endDate);
    }

    private static async Task<Dictionary<string, int>> GetAllWords()
    {
        string filePath = @"D:\Dev\Tests\Kata08ConflictingObjectives\src\Kata08ConflictingObjectives.Console\Data\words.txt";
        var words = new Dictionary<string, int>();
        
        try
        {
            string[] lines = await File.ReadAllLinesAsync(filePath, Encoding.UTF8);
    
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

    private static async Task<ConcurrentBag<Triplet>> GetAllTriplets(Dictionary<string, int> words, int wordLength = 6)
    {
        var results = new ConcurrentBag<Triplet>();
        var sixLetterWords = words.Where(x => x.Key.Length == wordLength);

        List<Task> tasks = new List<Task>();
        foreach (var word in sixLetterWords)
        {
            tasks.Add(Task.Run(() =>
            {
                for (int i = 1; i < word.Key.Length; i++)
                {
                    var part1 = word.Key.Substring(0, i);
                    var part2 = word.Key.Substring(i);
        
                    if (words.ContainsKey(part1) && words.ContainsKey(part2))
                    {
                        results.Add(new Triplet(word.Key, part1, part2));
                    }
                }
            }));
        }
        
        await Task.WhenAll(tasks);

        return results;
    }

    private static void DisplayTriplets(ConcurrentBag<Triplet> triplets, DateTime startDate, DateTime endDate)
    {
        Console.WriteLine($"Start Time: {startDate.ToLongTimeString()}");
        Console.WriteLine();
        
        foreach (var triplet in triplets.OrderBy(x => x.Word))
        {
            Console.WriteLine($"{triplet.Part1} + {triplet.Part2} => {triplet.Word}"); 
        }
        
        Console.WriteLine();
        Console.WriteLine($"Triplet Count: {triplets.Count}");
        Console.WriteLine($"End Time: {endDate.ToLongTimeString()}");
        Console.WriteLine($"Process Time: {(endDate - startDate).TotalSeconds} sec");
    }
}







