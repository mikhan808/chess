using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace chess
{
    public class Coordinata
    {
        public Point Begin;
        public Point End;
        public Coordinata(Point b, Point e)
        {
            Begin = new Point(b.X, b.Y);
            End = new Point(e.X, e.Y);
        }
        public Coordinata(Coordinata c)
        {
            Begin = new Point(c.Begin.X, c.Begin.Y);
            End = new Point(c.End.X, c.End.Y);
        }
        public bool Compar(Coordinata c)
        {
            return (Begin.X == c.End.X && Begin.Y == c.End.Y && End.X == c.Begin.X && End.Y == c.Begin.Y);
        }
        public bool Compar(Point b, Point e)
        {
            return (Begin.X == b.X && Begin.Y == b.Y && End.X == e.X && End.Y == e.Y);
        }
        public bool Compar(Point b, int x, int y)
        {
            return (Begin.X == x && Begin.Y == y && End.X == b.X && End.Y == b.Y);
        }
        public bool Compar(int X, int Y, Point e)
        {
            return (Begin.X == e.X && Begin.Y == e.Y && End.X == X && End.Y == Y);
        }
        public bool Compar(int X, int Y, int x, int y)
        {
            return (Begin.X == x && Begin.Y == y && End.X == X && End.Y == Y);
        }
    }
}
