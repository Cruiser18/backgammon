using System;
using System.Linq;
using System.Text;

namespace Backgammon
{
    internal class BackgammonGame
    {
        

        public bool GameOver { get; private set; }
        public int Winner { get; private set; }

        public Point[] _points { get; private set; }
        
        private Bar _bar { get; set; }
        private int _playerTurn { get; set; }

        private DiceService _diceService { get; set; }
        private MoveValidator _moveValidator { get; set; }

        public BackgammonGame(DiceService diceService, MoveValidator moveValidator)
        {
            _points = new Point[24];
            for (int i = 0; i < _points.Length; i++)
            {
                _points[i] = new Point();
            }

            GameOver = false;
            _playerTurn = 1;

            _bar = new Bar();

            SetupCheckers();

            OutputBoard();

            

            _diceService = diceService;
            _moveValidator = moveValidator;
        }

        public void RunGameLoop()
        {
            _diceService.ThrowDies();

            bool validInput =false;

            Console.WriteLine($"It is player {_playerTurn.ToString()}'s turn. The dies rolled to a {_diceService.Dice1.ToString()} and a {_diceService.Dice2.ToString()}. Please make you moves based off that.");

            while (!validInput || !_diceService.BothDiesHaveBeenUsed)
            {
                validInput = MakeMove();

                OutputBoard();
            }

            CheckWinConditions();

            SwitchPlayer();
        }

        private bool MakeMove()
        {
            var rawInput = Console.ReadLine();

            var parsedInputs = rawInput.Split(' ');

            int input1;
            int input2;

            var validInput1 = int.TryParse(parsedInputs[0], out input1);
            var validInput2 = int.TryParse(parsedInputs[1], out input2);

            if (!validInput1 ||
                !validInput2 ||
                input1 > 24 ||
                input1 < 1 ||
                input2 > 24 ||
                input2 < 1)
            {
                Console.WriteLine("The input is not valid! Please try again.");
                return false;
            }

            if(!MoveChecker(input1, input2))
            {
                Console.WriteLine("The move is not valid! Please try again.");
                return false;
            }

            return true;
        }

        private bool MoveChecker(int input1, int input2)
        {
            // There must not be more than one enemy checker on the position marked by input 2
            if(!_moveValidator.ValidateMove(input1, input2, _playerTurn, _points))
            {
                return false;
            }

            if (Math.Abs(input1 - input2) == _diceService.Dice1 && !_diceService.Dice1HasBeenUsed)
            {
                if (_playerTurn == 1 && _points[input2 - 1].Checkers.Count() == 1 && _points[input2 - 1].Checkers.First().Color == Color.Black)
                {
                    _points[input2 - 1].RemoveOneChecker();
                    _bar.AddBlackCheckerToBar(new Checker(Color.Black));
                    Console.WriteLine($"Player 1 hit player 2's blot on point {input2 - 1} and the checker has been placed on the bar!");
                }
                else if(_playerTurn == 2 && _points[input2 - 1].Checkers.Count() == 1 && _points[input2 - 1].Checkers.First().Color == Color.White)
                {
                    _points[input2 - 1].RemoveOneChecker();
                    _bar.AddWhiteCheckerToBar(new Checker(Color.White));
                    Console.WriteLine($"Player 2 hit player 1's blot on point {input2 - 1} and the checker has been placed on the bar!");
                }

                _points[input2 - 1].AddChecker(new Checker(_playerTurn == 1 ? Color.White : Color.Black));
                _points[input1 - 1].RemoveOneChecker();
                _diceService.Dice1HasBeenUsed = true;
                Console.WriteLine("Dice 1 has been used to make a move.");
            }
            else if(Math.Abs(input1 - input2) == _diceService.Dice2 && !_diceService.Dice2HasBeenUsed)
            {
                if (_playerTurn == 1 && _points[input2 - 1].Checkers.Count() == 1 && _points[input2 - 1].Checkers.First().Color == Color.Black)
                {
                    _points[input2 - 1].RemoveOneChecker();
                    _bar.AddBlackCheckerToBar(new Checker(Color.Black));
                    Console.WriteLine($"Player 1 hit player 2's blot on point {input2 - 1} and the checker has been placed on the bar!");
                }
                else if (_playerTurn == 2 && _points[input2 - 1].Checkers.Count() == 1 && _points[input2 - 1].Checkers.First().Color == Color.White)
                {
                    _points[input2 - 1].RemoveOneChecker();
                    _bar.AddWhiteCheckerToBar(new Checker(Color.White));
                    Console.WriteLine($"Player 2 hit player 1's blot on point {input2 - 1} and the checker has been placed on the bar!");
                }

                _points[input2 - 1].AddChecker(new Checker(_playerTurn == 1 ? Color.White : Color.Black));
                _points[input1 - 1].RemoveOneChecker();
                _diceService.Dice2HasBeenUsed = true;
                Console.WriteLine("Dice 2 has been used to make a move.");
            }
            
            return true;
        }

