using System;
using System.Linq;

namespace Backgammon
{
    public class MoveValidator
    {
        private DiceService _diceService;

        public MoveValidator(DiceService diceService)
        {
            _diceService = diceService;
        }

        public bool ValidateMove(int input1, int input2, int playerTurn, Point[] points)
        {
            // Ensure input matches dice and dice have not been used
            if ((Math.Abs(input1 - input2) != _diceService.Dice1 || _diceService.Dice1HasBeenUsed) && (Math.Abs(input1 - input2) != _diceService.Dice2 || _diceService.Dice2HasBeenUsed))
            {
                return false;
            }

            // Ensure move is in the right direction
            if (playerTurn == 1 && input1 < input2)
            {
                Console.WriteLine("Player 1 can only move to a lower numbered point!");
                return false;
            }
            else if (playerTurn == 2 && input1 > input2)
            {
                Console.WriteLine("Player 2 can only move to a lower numbered point!");
                return false;
            }

            // Check who is moving who's checkers
            if (playerTurn == 1 && points[input1 - 1].Checkers.First().Color != Color.White)
            {
                Console.WriteLine("Only player 1 can move player 1 checkers!");
                return false;
            }
            if (playerTurn == 2 && points[input1 - 1].Checkers.First().Color != Color.Black)
            {
                Console.WriteLine("Only player 2 can move player 2 checkers!");
                return false;
            }

            // Ensure move is made onto a slot that does not have more than 1 enemy checker on it
            if(playerTurn == 1 && points[input2 - 1].Checkers.Count() > 1 && points[input2 - 1].Checkers.First().Color == Color.Black)
            {
                Console.WriteLine("Player 1 cannot move onto a point with more than one opponent checker on it!");
                return false;
            }
            if (playerTurn == 2 && points[input2 - 1].Checkers.Count() > 1 && points[input2 - 1].Checkers.First().Color == Color.White)
            {
                Console.WriteLine("Player 2 cannot move onto a point with more than one opponent checker on it!");
                return false;
            }

            return true;
        }
    }
}