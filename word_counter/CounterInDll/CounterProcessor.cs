using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Diagnostics;
using System;
using System.Threading;

namespace CounterInDll
{
    public class CounterProcessor
    {
        private XmlNamespaceManager namespaceManager;
        private Regex regex = new Regex(@"[^ ,.\/\\><:;\-0-9\[\]\(\)=" + "?\"!{ }]+");
        private Dictionary<string, int> wordsFrequency = new Dictionary<string, int>();

        public CounterProcessor()
        {
            namespaceManager = new XmlNamespaceManager(new NameTable());
            namespaceManager.AddNamespace("fb", "http://www.gribuser.ru/xml/fictionbook/2.1");
        }

        private Dictionary<string, int> computeWords(XDocument document)
        {

            Stopwatch oneThreadStopwatch = new Stopwatch();
            Console.WriteLine("Method with one thread started");
            oneThreadStopwatch.Start();

            var body = document.Root.XPathSelectElements("fb:body/fb:section/fb:p", namespaceManager).ToList();

            body.ForEach(delegate (XElement name)
            {
                MatchCollection matches = regex.Matches(name.Value);

                foreach (Match match in matches)
                {
                    var matchAsString = match.ToString().ToLower();

                    if (wordsFrequency.ContainsKey(matchAsString))
                    {
                        wordsFrequency[matchAsString] += 1;
                    }
                    else
                    {
                        wordsFrequency[matchAsString] = 1;
                    }
                }
            });

            var sortedWordsFrequency = wordsFrequency.OrderBy(x => x.Value).Reverse().ToDictionary(x => x.Key, x => x.Value);

            oneThreadStopwatch.Stop();
            Console.WriteLine($"Method with one thread ended with time: {oneThreadStopwatch.Elapsed}");

            return sortedWordsFrequency;
        } 
        
        public Dictionary<string, int> MultithrededComputeWords(XDocument document)
        {

            Stopwatch multiThreadStopwatch = new Stopwatch();
            Console.WriteLine("Method with multi threads started");
            multiThreadStopwatch.Start();

            var body = document.Root.XPathSelectElements("fb:body/fb:section/fb:p", namespaceManager).ToList();

            var secondThreadEnd = body.Count();
            var firstThreadStart = 0;
            Console.WriteLine(secondThreadEnd + "");
            var firtsThreadEnd = (int)Math.Round((Double)secondThreadEnd / 2);
            Console.WriteLine(firtsThreadEnd + "");
            var secondThreadStart = firtsThreadEnd + 1;

            var firstHalfThread = new Thread(() => SplitParsingAndWriting(firstThreadStart, firtsThreadEnd, body));
            firstHalfThread.Start();
            var secondHalfThread = new Thread(() => SplitParsingAndWriting(secondThreadStart, secondThreadEnd, body));
            secondHalfThread.Start();

            body.ForEach(delegate (XElement name)
            {
                MatchCollection matches = regex.Matches(name.Value);

                foreach (Match match in matches)
                {
                    var matchAsString = match.ToString().ToLower();

                    if (wordsFrequency.ContainsKey(matchAsString))
                    {
                        wordsFrequency[matchAsString] += 1;
                    }
                    else
                    {
                        wordsFrequency[matchAsString] = 1;
                    }
                }
            });

            var sortedWordsFrequency = wordsFrequency.OrderBy(x => x.Value).Reverse().ToDictionary(x => x.Key, x => x.Value);

            multiThreadStopwatch.Stop();
            Console.WriteLine($"Method with multi threads ended with time: {multiThreadStopwatch.Elapsed}");

            return sortedWordsFrequency;
        }

        private void SplitParsingAndWriting(int threadStart, int threadEnd, List<XElement> body)
        {
            for (int i = threadStart; i < threadEnd; i++)
            {
                MatchCollection matches = regex.Matches(body[i].Value);

                foreach (Match match in matches)
                {
                    var matchAsString = match.ToString().ToLower();

                    if (wordsFrequency.ContainsKey(matchAsString))
                    {
                        wordsFrequency[matchAsString] += 1;
                    }
                    else
                    {
                        wordsFrequency[matchAsString] = 1;
                    }
                }
            }
        }
    }


}

