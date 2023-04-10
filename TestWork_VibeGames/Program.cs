using System.Xml.Linq;

namespace TestWork_VibeGames
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Hello, World!");
        }

        #region Поля
        double minCoord = 0.0d;
        double maxCoord = 100.0d;
        List<Player> players = new List<Player>();
        List<Car> cars = new List<Car>();
        string[] nameCar = new string[]
        {
                "Lada",
                "Skoda",
                "Audi",
                "Oka",
                "Honda",
                "Shaha"
        };
        string[] namePlayer = new string[]
        {
            "Sergio",
            "Billy",
            "Alan",
            "Baiden",
            "Tramp",
            "Inoske",
            "Narutooooo"
        };
        #endregion

        # region Методы
        private void AddRandomCar()
        {
            Random rnd = new Random();
            string name = "";
            Coordinate coordinate = new Coordinate();

            for(int i = 0; i < 201; i++) 
            {
                coordinate = GerRandomCoordinate();
                name = nameCar[rnd.Next(nameCar.Length)] + i;
                cars.Add(new Car(name, coordinate));
            }
        }
        private void AddRandomPlayer()
        {
            Random rnd = new Random();
            string nickName = "";
            Coordinate coordinate = new Coordinate();

            for (int i = 0; i <  1001; i++)
            {
                coordinate = GerRandomCoordinate();
                nickName = namePlayer[rnd.Next(namePlayer.Length)] + "_" + rnd.Next(65, 91);
                players.Add(new Player(nickName, coordinate));
            }
        }
        private Coordinate GerRandomCoordinate()
        {
            Random rnd = new Random();
            Coordinate coordinate = new Coordinate();
            coordinate = new Coordinate(rnd.NextDouble() +
                    rnd.Next((int)maxCoord - 1),
                    rnd.NextDouble() +
                    rnd.Next((int)maxCoord - 1));
            return coordinate;
        }
#endregion
    }
}