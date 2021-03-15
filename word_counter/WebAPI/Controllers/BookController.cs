using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;
using CounterInDll;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        [HttpPost]
        public string BookParsed(string book)
        {
            // Reading request body
            string bodyStr;
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                bodyStr = reader.ReadToEnd();
            }

            // Converting to XDocument
            XDocument xDocumentBook = XDocument.Parse(bodyStr);

            // Creating our Processor and proceeding it
            CounterProcessor counterProcessor = new CounterProcessor();

            var counted = counterProcessor.MultiThreadedComputeWords(xDocumentBook);

            // Converting to json
            string json = JsonConvert.SerializeObject(counted);

                
            return json;
        }
    }
}
