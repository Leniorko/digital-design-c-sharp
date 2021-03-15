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
            string bodyStr = "";
            using (StreamReader reader = new StreamReader(Request.Body))
            {
                bodyStr = reader.ReadToEnd();
            }

            XDocument xDocumentBook = XDocument.Parse(bodyStr);

            CounterProcessor counterProcessor = new CounterProcessor();

            var counted = counterProcessor.MultiThreadedComputeWords(xDocumentBook);

            string json = JsonConvert.SerializeObject(counted);
                
            return json;
        }
    }
}
