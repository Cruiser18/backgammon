using System.Collections.Generic;

namespace Backgammon
{
    public class Point
    {
        public List<Checker> Checkers { get; private set; }

        public Point()
        {
            Checkers = new List<Checker>();
        }

        public void AddChecker(Checker checker)
        {
            Checkers.Add(checker);
        }

        public void RemoveOneChecker()
        {
            Checkers.RemoveAt(0);
        }
    }
}