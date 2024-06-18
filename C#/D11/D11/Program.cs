using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks.Dataflow;

namespace D11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            State Initial = new State();
            using(StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                int floornr = 0;
                while(!sr.EndOfStream)
                {
                    string buffer = sr.ReadLine()!;
                    Regex rtgregex = new Regex(@"[A-Za-z]+ generator");
                    MatchCollection rtgc = rtgregex.Matches(buffer);
                    Regex mcregex = new Regex(@"[A-Za-z]+-compatible microchip");
                    MatchCollection mcc = mcregex.Matches(buffer);
                    foreach(Match match in rtgc)
                    {
                        Initial.Floors[floornr].Add(new RTG(match.Value.Split(' ')[0]));
                    }
                    foreach (Match match in mcc)
                    {
                        Initial.Floors[floornr].Add(new Microchip(match.Value.Split('-')[0]));
                    }
                    floornr++;
                }
            }
            Microchip.microchips.Sort((x1,x2) => string.Compare(x1.Name,x2.Name));
            RTG.rtgs.Sort((x1, x2) => string.Compare(x1.Name, x2.Name));
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(State.MoveAllToTopFloor(Initial));
            Initial.Floors[0].Add(new RTG("elerium"));
            Initial.Floors[0].Add(new RTG("dilithium"));
            Initial.Floors[0].Add(new Microchip("elerium"));
            Initial.Floors[0].Add(new Microchip("dilithium"));
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(State.MoveAllToTopFloor(Initial));
        }
    }
    public class ShortState
    {
        public List<int[]> Floors = new List<int[]> { new int[2], new int[2], new int[2], new int[2] };
        public int ElevatorLevel = 0;
    }
    public class State
    {
        public static int MoveAllToTopFloor(State initial)
        {
            Queue<State> states = new Queue<State>();
            List<ShortState> check = new List<ShortState>();
            states.Enqueue(initial);
            while (states.Count > 0)
            {
                State curr = states.Dequeue();
                if (curr.Solved())
                {
                    return curr.Step;
                }
                if (curr.Dupe(check))
                    continue;
                check.Add(curr.ToShortState());
                bool canmovetwoup = false;
                bool canmoveonedown = false;
                bool shouldgodown = true;
                for (int i = 0; i < curr.Floors[curr.ElevatorLevel].Count; i++)
                {                  
                    for(int j = i + 1; j < curr.Floors[curr.ElevatorLevel].Count; j++)
                    {
                        if(NextState(new IChemical[] { curr.Floors[curr.ElevatorLevel][i], curr.Floors[curr.ElevatorLevel][j] }, curr, states, 1))
                            canmovetwoup = true;
                    }
                    
                    for(int k = 0; k < curr.ElevatorLevel; k++)
                    {
                        if (curr.Floors[k].Any())
                            break;
                        if (k == curr.ElevatorLevel - 1)
                            shouldgodown = false;
                    }
                    if (shouldgodown && NextState(new IChemical[] { curr.Floors[curr.ElevatorLevel][i] }, curr, states, -1))
                        canmoveonedown = true;                    
                }
                if (!canmovetwoup) 
                {
                    for (int i = 0; i < curr.Floors[curr.ElevatorLevel].Count; i++)
                    {
                        NextState(new IChemical[] { curr.Floors[curr.ElevatorLevel][i] }, curr, states, 1);
                    }
                }
                if (!canmoveonedown && shouldgodown)
                {
                    for (int i = 0; i < curr.Floors[curr.ElevatorLevel].Count; i++)
                    {
                        for (int j = i + 1; j < curr.Floors[curr.ElevatorLevel].Count; j++)
                        {
                            NextState(new IChemical[] { curr.Floors[curr.ElevatorLevel][i], curr.Floors[curr.ElevatorLevel][j] }, curr, states, -1);
                        }
                    }
                }
            }
            return -1;
        }
        public static bool NextState(IChemical[] list, State current, Queue<State> states, int elevatoroffset)
        {
            State Next = current.Clone();
            Next.ElevatorLevel += elevatoroffset;
            if (Next.ElevatorLevel < 0 || Next.ElevatorLevel > 3)
                return false;
            Next.Step++;
            for (int i = 0; i < list.Length; i++)
            {
                Next.Floors[Next.ElevatorLevel].Add(list[i]);
                Next.Floors[current.ElevatorLevel].Remove(list[i]);
            }
            if (list.Length == 2 && list[0].Name == list[1].Name && elevatoroffset < 0)
                return false;
            if (Next.OK())
            {
                states.Enqueue(Next);
                return true;
            }
            return false;
        }

        public List<List<IChemical>> Floors = new List<List<IChemical>>() { new List<IChemical>(), new List<IChemical>(), new List<IChemical>(), new List<IChemical>() }; // 4 floors
        public int ElevatorLevel { get; private set; }
        int Step;
        public State()
        {
            ElevatorLevel = 0;
            Step = 0;
        }
        public State Clone()
        {
            State tor = new State();
            tor.ElevatorLevel = ElevatorLevel;
            tor.Step = Step;
            for(int i = 0; i < Floors.Count; i++)
            {
                for(int j = 0; j < Floors[i].Count; j++)
                {
                    tor.Floors[i].Add(Floors[i][j]);
                }

            }
            return tor;
        }
        public ShortState ToShortState()
        {
            ShortState tor = new ShortState();
            for(int i = 0; i < Floors.Count; i++)
            {
                for (int j = 0; j < Floors[i].Count; j++)
                {
                    if (Floors[i][j].GetType() == typeof(RTG))
                        tor.Floors[i][0]++;
                    else
                        tor.Floors[i][1]++;
                }
            }
            tor.ElevatorLevel = ElevatorLevel;
            return tor;
        }
        public bool Solved()
        {
            if (Floors[0].Count == 0 && Floors[1].Count == 0 && Floors[2].Count == 0 && Floors[3].Count > 0)
                return true;
            return false;
        }
        public bool Dupe(List<ShortState> list)
        {
            ShortState curr = this.ToShortState();
            foreach(ShortState state in list)
            {
                bool diff = false;
                for(int i = 0; i < 4; i++)
                {
                    if (curr.Floors[i][0] == state.Floors[i][0] && curr.Floors[i][1] == state.Floors[i][1])
                        continue;
                    else
                        diff = true;
                }
                if (diff) 
                    continue;
                if(ElevatorLevel == state.ElevatorLevel)
                    return true;
            }
            return false;            
        }
        public bool OK()
        {
            for(int i = 0; i < Floors.Count; i++)
            {
                if (!Floors[i].Where(x => x.GetType() == typeof(RTG)).Any() || !Floors[i].Where(x => x.GetType() == typeof(Microchip)).Any())
                    continue;
                foreach(Microchip m in Floors[i].Where(x => x.GetType() == typeof(Microchip)).Cast<Microchip>())
                {
                    bool found = false;
                    foreach (RTG rtg in Floors[i].Where(x => x.GetType() == typeof(RTG)).Cast<RTG>())
                    {
                        if (m.Name == rtg.Name)
                            found = true;
                    }
                    if (!found)
                        return false;
                }
            }
            return true;
        }
    }
    public interface IChemical
    {
        public string Name { get; }
    }
    public class RTG : IChemical
    {
        public static List<RTG> rtgs = new List<RTG>();
        public string Name { get; private set; }
        public RTG(string name)
        {
            Name = name;
            rtgs.Add(this);
        }
        public override string ToString()
        {
            return Name + " RTG";
        }
    }
    public class Microchip : IChemical
    { 
        public static List<Microchip> microchips = new List<Microchip>();
        public string Name { get; private set;} 
        public Microchip(string name)
        {
            Name = name;
            microchips.Add(this);
        }
        public override string ToString()
        {
            return Name + " M";
        }
    }
}