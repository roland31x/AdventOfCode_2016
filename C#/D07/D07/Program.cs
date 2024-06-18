using System.Text;

namespace D07
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int count1 = 0;
            int count2 = 0;
            using (StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    IpV7 tocheck = new IpV7(sr.ReadLine());
                    if(tocheck.OkTLS())
                        count1++;
                    if (tocheck.OkSSL())
                        count2++;
                }
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(count1);
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(count2);
        }
    }
    public class IpV7
    {
        public List<string> supernet = new List<string>();
        public List<string> hypernet = new List<string>();

        public IpV7(string toparse) 
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < toparse.Length; i++)
            {              
                if (toparse[i] != '[')
                    sb.Append(toparse[i]);
                else
                {
                    i++;
                    supernet.Add(sb.ToString());
                    sb.Clear();
                    while (toparse[i] != ']')
                    {
                        sb.Append(toparse[i]);
                        i++;
                    }
                    hypernet.Add(sb.ToString());
                    sb.Clear();
                }                                
            }
            if(sb.Length > 0)
                supernet.Add(sb.ToString());
        }
        public bool OkTLS()
        {
            foreach(string s in hypernet)
            {
                for(int i = 0; i < s.Length - 3; i++)
                {
                    if (s[i] == s[i + 3] && s[i + 1] == s[i+2] && s[i] != s[i + 1])
                    {
                        return false;
                    }
                }
            }
            foreach(string s in supernet)
            {
                for (int i = 0; i < s.Length - 3; i++)
                {
                    if (s[i] == s[i + 3] && s[i + 1] == s[i + 2] && s[i] != s[i + 1])
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public bool OkSSL()
        {
            foreach (string s in hypernet)
            {
                for (int i = 0; i < s.Length - 2; i++)
                {
                    if (s[i] == s[i + 2] && s[i + 1] != s[i])
                    {
                        foreach(string super in supernet)
                        {
                            for(int j = 0; j < super.Length - 2; j++)
                            {
                                if (super[j] == s[i + 1] && super[j + 1] == s[i] && super[j + 2] == s[i + 1])
                                    return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}