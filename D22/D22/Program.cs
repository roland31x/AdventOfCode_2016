using System.Drawing;
using System.Text.RegularExpressions;

namespace D22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NodeGrid myNodeGrid = new NodeGrid(@"..\..\..\input.txt");
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(myNodeGrid.GoodPairs());
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(myNodeGrid.MoveData());
        }
    }
    public class NodeGrid
    {
        static List<int[]> dir = new List<int[]>() { new int[] { 0, -1 }, new int[] { 0, 1 }, new int[] { 1, 0 }, new int[] { -1, 0 } };
        Node[,] grid = new Node[28, 38];
        int[,] pos = new int[28, 38];
        Node special;
        public NodeGrid(string FilePath) 
        {
            using(StreamReader sr = new StreamReader(FilePath))
            {
                sr.ReadLine();
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string nodebuf = sr.ReadLine()!;
                    Regex xc = new Regex(@"x[0-9]+");
                    Regex yc = new Regex(@"y[0-9]+");
                    Regex sz = new Regex(@"[0-9]+T");
                    int xcoord = int.Parse(xc.Match(nodebuf).Value.Replace("x", ""));
                    int ycoord = int.Parse(yc.Match(nodebuf).Value.Replace("y", ""));
                    int size = int.Parse(sz.Match(nodebuf).Value.Replace("T", ""));
                    int used = int.Parse(sz.Matches(nodebuf)[1].Value.Replace("T", ""));
                    grid[ycoord, xcoord] = new Node(xcoord, ycoord, size, used);
                }
            }
        }
        public int GoodPairs()
        {
            int count = 0;
            foreach(Node A in grid) 
            {
                foreach(Node B in grid)
                {
                    if (A == B || A.Used == 0)
                        continue;
                    if (A.Used <= B.Avail)
                    {
                        //Console.WriteLine(A.X + "," + A.Y + " + " + B.X + "," + B.Y); // there is always only one that can take data into itself
                        if(special == null)
                            special = B;
                        count++;
                    }                      
                }
            }

            return count;
        }
        public int MoveData()
        {
            int steps = 0;
            Node data = grid[0, 37];
            foreach(Node n in grid)
            {
                if(n == special)
                {
                    pos[n.Y, n.X] = 2;
                    continue;
                }
                if (n == data)
                {
                    pos[n.Y, n.X] = 9;
                    continue;
                }
                if (n.Used < special.Avail)
                {
                    pos[n.Y, n.X] = 1;
                }
            }

            // we get special to the left of the data node, so we can bring it step by step to target // also the data node is always a straight line away from target (0,0) in case it wasn't we'd have to assign FirstTarget to a BFS path of the data towards (0,0)
            while(data.X != 0 || data.Y != 0)
            {
                Node firsttarget = grid[data.Y, data.X - 1];
                steps += BFS(special, firsttarget, pos);
                SwapData(firsttarget, special);
                foreach (Node n in grid)
                    n.Mark = 0;
                SwapData(special, data);
                steps++;
            }

            return steps;
        }
        int BFS(Node special, Node target, int[,] pos)
        {
            Queue<Node> lee = new Queue<Node>();
            lee.Enqueue(special);
            while(lee.Count > 0)
            {
                Node n = lee.Dequeue();
                if(n.X == target.X && n.Y == target.Y)
                {
                    break;
                }
                for(int i = 0; i < 4; i++)
                {
                    EnqueueNeighbors(n, pos, i, lee);
                }
                
            }
            return target.Mark;
        }
        void EnqueueNeighbors(Node n, int[,] pos, int d, Queue<Node> lee)
        {
            int newx = n.X + dir[d][0];
            int newy = n.Y + dir[d][1];
            if (newx < 0 || newx >= pos.GetLength(1) || newy < 0 || newy >= pos.GetLength(0))
                return;
            if (pos[newy, newx] != 1)
                return;
            if (grid[newy, newx].Mark != 0)
                return;
            grid[newy, newx].Mark = n.Mark + 1;
            lee.Enqueue(grid[newy, newx]);
        }
        public void SwapData(Node toswap, Node other)
        {
            grid[other.Y, other.X] = toswap;
            grid[toswap.Y, toswap.X] = other;
            int temppos = pos[other.Y, other.X];
            pos[other.Y, other.X] = pos[toswap.Y, toswap.X];
            pos[toswap.Y, toswap.X] = temppos;
            int hx = other.X;
            int hy = other.Y;
            other.X = toswap.X;
            other.Y = toswap.Y;
            toswap.X = hx;
            toswap.Y = hy;
        }
    }
    public class Node
    {
        public int X;
        public int Y;
        public int Size;
        public int Used;
        public int Avail { get { return Size - Used; } }
        public int Mark = 0;

        public Node(int X, int Y, int Size, int Used) 
        {
            this.X = X;
            this.Y = Y;
            this.Size = Size;
            this.Used = Used;
        }
    }
}