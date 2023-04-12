namespace TestWork_VibeGames
{
    public class Game
    {
        #region Поля и свойтства
        List<Car> cars = new List<Car>();
        List<Player> players = new List<Player>();
        AutoResetEvent waitHandler = new AutoResetEvent(true);
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
        //double minCoord = 0.0d;
        double maxCoord = 100.0d;
        Thread myThread = null;

        public List<Car> Cars { get => cars; set => cars = value; }
        public List<Player> Players { get => players; set => players = value; }
        public Thread MyThread { get => myThread; set => myThread = value; }
        public AutoResetEvent WaitHandler { get => waitHandler; set => waitHandler = value; }
        public string[] NameCar { get => nameCar;}
        public string[] NamePlayer { get => namePlayer; }
        #endregion

        #region Методы

        /// <summary>
        /// Метод инициилизации данных
        /// </summary>
        public void Init()
        {
            Players.Clear();
            Cars.Clear();

            MyThread = new(AddPlayerInCar);
            Console.WriteLine("Запущен поток 1");
            MyThread.Start();
            
            AddRandomCar(NameCar);
            AddRandomPlayer(NamePlayer);
            WaitHandler.WaitOne();

        }

        /// <summary>
        /// Создание и добавление рандомных машин поле cars 
        /// </summary>
        private void AddRandomCar(string[] nameCar)
        {
            Console.WriteLine("Началось добавление рандомных машин");
            
            List<Car> _cars = new List<Car>();
            Random rnd = new Random();
            string name = "";
            Coordinate coordinate = new Coordinate();
            for (int j = 1; j < 3; j++)
            {
                for (int i = 0; i < 100; i++)
                {
                    coordinate = Coordinate.GerRandomCoordinate(maxCoord);
                    name = nameCar[rnd.Next(nameCar.Length)] + i * j;
                    Cars.Add(new Car(name, coordinate));
                }
                Console.WriteLine($"Добавлено {Cars.Count} машин");
                Cars.AddRange(_cars);
                _cars.Clear();
            }
        }

        /// <summary>
        /// Создание и добавление рандомных игроков в поле players
        /// </summary>
        private void AddRandomPlayer(string[] namePlayer)
        {

            Console.WriteLine("Началось добавление рандомных игроков");
            List<Player> _players = new List<Player>();
            Random rnd = new Random();
            string nickName = "";
            Coordinate coordinate = new Coordinate();

            for (int j = 1; j < 11; j++)
            {
                for (int i = 0; i < 100; i++)
                {
                    coordinate = Coordinate.GerRandomCoordinate(maxCoord);
                    nickName = namePlayer[rnd.Next(namePlayer.Length)] + "_" + rnd.Next(65, 91);
                    _players.Add(new Player(nickName, coordinate));
                }
                Players.AddRange(_players);
                Console.WriteLine($"Добавлено {Players.Count} игроков");
                _players.Clear();
                WaitHandler.Set();
            }
            WaitHandler.WaitOne();
        }
        /// <summary>
        /// Добавление игроков в автомобили
        /// </summary>
        private void AddPlayerInCar()
        {
            List<Player> _players = Players;
            List<Player> addedPlayers = new List<Player>();
            List<Car> _cars = Cars;
            List<Car> fullCar = new List<Car>();
            while (Cars.Count == 0 || Players.Count == 0)
            {
                Console.WriteLine($"Поток 1 простаивает из-за отсутствия игроков и машин");
                WaitHandler.WaitOne();
            }
            for (int i = 0; i < Cars.Count; i++)
            {
                
                Car car = Cars[i];
                Console.WriteLine($"Поток 1 начал добавление в машину {car.Name}\n");
                for (int j = 0; j < _players.Count; j++)
                {
                    Player player = _players[j];
                    if (!car.AddPlayer(player))
                    {
                        fullCar.Add(car);
                        Console.WriteLine($"Место в машине {car.Name} закончилось\n");
                        break;
                    }
                    addedPlayers.Add(player);
                    Console.WriteLine($"Поток добавил игрока {player.Nickname} в машину {car.Name}");
                }

                _players = new List<Player>(Players);
                _players.RemoveAll((x) => addedPlayers.Contains(x));
                _cars = new List<Car>(Cars);
                _cars.RemoveAll((x) =>  fullCar.Contains(x));
            }
            Console.WriteLine($"Поток 1 завершает работу");
            WaitHandler.Set();

        }
        /// <summary>
        /// Поучение N ранодомных машин из поля cars
        /// </summary>
        /// <param name="count">Кол-во машин</param>
        /// <returns></returns>
        public List<Car> GetRandomCar(int count)
        {
            List<Car> _cars = new List<Car>();
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                _cars.Add(Cars[random.Next(Cars.Count)]);
            }
            return _cars;
        }
        /// <summary>
        /// Получение игроков находящихся ближе N от Машины
        /// </summary>
        /// <param name="car">Автомобиль</param>
        /// <param name="maxLength">Максимальное расстояние от машины</param>
        /// <returns></returns>
        public Dictionary<Player, Double> GetNearbyPlayer(Car car, double maxLength, int count)
        {
            Dictionary<Player, Double> _players = new Dictionary<Player, double>();
            for (int j = 0, i = 0; j < count && i < players.Count; i++)
            {
                Player player = Players[i];
                double length = Coordinate.Length(player.Coordinate, car.Coordinate);
                if (length <= maxLength)
                {
                    _players.Add(player, length);
                    j++;
                }
            }
            return _players;
        }
        #endregion
    }
}
