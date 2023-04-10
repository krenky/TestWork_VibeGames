namespace TestWork_VibeGames
{
    public class Car
    {
        public Car() { }

        public Car(string name, Coordinate coordinate)
        {
            this.name = name;
            this.coordinate = coordinate;
        }

        private string name;
        private Coordinate coordinate;
        private Player driver;
        private List<Player> passengerList;

        public string Name { get => name; set => name = value; }
        public Coordinate Coordinate { get => coordinate; set => coordinate = value; }
        public Player Driver { get => driver; set => driver = value; }
        public List<Player> PassengerList { get => passengerList; }

        public bool AddPlayer(Player player)
        {
            player.Coordinate = Coordinate;
            if(Driver == null) 
            {
                Driver = player;
                return true;
            }

            if(PassengerList.Count < 3)
            {
                PassengerList.Add(player);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
