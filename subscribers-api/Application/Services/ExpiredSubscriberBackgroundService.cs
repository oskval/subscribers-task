using Application.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class ExpiredSubscriberBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ExpiredSubscriberBackgroundService> _logger;

        public ExpiredSubscriberBackgroundService(
            IServiceScopeFactory scopeFactory,
            ILogger<ExpiredSubscriberBackgroundService> logger
        )
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var repository = scope.ServiceProvider.GetRequiredService<ISubscribersRepository>();

            while (!stoppingToken.IsCancellationRequested)
            {
                await HandleExpiredSubscribers(repository);
                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }
        }

        private async Task HandleExpiredSubscribers(ISubscribersRepository subscribersRepository)
        {
            var expiredSubs = await subscribersRepository.GetNewlyExpiredSubscribersAsync();

            if (!expiredSubs.Any())
            {
                return;
            }

            var emails = string.Join(", ", expiredSubs.Select(s => s.Email));

            _logger.LogInformation(
                $"Subscribers with the following emails need to be unsubscribed: {emails}"
            );

            //TODO: Create, resolve and use services to unsubscribe
        }
    }
}
