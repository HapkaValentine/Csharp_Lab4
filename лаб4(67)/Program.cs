using Lab6_7;
using System;

namespace Lab6
{
    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("1 - тест конструкторов и ToString");
                Console.WriteLine("2 - расстояние DistanceTo");
                Console.WriteLine("3 - старые операторы '-' и '+' (теперь '+' это расстояние)");
                Console.WriteLine("4 - проверка на равенство (== / !=)");
                Console.WriteLine("5 - унарные операторы ++ и -- (Задание 7)");
                Console.WriteLine("6 - приведение типов int и double (Задание 7)");
                Console.WriteLine("7 - оператор '+' с целым числом (Задание 7)");
                Console.WriteLine("0 - выход");
                Console.Write("Выбор: ");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 0 || choice > 7)
                {
                    Console.Write("Ошибка! Введите 0-7: ");
                }

                if (choice == 0) break;

                switch (choice)
                {
                    case 1: TestConstructors(); break;
                    case 2: TestDistance(); break;
                    case 3: TestOperators(); break;
                    case 4: TestEquality(); break;
                    case 5: TestUnary(); break;
                    case 6: TestCast(); break;
                    case 7: TestPlusInt(); break;
                }
                Console.WriteLine();
            }
        }

        static double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine();
                if (double.TryParse(s, out double v) &&
                    !double.IsNaN(v) && !double.IsInfinity(v))
                {
                    return v;
                }
                Console.WriteLine("Ошибка! Введите число (конечное).");
            }
        }

        static Point ReadPoint(string name)
        {
            double x = ReadDouble($"  {name}.X = ");
            double y = ReadDouble($"  {name}.Y = ");
            return new Point(x, y);
        }

        static void TestConstructors()
        {
            Console.WriteLine("\n--- Конструкторы ---");

            var p1 = new Point();
            Console.WriteLine($"По умолчанию:        {p1}");

            var p2 = new Point(3, 4);
            Console.WriteLine($"С параметрами (3,4): {p2}");

            var p3 = new Point(p2);
            Console.WriteLine($"Копия p2:            {p3}");
            Console.WriteLine($"p2 == p3 ? {p2 == p3}");

            Console.WriteLine("\nПроверка защиты от NaN/Infinity:");
            try
            {
                var bad = new Point(double.NaN, 0);
                Console.WriteLine("Не должно было выполниться!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("  поймано: " + ex.Message);
            }

            try
            {
                var bad = new Point(0, double.PositiveInfinity);
                Console.WriteLine("Не должно было выполниться!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("  поймано: " + ex.Message);
            }

            Console.WriteLine("\nПроверка защиты через свойство X:");
            try
            {
                var p = new Point();
                p.X = double.NegativeInfinity;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("  поймано: " + ex.Message);
            }
        }

        static void TestDistance()
        {
            Console.WriteLine("\n--- Расстояние DistanceTo ---");
            Console.WriteLine("Введите точку A:");
            var a = ReadPoint("A");
            Console.WriteLine("Введите точку B:");
            var b = ReadPoint("B");

            double d = a.DistanceTo(b);
            Console.WriteLine($"Расстояние {a} -> {b} = {d:F4}");
        }

        static void TestOperators()
        {
            Console.WriteLine("\n--- Операторы '-' и '+' ---");
            Console.WriteLine("Введите точку A:");
            var a = ReadPoint("A");
            Console.WriteLine("Введите точку B:");
            var b = ReadPoint("B");

            double d1 = a - b;
            Console.WriteLine($"a - b (расстояние) = {d1:F4}");

            double d2 = a + b;
            Console.WriteLine($"a + b (расстояние) = {d2:F4}");

            try
            {
                Point n = null;
                double bad = a - n;
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine("  поймано: a - null -> ArgumentNullException");
            }
        }

        static void TestEquality()
        {
            Console.WriteLine("\n--- Равенство точек ---");
            Console.WriteLine("Введите точку A:");
            var a = ReadPoint("A");
            Console.WriteLine("Введите точку B:");
            var b = ReadPoint("B");

            Console.WriteLine($"a == b ? {a == b}");
            Console.WriteLine($"a != b ? {a != b}");
            Console.WriteLine($"a.Equals(b) ? {a.Equals(b)}");

            Point n = null;
            Console.WriteLine($"a == null ? {a == n}");
            Console.WriteLine($"null == null ? {n == null}");
        }

        // ================= НОВЫЕ ТЕСТЫ ДЛЯ 7 ЗАДАНИЯ =================

        static void TestUnary()
        {
            Console.WriteLine("\n--- Унарные ++ и -- ---");
            var p = ReadPoint("p");
            Console.WriteLine($"Исходная: {p}");

            var r1 = ++p;
            Console.WriteLine($"++p : результат = {r1}, p = {p}");

            var r2 = --p;
            Console.WriteLine($"--p : результат = {r2}, p = {p}");
        }

        static void TestCast()
        {
            Console.WriteLine("\n--- Приведение типов ---");
            var p = ReadPoint("p");

            int xi = (int)p;
            Console.WriteLine($"(int)p = {xi}   (целая часть X)");

            double yd = p; // неявное приведение
            Console.WriteLine($"double d = p;  d = {yd}   (значение Y)");

            // Проверка неявного приведения в выражении
            Console.WriteLine($"p + 0.5 = {p + 0.5}   (сработало неявное приведение Y)");
        }

        static void TestPlusInt()
        {
            Console.WriteLine("\n--- Оператор + с целым числом ---");
            var p = ReadPoint("p");

            Console.Write("  n = ");
            int n;
            while (!int.TryParse(Console.ReadLine(), out n)) Console.Write("Ошибка, введите число: ");

            Point right = p + n;
            Console.WriteLine($"p + n = {right}   (p не изменён: {p})");

            Point left = n + p;
            Console.WriteLine($"n + p = {left}   (p не изменён: {p})");
        }
    }
}