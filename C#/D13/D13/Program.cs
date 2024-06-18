using System.Drawing;

namespace D13
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Map myMap = new Map(new Point(1,1), new Point(31, 39), architectNumber: 1362); // your input number goes last as architect number
            Tuple<int,int> result = myMap.Result();
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(result.Item1);
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(result.Item2);
        }
    }
    public class MapPoint
    {
        public Point Point { get; set; }
        public int Mark { get; set; }
        public MapPoint(Point point, int mark) 
        {
            Point = point;
            Mark = mark;
        }
    }
    public class Map
    {
        int ArchitectNumber;
        Point player;
        Point Target;
        int[,] map = new int[100, 100];
        List<int[]> dirs = new List<int[]>() { new int[] { -1, 0 }, new int[] { 1, 0 }, new int[] { 0, -1 }, new int[] { 0, 1 } };
        public Map(Point player, Point target, int architectNumber)
        {
            this.player = player;
            this.Target = target;
            ArchitectNumber = architectNumber;
        }
        public Tuple<int,int> Result()
        {
            Point current = player;
            Queue<Point> lee = new Queue<Point>();
            lee.Enqueue(current);
            map[current.X, current.Y] = 1;
            int count = 0;
            while(lee.Count > 0 && current != Target)
            {
                current = lee.Dequeue();
                if (map[current.X, current.Y] <= 51)
                    count++;
                for(int i = 0; i < 4; i++)
                {
                    if(TryMark(current.X + dirs[i][0], current.Y + dirs[i][1], map[current.X, current.Y]))
                    {
                        lee.Enqueue(new Point(current.X + dirs[i][0], current.Y + dirs[i][1]));
                    }
                }
            }
            int part1 = map[current.X, current.Y];

            while(lee.Count > 0)
            {
                current = lee.Dequeue();
                if (map[current.X, current.Y] <= 51)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (TryMark(current.X + dirs[i][0], current.Y + dirs[i][1], map[current.X, current.Y]))
                        {
                            lee.Enqueue(new Point(current.X + dirs[i][0], current.Y + dirs[i][1]));
                        }
                    }
                    count++;
                }               
            }

            return new Tuple<int,int>(part1,count);
        }
        bool TryMark(int x, int y, int mark)
        {
            if (x < 0 || x > map.GetLength(0) || y < 0 || y > map.GetLength(1))
                return false;
            if (isWall(x, y))
                return false;
            if (map[x, y] != 0)
                return false;
            map[x, y] = mark + 1;
            return true;

        }
        bool isWall(int x, int y)
        {
            int number = x * x + 3 * x + 2 * x * y + y + y * y + ArchitectNumber;
            int count = 0;
            while (number > 0)
            {
                int check = number % 2;
                if (check == 1)
                    count++;
                number /= 2;
            }
            if (count % 2 == 0)
                return false;
            else
                return true;
        }

    }
}