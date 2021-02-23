using System;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Xml;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;


namespace word_counter
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {

            //// Doing procedural solution for file that already downloaded (fb2 format)
            //Dictionary<String, Int32> wordsFrequency = new Dictionary<string, int>();

            //// Regex that match every word (I hope it does)
            //Regex regex = new Regex(@"[^ ,.\/\\><:;\-0-9\[\]\(\)=" +  "?\"!{ }]+");

            //// Set there absolute path to your fb2 file
            //XDocument doc = XDocument.Load(@"P:\Programming\digital-design-test\word_counter_C#\word_counter\word_counter\tolstoj_lew_nikolaewich-text_0040.fb2");

            //// Adding namespace for XPath selection from book
            //var namespaceManager = new XmlNamespaceManager(new NameTable());
            //namespaceManager.AddNamespace("fb", "http://www.gribuser.ru/xml/fictionbook/2.1");

            ////Selection every p tag. I know that there is may be small error at word count because of that. But idk how to handle it.
            //var body = doc.Root.XPathSelectElements("fb:body/fb:section/fb:p", namespaceManager).ToList();

            //// Getting matches and adding them to the dictionary.
            //// There is I tried different types of foreach cycles.
            //body.ForEach(delegate (XElement name)
            //{
            //    MatchCollection matches = regex.Matches(name.Value);

            //    foreach (Match match in matches)
            //    {
            //        var matchAsString = match.ToString().ToLower();

            //        if (wordsFrequency.ContainsKey(matchAsString))
            //        {
            //            wordsFrequency[matchAsString] += 1;
            //        }
            //        else
            //        {
            //            wordsFrequency[matchAsString] = 1;
            //        }
            //    }
            //});

            //// Sorting words by frequency
            //var sortedWordsFrequency = from item in wordsFrequency orderby item.Value descending select item;

            //// File.Create(@"P:\words_frequency.txt");
            //using (StreamWriter outputFile = new StreamWriter(@"P:\words_frequency.txt"))
            //{
            //    foreach (var item in sortedWordsFrequency)
            //    {
            //        outputFile.WriteLine($"{item.Key} : {item.Value}");
            //    }
            //}

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Fb2ParserForm());

        }
    }
}