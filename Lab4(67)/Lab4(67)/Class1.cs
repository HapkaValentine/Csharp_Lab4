using System;

namespace Lab6_7
{
    public class Point
    {
        private double _x;
        private double _y;

        public double X
        {
            get { return _x; }
            set
            {
                if (double.IsNaN(value) || double.IsInfinity(value))
                    throw new ArgumentException("Координата X должна быть конечным числом.");
                _x = value;
            }
        }

        public double Y
        {
            get { return _y; }
            set
            {
                if (double.IsNaN(value) || double.IsInfinity(value))
                    throw new ArgumentException("Координата Y должна быть конечным числом.");
                _y = value;
            }
        }

        public Point()
        {
            _x = 0;
            _y = 0;
        }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Point(Point other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            X = other.X;
            Y = other.Y;
        }

        public override string ToString()
        {
            return string.Format("({0}; {1})", _x, _y);
        }

        public double DistanceTo(Point other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));
            double dx = _x - other._x;
            double dy = _y - other._y;
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
            return new Point(p._x + n, p._y);
        }

        public static Point operator +(int n, Point p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));
            return new Point(p._x + n, p._y);
        }

        public static Point operator ++(Point p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));
            p.X = p._x + 1;
            return p;
        }

        public static Point operator --(Point p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));
            p.X = p._x - 1;
            return p;
        }

        public static explicit operator int(Point p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));
            return (int)p._x;
        }

        public static implicit operator double(Point p)
        {
            if (p == null) throw new ArgumentNullException(nameof(p));
            return p._y;
        }

        public static bool operator ==(Point a, Point b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (a is null || b is null) return false;
            return a._x == b._x && a._y == b._y;
        }

        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            Point p = obj as Point;
            return p != null && this == p;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_x, _y);
        }
    }
}