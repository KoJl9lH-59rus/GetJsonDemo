using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System;
using System.Formats.Asn1;
using System.IO;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text.Json;
using GetJsonTest.Models;
using System.Text.Json.Nodes;
using System.Diagnostics;


namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static bool isCompressedJson;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public void CompressStatus(bool value) { 
            isCompressedJson = value;
        }

        public JsonResult ReadJson()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            string fileName = GetPathToFiles("\\wwwroot\\files\\Îáúåêòû1000.json");

            if (!System.IO.File.Exists(fileName))
            { }
            using (StreamReader file = new StreamReader(fileName))
            using (JsonReader reader = new JsonTextReader(file))
            {
                int countOfObjects = 0;

                List<string> jsonObjects = new List<string>();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.PropertyName)
                    {
                        if (reader.Value is string value)
                        {
                            if (value.Contains("Object"))
                                ReadValuesAsync(reader, jsonObjects);
                        }
                    }
                }

                stopwatch.Stop();

                return Json(
                    new {success = true, 
                        result = jsonObjects, 
                        processingTime = stopwatch.ElapsedMilliseconds });
            }
        }

        private List<string> ReadValuesAsync(JsonReader reader, List<string> jsonObjects)
        { 
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    if (Equals(reader.Value, "Value1")){
                        jsonObjects.Add(reader.ReadAsString());
                    }
                    else if (Equals(reader.Value, "Value2"))
                    {
                        jsonObjects.Add(reader.ReadAsString());
                    }
                    else if (Equals(reader.Value, "Value3"))
                    {
                        jsonObjects.Add(reader.ReadAsString());
                    }
                }
            }

            return jsonObjects;
        }

        private string GetPathToFiles(string path) => Directory.GetCurrentDirectory() + path;

        [HttpGet]
        public IActionResult GetResponseTime()
        {
            return Ok("");
        }
    }
}

