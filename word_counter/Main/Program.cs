using System;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using CounterInDll;
using System.Diagnostics;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            XDocument doc = XDocument.Load(@$"{path}\tolstoj_lew_nikolaewich-text_0040.fb2");

            //CounterProcessor counterProcessor = (CounterProcessor)typeof(CounterProcessor)
            //    .Assembly.CreateInstance("CounterInDll.CounterProcessor");

            //Dictionary<String, int> sortedWordsFrequency = (Dictionary<string, int>)counterProcessor.GetType()
            //    .GetMethod("computeWords", BindingFlags.NonPublic | BindingFlags.Instance)
            //    .Invoke(counterProcessor,new object[] { doc });


            //Dictionary<String, int> publicMultiThreaded = counterProcessor.MultiThreadedComputeWords(doc);


            //using (StreamWriter outputFile = new StreamWriter(@"words_frequency.txt"))
            //{
            //    foreach (var item in sortedWordsFrequency)
            //    {
            //        outputFile.WriteLine($"{item.Key} : {item.Value}");
            //    }
            //}

            var client = new RestClient("https://localhost:44365/");

            var actualRequest = new RestRequest("api/Book");
            actualRequest.AddParameter("text/xml", doc, ParameterType.RequestBody);

            var actualResponce = client.Post(actualRequest);

            var asdad = JsonDocument.Parse(actualResponce.Content);

            var asdad2 = JsonSerializer.Deserialize<Dictionary<string, string>>(actualResponce.Content); 

            Console.WriteLine(actualResponce.Content);


        }
    }
}