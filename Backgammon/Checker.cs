namespace Backgammon
{
    public class Checker
    {
        public Color Color { get; }

        public Checker(Color color)
        {
            Color = color;
        }
    }

    public enum Color
    {
        White,
        Black
    }
}