using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestWork_VibeGames
{
    public class Game
    {
        #region Поля
        List<Car> cars = new List<Car>();
        List<Player> players = new List<Player>();
        AutoResetEvent waitHandler = new AutoResetEvent(true);
        double minCoord = 0.0d;
        double maxCoord = 100.0d;
        Thread myThread = null;

        public List<Car> Cars { get => cars; set => cars = value; }
        public List<Player> Players { get => players; set => players = value; }
        public Thread MyThread { get => myThread; set => myThread = value; }
        #endregion

        #region Методы
        public void Init()
        {
            Players.Clear();
            Cars.Clear();

            MyThread = new(AddPlayerInCar);
            Console.WriteLine("Запущен поток 1");
            MyThread.Start();
            
            AddRandomCar();
            AddRandomPlayer();
#if DEBUG
            while (true)
            {
                Thread.Sleep(2000);
                if (MyThread.IsAlive)
                {
                    Console.WriteLine($"поток 1 работает {MyThread.ThreadState}");
                }
                else
                {
                    //Console.WriteLine($"поток 1 не работает {myThread.ThreadState}");
                    //Console.WriteLine($"{String.Join(", \n", GetRandomCar())}");
                    //Random random = new Random();
                    //Car _car = cars[random.Next(cars.Count)];
                    //Console.WriteLine("\n");
                    //Console.WriteLine($@"{GetNearbyPlayer(_car)}");
                    break;
                }
            }
#endif
        }
        private void AddRandomCar()
        {
            Console.WriteLine("Началось добавление рандомных машин");
            string[] nameCar = new string[]
            {
                "Lada",
                "Skoda",
                "Audi",
                "Oka",
                "Honda",
                "Shaha"
            };
            List<Car> _cars = new List<Car>();
            Random rnd = new Random();
            string name = "";
            Coordinate coordinate = new Coordinate();
            for (int j = 1; j < 3; j++)
            {
                for (int i = 0; i < 100; i++)
                {
                    coordinate = GerRandomCoordinate();
                    name = nameCar[rnd.Next(nameCar.Length)] + i * j;
                    Cars.Add(new Car(name, coordinate));
                }
                Console.WriteLine($"Добавлено {Cars.Count} машин");
                Cars.AddRange(_cars);
                _cars.Clear();
            }
        }

        private void AddRandomPlayer()
        {
            Console.WriteLine("Началось добавление рандомных игроков");
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
            List<Player> _players = new List<Player>();
            Random rnd = new Random();
            string nickName = "";
            Coordinate coordinate = new Coordinate();

            for (int j = 1; j < 11; j++)
            {
                for (int i = 0; i < 100; i++)
                {
                    coordinate = GerRandomCoordinate();
                    nickName = namePlayer[rnd.Next(namePlayer.Length)] + "_" + rnd.Next(65, 91);
                    _players.Add(new Player(nickName, coordinate));
                }
                Players.AddRange(_players);
                Console.WriteLine($"Добавлено {Players.Count} игроков");
                _players.Clear();
                waitHandler.Set();
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

        private void AddPlayerInCar()
        {
            List<Player> _players = Players;
            List<Player> addedPlayers = new List<Player>();
            List<Car> _cars = Cars;
            List<Car> fullCar = new List<Car>();
            while (Cars.Count == 0 || Players.Count == 0)
            {
                waitHandler.WaitOne();
                Console.WriteLine($"Поток 1 простаивает из-за отсутствия игроков и машин");
            }
            for (int i = 0; i < Cars.Count; i++)
            {
                
                Car car = Cars[i];
                Console.WriteLine($"Поток 1 начал добавление в машину {car.Name}");
                for (int j = 0; j < _players.Count; j++)
                {
                    Player player = _players[j];
                    if (!car.AddPlayer(player))
                    {
                        fullCar.Add(car);
                        Console.WriteLine($"Место в машине {car.Name} закончилось");
                        break;
                    }
                    addedPlayers.Add(player);
                    Console.WriteLine($"Поток доавил игрока {player.Nickname} в машину {car.Name}");
                }
                //waitHandler.Set();

                _players = new List<Player>(Players);
                _players.RemoveAll((x) => addedPlayers.Contains(x));
                _cars = new List<Car>(Cars);
                _cars.RemoveAll((x) =>  fullCar.Contains(x));
            }
            Console.WriteLine("Остановка потока 1");
            waitHandler.Close();

        }

        public List<Car> GetRandomCar()
        {
            List<Car> _cars = new List<Car>();
            Random random = new Random();
            for (int i = 0; i < 5; i++)
            {
                _cars.Add(Cars[random.Next(Cars.Count)]);
            }
            return _cars;
        }

        public Dictionary<Player, Double> GetNearbyPlayer(Car car)
        {
            //Random random = new Random();
            //List<Player> _players = new List<Player>();
            Dictionary<Player, Double> _players = new Dictionary<Player, double>();
            //Car _car = cars[random.Next(cars.Count)];
            foreach (Player player in Players)
            {
                double length = Coordinate.Length(player.Coordinate, car.Coordinate);
                if (length <= 15.0d)
                _players.Add(player, length);
            }
            return _players;
        }
        #endregion
    }
}
