using System;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace WalletHub.Crawler.Test
{
    public class MesghalCrawlerTest
    {
        [Fact]
        public void GetPricesTest()
        {
            
            //Arrange
            Mock<ILogger<MesghalCrawler>> loggerMock = new Mock<ILogger<MesghalCrawler>>();
            
            var crawler = new MesghalCrawler(loggerMock.Object);

            
            //Act
            var prices = crawler.GetPrices();
            
            
            //Assert
            Assert.NotNull(prices);
            Assert.NotEmpty(prices);
            
        }
    }
}