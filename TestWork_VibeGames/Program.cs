using System.Threading;
using System.Xml.Linq;

namespace TestWork_VibeGames
{
    internal class Program
    {
        AutoResetEvent waitHandler = new AutoResetEvent(true);
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Init();

            Console.WriteLine($"поток 1 не работает {game.MyThread.ThreadState}");
            Console.WriteLine($"{String.Join(", \n", game.GetRandomCar())}");
            Random random = new Random();
            Car _car = game.Cars[random.Next(game.Cars.Count)];
            Console.WriteLine("\n");
            Console.WriteLine(_car);
            Console.WriteLine($@"{game.GetNearbyPlayer(_car)}");
            Console.WriteLine("Hello, World!");
            foreach(var dict in game.GetNearbyPlayer(_car))
            {
                Console.WriteLine(@$"{dict.Key.Nickname}: {dict.Value}");
            }
        }
       
    }
}