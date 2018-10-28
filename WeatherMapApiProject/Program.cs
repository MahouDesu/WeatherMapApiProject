using System;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;

namespace WeatherMapApiProject
{
    class Program
    {
        static void Main(string[] args)
        {
#if DEBUG
            string weatherApiKey = File.ReadAllText("appsettings.development.txt");
#else
            string weatherApiKey = File.ReadAllText("appsettings.release.txt");
#endif
            WebClient web = new WebClient();
            Console.WriteLine("Please enter your zip code...");
            int zip = int.Parse(Console.ReadLine());
            string apiLink = $"https://api.openweathermap.org/data/2.5/weather?zip={zip}" + weatherApiKey;
            string apiObject = web.DownloadString(apiLink);
            JObject api = JObject.Parse(apiObject);
            double temperature = double.Parse(api.GetValue("main")["temp"].ToString());
            double tempConversion = temperature * 9 / 5 - 459.67;
            tempConversion = Math.Round(tempConversion, 1);
            Console.WriteLine($"The temperature is {tempConversion}");
            Console.ReadLine();
        }
    }
}
