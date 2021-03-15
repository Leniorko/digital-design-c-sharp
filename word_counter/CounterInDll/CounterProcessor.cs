using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using System.Diagnostics;
using System;
using System.Collections.Concurrent;
using System.Threading;

namespace CounterInDll
{
    public class CounterProcessor
    {
        private XmlNamespaceManager namespaceManager;
        private Regex regex = new Regex(@"[^ ,.\/\\><:;\-0-9\[\]\(\)=" + "?\"!{ }]+");
        private ConcurrentDictionary<string, int> wordsFrequency = new ConcurrentDictionary<string, int>();

        public CounterProcessor()
        {
            namespaceManager = new XmlNamespaceManager(new NameTable());
            namespaceManager.AddNamespace("fb", "http://www.gribuser.ru/xml/fictionbook/2.1");
        }

        private Dictionary<string, int> computeWords(XDocument document)
        {

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

            var sortedWordsFrequency =
                wordsFrequency.OrderBy(x => x.Value).Reverse()
                    .ToDictionary(x => x.Key, x => x.Value);

            return sortedWordsFrequency;
        } 
        
        public ConcurrentDictionary<string, int> MultiThreadedComputeWords(XDocument document)
        {
            
            var body = document.Root.XPathSelectElements("fb:body/fb:section/fb:p", namespaceManager).ToList(); 

            var secondThreadEnd = body.Count();
            var firstThreadStart = 0;
            var firtsThreadEnd = (int)Math.Round((Double)secondThreadEnd / 2);
            var secondThreadStart = firtsThreadEnd + 1;

            
            var firstHalfThread = new Thread(() => SplitParsingAndWriting(firstThreadStart, firtsThreadEnd, body));

            firstHalfThread.Start();
            var secondHalfThread = new Thread(() => SplitParsingAndWriting(secondThreadStart, secondThreadEnd, body));
            secondHalfThread.Start();


            //var sortedWordsFrequency = wordsFrequency.OrderBy(x => x.Value).Reverse().ToDictionary(x => x.Key, x => x.Value);

            return wordsFrequency;
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

