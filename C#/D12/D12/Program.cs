using System.Numerics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace D12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] lines = File.ReadAllLines(@"..\..\..\input.txt");
            MyProgram mp = new MyProgram(lines);
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(mp.Run());
            MyProgram mp2 = new MyProgram(lines);
            Console.WriteLine("Part 2 solution:");
            mp2.OverrideRegister("c", 1);
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
                Exec(lines[Driver]);
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
                    Driver += int.Parse(line.Split(' ')[2]) - 1;
            }
            else
            {
                if (CPU.GetRegister(line.Split(' ')[1]).Value != 0)
                    Driver += int.Parse(line.Split(' ')[2]) - 1;
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