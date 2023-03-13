using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleApp15.Program;
using System.Xml.Linq;

namespace ConsoleApp16
{
    public class Words
    {
        private string type;
        public void Zapusk()
        {
            Console.WriteLine("1-Create file\t2-Open saved file");
            int vibiraem = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            if (vibiraem == 1)
            {
                Console.WriteLine("1-Russian-English\t2-English-rus");
                int vibor2 = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                if (vibor2 == 1)
                {
                    type = "../../Rus_to_english.xml";
                    FileStream stream = new FileStream(type, FileMode.Create); stream.Close();
                    Console.Clear();
                    Console.Write("Enter a word ");
                    string buffer_1 = Console.ReadLine();
                    Console.Clear();
                    Console.Write("Enter translation ");
                    string buffer_2 = Console.ReadLine();
                    Console.Clear();
                    Slovo slovo = new Slovo(buffer_1, buffer_2);
                    Console.WriteLine("1-Add another translation\t2-Skip");
                    int vibor = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    while (vibor == 1)
                    {
                        Console.Write("Enter translation ");
                        buffer_2 = Console.ReadLine();
                        slovo.AddTr(buffer_2);
                        Console.Clear();
                        Console.WriteLine("1-Add another translation\t2-Skip");
                        vibor = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();
                    }
                    XDocument doc = new XDocument(new XElement("Words",
                    new XElement("Word",
                        new XAttribute("slovo", slovo.slovo),
                        new XAttribute("perevod", slovo.perevod),
                        new XElement("perevodi", slovo.perevodi))));
                    doc.Save(type);
                    Programa();
                }
                else if (vibor2 == 2)
                {
                    type = "../../Engl_to_Russia.xml";
                    FileStream stream = new FileStream(type, FileMode.Create); stream.Close();
                    Console.Clear();
                    Console.Write("Enter Word ");
                    string buffer_1 = Console.ReadLine();
                    Console.Clear();
                    Console.Write("Enter translation ");
                    string buffer_2 = Console.ReadLine();
                    Console.Clear();
                    Slovo slovo = new Slovo(buffer_1, buffer_2);
                    Console.WriteLine("1-Add another translation\t2-Skip");
                    int vibor = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    while (vibor == 1)
                    {
                        Console.Write("Enter translation ");
                        buffer_2 = Console.ReadLine();
                        slovo.AddTr(buffer_2);
                        Console.Clear();
                        Console.WriteLine("1-Add another translation\t2-Skip");
                        vibor = Convert.ToInt32(Console.ReadLine());
                        Console.Clear();
                    }
                    XDocument doc = new XDocument(new XElement("Words",
                    new XElement("Word",
                        new XAttribute("slovo", slovo.slovo),
                        new XAttribute("perevod", slovo.perevod),
                        new XElement("perevodi", slovo.perevodi))));
                    doc.Save(type);
                    Programa();
                }
            }
            else if (vibiraem == 2)
            {
                Console.WriteLine("1-Russian-English\t2-English-rus");
                vibiraem = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                if (vibiraem == 1)
                {
                    type = "../../Rus_to_english.xml"; Programa();
                }
                else if (vibiraem == 2)
                {
                    type = "../../Engl_to_Russia.xml"; Programa();
                }
            }
        }
        #region Add word
        public void Dobavit_Slovo()
        {
            Console.Clear();
            Console.Write("Enter a word ");
            string buffer_1 = Console.ReadLine();
            Console.Clear();
            Console.Write("Enter translation ");
            string buffer_2 = Console.ReadLine();
            Console.Clear();
            Slovo slovo = new Slovo(buffer_1, buffer_2);
            Console.WriteLine("1-Add another translation\t2-Skip");
            int vibiraem = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            while (vibiraem == 1)
            {
                Console.Write("Enter translation ");
                buffer_2 = Console.ReadLine();
                slovo.AddTr(buffer_2);
                Console.Clear();
                Console.WriteLine("1-Add another translation\t2-Skip");
                vibiraem = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
            }
            XDocument x = XDocument.Load(type);
            x.Root.Add(new XElement("Word",
                new XAttribute("slovo", slovo.slovo),
                new XAttribute("perevod", slovo.perevod),
                new XElement("perevodi", slovo.perevodi)));
            x.Save(type);
            Programa();
        }
        #endregion

