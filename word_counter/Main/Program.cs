using System;
using System.Xml.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using CounterInDll;
using System.Diagnostics;
using System.Linq;
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


            //RestSharp Client
            var client = new RestClient("https://localhost:44365/");

            //Passing book text and sending it
            var actualRequest = new RestRequest("api/Book");
            actualRequest.AddParameter("text/xml", doc, ParameterType.RequestBody);

            var actualResponce = client.Post(actualRequest);

            // Deserializing data twice because once is not enough
            var deserializeObject = (JObject)JsonConvert.DeserializeObject((string)JsonConvert.DeserializeObject(actualResponce.Content));

            // Converting to Dictionary, sorting and writing to the file
            Dictionary<string, int> wordsFrequency = new Dictionary<string, int>();

            foreach (var item in deserializeObject)
            {
                wordsFrequency[item.Key] = (int)item.Value;
            }

            var sortedWordsFrequency = wordsFrequency.OrderBy(x => x.Value).
                Reverse().ToDictionary(x => x.Key, x => x.Value);

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