        private void CheckWinConditions()
        {
            // Determine win conditions
            // DeclareWinner(_playerTurn);
        }

        private void DeclareWinner(int player)
        {
            Winner = _playerTurn;
            GameOver = true;
        }

        private void SwitchPlayer()
        {
            _playerTurn = _playerTurn == 1 ? 2 : 1;
        }

        private void SetupCheckers()
        {
            // Black checkers
            for (int i = 0; i < 7; i++)
            {
                _points[5].AddChecker(new Checker(Color.Black));
            }
            for (int i = 0; i < 4; i++)
            {
                _points[7].AddChecker(new Checker(Color.Black));
            }
            for (int i = 0; i < 7; i++)
            {
                _points[12].AddChecker(new Checker(Color.Black));
            }
            for (int i = 0; i < 3; i++)
            {
                _points[23].AddChecker(new Checker(Color.Black));
            }

            // White checkers
            for (int i = 0; i < 7; i++)
            {
                _points[0].AddChecker(new Checker(Color.White));
            }
            for (int i = 0; i < 4; i++)
            {
                _points[11].AddChecker(new Checker(Color.White));
            }
            for (int i = 0; i < 7; i++)
            {
                _points[16].AddChecker(new Checker(Color.White));
            }
            for (int i = 0; i < 3; i++)
            {
                _points[18].AddChecker(new Checker(Color.White));
            }
        }

        private void OutputBoard()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.AppendLine();

            stringBuilder.AppendLine("24 23 22 21 20 19  18 17 16 15 14 13");

            for (int i = 23; i > 11; i--)
            {
                var numCheckers = _points[i].Checkers.Count;
                var stringColor = "";
                var checkersString = "";

                if (numCheckers > 0)
                {
                    var color = _points[i].Checkers.First().Color;
                    stringColor = color == Color.Black ? "B" : "W";
                    checkersString = numCheckers.ToString("0");
                }
                else
                {
                    checkersString = numCheckers.ToString("00");
                }
                if (i == 18)
                    checkersString += " ";

                stringBuilder.Append(stringColor + checkersString + " ");
            }

            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"                 B{_bar.BlackCheckers.Count.ToString("0")}");
            stringBuilder.AppendLine($"                 W{_bar.WhiteCheckers.Count.ToString("0")}");
            stringBuilder.AppendLine();

            for (int i = 0; i < 12; i++)
            {
                var numCheckers = _points[i].Checkers.Count;
                var stringColor = "";
                var checkersString = "";

                if (numCheckers > 0)
                {
                    var color = _points[i].Checkers.First().Color;
                    stringColor = color == Color.Black ? "B" : "W";
                    checkersString = numCheckers.ToString("0");
                }
                else
                {
                    checkersString = numCheckers.ToString("00");
                }
                if (i == 5)
                    checkersString += " ";

                stringBuilder.Append(stringColor + checkersString + " ");
            }

            stringBuilder.AppendLine().AppendLine("1  2  3  4  5  6   7  8  9  10 11 12");

            Console.WriteLine(stringBuilder);
        }
    }
}