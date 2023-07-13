namespace D01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string line = File.ReadAllText(@"..\..\..\input.txt");
            string[] tokens = line.Split(", ");
            Player bunny = new Player();
            foreach(string cmd in tokens)
            {
                if (cmd[0] == 'R')
                {
                    bunny.TurnRight();
                }
                else
                {
                    bunny.TurnLeft();
                }
                int nr = 0;
                for(int i = 1; i < cmd.Length; i++)
                {
                    nr *= 10;
                    nr += cmd[i] - '0';
                }
                for (int i = 0; i < nr; i++)
                    bunny.Move();
            }
            Console.WriteLine("Part 1 solution");
            Console.WriteLine(Math.Abs(bunny.X) + Math.Abs(bunny.Y));
            Console.WriteLine("Part 2 solution");
            Console.WriteLine(Math.Abs(bunny.firstdupe[0]) + Math.Abs(bunny.firstdupe[1]));
        }
    }
    public class Player
    {
        public int X = 0;
        public int Y = 0;
        public int dir = 0; // 0 - up, 1 - right, 2 - down, 3 - left
        public int[] firstdupe;
        public List<int[]> pos = new List<int[]>();

        public void TurnLeft()
        {
            dir--;
            if (dir < 0)
                dir = 3;
        }
        public void TurnRight()
        {
            dir++;
            if (dir > 3)
                dir = 0;
        }
        public void Move()
        {
            switch (dir)
            {
                case 0: Y++; break;
                case 1: X++; break;
                case 2: Y--; break;
                case 3: X--; break;
            }
            int[] newpos = new int[] { Y, X };
            if (firstdupe == null)
            {
                bool ok = true;
                foreach (int[] c in pos)
                {
                    if (c[0] == Y && c[1] == X)
                        ok = false;
                }
                if (!ok)
                {
                    firstdupe = newpos;
                }
                else
                    pos.Add(newpos);
            }
                
        }
    }
}