        public void Naiti_Slovo()
        {
            Console.Clear();
            Console.Write("Enter a word ");
            string poisk = Console.ReadLine();
            Console.Clear();
            XDocument x = XDocument.Load(type);
            XElement e = x.Root;
            var temp = x.Element("Words")?
                .Elements("Word")
                .FirstOrDefault(p => p.Attribute("slovo")?.Value == poisk);
            if (temp != null)
            {
                var perevod = temp.Attribute("perevod");
                var perevodi = temp.Element("perevodi");
                Console.WriteLine("Translate " + perevod.Value + " " + perevodi.Value);
                Console.ReadLine();
            }
            Programa();
        }
        #region Delete word
        public void Ydalit_slovo()
        {
            Console.Clear();
            Console.WriteLine("1-Delete word\t2-Delete translation");
            int vibiraem = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            Console.Write("Enter a word ");
            string poisk = Console.ReadLine();
            Console.Clear();
            if (vibiraem == 1)
            {
                XDocument x = XDocument.Load(type);
                XElement e = x.Root;
                if (e != null)
                {
                    var temp = x.Element("Words")?
                    .Elements("Word")
                    .FirstOrDefault(p => p.Attribute("slovo")?.Value == poisk);
                    if (temp != null)
                    {
                        temp.Remove();
                        x.Save(type);
                        Console.WriteLine("The word was successfully deleted");
                        Console.ReadLine();
                    }
                }
            }
            else
            {
                XDocument x = XDocument.Load(type);
                XElement e = x.Root;
                if (e != null)
                {
                    var temp = x.Element("Words")?
                    .Elements("Word")
                    .FirstOrDefault(p => p.Attribute("slovo")?.Value == poisk);
                    if (temp != null)
                    {
                        var perevod = temp.Element("perevodi").Value;
                        if (perevod == "" || perevod == null)
                        {
                            Console.Write("Cannot be deleted because there are no other translations for the word ");
                        }
                        else
                        {
                            string[] buff = temp.Element("perevodi").Value.Split(' ');
                            List<string> list = new List<string>(buff);
                            var perevode = temp.Attribute("perevod");
                            if (perevode != null) perevode.Value = list[0];
                            list.RemoveAt(0);
                            string buff_2 = "";
                            if (list.Count != 0)
                            {
                                for (int i = 0; i < list.Count; i++)
                                    buff_2 += list[i] + " ";
                                buff_2 = buff_2.Remove(buff_2.Length - 1);
                                temp.Element("perevodi").Value = buff_2;
                            }
                            Console.WriteLine("Translation has been successfully replaced");
                            Console.ReadLine();
                        }

                    }
                }
                x.Save(type);
            }
            Programa();
            #endregion
        }
        #region 4 - Change words
        public void Izmenit_perevod()
        {
            Console.Clear();
            Console.WriteLine("1-Replace word\t2-Replace translation");
            int vibiraem = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            Console.Write("Enter a word ");
            string poisk = Console.ReadLine();
            Console.Clear();
            Console.Write("Enter a new word ");
            string izmenit = Console.ReadLine();
            Console.Clear();
            if (vibiraem == 1)
            {
                XDocument x = XDocument.Load(type);
                XElement e = x.Root;
                var temp = x.Element("Words")?
                    .Elements("Word")
                    .FirstOrDefault(p => p.Attribute("slovo")?.Value == poisk);
                if (temp != null)
                {
                    var word = temp.Attribute("slovo");
                    if (word != null) word.Value = izmenit;
                    x.Save(type);
                    Console.WriteLine("The word was successfully replaced");
                    Console.ReadLine();
                }
            }
            else
            {
                XDocument x = XDocument.Load(type);
                XElement e = x.Root;
                var temp = x.Element("Words")?
                    .Elements("Word")
                    .FirstOrDefault(p => p.Attribute("perevod")?.Value == poisk);
                if (temp != null)
                {
                    var perevod = temp.Attribute("perevod");
                    if (perevod != null) perevod.Value = izmenit;
                    x.Save(type);
                    Console.WriteLine("The word was successfully replaced");
                    Console.ReadLine();
                }
            }
            Programa();
        }
        #endregion 

        #region 5 - Save word
        public void Soxranit_Slovo()
        {
            Console.Clear();
            Console.Write("Enter a word ");
            string temp = Console.ReadLine();
            Console.Clear();
            XDocument x = XDocument.Load(type);
            XElement e = x.Root;
            var poisk = x.Element("Words")?
                .Elements("Word")?
                .FirstOrDefault(p => p.Attribute("slovo")?.Value == temp);
            string slovo = poisk?.Attribute("slovo")?.Value;
            string perevod = poisk?.Attribute("perevod")?.Value;
            string perevodi = poisk?.Element("perevodi")?.Value;
            FileStream stream = new FileStream("../../word.xml", FileMode.Create); stream.Close();
            XDocument doc = new XDocument(new XElement("Words",
           new XElement("Word",
               new XAttribute("slovo", slovo),
               new XAttribute("perevod", perevod),
               new XElement("perevodi", perevodi))));
            doc.Save("../../word.xml");
            Console.WriteLine("The word was successfully exported");
            Console.ReadLine();
            Programa();
        }
        #endregion
        public void Programa()
        {
            Console.Clear();
            Console.WriteLine("1-Add word\t2-Search\t3-Delete word\t4-Change words\t5-Save word\t6-Exit");
            string menu = Console.ReadLine();
            switch (menu)
            {
                case "1":
                    Dobavit_Slovo();
                    break;
                case "2":
                    Naiti_Slovo();
                    break;
                case "3":
                    Ydalit_slovo();
                    break;
                case "4":
                    Izmenit_perevod();
                    break;
                case "5":
                    Soxranit_Slovo();
                    break;
                case "6":
                    Environment.Exit(0);
                    break;
            }
        }
    }
}