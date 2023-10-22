using System.Diagnostics;


namespace Homework1_MultiThreaded
{
    internal class Program
    {
        static string[] file;

        static int wordsCount = 0;
        static string shortestWord;
        static string longestWord;
        static int averageWordLength;
        static string[] mostCommonWords = new string[5];
        static string[] leastCommonWords = new string[5];

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

        static void Count_Words()
        {
            int count = 0;
            foreach (var item in file)
            {
                if (item.Length > 2) { continue; }
                count++;
            }
            wordsCount = count;
        }

        static void Shortest_Word()
        {
            string word = "No words in the document!";
            foreach (var item in file)
            {
                if (item.Length < 2) { continue; }

                if (item.Length < word.Length && item.Length > 3) { word = item; continue; }

                if (item.Length == 3) { word = item; break; }
            }
            shortestWord = word;
        }

        static void Longest_Word()
        {
            string word = "No words in the document!";
            foreach (var item in file)
            {
                if (item.Length > word.Length && item.Length > 2)
                {
                    word = item;
                }
            }
            longestWord = word;
        }

        static void Average_Word_Length()
        {
            foreach (var item in file)
            {
                if (item.Length > 2)
                {
                    averageWordLength = averageWordLength + item.Length;
                }
            }

            averageWordLength = averageWordLength / wordsCount;
        }

        static void Most_Common_Words()
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

            int i = 0;

            foreach (var item in words.OrderByDescending(key => key.Value))
            {
                if (i >= mostCommonWords.Length) break;
                mostCommonWords[i] = item.Key;
                i++;

            }
        }

        static void Least_Common_Words()
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

            int i = 0;

            foreach (var item in words.OrderBy(key => key.Value))
            {
                if (i >= leastCommonWords.Length) break;
                leastCommonWords[i] = item.Key;
                i++;
            }
        }

        public static void MultiThreaded(string[] array)
        {
            int coreCount = 6;
            Thread[] threads = new Thread[coreCount];

            threads[0] = new Thread(Count_Words);
            threads[0].Start();

            threads[1] = new Thread(Shortest_Word);
            threads[1].Start();

            threads[2] = new Thread(Longest_Word);
            threads[2].Start();

            threads[3] = new Thread(Average_Word_Length);
            threads[3].Start();

            threads[4] = new Thread(Most_Common_Words);
            threads[4].Start();

            threads[5] = new Thread(Least_Common_Words);
            threads[5].Start();

            foreach (var item in threads) { item.Join(); }

        }

        static void Main(string[] args)
        {
            Console.Write("Enter the path to the file: ");
            string location = Console.ReadLine();

            Stopwatch sw = new Stopwatch();
            sw.Start();

            string literature = "";
            if (File.Exists(location)) { literature = File.ReadAllText(location); }

            literature = Remove_Punctuation(literature);

            file = literature.Split(" ");

            MultiThreaded(file);

            Console.WriteLine($"Words Count: {wordsCount}");
            Console.WriteLine($"Shortest word is: {shortestWord}");
            Console.WriteLine($"Longest word is: {longestWord}");
            Console.WriteLine($"Average word length is: {averageWordLength}");

            Console.Write($"Most Common words are: ");
            foreach (var item in mostCommonWords)
            {
                Console.Write($" {item},");
            }
            Console.WriteLine();

            Console.Write($"Least Common words are: ");
            foreach (var item in leastCommonWords)
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
