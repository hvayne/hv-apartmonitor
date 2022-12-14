using System;
using System.Threading;
using TelegramNotifier;
using System.Collections.Generic;
using ApartmentCrawler.DbProvider;
using Unidecode.NET;
using Newtonsoft.Json;
using ApartmentCrawler.Commands;

namespace ApartmentCrawler
{
    internal class Crawler
    {
        private readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private Notifier TgNotifyer = Notifier.Instance;
        public void Start()
        {
            RegisterNotifierCommands();
            bool firstIteration = true;
            Client client = new();
            // main loop
            while (true)
            {
                for (int i = 0; i < 70; i++)
                {
                    using AptCrawlerContext db = new();
                    Logger.Trace($"Fetching page#{i}...");
                    var productsResp = client.GetProductsPage(i);
                    if (null == productsResp)
                    {
                        Logger.Debug("productsResp was null. Sleep for a 5 seconds");
                        Thread.Sleep(5000);
                        continue;
                    }
                    // foreach by pr_id
                    foreach (KeyValuePair<string, Entities.User> user in productsResp.Data.UsersWrappedData.UsersDictionary)
                    {
                        Entities.User found = db.Users.Find(user.Value.UserId);
                        if (found == null)
                        {
                            Logger.Trace($"New user#{user.Value.UserId} found! Adding to database...");
                            db.Users.Add(user.Value);
                        }
                    }
                    foreach (Entities.Product product in productsResp.Data.Products)
                    {
                        Entities.Product found = db.Products.Find(product.ProductId);
                        if (found == null)
                        {
                            Logger.Trace($"New product#{product.ProductId} found! Adding to database...");
                            db.Products.Add(product);
                            if (!firstIteration)
                            {
                                // filtration by ownerType
                                if (product.OwnerTypeId != "1")
                                    continue;
                                // POST IT!
                                PostProductToChannel(product);
                                Thread.Sleep(10000);
                                Logger.Info("New product to post it to channel!");
                                Logger.Info($"price = {product.Price}, currencyId= {product.CurrencyId}" +
                                    $" square = {product.AreaSizeValue}; id={product.ProductId}; vip={product.Vip}");
                            }
                            else
                            {
                                Logger.Info($"price = {product.Price}, currencyId= {product.CurrencyId}" +
                                    $" square = {product.AreaSizeValue}; id={product.ProductId}; vip={product.Vip}");
                                Logger.Trace("Can be posted to channel, but it's first iteration since we launch the program");
                            }
                        }
                    }
                    db.SaveChanges();
                    // add to db
                    // send notification
                    Logger.Info("Sleep 1337ms");
                    Thread.Sleep(1337);
                }
                firstIteration = false;
                Logger.Info("Crawling ended, let's sleep for a short time");
                Thread.Sleep(TimeSpan.FromMinutes(5));
            }
        }
        private void PostProductToChannel(Entities.Product product)
        {
            string currency = product.CurrencyId == "1" ? "USD" : "GEL";
            Logger.Debug(JsonConvert.SerializeObject(product, Formatting.Indented));
            double priceUsd = Convert.ToDouble(product.Price, System.Globalization.CultureInfo.InvariantCulture);
            if (product.CurrencyId != "1")
            {
                double priceGel = Convert.ToDouble(product.Price, System.Globalization.CultureInfo.InvariantCulture);
                double USDGEL = 2.76;
                priceUsd = (int)(priceGel / USDGEL);
            }
            double areaSize = Convert.ToDouble(product.AreaSize, System.Globalization.CultureInfo.InvariantCulture);
            double pricePerM = 9999999;
            if (areaSize > 0)
            {
                pricePerM = priceUsd / areaSize;
            }
            else
            {
                Logger.Debug($"Area size of apartment was 0 m2!");
            }
            double rooms = Convert.ToDouble(product.Rooms, System.Globalization.CultureInfo.InvariantCulture);
            string general = $"{rooms:0} комнаты за {priceUsd:0}$.\n";
            if (rooms >= 5)
                general = $"{rooms:0} комнат за {priceUsd:0}$.\n";
            string sizeInfo = $"Площадь {product.AreaSize:0} m2\n";
            string productLink = $"<a href=\"https://www.myhome.ge/en/pr/{product.ProductId}\">Объявление#{product.ProductId}</a> \n";
            string userLink = $"<a href=\"https://www.myhome.ge/en/search/?UserID={product.UserId}\">Пользователь#{product.UserId}</a> \n";
            string elseInfo = $"| ParentId = {product.ParentId}. MaklerId = {product.MaklerId}. MaklerName = {product.MaklerName} | " +
                $"Адрес: <code>{product.StreetAddress.Unidecode().Replace("’", "")}</code> \n";
            string caption = $"{pricePerM:0.0}$\\m2\n{general}{sizeInfo}{productLink}{userLink}{elseInfo}";

            List<string> urls = new();
            for (int i = 1; i < Convert.ToInt32(product.PhotosCount); i++)
            {
                if (i > 3)
                    break;
                urls.Add($"https://static.my.ge/myhome/photos/{product.Photo}/large/{product.ProductId}_{i}.jpg");
            }
            switch (product.AdtypeId)
            {
                case "1":
                    TgNotifyer.NotifyWithImages(urls, caption, false);
                    break;
                case "3":
                    TgNotifyer.NotifyWithImages(urls, caption, true);
                    break;
                default:
                    // ignore
                    break;
            }

        }

        public void Dev()
        {
            Console.WriteLine("ფარნავაზ მეფის парнаваз мепе 108, Rustaveli District, Batumi, Adjara".Unidecode());
        }

        private void RegisterNotifierCommands()
        {
            Handlers.RegisterCommand("/ban", x => new BanCommand(x));
        }
    }
}
