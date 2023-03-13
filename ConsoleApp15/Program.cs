using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;
using System.Security.Policy;
using System.Xml;
using System.Threading;
using ConsoleApp16;

namespace ConsoleApp15
{
    internal class Program
    {
        public class Slovo
        {
            public string slovo { get; set; }
            public string perevod { get; set; }
            public string perevodi { get; set; } = "";
            public Slovo() { }
            public Slovo(string word, string translate)
            {
                this.slovo = word;
                this.perevod = translate;
            }
            public void AddTr(string temp) { perevodi += (temp + " "); }
        }
        static void Main(string[] args)
        {
            Words words= new Words();
            words.Zapusk();
        }
    }
}
