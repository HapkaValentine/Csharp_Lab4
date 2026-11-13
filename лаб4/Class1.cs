using System;
using System.Collections.Generic;

namespace Lab4Charps
{
    public class L1<T> where T : IComparable<T>
    {
        private List<T> args;
        public L1(List<T> initialArgs) { args = initialArgs; }
        public List<T> GetArgs() { return args; }

        public void InsertInOrder(T item)
        {
            int insertIndex = 0;
            while (insertIndex < args.Count && args[insertIndex].CompareTo(item) < 0)
            {
                insertIndex++;
            }
            args.Insert(insertIndex, item);
        }

        public int CountEqualNeighbors()
        {
            int count = 0;
            for (int i = 1; i < args.Count - 1; i++)
                if (args[i - 1].CompareTo(args[i + 1]) == 0)
                    count++;
            return count;
        }
    }

    public class L2<T> where T : IComparable<T>
    {
        private List<T> args;
        public L2(List<T> initialArgs) { args = initialArgs; }
        public List<T> GetArgs() { return args; }

        public void InsertInOrder(T item)
        {
            int insertIndex = 0;
            while (insertIndex < args.Count && args[insertIndex].CompareTo(item) < 0)
            {
                insertIndex++;
            }
            args.Insert(insertIndex, item);
        }
    }

    public class Cafe
    {
        private List<string> menu;
        private List<HashSet<string>> visitorsOrders;

        public Cafe(List<string> menu)
        {
            this.menu = menu;
            visitorsOrders = new List<HashSet<string>>();
        }

        public void AddVisitorOrder(HashSet<string> order)
        {
            visitorsOrders.Add(order);
        }

        public List<string> GetMenu() => menu;
        public List<HashSet<string>> GetVisitorsOrders() => visitorsOrders;
        public int CountOrderedBy(string dish)
        {
            int count = 0;
            foreach (var order in visitorsOrders)
                if (order.Contains(dish))
                    count++;
            return count;
        }
        public List<string> DishesOrderedByAll()
        {
            var result = new List<string>();
            if (visitorsOrders.Count == 0) return result;

            foreach (string dish in menu)
                if (CountOrderedBy(dish) == visitorsOrders.Count)
                    result.Add(dish);
            return result;
        }
        public List<string> DishesOrderedBySome()
        {
            var result = new List<string>();
            if (visitorsOrders.Count == 0) return result;

            foreach (string dish in menu)
            {
                int c = CountOrderedBy(dish);
                if (c > 0 && c < visitorsOrders.Count)
                    result.Add(dish);
            }
            return result;
        }
        public List<string> DishesOrderedByNobody()
        {
            var result = new List<string>();
            foreach (string dish in menu)
                if (CountOrderedBy(dish) == 0)
                    result.Add(dish);
            return result;
        }
    }
    public class TextAnalyzer
    {
        private string text;
        public static readonly HashSet<char> Consonants =
            new HashSet<char>("бвгджзйклмнпрстфхцчшщ");

        public TextAnalyzer(string text)
        {
            this.text = text;
        }

        public List<char> ConsonantsInExactlyOneWord()
        {
            char[] separators = {
            ' ', '.', ',', '!', '?', ';', ':', '-', '—',
            '\n', '\r', '\t', '"', '\'', '(', ')', '«', '»'
        };

            string[] words = text.ToLower()
                .Split(separators, StringSplitOptions.RemoveEmptyEntries);

            var wordCount = new Dictionary<char, int>();
            foreach (char c in Consonants)
                wordCount[c] = 0;

            foreach (string word in words)
            {
                var lettersInWord = new HashSet<char>();
                foreach (char c in word)
                    if (Consonants.Contains(c))
                        lettersInWord.Add(c);

                foreach (char c in lettersInWord)
                    wordCount[c]++;
            }

            var result = new List<char>();
            foreach (var pair in wordCount)
                if (pair.Value == 1)
                    result.Add(pair.Key);

            result.Sort();
            return result;
        }
    }
    public class Multathlon
    {
        private Dictionary<string, int> results;

        public Multathlon()
        {
            results = new Dictionary<string, int>();
        }

        public Dictionary<string, int> GetResults() => results;

        public void LoadFromFile(string path)
        {
            results.Clear();

            string[] lines = System.IO.File.ReadAllLines(path);
            if (lines.Length == 0) return;

            int n = int.Parse(lines[0].Trim());

            for (int i = 1; i <= n && i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (line.Length == 0) continue;

                string[] parts = line.Split(
                    new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length < 6) continue;

                string name = parts[0] + " " + parts[1];

                int sum = 0;
                for (int j = 2; j < 6; j++)
                    sum += int.Parse(parts[j]);

                if (!results.ContainsKey(name) || results[name] < sum)
                    results[name] = sum;
            }
        }

        public List<KeyValuePair<string, int>> GetTopWithTies(int top = 3)
        {
            var result = new List<KeyValuePair<string, int>>();
            if (results.Count == 0) return result;

            var sorted = new List<KeyValuePair<string, int>>(results);
            sorted.Sort((a, b) => b.Value.CompareTo(a.Value));

            int realTop = Math.Min(top, sorted.Count);
            int threshold = sorted[realTop - 1].Value;

            foreach (var kv in sorted)
                if (kv.Value >= threshold)
                    result.Add(kv);

            return result;
        }

        public List<KeyValuePair<string, int>> GetTopWithTiesSortedList(int top = 3)
        {
            var sortedList = new SortedList<string, int>(results);
            var temp = new List<KeyValuePair<string, int>>(sortedList);
            temp.Sort((a, b) => b.Value.CompareTo(a.Value));

            var result = new List<KeyValuePair<string, int>>();
            if (temp.Count == 0) return result;

            int realTop = Math.Min(top, temp.Count);
            int threshold = temp[realTop - 1].Value;

            foreach (var kv in temp)
                if (kv.Value >= threshold)
                    result.Add(kv);

            return result;
        }
    }



}
