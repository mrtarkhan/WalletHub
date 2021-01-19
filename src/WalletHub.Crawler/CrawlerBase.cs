using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;


namespace WalletHub.Crawler
{
    public abstract class CrawlerBase
    {

        public IWebDriver WebDriver;

        protected CrawlerBase()
        {
            var chromeOptions = new ChromeOptions
            {
                BrowserVersion = "86",
                PlatformName = "Linux"
            };
            WebDriver = new RemoteWebDriver(new Uri("http://localhost:4444/wd/hub"), chromeOptions);
        }
        
    }
}