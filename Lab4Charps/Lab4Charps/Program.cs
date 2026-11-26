using Lab4Charps;
using System;
using System.Collections.Generic;
using System.IO;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n1 - объединить L1 и L2");
                Console.WriteLine("2 - подсчитать элементы с равными соседями");
                Console.WriteLine("3 - анализ заказа блюд");
                Console.WriteLine("4 - согласные ровно в одном слове");
                Console.WriteLine("5 - многоборье: топ-3");
                Console.WriteLine("0 - выход");

                int choice = ReadInt("Выбор: ", 0, 5);
                if (choice == 0) break;

                switch (choice)
                {
                    case 1: Task1(); break;
                    case 2: Task2(); break;
                    case 3: Task3(); break;
                    case 4: Task4(); break;
                    case 5: Task5(); break;
                }
            }
        }

        static int ReadInt(string prompt, int min, int max)
        {
            int result;
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out result) && result >= min && result <= max)
                {
                    return result;
                }
                Console.WriteLine($"Ошибка! Введите целое число от {min} до {max}.");
            }
        }

        static void Task1()
        {
            int choice1 = ReadInt("Тип элементов L1 (1 - числа, 2 - текст): ", 1, 2);
            Console.Write("Элементы L1 через пробел: ");
            string[] tokens1 = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            L1<int> list1Int = null;
            L1<string> list1Str = null;

            if (choice1 == 1)
            {
                list1Int = new L1<int>(new List<int>());
                foreach (string t in tokens1)
                {
                    int val;
                    if (int.TryParse(t, out val)) list1Int.InsertInOrder(val);
                }
            }
            else
            {
                list1Str = new L1<string>(new List<string>());
                foreach (string t in tokens1) list1Str.InsertInOrder(t);
            }

            int choice2 = ReadInt("Тип элементов L2 (1 - числа, 2 - текст): ", 1, 2);
            Console.Write("Элементы L2 через пробел: ");
            string[] tokens2 = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            L2<int> list2Int = null;
            L2<string> list2Str = null;

            if (choice2 == 1)
            {
                list2Int = new L2<int>(new List<int>());
                foreach (string t in tokens2)
                {
                    int val;
                    if (int.TryParse(t, out val)) list2Int.InsertInOrder(val);
                }
            }
            else
            {
                list2Str = new L2<string>(new List<string>());
                foreach (string t in tokens2) list2Str.InsertInOrder(t);
            }

            Console.Write("\nL1: ");
            if (choice1 == 1) { foreach (int i in list1Int.GetArgs()) Console.Write(i + " "); }
            else { foreach (string s in list1Str.GetArgs()) Console.Write(s + " "); }
            Console.WriteLine();

            Console.Write("L2: ");
            if (choice2 == 1) { foreach (int i in list2Int.GetArgs()) Console.Write(i + " "); }
            else { foreach (string s in list2Str.GetArgs()) Console.Write(s + " "); }
            Console.WriteLine();

            Console.Write("\nРезультат: ");
            if (choice1 == 1 && choice2 == 1)
            {
                foreach (int i in list2Int.GetArgs()) list1Int.InsertInOrder(i);
                foreach (int i in list1Int.GetArgs()) Console.Write(i + " ");
            }
            else if (choice1 == 2 && choice2 == 2)
            {
                foreach (string s in list2Str.GetArgs()) list1Str.InsertInOrder(s);
                foreach (string s in list1Str.GetArgs()) Console.Write(s + " ");
            }
            else
            {
                if (choice1 == 1)
                {
                    foreach (int i in list1Int.GetArgs()) Console.Write(i + " ");
                    foreach (string s in list2Str.GetArgs()) Console.Write(s + " ");
                }
                else
                {
                    foreach (string s in list1Str.GetArgs()) Console.Write(s + " ");
                    foreach (int i in list2Int.GetArgs()) Console.Write(i + " ");
                }
            }
            Console.WriteLine();
        }

        static void Task2()
        {
            int type = ReadInt("Тип элементов списка (1 - числа, 2 - текст): ", 1, 2);
            Console.Write("Элементы списка через пробел: ");
            string[] tokens = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            int count = 0;

            if (type == 1)
            {
                L1<int> list = new L1<int>(new List<int>());
                foreach (string t in tokens)
                {
                    int val;
                    if (int.TryParse(t, out val)) list.InsertInOrder(val);
                }
                Console.Write("Список: ");
                foreach (int i in list.GetArgs()) Console.Write(i + " ");
                Console.WriteLine();
                count = list.CountEqualNeighbors();
            }
            else
            {
                L1<string> list = new L1<string>(new List<string>());
                foreach (string t in tokens) list.InsertInOrder(t);
                Console.Write("Список: ");
                foreach (string s in list.GetArgs()) Console.Write(s + " ");
                Console.WriteLine();
                count = list.CountEqualNeighbors();
            }

            Console.WriteLine($"Элементов с равными соседями: {count}");
        }

        static void Task3()
        {
            Console.Write("Введите блюда меню через запятую: ");
            string[] menuTokens = Console.ReadLine().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            List<string> menu = new List<string>();
            foreach (string m in menuTokens)
            {
                if (m.Trim().Length > 0) menu.Add(m.Trim());
            }

            Cafe cafe = new Cafe(menu);
            int n = ReadInt("Сколько посетителей? ", 0, 1000);

            for (int i = 0; i < n; i++)
            {
                Console.Write($"Заказ посетителя #{i + 1} (через запятую): ");
                string line = Console.ReadLine();
                HashSet<string> order = new HashSet<string>();
                foreach (string s in line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (s.Trim().Length > 0) order.Add(s.Trim());
                }
                cafe.AddVisitorOrder(order);
            }

            List<string> all = cafe.DishesOrderedByAll();
            List<string> some = cafe.DishesOrderedBySome();
            List<string> nobody = cafe.DishesOrderedByNobody();

            Console.WriteLine("\n--- Результат ---");
            Console.WriteLine("Заказывали ВСЕ:        " + (all.Count > 0 ? string.Join(", ", all) : "—"));
            Console.WriteLine("Заказывали НЕКОТОРЫЕ:  " + (some.Count > 0 ? string.Join(", ", some) : "—"));
            Console.WriteLine("Не заказывал НИКТО:    " + (nobody.Count > 0 ? string.Join(", ", nobody) : "—"));
        }

        static void Task4()
        {
            string path = FileHelper.FindProjectFile("task4.txt");
            if (path == null)
            {
                Console.WriteLine("Файл task4.txt не найден в папке проекта.");
                return;
            }

            Console.WriteLine("Читаю файл: " + path);
            string text = File.ReadAllText(path);

            TextAnalyzer analyzer = new TextAnalyzer(text);
            List<char> result = analyzer.ConsonantsInExactlyOneWord();

            if (result.Count == 0)
                Console.WriteLine("Нет согласных, входящих ровно в одно слово.");
            else
                Console.WriteLine("Согласные, входящие ровно в одно слово: " + string.Join(", ", result));
        }

        static void CreateMultathlonFile(string path)
        {
            int n = ReadInt("Сколько учеников (0-100)? ", 0, 100);

            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(n);
                for (int i = 0; i < n; i++)
                {
                    while (true)
                    {
                        Console.Write($"Ученик #{i + 1} (Фамилия Имя б1 б2 б3 б4): ");
                        string line = Console.ReadLine().Trim();
                        string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        if (parts.Length == 6)
                        {
                            writer.WriteLine(line);
                            break;
                        }
                        Console.WriteLine("Ошибка! Введите ровно 6 элементов (Фамилия, Имя и 4 оценки).");
                    }
                }
            }
            Console.WriteLine("Файл создан: " + path);
        }

        static void Task5()
        {
            string projectDir = FileHelper.FindProjectFolder();
            string path = Path.Combine(projectDir, "task5.txt");

            if (!File.Exists(path))
            {
                Console.Write($"Файл task5.txt не найден. Создать новый? (1 - да, 0 - нет): ");
                if (ReadInt("", 0, 1) == 1)
                {
                    CreateMultathlonFile(path);
                }
                else
                {
                    return;
                }
            }

            Console.WriteLine("Читаю файл: " + path);
            Multathlon multathlon = new Multathlon();
            multathlon.LoadFromFile(path);

            List<KeyValuePair<string, int>> top = multathlon.GetTopWithTies(3);

            Console.WriteLine("\n--- Топ-3 (с учётом равных баллов) ---");
            if (top.Count == 0)
            {
                Console.WriteLine("Нет данных.");
                return;
            }

            foreach (KeyValuePair<string, int> kv in top)
            {
                Console.WriteLine($"{kv.Key}: {kv.Value}");
            }
        }
    }
}