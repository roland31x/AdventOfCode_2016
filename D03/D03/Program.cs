namespace D03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines(@"..\..\..\input.txt");
            int count1 = 0;
            int count2 = 0;
            int idx = 0;
            foreach(string line in lines)
            {
                string[] tokens = line.Split(new char[] {' '},StringSplitOptions.RemoveEmptyEntries);
                int[] lengths = new int[tokens.Length];
                for(int i = 0; i < tokens.Length; i++)
                {
                    lengths[i] = int.Parse(tokens[i]);
                }
                Array.Sort(lengths);
                if (lengths[0] + lengths[1] > lengths[2])
                    count1++;
                if(idx % 3 == 2)
                {                  
                    for(int j = 0; j < 3; j++)
                    {
                        int[] t1 = new int[3];
                        for (int i = 0; i < 3; i++)
                        {
                            t1[i] = int.Parse(lines[idx - i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[j]);
                        }
                        Array.Sort(t1);
                        if (t1[0] + t1[1] > t1[2])
                            count2++;
                    }                  
                }
                idx++;
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(count1);
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(count2);
        }
    }
}