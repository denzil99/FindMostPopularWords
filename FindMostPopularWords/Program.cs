using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace FindMostPopularWords
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = "C:\\Users\\Denzil\\Desktop\\Text1.txt";
            Stopwatch sw = new Stopwatch();
            StreamReader sr = new StreamReader(path);
            HashSet<string> set;
            Dictionary<string, int> dict = new Dictionary<string, int>();


            var txt = sr.ReadToEnd();
            var noPunctuationText = new string(txt.Where(c => !char.IsPunctuation(c)).ToArray());
            string[] words = noPunctuationText.Split(new char[] { ' ', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);


            set = new HashSet<string>(words);


            sw.Start();


            foreach (string word in set)
            {
                dict.Add(word, 0);

                foreach (string word2 in words)
                {
                    if (word == word2)
                    {
                        dict[word]++;
                    }
                }
            }
            

            sw.Stop();


            var sortedDict = (from entry in dict orderby entry.Value descending select entry)
                .Take(10)
                .ToDictionary(pair => pair.Key, pair => pair.Value);


            foreach (var item in sortedDict)
            {
                Console.WriteLine($"{item.Key} - {item.Value}"); 
            }


            Console.WriteLine(sw.Elapsed.ToString());


            Console.ReadKey();
        }
    }
}
