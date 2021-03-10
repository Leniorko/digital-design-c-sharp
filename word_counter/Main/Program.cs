using System;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using CounterInDll;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument doc = XDocument.Load(@"P:\TaskFiles\tolstoj_lew_nikolaewich-text_0040.fb2");

            CounterProcessor counterProcessor = (CounterProcessor)typeof(CounterProcessor)
                .Assembly.CreateInstance("CounterInDll.CounterProcessor");

            Dictionary<String, int> sortedWordsFrequency = (Dictionary<string, int>)counterProcessor.GetType()
                .GetMethod("computeWords", BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(counterProcessor,new object[] { doc });

            using (StreamWriter outputFile = new StreamWriter(@"words_frequency.txt"))
            {
                foreach (var item in sortedWordsFrequency)
                {
                    outputFile.WriteLine($"{item.Key} : {item.Value}");
                }
            }
        }
    }
}