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
                Console.WriteLine("1 - объединить L1 и L2");
                Console.WriteLine("2 - подсчитать элементы с равными соседями");
                Console.WriteLine("3 - анализ заказа блюд");
                Console.WriteLine("4 - согласные ровно в одном слове");
                Console.WriteLine("5 - многоборье: топ-3");
                Console.WriteLine("6 - расстояние от одной точки до другой");
                Console.WriteLine("7 - операции");
                Console.WriteLine("0 - выход");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) ||
                       choice < 0 || choice > 7)
                {
                    Console.Write("Ошибка! Введите 0-7: ");
                }

                if (choice == 0) break;

                switch (choice)
                {
                    case 1: Task1(); break;
                    case 2: Task2(); break;
                    case 3: Task3(); break;
                    case 4: Task4(); break;
                    case 5: Task5(); break;
                }

                Console.WriteLine();
            }
        }

        static void Task1()
        {
            Console.Write("Тип элементов L1 (1 - числа, 2 - текст): ");
            string choice1 = Console.ReadLine();

            Console.Write("Элементы L1 через пробел: ");
            string[] tokens1 = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            L1<int> list1Int = null;
            L1<string> list1Str = null;

            if (choice1 == "1")
            {
                list1Int = new L1<int>(new List<int>());
                foreach (string t in tokens1) list1Int.InsertInOrder(int.Parse(t));
            }
            else
            {
                list1Str = new L1<string>(new List<string>());
                foreach (string t in tokens1) list1Str.InsertInOrder(t);
            }

            Console.Write("Тип элементов L2 (1 - числа, 2 - текст): ");
            string choice2 = Console.ReadLine();

            Console.Write("Элементы L2 через пробел: ");
            string[] tokens2 = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            L2<int> list2Int = null;
            L2<string> list2Str = null;

            if (choice2 == "1")
            {
                list2Int = new L2<int>(new List<int>());
                foreach (string t in tokens2) list2Int.InsertInOrder(int.Parse(t));
            }
            else
            {
                list2Str = new L2<string>(new List<string>());
                foreach (string t in tokens2) list2Str.InsertInOrder(t);
            }

            Console.Write("\nL1: ");
            if (choice1 == "1") { foreach (int i in list1Int.GetArgs()) Console.Write(i + " "); }
            else { foreach (string s in list1Str.GetArgs()) Console.Write(s + " "); }
            Console.WriteLine();

            Console.Write("L2: ");
            if (choice2 == "1") { foreach (int i in list2Int.GetArgs()) Console.Write(i + " "); }
            else { foreach (string s in list2Str.GetArgs()) Console.Write(s + " "); }
            Console.WriteLine();

            Console.Write("\nРезультат: ");

            if (choice1 == "1" && choice2 == "1")
            {
                foreach (int i in list2Int.GetArgs()) list1Int.InsertInOrder(i);
                foreach (int i in list1Int.GetArgs()) Console.Write(i + " ");
            }
            else if (choice1 == "2" && choice2 == "2")
            {
                foreach (string s in list2Str.GetArgs()) list1Str.InsertInOrder(s);
                foreach (string s in list1Str.GetArgs()) Console.Write(s + " ");
            }
            else
            {
                if (choice1 == "1")
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
            Console.Write("Тип элементов списка (1 - числа, 2 - текст): ");
            string type = Console.ReadLine();

            Console.Write("Элементы списка через пробел: ");
            string[] tokens = Console.ReadLine()
                .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            int count;

            if (type == "1")
            {
                var list = new L1<int>(new List<int>());
                foreach (string t in tokens) list.InsertInOrder(int.Parse(t));

                Console.Write("Список: ");
                foreach (int i in list.GetArgs()) Console.Write(i + " ");
                Console.WriteLine();

                count = list.CountEqualNeighbors();
            }
            else
            {
                var list = new L1<string>(new List<string>());
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
            string[] menuTokens = Console.ReadLine()
                .Split(',', StringSplitOptions.RemoveEmptyEntries);

            var menu = new List<string>();
            foreach (string m in menuTokens)
                menu.Add(m.Trim());

            var cafe = new Cafe(menu);

            Console.Write("Сколько посетителей? ");
            int n;
            while (!int.TryParse(Console.ReadLine(), out n) || n < 0)
            {
                Console.Write("Ошибка! Введите целое неотрицательное число: ");
            }

            for (int i = 0; i < n; i++)
            {
                Console.Write($"Заказ посетителя #{i + 1} (через запятую): ");
                string line = Console.ReadLine();

                var order = new HashSet<string>();
                foreach (string s in line.Split(',', StringSplitOptions.RemoveEmptyEntries))
                    order.Add(s.Trim());

                cafe.AddVisitorOrder(order);
            }

            var all = cafe.DishesOrderedByAll();
            var some = cafe.DishesOrderedBySome();
            var nobody = cafe.DishesOrderedByNobody();

            Console.WriteLine("\n--- Результат ---");
            Console.WriteLine("Заказывали ВСЕ:        " +
                (all.Count > 0 ? string.Join(", ", all) : "—"));
            Console.WriteLine("Заказывали НЕКОТОРЫЕ:  " +
                (some.Count > 0 ? string.Join(", ", some) : "—"));
            Console.WriteLine("Не заказывал НИКТО:    " +
                (nobody.Count > 0 ? string.Join(", ", nobody) : "—"));
        }

        static void Task4()
        {
            Console.Write("Путь к файлу с текстом: ");
            string path = Console.ReadLine().Trim('"');

            if (!System.IO.File.Exists(path))
            {
                Console.WriteLine("Файл не найден: " + path);
                return;
            }

            string text = System.IO.File.ReadAllText(path);

            var analyzer = new TextAnalyzer(text);
            var result = analyzer.ConsonantsInExactlyOneWord();

            if (result.Count == 0)
                Console.WriteLine("Нет согласных, входящих ровно в одно слово.");
            else
                Console.WriteLine("Согласные, входящие ровно в одно слово: "
                                  + string.Join(", ", result));
        }
        static void CreateMultathlonFile(string path)
        {
            Console.Write("Сколько учеников (0-100)? ");
            int n;
            while (!int.TryParse(Console.ReadLine(), out n) || n < 0 || n > 100)
                Console.Write("Ошибка (0-100): ");

            using (var writer = new StreamWriter(path))
            {
                writer.WriteLine(n);
                for (int i = 0; i < n; i++)
                {
                    Console.Write($"Ученик #{i + 1} (Фамилия Имя б1 б2 б3 б4): ");
                    string line = Console.ReadLine().Trim();
                    writer.WriteLine(line);
                }
            }

            Console.WriteLine("Файл создан: " + path);
        }

        static void Task5()
        {
            Console.Write("Путь к файлу: ");
            string path = Console.ReadLine().Trim('"');

            if (!File.Exists(path))
            {
                Console.Write("Файл не найден. Создать новый? (1 - да, 0 - нет): ");
                if (Console.ReadLine() == "1")
                    CreateMultathlonFile(path);
                else
                    return;
            }

            var multathlon = new Multathlon();
            multathlon.LoadFromFile(path);

            var top = multathlon.GetTopWithTies(3);

            Console.WriteLine("\n--- Топ-3 (с учётом равных баллов) ---");
            if (top.Count == 0)
            {
                Console.WriteLine("Нет данных.");
                return;
            }

            foreach (var kv in top)
                Console.WriteLine($"{kv.Key}: {kv.Value}");
        }
    }
}