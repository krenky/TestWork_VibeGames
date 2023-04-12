namespace TestWork_VibeGames
{
    public class Car
    {

        public Car(string name, Coordinate coordinate)
        {
            this.name = name;
            this.coordinate = coordinate;
            passengerList = new List<Player>();
        }

        private string name;
        private Coordinate coordinate;
        private Player driver;
        private List<Player> passengerList;

        public string Name { get => name; set => name = value; }
        public Coordinate Coordinate { get => coordinate; set => coordinate = value; }
        public Player Driver { get => driver; set => driver = value; }
        public List<Player> PassengerList { get => passengerList; }

        /// <summary>
        /// Добавления Игрока в машину.
        /// В первую очередь добавляется водитель,
        /// потом занимаются поссажирские места(макс 3.)
        /// </summary>
        /// <param name="player">Добавляемый игрок</param>
        /// <returns></returns>
        public bool AddPlayer(Player player)
        {
            player.Coordinate = Coordinate;
            if(Driver == null) 
            {
                Driver = player;
                return true;
            }

            if(PassengerList.Count < 3 && !PassengerList.Contains(player) && Driver != player)
            {
                PassengerList.Add(player);
                return true;
            }
            else
            {
                return false;
            }
        }
        public override string ToString()
        {
            return $@"
                    Машина: {name}
                    Координаты: X = {Coordinate.X}; Y = {Coordinate.Y}
                    Пассажиры: {String.Join(", ", passengerList.Select(x => x.Nickname))}
                    Водитель: {(Driver != null ? Driver.Nickname:"Водитель отсутствует")}";
        }
    }
}
