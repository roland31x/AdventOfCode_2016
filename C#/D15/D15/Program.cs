using System;

namespace D15
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Disc> discs = new List<Disc>();
            using(StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                while(!sr.EndOfStream)
                {
                    string buffer = sr.ReadLine()!;
                    discs.Add(new Disc(int.Parse(buffer.Split(' ').Last().Replace(".","")), int.Parse(buffer.Split(' ')[3])));
                }
            }
            int time = 0;
            Sim(discs, ref time);
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(time - 1);

            discs.Add(new Disc(0, 11));
            Sim(discs, ref time);
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(time - 1);
        }
        static void Sim(List<Disc> discs, ref int time)
        {
            bool found = false;
            while (!found)
            {
                for (int i = 0; i < discs.Count; i++)
                {
                    if ((discs[i].Position + time + i + 1) % discs[i].Max != 0)
                        break;
                    if (i == discs.Count - 1)
                    {
                        found = true;
                        break;
                    }
                }
                time++;
            }
        }
    }
    public class Disc
    {
        public int Position;
        public int Max;
        public Disc(int position, int max)
        {
            Position = position;
            Max = max;
        }
    }
}