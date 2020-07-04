using System.Collections.Generic;

namespace Backgammon
{
    public class Bar
    {
        public List<Checker> WhiteCheckers { get; private set; }
        public List<Checker> BlackCheckers { get; private set; }

        public Bar()
        {
            WhiteCheckers = new List<Checker>();
            BlackCheckers = new List<Checker>();
        }

        public void AddWhiteCheckerToBar(Checker checker)
        {
            WhiteCheckers.Add(checker);
        }

        public void AddBlackCheckerToBar(Checker checker)
        {
            BlackCheckers.Add(checker);
        }
    }
}