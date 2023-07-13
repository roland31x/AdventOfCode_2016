using System.Globalization;

namespace D08
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[,] display = new int[6, 50];
            using(StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                while(!sr.EndOfStream)
                {
                    string command = sr.ReadLine()!;
                    if (command.Contains("rect"))
                    {
                        string sizes = command.Split(' ')[1];
                        SpawnRect(display, int.Parse(sizes.Split('x')[0]), int.Parse(sizes.Split('x')[1]));
                    }
                    else
                    {
                        string[] tokens = command.Split(' ');
                        int what = int.Parse(tokens[2].Split('=')[1]);
                        int times = int.Parse(tokens[4]);
                        if (tokens[1] == "row")
                            Rotate(display, what, times, row: true);
                        else
                            Rotate(display, what, times, row: false);
                    }
                }               
            }
            int count = 0;
            Console.WriteLine("Part 2 solution:");
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 50; j++)
                    if (display[i, j] == 1)
                    {
                        Console.Write('#');
                        count++;
                    }
                    else
                    {
                        Console.Write('.');
                    }
                Console.WriteLine();
            }
                                       

            Console.WriteLine("Part 1 solution:");
            Console.Write(count);
        }
        static void SpawnRect(int[,] display, int jsize, int isize)
        {
            for(int i = 0; i < isize; i++)
            {
                for(int j = 0; j < jsize; j++)
                {
                    display[i, j] = 1;
                }
            }
        }
        static void Rotate(int[,] display, int what, int times, bool row)
        {
            int size = row ? display.GetLength(1) : display.GetLength(0);

            if (row)
            {
                List<int> newrow = new List<int>();
                for(int j = size - times; j < size - times + size; j++)
                {
                    newrow.Add(display[what, j % size]);
                }
                for (int j = 0; j < size; j++)
                    display[what, j] = newrow[j];
            }
            else
            {
                List<int> newrow = new List<int>();
                for (int j = size - times; j < size - times + size; j++)
                {
                    newrow.Add(display[j % size, what]);
                }
                for (int j = 0; j < size; j++)
                    display[j, what] = newrow[j];
            }         
        }
    }
}