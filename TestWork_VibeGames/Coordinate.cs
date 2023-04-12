namespace TestWork_VibeGames
{
    public class Coordinate
    {
        public Coordinate() { }
        public Coordinate(double x, double y)
        {
            X = x; 
            Y = y;
        }
        private double x;
        private double y;

        public double X {
            get
            {
                return x;
            }
            set
            {
                if (value < 0)
                    throw new Exception("Координата X не может быть меньше 0");
                else
                    x = value;
            }
        }
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                if (value < 0)
                    throw new Exception("Координата Y не может быть меньше 0");
                else
                    y = value;
            }
        }

        /// <summary>
        /// Получение расстояния между координатами
        /// </summary>
        /// <param name="firstCoord">Координата 1</param>
        /// <param name="secondCoord">Координата 2</param>
        /// <returns></returns>
        public static double Length(Coordinate firstCoord, Coordinate secondCoord)
        {
            double length = Math.Sqrt(Math.Pow(firstCoord.X - secondCoord.X, 2) + Math.Pow(firstCoord.Y -secondCoord.Y, 2));
            return length;
        }

        public static Coordinate GerRandomCoordinate(double maxCoord)
        {
            Random rnd = new Random();
            Coordinate coordinate = new Coordinate();
            coordinate = new Coordinate(rnd.NextDouble() +
                    rnd.Next((int)maxCoord - 1),
                    rnd.NextDouble() +
                    rnd.Next((int)maxCoord - 1));
            return coordinate;
        }

    }
}
