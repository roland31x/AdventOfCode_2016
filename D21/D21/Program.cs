using System.Runtime.InteropServices;
using System.Text;

namespace D21
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string start = "abcdefgh";
            Scrambler sc = new Scrambler(start);
            sc.Scramble(@"..\..\..\input.txt");
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(sc.ToString());

            int[] selected = new int[start.Length];
            char[] output = new char[start.Length];
            char[] data = start.ToCharArray();
            Back(0, data.Length, selected, output, data);
        }
        static void Back(int k, int n, int[] selected, char[] output, char[] data)
        {
            if(k >= n)
            {
                Scrambler sc = new Scrambler(output);
                sc.Scramble(@"..\..\..\input.txt");
                if(sc.ToString() == "fbgdceah")
                {
                    Console.WriteLine("Part 2 solution:");
                    for(int i = 0; i < output.Length; i++)
                    {
                        Console.Write(output[i]);
                    }
                    Console.WriteLine();
                }
            }
            else
            {
                for(int i = 0; i < data.Length; i++)
                {
                    if (selected[i] == 0)
                    {
                        output[k] = data[i];
                        selected[i] = 1;
                        Back(k + 1, n, selected, output, data);
                        selected[i] = 0;
                    }
                }
            }
        }
    }
    public class Scrambler
    {
        char[] arr;
        public Scrambler(char[] data)
        {
            arr = new char[data.Length];
            for(int i = 0; i < data.Length; i++)
            {
                arr[i] = data[i];
            }
        }
        public Scrambler(string password) 
        {
            arr = password.ToCharArray();
        }
        public void Scramble(string filepath)
        {
            using(StreamReader sr = new StreamReader(filepath)) 
            {
                while (!sr.EndOfStream)
                {
                    string command = sr.ReadLine()!;
                    Execute(command);
                }
            }
        }
        void Execute(string command)
        {
            if (command.Contains("swap"))
                Swap(command);
            else if (command.Contains("move"))
                Move(command);
            else if (command.Contains("rotate"))
                Rotate(command);
            else if (command.Contains("reverse"))
                Reverse(command);
        }
        void Swap(string command)
        {
            
            if (command.Contains("position"))
            {
                int firstparam = int.Parse(command.Split(' ')[2]);
                int secondparam = int.Parse(command.Split(' ')[5]);
                char safe = arr[firstparam];
                arr[firstparam] = arr[secondparam];
                arr[secondparam] = safe;
            }
            else
            {
                char firstparam = char.Parse(command.Split(' ')[2]);
                char secondparam = char.Parse(command.Split(' ')[5]);
                for(int i = 0; i < arr.Length; i++)
                {
                    if (arr[i] == firstparam)
                        arr[i] = secondparam;
                    else if (arr[i] == secondparam)
                        arr[i] = firstparam;
                }                
            }
        }
        void Move(string command)
        {
            int postomove = int.Parse(command.Split(' ')[2]);
            int where = int.Parse(command.Split(' ')[5]);
            if(postomove < where)
            {
                for (int i = postomove; i < where; i++)
                {
                    (arr[i], arr[i + 1]) = (arr[i + 1], arr[i]);
                }
            }
            else
            {
                for (int i = postomove; i > where; i--)
                {
                    (arr[i], arr[i - 1]) = (arr[i - 1], arr[i]);
                }
            }
            
        }
        void Rotate(string command)
        {
            if (command.Contains("based"))
            {
                char target = char.Parse(command.Split(' ').Last());
                int idx = 0;
                for (int i = 0; i < arr.Length; i++)
                    if (arr[i] == target)
                    {
                        idx = i; break;
                    }

                if (idx >= 4)
                    idx++;
                idx++;
                char[] newarr = new char[arr.Length];
                for (int i = 0; i < arr.Length; i++)
                {
                    newarr[(arr.Length + i + idx) % arr.Length] = arr[i];
                }
                arr = newarr;
            }
            else
            {
                int dir = 1;
                if (command.Contains("left"))
                    dir = -1;
                int times = int.Parse(command.Split(' ')[2]);
                char[] newarr = new char[arr.Length];
                for(int i = 0; i < arr.Length; i++)
                {
                    newarr[(arr.Length + i + times * dir) % arr.Length] = arr[i];
                }
                arr = newarr;
            }
        }
        void Reverse(string command)
        {
            int startpos = int.Parse(command.Split(' ')[2]);
            int endpos = int.Parse(command.Split(' ')[4]);
            char[] torep = new char[endpos - startpos + 1];
            for (int i = 0; i < torep.Length; i++)
                torep[i] = arr[endpos - i];
            for (int i = 0; i < torep.Length; i++)
                arr[startpos + i] = torep[i];

        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in arr)
                sb.Append(c);
            return sb.ToString();
        }
    }
}