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
                Console.WriteLine("\n1 - тест конструкторов и ToString");
                Console.WriteLine("2 - расстояние DistanceTo");
                Console.WriteLine("3 - старые операторы '-' и '+' (теперь '+' это расстояние)");
                Console.WriteLine("4 - проверка на равенство (== / !=)");
                Console.WriteLine("5 - унарные операторы ++ и -- (Задание 7)");
                Console.WriteLine("6 - приведение типов int и double (Задание 7)");
                Console.WriteLine("7 - оператор '+' с целым числом (Задание 7)");
                Console.WriteLine("0 - выход");

                int choice = ReadInt("Выбор: ", 0, 7);
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

        static double ReadDouble(string prompt)
        {
            double result;
            while (true)
            {
                Console.Write(prompt);
                if (double.TryParse(Console.ReadLine(), out result) && !double.IsNaN(result) && !double.IsInfinity(result))
                {
                    return result;
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
            Point p1 = new Point();
            Console.WriteLine($"По умолчанию:        {p1}");

            Point p2 = new Point(3, 4);
            Console.WriteLine($"С параметрами (3,4): {p2}");

            Point p3 = new Point(p2);
            Console.WriteLine($"Копия p2:            {p3}");
            Console.WriteLine($"p2 == p3 ? {p2 == p3}");

            Console.WriteLine("\nПроверка защиты от NaN/Infinity:");
            try
            {
                Point bad = new Point(double.NaN, 0);
                Console.WriteLine("Не должно было выполниться!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("  поймано: " + ex.Message);
            }

            try
            {
                Point bad = new Point(0, double.PositiveInfinity);
                Console.WriteLine("Не должно было выполниться!");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("  поймано: " + ex.Message);
            }

            Console.WriteLine("\nПроверка защиты через свойство X:");
            try
            {
                Point p = new Point();
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
            Point a = ReadPoint("A");
            Console.WriteLine("Введите точку B:");
            Point b = ReadPoint("B");

            double d = a.DistanceTo(b);
            Console.WriteLine($"Расстояние {a} -> {b} = {d:F4}");
        }

        static void TestOperators()
        {
            Console.WriteLine("\n--- Операторы '-' и '+' ---");
            Console.WriteLine("Введите точку A:");
            Point a = ReadPoint("A");
            Console.WriteLine("Введите точку B:");
            Point b = ReadPoint("B");

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
            Point a = ReadPoint("A");
            Console.WriteLine("Введите точку B:");
            Point b = ReadPoint("B");

            Console.WriteLine($"a == b ? {a == b}");
            Console.WriteLine($"a != b ? {a != b}");
            Console.WriteLine($"a.Equals(b) ? {a.Equals(b)}");

            Point n = null;
            Console.WriteLine($"a == null ? {a == n}");
            Console.WriteLine($"null == null ? {n == null}");
        }

        static void TestUnary()
        {
            Console.WriteLine("\n--- Унарные ++ и -- ---");
            Point p = ReadPoint("p");
            Console.WriteLine($"Исходная: {p}");

            Point r1 = ++p;
            Console.WriteLine($"++p : результат = {r1}, p = {p}");

            Point r2 = --p;
            Console.WriteLine($"--p : результат = {r2}, p = {p}");
        }

        static void TestCast()
        {
            Console.WriteLine("\n--- Приведение типов ---");
            Point p = ReadPoint("p");

            int xi = (int)p;
            Console.WriteLine($"(int)p = {xi}   (целая часть X)");

            double yd = p;
            Console.WriteLine($"double d = p;  d = {yd}   (значение Y)");

            Console.WriteLine($"p + 0.5 = {p + 0.5}   (сработало неявное приведение Y)");
        }

        static void TestPlusInt()
        {
            Console.WriteLine("\n--- Оператор + с целым числом ---");
            Point p = ReadPoint("p");

            int n = ReadInt("  n = ", int.MinValue, int.MaxValue);

            Point right = p + n;
            Console.WriteLine($"p + n = {right}   (p не изменён: {p})");

            Point left = n + p;
            Console.WriteLine($"n + p = {left}   (p не изменён: {p})");
        }
    }
}