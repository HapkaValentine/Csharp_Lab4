using System;
using System.Collections.Generic;
using System.IO;

namespace Lab4Charps
{
    public static class FileHelper
    {
        public static string FindProjectFolder()
        {
            DirectoryInfo dir = new DirectoryInfo(AppContext.BaseDirectory);
            while (dir != null)
            {
                if (dir.GetFiles("*.csproj").Length > 0)
                    return dir.FullName;
                dir = dir.Parent;
            }
            return AppContext.BaseDirectory;
        }

        public static string FindProjectFile(string fileName)
        {
            string nearExe = Path.Combine(AppContext.BaseDirectory, fileName);
            if (File.Exists(nearExe)) return nearExe;

            string projectDir = FindProjectFolder();
            string inProject = Path.Combine(projectDir, fileName);
            if (File.Exists(inProject)) return inProject;

            return null;
        }
    }

    public class L1<T> where T : IComparable<T>
    {
        private List<T> _args;

        public L1(List<T> initialArgs)
        {
            _args = initialArgs;
        }

        public List<T> GetArgs()
        {
            return _args;
        }

        public void InsertInOrder(T item)
        {
            int insertIndex = 0;
            while (insertIndex < _args.Count && _args[insertIndex].CompareTo(item) < 0)
            {
                insertIndex++;
            }
            _args.Insert(insertIndex, item);
        }

        public int CountEqualNeighbors()
        {
            int count = 0;
            for (int i = 1; i < _args.Count - 1; i++)
            {
                if (_args[i - 1].CompareTo(_args[i + 1]) == 0)
                {
                    count++;
                }
            }
            return count;
        }
    }

    public class L2<T> where T : IComparable<T>
    {
        private List<T> _args;

        public L2(List<T> initialArgs)
        {
            _args = initialArgs;
        }

        public List<T> GetArgs()
        {
            return _args;
        }

        public void InsertInOrder(T item)
        {
            int insertIndex = 0;
            while (insertIndex < _args.Count && _args[insertIndex].CompareTo(item) < 0)
            {
                insertIndex++;
            }
            _args.Insert(insertIndex, item);
        }
    }

    public class Cafe
    {
        private List<string> _menu;
        private List<HashSet<string>> _visitorsOrders;

        public Cafe(List<string> menu)
        {
            _menu = menu;
            _visitorsOrders = new List<HashSet<string>>();
        }

        public void AddVisitorOrder(HashSet<string> order)
        {
            _visitorsOrders.Add(order);
        }

        public List<string> GetMenu()
        {
            return _menu;
        }

        public List<HashSet<string>> GetVisitorsOrders()
        {
            return _visitorsOrders;
        }

        public int CountOrderedBy(string dish)
        {
            int count = 0;
            foreach (HashSet<string> order in _visitorsOrders)
            {
                if (order.Contains(dish))
                {
                    count++;
                }
            }
            return count;
        }

        public List<string> DishesOrderedByAll()
        {
            List<string> result = new List<string>();
            if (_visitorsOrders.Count == 0) return result;

            foreach (string dish in _menu)
            {
                if (CountOrderedBy(dish) == _visitorsOrders.Count)
                {
                    result.Add(dish);
                }
            }
            return result;
        }

        public List<string> DishesOrderedBySome()
        {
            List<string> result = new List<string>();
            if (_visitorsOrders.Count == 0) return result;

            foreach (string dish in _menu)
            {
                int count = CountOrderedBy(dish);
                if (count > 0 && count < _visitorsOrders.Count)
                {
                    result.Add(dish);
                }
            }
            return result;
        }

        public List<string> DishesOrderedByNobody()
        {
            List<string> result = new List<string>();
            foreach (string dish in _menu)
            {
                if (CountOrderedBy(dish) == 0)
                {
                    result.Add(dish);
                }
            }
            return result;
        }
    }

    public class TextAnalyzer
    {
        private string _text;
        public static readonly HashSet<char> Consonants = new HashSet<char>("бвгджзйклмнпрстфхцчшщ");

        public TextAnalyzer(string text)
        {
            _text = text;
        }

        public List<char> ConsonantsInExactlyOneWord()
        {
            char[] separators = new char[] {
                ' ', '.', ',', '!', '?', ';', ':', '-', '—',
                '\n', '\r', '\t', '"', '\'', '(', ')', '«', '»'
            };

            string[] words = _text.ToLower().Split(separators, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<char, int> wordCount = new Dictionary<char, int>();
            foreach (char c in Consonants)
            {
                wordCount[c] = 0;
            }

            foreach (string word in words)
            {
                HashSet<char> lettersInWord = new HashSet<char>();
                foreach (char c in word)
                {
                    if (Consonants.Contains(c))
                    {
                        lettersInWord.Add(c);
                    }
                }

                foreach (char c in lettersInWord)
                {
                    wordCount[c]++;
                }
            }

            List<char> result = new List<char>();
            foreach (KeyValuePair<char, int> pair in wordCount)
            {
                if (pair.Value == 1)
                {
                    result.Add(pair.Key);
                }
            }

            result.Sort();
            return result;
        }
    }

    public class Multathlon
    {
        private Dictionary<string, int> _results;

        public Multathlon()
        {
            _results = new Dictionary<string, int>();
        }

        public Dictionary<string, int> GetResults()
        {
            return _results;
        }

        public void LoadFromFile(string path)
        {
            _results.Clear();
            string[] lines = File.ReadAllLines(path);
            if (lines.Length == 0) return;

            int n;
            if (!int.TryParse(lines[0].Trim(), out n)) return;

            for (int i = 1; i <= n && i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (line.Length == 0) continue;

                string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 6) continue;

                string name = parts[0] + " " + parts[1];
                int sum = 0;
                bool isValid = true;

                for (int j = 2; j < 6; j++)
                {
                    int score;
                    if (int.TryParse(parts[j], out score) && score >= 0 && score <= 10)
                    {
                        sum += score;
                    }
                    else
                    {
                        isValid = false;
                        break;
                    }
                }

                if (isValid)
                {
                    if (!_results.ContainsKey(name) || _results[name] < sum)
                    {
                        _results[name] = sum;
                    }
                }
            }
        }

        private class DescendingComparer : IComparer<KeyValuePair<string, int>>
        {
            public int Compare(KeyValuePair<string, int> x, KeyValuePair<string, int> y)
            {
                return y.Value.CompareTo(x.Value);
            }
        }

        public List<KeyValuePair<string, int>> GetTopWithTies(int top = 3)
        {
            List<KeyValuePair<string, int>> result = new List<KeyValuePair<string, int>>();
            if (_results.Count == 0) return result;

            List<KeyValuePair<string, int>> sorted = new List<KeyValuePair<string, int>>(_results);
            sorted.Sort(new DescendingComparer());

            int realTop = Math.Min(top, sorted.Count);
            int threshold = sorted[realTop - 1].Value;

            foreach (KeyValuePair<string, int> kv in sorted)
            {
                if (kv.Value >= threshold)
                {
                    result.Add(kv);
                }
            }

            return result;
        }
    }
}