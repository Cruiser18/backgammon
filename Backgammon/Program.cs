using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;

namespace Backgammon
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Instructions here!\n");

            var container = new UnityContainer();

            var diceService = container.Resolve<DiceService>();
            container.RegisterInstance(diceService, InstanceLifetime.Singleton);

            container.RegisterType<MoveValidator>();

            var game = container.Resolve<BackgammonGame>();

            while (!game.GameOver)
            {
                game.RunGameLoop();
            }

            Console.WriteLine($"The game has been concluded. The winner is {game.Winner}! Press any key to close program.");

            Console.ReadLine();
        }
    }
}
