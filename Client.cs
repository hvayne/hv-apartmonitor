using System;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ApartmentCrawler
{
    public class Client
    {
        Browser browser = new();
        private readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public DTO.MyHomeResponse GetProductsPage(int page)
        {
            try
            {
                Task<System.Net.Http.HttpResponseMessage> resp = browser.UrlGetToHttp($"https://www.myhome.ge/en/s/?AdTypeID=1&PrTypeID=1&Page={page}&Ajax=1"); // &cities=8742159&GID=8742159
                string respstr = resp.Result.Content.ReadAsStringAsync().Result;
                DTO.MyHomeResponse deserialized = JsonConvert.DeserializeObject<DTO.MyHomeResponse>(respstr);
                File.WriteAllText($"Dumps\\page{page:000000}_{DateTime.UtcNow:YYYYMMDDHHmmss}.json", respstr);
                return deserialized;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }
    }
}
