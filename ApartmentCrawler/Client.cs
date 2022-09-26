using System;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using ApartmentCrawler.Entities;

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
                Task<System.Net.Http.HttpResponseMessage> resp = browser.UrlGetToHttp($"https://www.myhome.ge/en/s/?GID=8742159&PrTypeID=1&Page={page}&Ajax=1");
                string respstr = resp.Result.Content.ReadAsStringAsync().Result;
                DTO.MyHomeResponse deserialized = JsonConvert.DeserializeObject<DTO.MyHomeResponse>(respstr);
                return deserialized;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return null;
            }
        }
        public void GetImage(Product product, int num)
        {
            try
            {
                Task<System.Net.Http.HttpResponseMessage> resp = browser.UrlGetToHttp($"https://static.my.ge/myhome/photos/{product.Photo}/large/{product.ProductId}_{num}");
                resp.Result.Content.ReadAsStream();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return;
            }
        }
    }
}
