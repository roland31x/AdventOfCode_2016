namespace D23
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines(@"..\..\..\input.txt");
            MyProgram mp = new MyProgram(lines);
            Console.WriteLine("Part 1 solution:");
            mp.OverrideRegister("a", 7);
            Console.WriteLine(mp.Run());

            lines = File.ReadAllLines(@"..\..\..\input.txt");
            MyProgram mp2 = new MyProgram(lines);
            Console.WriteLine("Part 2 solution:");
            mp2.OverrideRegister("a", 12);
            Console.WriteLine(mp2.Run());
        }
    }
    public class Register
    {
        public string Name { get; private set; }
        public int Value = 0;
        public Register(string name)
        {
            Name = name;
        }
    }
    public class CPU
    {
        public List<Register> Registers = new List<Register>() { new Register("a"), new Register("b"), new Register("c"), new Register("d"), };
        public Register GetRegister(string name)
        {
            foreach (Register register in Registers)
            {
                if (name == register.Name)
                    return register;
            }
            throw new InvalidDataException();
        }
    }
    public class MyProgram
    {
        string[] lines;
        int Driver = 0;
        CPU CPU = new CPU();
        public MyProgram(string[] lines)
        {
            this.lines = lines;
        }
        public void OverrideRegister(string register, int value)
        {
            CPU.GetRegister(register).Value = value;
        }
        public int Run()
        {
            while (Driver < lines.Length)
            {
                if(Driver == 16)
                {
                    int x = 0;
                }
                try
                {
                    Exec(lines[Driver]);
                }
                catch(Exception e) 
                {
                    Console.WriteLine(e.Message);
                }
                Driver++;
            }
            return CPU.GetRegister("a").Value;
        }
        void Exec(string line)
        {
            string command = line.Split(' ')[0];
            switch (command)
            {
                case "cpy":
                    ExecCopy(line);
                    break;
                case "jnz":
                    JumpNotZero(line);
                    break;
                case "inc":
                    Increment(line);
                    break;
                case "dec":
                    Decrement(line);
                    break;
                case "tgl":
                    Toggle(line);
                    break;

            }
        }
        void Toggle(string line)
        {
            int linetotoggle = CPU.GetRegister(line.Split(' ')[1]).Value;
            if (Driver + linetotoggle >= lines.Length)
                return;
            if (lines[Driver + linetotoggle].Contains("inc"))
            {
                lines[Driver + linetotoggle] = lines[Driver + linetotoggle].Replace("inc", "dec");
            }
            else if(lines[Driver + linetotoggle].Contains("jnz"))
            {
                lines[Driver + linetotoggle] = lines[Driver + linetotoggle].Replace("jnz", "cpy");
            }
            else
            {
                switch(lines[Driver + linetotoggle].Split(' ').Length)
                {
                    case 2:
                        lines[Driver + linetotoggle] = lines[Driver + linetotoggle].Replace(lines[Driver + linetotoggle].Split(' ')[0], "inc");
                        break;
                    case 3:
                        lines[Driver + linetotoggle] = lines[Driver + linetotoggle].Replace(lines[Driver + linetotoggle].Split(' ')[0], "jnz");
                        break;
                }
            }
        }
        void ExecCopy(string line)
        {
            if (int.TryParse(line.Split(' ')[1], out int valuetocopy))
            {
                CPU.GetRegister(line.Split(' ')[2]).Value = valuetocopy;
            }
            else
                CPU.GetRegister(line.Split(' ')[2]).Value = CPU.GetRegister(line.Split(' ')[1]).Value;
        }
        void JumpNotZero(string line)
        {
            if (int.TryParse(line.Split(' ')[1], out int value))
            {
                if (value != 0)
                {
                    if (int.TryParse(line.Split(' ')[2], out int toskip))
                    {
                        Driver += toskip - 1;
                    }
                    else
                        Driver += CPU.GetRegister(line.Split(' ')[2]).Value - 1;
                }
            }
            else
            {
                if (CPU.GetRegister(line.Split(' ')[1]).Value != 0)
                {
                    if (int.TryParse(line.Split(' ')[2], out int toskip))
                    {
                        Driver += toskip - 1;
                    }
                    else
                        Driver += CPU.GetRegister(line.Split(' ')[2]).Value - 1;
                }
            }
        }
        void Increment(string line)
        {
            CPU.GetRegister(line.Split(' ')[1]).Value++;
        }
        void Decrement(string line)
        {
            CPU.GetRegister(line.Split(' ')[1]).Value--;
        }
    }
}