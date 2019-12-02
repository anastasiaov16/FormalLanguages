using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automation1
{
    public class LexAnalyzer
    {
        public static List<KeyValuePair<string, string>> Analyzer(List<Automate> lexClas, string text)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

            int idx = 0;
            while (idx < text.Length)
            {
                string currentLexemeClass = String.Empty;
                int currentPriority = 0;
                int maxLength = 0;

                foreach (Automate lexemeClass in lexClas)
                {
                    KeyValuePair<bool, int> temp = lexemeClass.MaxString(text, idx);
                    if (temp.Key)
                    {
                        if (maxLength < temp.Value)
                        {
                            currentLexemeClass = lexemeClass.Name;
                            currentPriority = lexemeClass.Priority;
                            maxLength = temp.Value;
                        }
                        else if (maxLength == temp.Value && currentPriority < lexemeClass.Priority)
                        {
                            currentLexemeClass = lexemeClass.Name;
                            currentPriority = lexemeClass.Priority;
                            maxLength = temp.Value;
                        }
                    }
                }

                if (maxLength > 0)
                {
                    StringBuilder temp = new StringBuilder(text.Substring(idx, maxLength));
                    temp.Replace('\n'.ToString(), "\\n");
                    temp.Replace('\r'.ToString(), "\\r");
                    temp.Replace('\t'.ToString(), "\\t");
                    temp.Replace(' '.ToString(), " ");
                    result.Add(new KeyValuePair<string, string>(currentLexemeClass, temp.ToString()));
                    idx += maxLength;
                }
                else
                {
                    result.Add(new KeyValuePair<string, string>("ERROR", idx.ToString()));
                    idx++;
                }
            }

            return result;
        }
    }
}
