using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bot
{
    class Word
    {
        Dictionary<string, int> words = new Dictionary<string, int>();
        string caption;
        public string _caption { get { return caption; } set { this.caption = value; } }
        public Word(string caption)
        {
            this.caption = caption;
        }
        public void AddWord(string w)
        {
            if (words.ContainsKey(w))
                words[w]++;
            else
                try
                {
                    words.Add(w, 1);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
        }
        public void AddWord(string w, int n)
        {
            words.Add(w, n);
        }
        public Word()
        {

        }
        public string GetWord()
        {
            words = words.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            return words.Keys.ToArray()[words.Keys.ToArray().Length - 1];
        }
        public string BackupWord()
        {
            string k = this.caption + " ";
            foreach (KeyValuePair<string, int> kv in this.words)
            {
                k += kv.Key + " " + kv.Value + " ";
            }
            return k;


        }
    }
}
