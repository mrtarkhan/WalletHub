using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WalletHub.Crawler
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly MesghalCrawler _mesghalCrawler;

        public Worker(ILogger<Worker> logger, MesghalCrawler mesghalCrawler)
        {
            _logger = logger;
            _mesghalCrawler = mesghalCrawler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {

                var result = _mesghalCrawler.GetPrices();

                var dateTime = DateTime.Now;


                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);

            }
        }
    }
}