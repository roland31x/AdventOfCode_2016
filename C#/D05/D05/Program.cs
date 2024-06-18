using System.Security.Cryptography;
using System.Text;

namespace D05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string pw = "ugkcyxxp"; // <-- your input
            string finalpwpart1 = "";
            string[] finalpwpart2 = new string[8];
            int part2count = 0;
            int iP1 = 0;
            MD5 md5 = MD5.Create();
            for(int i = 0; i < 8; i++)
            {
                bool found = false;
                string res = "";
                while (!found)
                {
                    iP1++;
                    res = pw + iP1.ToString();
                    byte[] arr = ASCIIEncoding.ASCII.GetBytes(res);
                    md5.ComputeHash(arr);
                    res = Convert.ToHexString(md5.Hash);
                    for (int k = 0; k < 5; k++)
                    {
                        if (res[k] - '0' != 0)
                        {
                            break;
                        }
                        if (k == 4)
                        {
                            found = true;
                        }
                    }
                    if (found)
                    {
                        finalpwpart1 += res[5];
                        if (res[5] - '0' < 8 && finalpwpart2[res[5] - '0'] == null)
                        {
                            finalpwpart2[res[5] - '0'] = res[6].ToString();
                            part2count++;
                        }                           
                    }
                }
                while(part2count < 8)
                {
                    found = false;
                    while (!found)
                    {
                        iP1++;
                        res = pw + iP1.ToString();
                        byte[] arr = ASCIIEncoding.ASCII.GetBytes(res);
                        md5.ComputeHash(arr);
                        res = Convert.ToHexString(md5.Hash);
                        for (int k = 0; k < 5; k++)
                        {
                            if (res[k] - '0' != 0)
                            {
                                break;
                            }
                            if (k == 4)
                            {
                                found = true;
                            }
                        }
                        if (found)
                        {
                            if (res[5] - '0' < 8 && finalpwpart2[res[5] - '0'] == null)
                            {
                                finalpwpart2[res[5] - '0'] = res[6].ToString();
                                part2count++;
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Part 1 solution:");
            Console.WriteLine(finalpwpart1);
            Console.WriteLine("Part 2 solution:");
            for(int i = 0; i < finalpwpart2.Length; i++)
            {
                Console.Write(finalpwpart2[i]);
            }          
        }
    }
}