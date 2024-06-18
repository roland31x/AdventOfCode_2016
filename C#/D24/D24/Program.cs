using System.ComponentModel;

namespace D24
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines(@"..\..\..\input.txt");
            Map myMap = new Map(lines);
            myMap.ShortestToAllTargets();
        }
    }
    public class Map
    {
        static List<int[]> dir = new List<int[]>() { new int[] { 1, 0 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 0, -1 }, };
        TriData[,] map;
        TriData start;
        List<TriData> targets = new List<TriData>();
        public Map(string[] lines)
        {
            map = new TriData[lines.Length,lines[0].Length];
            for (int i = 0; i < lines.Length; i++)
            {
                for(int j = 0; j < lines[i].Length; j++)
                {
                    TriData current = new TriData(i, j, 0, 0, false, -1);
                    if (lines[i][j] == '#')
                        current.Value = -1;
                    else
                        current.Value = 0;
                    if (lines[i][j] - '0' >= 0 && lines[i][j] - '0' <= 9)
                    {
                        current.IsTarget = true;
                        current.TargetVal = lines[i][j] - '0';
                        if (current.TargetVal == 0)
                            start = current;
                        else
                            targets.Add(current);
                    }
                    map[i, j] = current;
                }
            }
        }
        int BFS(TriData start, TriData end)
        {
            Queue<TriData> lee = new Queue<TriData>();
            lee.Enqueue(start);
            while(lee.Count > 0)
            {
                TriData current = lee.Dequeue();
                if (current == end)
                    return current.Mark;
                for(int i = 0; i < 4; i++)
                    TryEnqueue(current.I + dir[i][0], current.J + dir[i][1], current.Mark + 1, lee);               
            }
            throw new Exception("Path not found");
        }

        void TryEnqueue(int I, int J, int mark, Queue<TriData> lee)
        {
            if (I < 0 || I >= map.GetLength(0) || J < 0 || J >= map.GetLength(1))
                return;
            if (map[I, J].Mark != 0 || map[I,J].Value == -1)
                return;
            map[I, J].Mark = mark;
            lee.Enqueue(map[I,J]);
        }

        public void ShortestToAllTargets()
        {
            int steps = int.MaxValue;
            int returnsteps = int.MaxValue;
            TriData[] output = new TriData[targets.Count];
            TriData[] data = targets.ToArray();
            int[] selected = new int[data.Length];
            Back(0, output.Length, selected, output, data, ref steps, ref returnsteps);

            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(steps);
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(returnsteps);
        }
        void Back(int k, int n, int[] selected, TriData[] output, TriData[] data, ref int steps, ref int returnsteps)
        {
            if(k >= n)
            {
                int thissteps = 0;
                TriData start = this.start;
                for(int i = 0; i < output.Length; i++)
                {
                    thissteps += BFS(start, output[i]);
                    start = output[i];
                    foreach (TriData t in map)
                        t.Mark = 0;
                }

                int thisreturn = thissteps;
                thisreturn += BFS(start, this.start);
                foreach (TriData t in map)
                    t.Mark = 0;

                if (thisreturn < returnsteps)
                    returnsteps = thisreturn;
                if (thissteps < steps)
                    steps = thissteps;
            }
            else
            {
                for(int i = 0; i < data.Length; i++)
                {
                    if (selected[i] == 0)
                    {
                        selected[i] = 1;
                        output[k] = data[i];
                        Back(k + 1, n, selected, output, data, ref steps, ref returnsteps);
                        selected[i] = 0;
                    }
                }
            }
        }
    }
    class TriData
    {
        public int I;
        public int J;
        public int Value;
        public bool IsTarget;
        public int TargetVal;
        public int Mark;
        public TriData(int i, int j, int value, int mark, bool target, int tval)
        {
            I = i;
            J = j;
            Value = value;
            Mark = mark;
            IsTarget = target;
            TargetVal = tval;
        }
        public override string ToString()
        {
            if (IsTarget)
            {
                return "Target " + TargetVal;
            }
            else
                return Value.ToString();
        }
    }
}