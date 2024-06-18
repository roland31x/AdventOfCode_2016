namespace D18
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = ".^..^....^....^^.^^.^.^^.^.....^.^..^...^^^^^^.^^^^.^.^^^^^^^.^^^^^..^.^^^.^^..^.^^.^....^.^...^^.^.";
            int safecount = 0;
            List<int> initial = new List<int>();
            for(int i = 0; i < input.Length; i++)
            {
                if (input[i] == '.')
                {
                    initial.Add(0);
                    safecount++;
                }                    
                else
                    initial.Add(1);
            }

            List<int> last = initial;
            for(int i = 1; i < 400000; i++)
            {
                List<int> newlist = new List<int>();
                for (int j = 0; j < last.Count; j++)
                {
                    if (isSafe(j, last))
                    {
                        safecount++;
                        newlist.Add(0);
                    }
                    else
                        newlist.Add(1);
                }
                last = newlist;
                if(i == 39)
                {
                    Console.WriteLine("Part 1 solution:");
                    Console.WriteLine(safecount);
                }
            }
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(safecount);
        }
        static bool isSafe(int j, List<int> row)
        {
            int left, right, center;
            if (j == 0)
                left = 0;
            else
                left = row[j - 1];
            if (j == row.Count - 1)
                right = 0;
            else
                right = row[j + 1];
            center = row[j];

            if (left == 1 && center == 1 && right == 0)
                return false;
            if (left == 0 && center == 1 && right == 1)
                return false;
            if (left == 1 && center == 0 && right == 0)
                return false;
            if (left == 0 && center == 0 && right == 1)
                return false;


            return true;
        }
    }
}