using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Automation1
{
    class Program
    {
        static void Main()
        {
            List<string> result = new List<string>();
            StreamReader f = new StreamReader("input.txt", Encoding.Default);
            string str = f.ReadToEnd();
            Automate auto = new Automate();
            int k = 0;
            while (k < str.Length)
            {
                KeyValuePair<bool, int> resString = auto.MaxString(str, k);
                if (resString.Key == true)
                {
                    result.Add(str.Substring(k, resString.Value));
                    k += resString.Value;
                }
                else
                {
                    k++;
                }
            }
            auto.Show(result);
        }
    }
}
