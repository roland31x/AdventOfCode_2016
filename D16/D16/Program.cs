namespace D16
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = "10011111011011001";
            List<int> initial = new List<int>();
            for (int i = 0; i < input.Length; i++)
            {
                initial.Add(input[i] - '0');
            }
            List<int> Filled = FillDisk(272, initial);
            List<int> checksum = GetChecksum(Filled);
            Console.WriteLine("Part 1 solution: ");
            for (int i = 0; i < checksum.Count; i++)
            {
                Console.Write(checksum[i]);
            }
            Console.WriteLine();

            initial = new List<int>();
            for (int i = 0; i < input.Length; i++)
            {
                initial.Add(input[i] - '0');
            }
            Filled = FillDisk(35651584, initial);
            checksum = GetChecksum(Filled);
            Console.WriteLine("Part 2 solution: ");
            for (int i = 0; i < checksum.Count; i++)
            {
                Console.Write(checksum[i]);
            }
            Console.WriteLine();
        }
        static List<int> GetChecksum(List<int> input) 
        {
            List<int> checksum = new List<int>();
            for(int i = 0; i < input.Count; i+= 2)
            {
                checksum.Add((input[i] ^ input[i+1] + 1) % 2);
            }
            if (checksum.Count % 2 == 0)
                return GetChecksum(checksum);
            else return checksum;
        }
        static List<int> FillDisk(int size, List<int> initial)
        {
            int n = initial.Count;
            while(n < size)
            {
                List<int> nextiter = new List<int>();
                for(int i = 0; i < n; i++)
                {
                    nextiter.Add(initial[i]);
                }
                nextiter.Add(0);
                for (int i = n - 1; i >= 0; i--)
                {
                    nextiter.Add(initial[i] ^ 1);
                }
                initial = nextiter;
                n = initial.Count;
            }
            List<int> tor = new List<int>();
            for(int i = 0; i < size; i++)
            {
                tor.Add(initial[i]);
            }
            return tor;
        }
    }
}