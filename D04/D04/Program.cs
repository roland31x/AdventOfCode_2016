using System.Globalization;
using System.Text;

namespace D04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int count1 = 0;
            using(StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                while (!sr.EndOfStream)
                {
                    Room room = new Room(sr.ReadLine());
                    if (room.Ok())
                    {
                        count1 += room.sector;
                        room.GetActualName();
                        if (room.actualname.Contains("northpole"))
                        {
                            Console.WriteLine("Part 2 solution: ");
                            Console.WriteLine(room.actualname + "   >>SECTOR ID: " + room.sector);
                        }
                    }
                        
                }            
            }
            Console.WriteLine("Part 1 solution: ");
            Console.WriteLine(count1);
        }
    }
    public class Room
    {
        static int dictionarylen = 'z' - 'a' + 1;
        string checksum;
        public int sector;
        string name = "";
        public string actualname = "";
        public Room(string details)
        {
            string[] tokens = details.Split('-');
            sector = int.Parse(tokens.Last().Split('[')[0]);
            checksum = tokens.Last().Split('[')[1].Replace("]", "");
            for(int i = 0; i < tokens.Length - 1; i++)
            {
                name += tokens[i];
            }
        }
        public void GetActualName()
        {
            char[] cypher = name.ToCharArray();
            for(int k = 0; k < sector % dictionarylen; k++)
            {
                for(int i = 0; i < name.Length; i++)
                {
                    cypher[i] = Convert.ToChar(((cypher[i] - 'a' + 1) % dictionarylen) + 'a');
                }              
            }
            StringBuilder sb = new StringBuilder();
            foreach (char c in cypher)
                sb.Append(c);
            actualname = sb.ToString();
        }
        public bool Ok()
        {
            int[] fq = new int[dictionarylen];
            for(int i = 0; i < name.Length; i++)
            {
                fq[name[i] - 'a']++;
            }

            string actual = "";
            int idx = 0;
            int max = 0;
            for(int i = 0; i < 5; i++)
            {
                for(int j = 0; j < fq.Length; j++)
                {
                    if (fq[j] > max)
                    {
                        max = fq[j];
                        idx = j;
                    }
                }
                actual += Convert.ToChar(idx + 'a').ToString();
                fq[idx] = 0;
                max = 0;
            }

            return (actual == checksum);
        }
    }
}