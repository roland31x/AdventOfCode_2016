using System.Security.Cryptography;
using System.Text;

namespace D14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string pw = "ahsbgdzn"; // input
            int idx = 0;
            MD5 md5 = MD5.Create();
            bool done = false;
            int keyidx = 0;
            int keyidx2 = 0;
            List<PWKey> doable = new List<PWKey>();
            List<PWKey> doable2 = new List<PWKey>();
            while (!done)
            {
                string res, res2;               
                res = pw + idx.ToString();
                byte[] arr = ASCIIEncoding.ASCII.GetBytes(res);
                md5.ComputeHash(arr);
                res = Convert.ToHexString(md5.Hash);
                for(int i = 0; i < 2016; i++)
                {
                    res2 = Convert.ToHexString(md5.Hash);
                    arr = ASCIIEncoding.ASCII.GetBytes(res2.ToLower());
                    md5.ComputeHash(arr);
                }
                res2 = Convert.ToHexString(md5.Hash);

                FindFive(res, doable, ref keyidx, firstPart: true);
                FindFive(res2, doable2, ref keyidx2, firstPart: false);

                if (keyidx >= 64 && keyidx2 >= 64)
                    done = true;

                doable = doable.Where(x => x.Hashesleft > 0).ToList();
                doable2 = doable2.Where(x => x.Hashesleft > 0).ToList();

                FindTriplets(res, doable, idx);
                FindTriplets(res2, doable2, idx);

                idx++;
            }                          
        }
        static void FindTriplets(string res, List<PWKey> doable, int idx)
        {
            for (int k = 0; k < res.Length - 2; k++)
            {
                if (res[k] == res[k + 1] && res[k] == res[k + 2])
                {
                    doable.Add(new PWKey(res[k], idx));
                    break;
                }
            }
        }
        static void FindFive(string res, List<PWKey> doable, ref int keyidx, bool firstPart)
        {
            foreach (PWKey pk in doable)
            {
                if (pk.Hashesleft == 0)
                {
                    continue;
                }
                for (int k = 0; k < res.Length - 4; k++)
                {

                    if (pk.Key == res[k] && res[k] == res[k + 1] && res[k] == res[k + 2] && res[k] == res[k + 3] && res[k] == res[k + 4])
                    {
                        keyidx++;
                        if (keyidx == 64)
                        {
                            if (firstPart)
                                Console.WriteLine("Part 1 solution:");
                            else
                                Console.WriteLine("Part 2 solution:");
                            Console.WriteLine(pk.Number);
                        }
                        pk.Hashesleft = 0;
                    }
                }
                pk.Hashesleft--;
            }
        }
    }
    public class PWKey
    {
        public int Hashesleft = 1000;
        public char Key;
        public int Number;
        public PWKey(char Key, int number)
        {
            this.Key = Key;
            Number = number;
        }
    }
}