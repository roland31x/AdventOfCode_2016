namespace D06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int[]> list = new List<int[]>();
            for(int i = 0; i < 16; i++)
            {
                list.Add(new int['z' - 'a' + 1]);
            }
            using(StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                while(!sr.EndOfStream)
                {
                    string newword = sr.ReadLine()!;
                    for(int i = 0; i < newword.Length; i++)
                    {
                        list[i][newword[i] - 'a']++;
                    }
                }
            }
            Console.WriteLine("Part 1 solution:");
            for(int i = 0; i < list.Count; i++)
            {
                int maxidx = -1;
                int maxcount = 0;
                for(int j = 0; j < list[i].Length; j++)
                {
                    if (list[i][j] > maxcount)
                    {
                        maxidx = j;
                        maxcount = list[i][j];
                    }
                }
                if (maxidx != -1)
                    Console.Write(Convert.ToChar(maxidx + 'a'));
            }
            Console.WriteLine();
            Console.WriteLine("Part 2 solution:");
            for (int i = 0; i < list.Count; i++)
            {
                int minidx = -1;
                int mincount = int.MaxValue;
                for (int j = 0; j < list[i].Length; j++)
                {
                    if (list[i][j] < mincount && list[i][j] > 0)
                    {
                        minidx = j;
                        mincount = list[i][j];
                    }
                }
                if (minidx != -1)
                    Console.Write(Convert.ToChar(minidx + 'a'));
            }
        }
    }
}