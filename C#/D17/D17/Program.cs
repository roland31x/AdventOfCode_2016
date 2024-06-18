using System.Security.Cryptography;
using System.Text;

namespace D17
{
    internal class Program
    {
        static List<int[]> dirs = new List<int[]>() { new int[] { -1, 0 }, new int[] { 1, 0 }, new int[] { 0, -1 }, new int[] { 0, 1 }, }; // U D L R 
        static List<string> sdirs = new List<string>() { "U", "D", "L", "R" };
        static void Main(string[] args)
        {
            string input = "dmypynyp";
            TriData start = new TriData(input, 0, 0, 0);
            Queue<TriData> queue = new Queue<TriData>();
            queue.Enqueue(start);
            bool found = false;
            int longest = 0;
            while(queue.Count > 0) 
            {
                TriData curr = queue.Dequeue();
                if (curr.I == 3 && curr.J == 3)
                {
                    int len = curr.current.Replace(input, "").Length;
                    if (!found)
                    {
                        Console.WriteLine("Part 1 solution:");
                        Console.WriteLine(curr.current.Replace(input, ""));
                        found = true;
                    }
                    if (longest < len)
                        longest = len;                 
                }
                else
                {
                    TryEnqueue(queue, curr);
                }                
            }
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(longest);
        }
        static void TryEnqueue(Queue<TriData> queue, TriData curr)
        {
            string hash = Convert.ToHexString(MD5.HashData(ASCIIEncoding.ASCII.GetBytes(curr.current))).ToLower();
            for (int i = 0; i < dirs.Count; i++) 
            {
                int newI = curr.I + dirs[i][0];
                int newJ = curr.J + dirs[i][1];
                if (newI < 0 || newI > 3 || newJ < 0 || newJ > 3)
                    continue;
                int check = hash[i];
                if (check - 'b' < 0 || check - 'b' > 'f' - 'b')
                    continue;
                queue.Enqueue(new TriData(curr.current + sdirs[i], newI, newJ, curr.Step++));
            }
        }
    }
    public class TriData 
    {
        public string current;
        public int I;
        public int J;
        public int Step;
        public TriData(string current, int I, int J,int step) 
        {
            this.current = current;
            this.I = I;
            this.J = J;
            this.Step = step;
        }
    }
}