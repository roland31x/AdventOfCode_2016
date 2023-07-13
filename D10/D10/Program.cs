using System.Reflection.Metadata;

namespace D10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using(StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                string buffer = sr.ReadLine();
                while(buffer != null)
                {
                    if (buffer.Contains("value"))
                    {
                        Robot.FindRobot(buffer.Split(' ').Last(), isRobot: true).MicroChips.Add(int.Parse(buffer.Split(' ')[1]));
                    }
                    else
                    {
                        Robot.Calibrate(Robot.FindRobot(buffer.Split(' ')[1], true), buffer);
                    }
                    buffer = sr.ReadLine();
                }
            }
            while(Robot.Robots.Where(x => x.MicroChips.Count > 0 && x.Type == "robot").Count() > 0)
            {
                for(int i = 0; i < Robot.Robots.Count; i++)
                {
                    if (Robot.Robots[i].MicroChips.Count == 2 && Robot.Robots[i].Type == "robot")
                    {
                        Robot.Robots[i].Operate();
                        break;
                    }                       
                }
            }
            int res = 1;
            for(int i = 0; i < 3; i++)
            {
                res *= Robot.FindRobot(i.ToString(), false).MicroChips[0];
            }
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(res);
        }
    }
    public class Robot
    {
        public static List<Robot> Robots = new List<Robot>();
        public static Robot FindRobot(string name, bool isRobot) 
        {
            foreach(Robot robot in (isRobot ? Robots.Where(x => x.Type == "robot") : Robots.Where(x => x.Type == "output")))
            {
                if (robot.Number == int.Parse(name))
                    return robot;
            }
            return new Robot(name, isRobot);
        }
        public static void Calibrate(Robot r, string buffer)
        {
            string[] tokens = buffer.Split(' ');
            if (tokens[5] == "bot")
                r.LowRobot = true;               
            else
                r.LowRobot = false;
            if (tokens[10] == "bot")
                r.HighRobot = true;
            else
                r.HighRobot = false;
            r.Low = tokens[6];
            r.High = tokens[11];
        }
        public List<int> MicroChips = new List<int>();
        public string High;
        bool HighRobot;
        public string Low;
        bool LowRobot;
        public int Number;
        public string Type;
        public Robot(string name, bool isRobot)
        {
            Number = int.Parse(name);
            if (isRobot)
                Type = "robot";
            else
                Type = "output";
            Robots.Add(this);
        }
        public void Operate()
        {
            if (Type == "output" || MicroChips.Count != 2)
                return;
            if (MicroChips.Contains(61) && MicroChips.Contains(17))
            {
                Console.WriteLine("Part 1 solution:");
                Console.WriteLine(Number);
            } 
                

            Robot.FindRobot(High, HighRobot).MicroChips.Add(MicroChips.Max());
            Robot.FindRobot(Low, LowRobot).MicroChips.Add(MicroChips.Min());
            MicroChips.Clear();
        }
        public override string ToString()
        {
            return Type + " " + Number.ToString() + " " + MicroChips.Count();
        }
    }
}