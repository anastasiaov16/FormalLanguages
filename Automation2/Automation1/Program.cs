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
            Automate auto = new Automate();
            string str = auto.ReadStr();
            List<Automate> lexClasses = new List<Automate>
            {
                new Automate("KeyW.txt"),
                new Automate("Automate.txt"),
                new Automate("WhiteSpace.txt"),
                new Automate("Operation.txt")
            };

            List<KeyValuePair<string, string>> result = LexAnalyzer.Analyzer(lexClasses, str);

            foreach (KeyValuePair<string, string> res in result)
            {
                Console.WriteLine(res);
            }        
        }
    }
}
