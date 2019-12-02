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
        Dictionary<string, Dictionary<string, List<string>>> Transition = new Dictionary<string, Dictionary<string, List<string>>>();
        public string Name;
        public string[] State;
        public string [] Alph;
        public List<string>  Stop;
        public List<string> Start;
        public int Priority;

        public Automate() { }

        public Automate(string filestream)
        {
            var line = File.ReadAllLines(filestream);
            Name = line[0];
            State = line[1].Split(',');
            Alph = line[2].Split(',');
            Start = line[line.Length - 3].Split(',').ToList();
            Stop = line[line.Length - 2].Split(',').ToList();
            Priority = int.Parse(line[line.Length - 1]);

            if(Name == "WhiteSpace")
            {
                for(var i = 0; i < Alph.Length; i++)
                {
                    if (Alph[i] == "\\n")
                    {
                        Alph[i] = '\n'.ToString();
                    }
                    if (Alph[i] == "\\t")
                    {
                        Alph[i] = '\t'.ToString();
                    }
                    if (Alph[i] == "\\r")
                    {
                        Alph[i] = '\r'.ToString();
                    }
                    if (Alph[i] == " ")
                    {
                        Alph[i] = ' '.ToString();
                    }
                }
            }

            for (var i = 3; i < line.Length - 3; i++)
            {
                var str2 = line[i].Split(':');

                if (str2.Length == 1)
                {
                    string k = "";
                    Dictionary<string, List<string>> temp = new Dictionary<string, List<string>>();

                    if (Name == "WhiteSpace")
                    {
                        for (var j = i + 1; j < i + 3; j++)
                        {
                            List<string> tm = new List<string>();
                            var strj = line[j].Split(':');
                            tm.Add(strj[1]);
                            temp.Add(strj[0], tm);
                        }
                        k = String.Join("", str2);
                        if (k == "\\n")
                        {
                            k = '\n'.ToString();
                        }
                        if (k == "\\t")
                        {
                            k = '\t'.ToString();
                        }
                        if (k == "\\r")
                        {
                            k = '\r'.ToString();
                        }
                        if (k == " ")
                        {
                            k = ' '.ToString();
                        }
                        Transition.Add(k, temp);
                        i += 2;
                    }
                    else if (Name == "Operation")
                    {
                        for (var j = i + 1; j < i + 8; j++)
                        {
                            List<string> tm = new List<string>();
                            var strj = line[j].Split(':');
                            tm.Add(strj[1]);
                            temp.Add(strj[0], tm);
                        }
                        k = String.Join("", str2);
                        Transition.Add(k, temp);
                        i += 7;
                    }
                    else
                    {
                        for (var j = i + 1; j < i + 10; j++)
                        {
                            List<string> tm = new List<string>();
                            var strj = line[j].Split(':');
                            tm.Add(strj[1]);
                            temp.Add(strj[0], tm);
                        }
                        k = String.Join("", str2);
                        Transition.Add(k, temp);
                        i += 9;
                    }  
                }
            }
        }

        public KeyValuePair<bool, int> MaxString(string text, int startIndex)
        {
            bool flag = false;
            int maxLength = 0;
            int idx = startIndex;
            List<string> currentStates = new List<string>();
            currentStates.AddRange(Start);

            if (Stop.Intersect(currentStates).ToList().Count > 0)
            {
                flag = true;
            }

            
            for (int i = idx; i < text.Length; i++)
            {
                if (Alph.Contains(text[i].ToString()))
                {
                    int count = currentStates.Count;

                    for (int j = 0; j < count; j++)
                    {
                         if (Transition[text[i].ToString()].ContainsKey(currentStates[j]))
                         {
                             currentStates.AddRange(Transition[text[i].ToString()][currentStates[j]]);
                             currentStates.RemoveRange(0, count);
                         }
                    }


                    if (Stop.Intersect(currentStates).ToList().Count > 0)
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

        public string ReadStr()
        {
            StringBuilder res = new StringBuilder();
            string str;
            using (StreamReader reader = new StreamReader("input.txt", true))
            {
                res.Append(reader.ReadToEnd());
            }
            str = res.ToString();
            return str;
        }
    }
}


