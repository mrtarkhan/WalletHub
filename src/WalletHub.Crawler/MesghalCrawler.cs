using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using Polly;

namespace WalletHub.Crawler
{
    public class MesghalCrawler : CrawlerBase
    {
        private readonly ILogger<MesghalCrawler> _logger;


        public MesghalCrawler(ILogger<MesghalCrawler> logger) : base()
        {
            _logger = logger;
        }


        public Dictionary<string, string> GetPrices()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            WebDriver.Navigate().GoToUrl("http://mesghal.com");

            Task.Delay(TimeSpan.FromSeconds(2));
            
            Policy
                .Handle<Exception>()
                .WaitAndRetry(new[]
                {
                    TimeSpan.FromSeconds(3),
                    TimeSpan.FromSeconds(8),
                    TimeSpan.FromSeconds(20)
                })
                .Execute(() =>
                {
                    var pageLoaded = WebDriver
                        .ExecuteJavaScript<string>("return document.readyState")
                        .Equals("complete");
                    
                    if (!pageLoaded)
                    {
                        _logger.LogError("Mesghal.com is not available {notAvailableAt}", DateTime.Now);
                        throw new Exception("Mesghal.com is not available");
                    }

                    WebDriver
                        .ExecuteJavaScript("window.scrollBy(0,10000000)");

                });

            return Policy
                .Handle<Exception>()
                .WaitAndRetry(new[]
                {
                    TimeSpan.FromSeconds(3),
                    TimeSpan.FromSeconds(8),
                    TimeSpan.FromSeconds(20)
                })
                .Execute(() =>
                {
                    try
                    {
                        IList<IWebElement> prices =
                            WebDriver.FindElements(By.XPath("//div[@id='Prices']//tr[@class='danger']"));

                        foreach (IWebElement item in prices)
                        {
                            var data = item.FindElements(By.XPath("./td"));
                            var title = data[0].Text;
                            var value = data[2].Text;

                            result.TryAdd(title, value);
                        }

                        return result;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        _logger.LogError("error on crawling data: {exceptionMessage} - {innerException} - {callStack} ",
                            e.Message, e.InnerException?.Message, e.StackTrace);

                        return result;
                    }
                });
        }
    }
}