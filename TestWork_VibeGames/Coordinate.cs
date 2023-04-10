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

        public static double Length(Coordinate firstCoord, Coordinate secondCoord)
        {
            double length = Math.Sqrt(Math.Pow(firstCoord.X - secondCoord.X, 2) + Math.Pow(firstCoord.Y -secondCoord.Y, 2));
            return length;
        }

    }
}
