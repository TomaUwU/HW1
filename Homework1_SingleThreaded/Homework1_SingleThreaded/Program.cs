using System.Diagnostics;
using System.Text;

namespace Homework1_SingleThreaded
{
    internal class Program
    {
        static string Remove_Punctuation(string words)
        {
            words = words.Replace(".", "");
            words = words.Replace("!", "");
            words = words.Replace(",", "");
            words = words.Replace("?", "");
            words = words.Replace(":", "");
            words = words.Replace(";", "");
            words = words.Replace("”", "");
            words = words.Replace("\"", "");
            words = words.Replace("*", "");
            words = words.Replace("-", "");
            words = words.Replace("_", "");
            words = words.Replace("\\", "");
            words = words.Replace("(", "");
            words = words.Replace(")", "");
            words = words.Replace("$", "");
            words = words.Replace("“", "");
            words = words.Replace("|", "");
            words = words.Replace("/", "");
            words = words.Trim();

            return words;
        }

        static int Count_Words(string[] file)
        {
            int count = 0;
            foreach (var item in file)
            {
                if (item.Length > 2) { continue; }
                count++;
            }
            return count;
        }

        static string Shortest_Word(string[] file)
        {
            string word = "No words in the document!";
            foreach (var item in file)
            {
                if (item.Length < word.Length && item.Length > 3)
                {
                    word = item;
                }
                if (item.Length == 3) { return item; }
            }
            return word;
        }

        static string Longest_Word(string[] file)
        {
            string word = "No words in the document!";
            foreach (var item in file)
            {
                if (item.Length > word.Length && item.Length > 2)
                {
                    word = item;
                }
            }
            return word;
        }

        static int Average_Word_Length(string[] file)
        {
            int count = Count_Words(file);
            int average = 0;
            foreach (var item in file)
            {
                if (item.Length > 2)
                {
                    average = average + item.Length;
                }
            }

            return average / count;
        }

        static string[] Most_Common_Words(string[] file)
        {
            Dictionary<string, int> words = new Dictionary<string, int>();

            foreach (var item in file)
            {
                if (item.Length < 3)
                {
                    continue;
                }
                if (words.ContainsKey(item))
                {
                    words[item] = words[item] + 1;
                    continue;
                }
                words.Add(item, 1);
            }

            string[] strings = new string[5];
            int i = 0;

            foreach (var item in words.OrderByDescending(key => key.Value))
            {
                if (i >= strings.Length) break;
                strings[i] = item.Key;
                i++;

            }

            return strings;
        }

        static string[] Least_Common_Words(string[] file)
        {
            Dictionary<string, int> words = new Dictionary<string, int>();

            foreach (var item in file)
            {
                if (item.Length < 3)
                {
                    continue;
                }
                if (words.ContainsKey(item))
                {
                    words[item] = words[item] + 1;
                    continue;
                }
                words.Add(item, 1);
            }

            string[] strings = new string[5];
            int i = 0;

            foreach (var item in words.OrderBy(key => key.Value))
            {
                if (i >= strings.Length) break;
                strings[i] = item.Key;
                i++;
            }

            return strings;
        }

        static void Main(string[] args)
        {
            Console.Write("Input the path to the file: ");
            string location = Console.ReadLine();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            string literature = "";
            if (File.Exists(location)) { literature = File.ReadAllText(location, Encoding.Unicode); }

            literature = Remove_Punctuation(literature);

            string[] array = literature.Split(" ");

            Console.WriteLine($"Words Count: {Count_Words(array)}");
            Console.WriteLine($"Shortest word is: {Shortest_Word(array)}");
            Console.WriteLine($"Longest word is: {Longest_Word(array)}");
            Console.WriteLine($"Average word length is: {Average_Word_Length(array)}");
            Console.Write($"Most Common words are: ");
            foreach (var item in Most_Common_Words(array))
            {
                Console.Write($" {item},");
            }
            Console.WriteLine();
            Console.Write($"Least Common words are: ");
            foreach (var item in Least_Common_Words(array))
            {
                Console.Write($" {item},");
            }
            Console.WriteLine();
            sw.Stop();
            Console.WriteLine($"Compilation time: {sw.ElapsedMilliseconds} ms");
            Console.ReadLine();

        }
    }
}