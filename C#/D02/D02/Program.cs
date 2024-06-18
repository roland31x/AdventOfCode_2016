namespace D02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines(@"..\..\..\input.txt");
            string past = "5";
            
            string[,] keypadp1 = new string[,]
            {
                { "1","2","3"},
                { "4","5","6"},
                { "7","8","9"}
            };
            Console.WriteLine("Part 1 solution: ");
            foreach (string line in lines)
            {
                past = GetCode(line, past, keypadp1);
                Console.Write(past);
            }
            Console.WriteLine();

            string[,] keypadp2 = new string[,]
            {
                { "0","0","1","0","0"},
                { "0","2","3","4","0"},
                { "5","6","7","8","9"},
                { "0","A","B","C","0"},
                { "0","0","D","0","0"},
            };
            Console.WriteLine("Part 2 solution: ");
            past = "5";
            foreach (string line in lines)
            {
                past = GetCode(line, past, keypadp2);
                Console.Write(past);
            }
            Console.WriteLine();
        }
        static string GetCode(string line, string past, string[,] keypad)
        {
            string current = past;
            for(int i = 0; i < line.Length; i++)
            {
                string safe = current;
                int currentposi = 0;
                int currentposj = 0;
                for(int y = 0; y < keypad.GetLength(0); y++)
                {
                    for(int x = 0; x < keypad.GetLength(1); x++)
                    {
                        if (keypad[y,x] == current)
                        {
                            currentposi = y;
                            currentposj = x;
                        }
                    }
                }

                switch(line[i])
                {
                    case 'U':
                        currentposi--;
                        break;
                    case 'L':
                        currentposj--;
                        break;
                    case 'D':
                        currentposi++;
                        break;
                    case 'R':
                        currentposj++;
                        break;
                }
                try
                {
                    current = keypad[currentposi, currentposj];
                    if (current == "0")
                        current = safe;
                }
                catch(IndexOutOfRangeException)
                {
                    continue;
                }
            }
            return current;
        }
    }
}