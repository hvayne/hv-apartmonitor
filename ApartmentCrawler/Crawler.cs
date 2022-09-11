using System;
using System.Threading;
using TelegramNotifier;
using System.Collections.Generic;
using ApartmentCrawler.DbProvider;

namespace ApartmentCrawler
{
    internal class Crawler
    {
        private readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private Notifier TgNotifyer = Notifier.Instance;
        public void Start()
        {
            bool firstIteration = false;
            Client client = new();
            // main loop
            while (true)
            {
                for (int i = 0; i < 10; i++)
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
                        else
                        {
                            Logger.Info("Same user again...");
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
                                // filtration
                                if (product.OwnerTypeId != "1")
                                    continue;
                                string currency = product.CurrencyId == "1" ? "USD" : "GEL";
                                TgNotifyer.Notify($"{product.Rooms} room apartment for {product.Price}{currency}. Area size is {product.AreaSize}m2. <a href=\"https://www.myhome.ge/en/pr/{product.ProductId}\">#{product.ProductId}</a>");
                                Thread.Sleep(5000);
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
                        else
                        {
                            Logger.Info("Same product again...");
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
                Thread.Sleep(TimeSpan.FromMinutes(7));
            }
        }

        public void Dev()
        {
            AptCrawlerContext db = new();
            Entities.User found = db.Users.Find("1");
            if (found == null)
            {
                Logger.Info($"New user#1 found! Adding to database...");
                db.Users.Add(new Entities.User { UserId = "1", Username = "durov" });
            }
            Entities.User found2 = db.Users.Find("1");
            if (found == null)
            {
                Logger.Info($"New user#{found2.UserId} found! Adding to database...");
                db.Users.Add(new Entities.User { UserId = "1", Username = "durov" });
            }
            else
            {
                Logger.Info("User already in database");
            }

            // db.SaveChanges();
            Console.WriteLine();
        }
    }
}
