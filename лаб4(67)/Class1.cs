using System;

namespace Lab6_7
{
    public class Point
    {
        private double x;
        private double y;

        public double X
        {
            get => x;
            set
            {
                if (double.IsNaN(value) || double.IsInfinity(value))
                    throw new ArgumentException("Координата X должна быть конечным числом.");
                x = value;
            }
        }

        public double Y
        {
            get => y;
            set
            {
                if (double.IsNaN(value) || double.IsInfinity(value))
                    throw new ArgumentException("Координата Y должна быть конечным числом.");
                y = value;
            }
        }

        public Point() { x = 0; y = 0; }
        public Point(double x, double y) { X = x; Y = y; }
        public Point(Point other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            X = other.X;
            Y = other.Y;
        }

        public override string ToString() => $"({x}; {y})";

        public double DistanceTo(Point other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            double dx = x - other.x;
            double dy = y - other.y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public static double operator -(Point a, Point b)
        {
            if (a == null || b == null) throw new ArgumentNullException();
            return a.DistanceTo(b);
        }

        public static double operator +(Point a, Point b)
        {
            if (a == null || b == null) throw new ArgumentNullException();
            return a.DistanceTo(b);
        }

        public static Point operator +(Point p, int n)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));
            return new Point(p.x + n, p.y);
        }

        public static Point operator +(int n, Point p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));
            return new Point(p.x + n, p.y);
        }

        public static Point operator ++(Point p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));
            p.X = p.x + 1;
            return p;
        }

        public static Point operator --(Point p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));
            p.X = p.x - 1;
            return p;
        }

        public static explicit operator int(Point p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));
            return (int)p.x;
        }

        public static implicit operator double(Point p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));
            return p.y;
        }

        public static bool operator ==(Point a, Point b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a.x == b.x && a.y == b.y;
        }

        public static bool operator !=(Point a, Point b) => !(a == b);
        public override bool Equals(object obj) => obj is Point p && this == p;
        public override int GetHashCode() => HashCode.Combine(x, y);
    }
}