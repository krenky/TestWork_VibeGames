using System.Threading;
using System.Xml.Linq;

namespace TestWork_VibeGames
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Init();

            Random random = new Random();
            Car _car = game.Cars[random.Next(game.Cars.Count)];

            Console.WriteLine($"поток 1 не работает {game.MyThread.ThreadState}");
            Console.WriteLine("===================================================================");
            Console.WriteLine($"5 случайных авто \n\n {String.Join(", \n", game.GetRandomCar(5))}");
            Console.WriteLine("===================================================================");
            Console.WriteLine("\n Случайная машина \n");
            Console.WriteLine(_car);
            Console.WriteLine("\nИгроки находящийеся ближе 15 единиц:\n");
            foreach(var dict in game.GetNearbyPlayer(_car, 15.0d))
            {
                Console.WriteLine(@$"{dict.Key.Nickname}: {dict.Value}");
            }
            Console.WriteLine("===================================================================");
        }
       
    }
}