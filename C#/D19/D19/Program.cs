namespace D19
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int input = 3017957; // input
            List<int> list = new List<int>();
            for (int i = 0; i < input; i++)
            {
                list.Add(i + 1);
            }
            while (list.Count > 1)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] != 0)
                        list[(i + 1) % list.Count] = 0;
                }
                list = list.Where(x => x != 0).ToList();
            }
            Console.WriteLine("Part 1 solution");
            Console.WriteLine(list[0]);

            list.Clear();
            for (int i = 0; i < input; i++)
            {
                list.Add(i + 1);
            }
            while (list.Count > 1)
            {
                int n = list.Count;
                int count = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i] != 0)
                    {
                        int target = (i + n / 2 + count) % list.Count; // i have no idea how this works i just guessed it would work counting the already skipped seats as we go around the numbers with a counter, and we can determine the next index without actually removing them
                        list[target] = 0;
                        n--;
                        count++;
                    }
                    else
                    {
                        count--;
                    }
                }
                list = list.Where(x => x != 0).ToList();
            }
            Console.WriteLine("Part 2 solution");
            Console.WriteLine(list[0]);
        }
    }
}