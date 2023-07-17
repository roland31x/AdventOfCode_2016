namespace D25
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines(@"..\..\..\input.txt");
            RunProg(0, lines);
        }
        static void RunProg(int idx, string[] lines)
        {
            MyProgram mp = new MyProgram(lines);
            mp.OverrideRegister("a", idx);
            mp.Run();
            string res = mp.Result;
            string pattern = Analyze(res);

            long offset = 0;
            long pwr = 0;
            for(int i = 0; i < pattern.Length; i++)
            {
                offset += (long)(pattern[i] - '0') * PWR(2, pwr);
                pwr++;
            }

            string needed = "";
            for(int i = 0; i < pattern.Length; i++)
            {
                needed += (i % 2).ToString();
            }

            long needednr = 0;
            pwr = 0;
            for (int i = 0; i < needed.Length; i++)
            {
                needednr += (long)(needed[i] - '0') * PWR(2, pwr);
                pwr++;
            }

            Console.WriteLine("Solution:");
            Console.WriteLine(needednr - offset);
        }
        static long PWR(long nr, long pwr)
        {
            if (pwr == 0)
                return 1;
            return nr * PWR(nr, pwr - 1);
        }
        static string Analyze(string res) 
        {

            for (int k = 1; k < res.Length / 2; k++)
            {
                int ok = 0;
                int j = k;
                string need = "";
                for(int l = 0; l < k; l++)
                {
                    need += res[l];
                }
                while (j + k < res.Length)
                {                       
                    string totest = "";
                    for(int l = j; l < j + k; l++)
                    {
                        totest += res[l];
                    }
                    if (totest == need)
                    {
                        ok++;
                        j = j + k;
                    }
                    else
                        break;
                }
                if (ok >= 10)
                {
                    return need;                      
                }
            }

            throw new Exception("no pattenrs found");
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
        public string Result = "";
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
                if (Result.Length > 299)
                    break;
                try
                {
                    Exec(lines[Driver]);
                }
                catch (Exception e)
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
                case "out":
                    Out(line);
                    break;
            }
        }
        void Out(string line)
        {
            Result += CPU.GetRegister(line.Split(' ')[1]).Value.ToString();
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