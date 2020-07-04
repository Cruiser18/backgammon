using System;

namespace Backgammon
{
    public class DiceService
    {
        public int Dice1 { get; private set; }
        public int Dice2 { get; private set; }
        public bool Dice1HasBeenUsed { get; set; }
        public bool Dice2HasBeenUsed { get; set; }

        public bool BothDiesHaveBeenUsed
        {
            get { return Dice1HasBeenUsed && Dice2HasBeenUsed; }
        }

        private Random _randomizer { get; set; }

        public DiceService()
        {
            _randomizer = new Random();
        }

        public void ThrowDies()
        {
            Dice1 = _randomizer.Next(1, 6);
            Dice2 = _randomizer.Next(1, 6);

            Dice1HasBeenUsed = false;
            Dice2HasBeenUsed = false;
        }
    }
}