using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

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

            return sortedWordsFrequency;
        }
    }


}

