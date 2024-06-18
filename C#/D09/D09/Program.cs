using System.Text;

namespace D09
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string compressed = File.ReadAllText(@"..\..\..\input.txt");
            
            long sol = Decompressed(compressed, false);
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(sol);
            Console.WriteLine("Part 2 solution:");
            sol = Decompressed(compressed, true);
            Console.WriteLine(sol);
        }
        public static long Decompressed(string compressed, bool goInDepth)
        {
            
            long count = 0;
            int driver = 0;
            while (driver < compressed.Length)
            {
                if (compressed[driver] == '(')
                {
                    int nextchars = 0;
                    driver++;
                    while (compressed[driver] != 'x')
                    {
                        nextchars *= 10;
                        nextchars += compressed[driver] - '0';
                        driver++;
                    }

                    long amountoftimes = 0;
                    driver++;
                    while (compressed[driver] != ')')
                    {
                        amountoftimes *= 10;
                        amountoftimes += compressed[driver] - '0';
                        driver++;
                    }

                    if (!goInDepth)
                        count += nextchars * amountoftimes;
                    else
                    {
                        StringBuilder sb = new StringBuilder();
                        for(int i = driver + 1; i < driver + 1 + nextchars; i++)
                        {
                            sb.Append(compressed[i]);
                        }
                        count += Decompressed(sb.ToString(), goInDepth) * amountoftimes;
                    }
                    driver += nextchars;
                }
                else
                {
                    count++;
                }
                driver++;
            }

            return count;
        }
    }
}