using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Automation1
{
    public class Automate
    {
        Dictionary<string, Dictionary<string, string>> Transition = new Dictionary<string, Dictionary<string, string>>();
        public string[] State;
        public string[] Alph;
        public List<string> Stop;
        public List<string> Start;
        public string[] line = File.ReadAllLines("Automate.txt");

        public Automate()
        {
            State = line[0].Split(',');
            Alph = line[1].Split(',');
            Start = line[line.Length - 2].Split(',').ToList();
            Stop = line[line.Length - 1].Split(',').ToList();

            for (var i = 2; i < line.Length - 2; i++)
            {
                var str2 = line[i].Split(':');

                if (str2.Length == 1)
                {
                    string k = "";
                    Dictionary<string, string> temp = new Dictionary<string, string>();
                    for (var j = i + 1; j < i + 10; j++)
                    {
                        var strj = line[j].Split(':');
                        temp.Add(strj[0], strj[1]);
                    }
                    k = String.Join("", str2);
                    Transition.Add(k, temp);
                    i += 9;
                }
            }
        }

        public KeyValuePair<bool, int> MaxString(string text, int startIndex)
        {
            bool flag = false;
            int maxLength = 0;
            int idx = startIndex;
            string currentStates = String.Join(" ", Start);


            if (Stop.Contains(currentStates.ToString()))
            {
                flag = true;
            }


            for (int i = idx; i < text.Length; i++)
            {
                if (Alph.Contains(text[i].ToString()) && currentStates != " ")
                {
                    currentStates = Transition[text[i].ToString()][currentStates].ToString();
                    if (Stop.Contains(currentStates.ToString()))
                    {
                        flag = true;
                        maxLength = i + 1 - idx;
                    }

                }
                else
                {
                    return new KeyValuePair<bool, int>(flag, maxLength);
                }
            }
            return new KeyValuePair<bool, int>(flag, maxLength);
        }

        private static int CompareByLength(string a, string b)
        {
            if (a.Length > b.Length)
                return 1;
            if (a.Length == b.Length)
                return 0;
            else return -1;
        }

        public void Show(List<string> lst)
        {
            Console.WriteLine("Найдено слов: ");
            foreach (var item in lst)
            {
                Console.WriteLine(item);
            }

            lst.Sort(CompareByLength);
            Console.WriteLine("Слово максимальной длины: ");
            Console.WriteLine(lst[lst.Count - 1]);

        }
    }
}


