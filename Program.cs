using System;

namespace ApartmentCrawler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Browser browser = new();
            var resp = browser.UrlGetToHttp("https://www.myhome.ge/en/s/?AdTypeID=1&PrTypeID=1&Page=27&SortID=1&Ajax=1");
            var respstr = resp.Result.Content.ReadAsStringAsync().Result;
            Console.WriteLine("Hello World!");
        }
    }
}
