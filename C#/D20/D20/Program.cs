namespace D20
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Range> ranges = new List<Range>();
            using(StreamReader sr = new StreamReader(@"..\..\..\input.txt"))
            {
                while(!sr.EndOfStream) 
                {
                    string buffer = sr.ReadLine()!;
                    ranges.Add(new Range(uint.Parse(buffer.Split('-')[0]), uint.Parse(buffer.Split("-")[1])));
                }
            }
            
            ranges.Sort((x1,x2) => x1.start.CompareTo(x2.start));
            int idx = 0;
            int ok = 0;
            Range start = ranges[idx];
            while (idx < ranges.Count - 1)
            {
                if (ranges[idx + 1].end > start.end) 
                {
                    if (start.end + 1 >= ranges[idx + 1].start)
                        start = ranges[idx + 1];
                    else
                    {
                        ok++;                       
                        if (ok == 1)
                        {
                            Console.WriteLine("Part 1 solution:"); 
                            Console.WriteLine(start.end + 1);
                        }                            
                        uint curr = start.end + 2;
                        while (curr < ranges[idx + 1].start)
                        {
                            ok++;
                            curr++;
                        }                          
                        start = ranges[idx + 1];
                    }
                    idx++;
                }
                else
                {
                    idx++;
                }
            }
            if (start.end != uint.MaxValue)
                ok += (int)(uint.MaxValue - start.end);
            Console.WriteLine("Part 2 solution:");
            Console.WriteLine(ok);

        }
    }
    public class Range
    {
        public uint start;
        public uint end;
        public Range(uint start, uint end)
        {
            this.start = start;
            this.end = end;
        }
    }
}