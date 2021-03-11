using System;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using CounterInDll;
using System.Diagnostics;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch globalStopwatch = new Stopwatch();
            Console.WriteLine("Global Timer Started");
            globalStopwatch.Start();

            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            XDocument doc = XDocument.Load(@$"{path}\tolstoj_lew_nikolaewich-text_0040.fb2");

            CounterProcessor counterProcessor = (CounterProcessor)typeof(CounterProcessor)
                .Assembly.CreateInstance("CounterInDll.CounterProcessor");

            //Stopwatch oneThreadStopwatch = new Stopwatch();
            //Console.WriteLine("Method with one thread started");
            //oneThreadStopwatch.Start();

            Dictionary<String, int> sortedWordsFrequency = (Dictionary<string, int>)counterProcessor.GetType()
                .GetMethod("computeWords", BindingFlags.NonPublic | BindingFlags.Instance)
                .Invoke(counterProcessor,new object[] { doc });

            //oneThreadStopwatch.Stop();
            //Console.WriteLine($"Method with one thread ended with time: {oneThreadStopwatch.Elapsed}");


            //Stopwatch multiThreadStopwatch = new Stopwatch();
            //Console.WriteLine("Method with multi threads started");
            //multiThreadStopwatch.Start();

            Dictionary<String, int> publicMultiThreaded = counterProcessor.MultithrededComputeWords(doc);

            //multiThreadStopwatch.Stop();
            //Console.WriteLine($"Method with multi threads ended with time: {multiThreadStopwatch.Elapsed}");

            using (StreamWriter outputFile = new StreamWriter(@"words_frequency.txt"))
            {
                foreach (var item in sortedWordsFrequency)
                {
                    outputFile.WriteLine($"{item.Key} : {item.Value}");
                }
            }

            globalStopwatch.Stop();
            Console.WriteLine($"Global timer ended with time: {globalStopwatch.Elapsed}");
        }
    }
}