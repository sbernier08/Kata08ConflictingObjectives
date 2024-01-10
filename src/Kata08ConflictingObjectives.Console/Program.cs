using System.Text;
using Kata08ConflictingObjectives.Console.Models;

namespace Kata08ConflictingObjectives.Console;

public class Program
{
    public static void Main()
    {
        var startDate = DateTime.Now;
        var words = GetAllWords();
        var triplets = GetAllTriplets(words);
        var endDate = DateTime.Now;

        DisplayTriplets(triplets, startDate, endDate);
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
            System.Console.WriteLine($"Error reading the file: {ex.Message}");
        }
        
        return words;
    }

    private static List<Triplet> GetAllTriplets(Dictionary<string, int> words, int wordLength = 6)
    {
        var results = new List<Triplet>();
        foreach (var word in words.Where(x => x.Key.Length == wordLength))
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
        }

        return results;
    }

    private static void DisplayTriplets(List<Triplet> triplets, DateTime startDate, DateTime endDate)
    {
        System.Console.WriteLine($"Start Time: {startDate.ToLongTimeString()}");
        System.Console.WriteLine();
        
        foreach (var triplet in triplets)
        {
            System.Console.WriteLine($"{triplet.Part1} + {triplet.Part2} => {triplet.Word}"); 
        }
        
        System.Console.WriteLine();
        System.Console.WriteLine($"Triplet Count: {triplets.Count}");
        System.Console.WriteLine($"End Time: {endDate.ToLongTimeString()}");
        System.Console.WriteLine($"Process Time: {(endDate - startDate).TotalSeconds} sec");
    }
}







